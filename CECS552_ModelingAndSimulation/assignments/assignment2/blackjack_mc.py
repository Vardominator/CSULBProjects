import random
import numpy as np
import json

# P(X=i) = 16/224 = 1/14 for all i=1,...,9,11
# P(X=10)= 64/224 = 2/7


"""
NAIVE STRATEGY: P hits if one of the following is true
    1. P(X + S <= 21) > p1
        P's opponent has not yet decided to hold
        S: the sum of P's cards
        X: value of the next card that will be dealt if P decides to hit
        P hits provided there is sufficient probability that the next card will not bring her sum to 21
    
    2. E[X|X + S_v < 21] + S_v > S
        P's opponent has already decided to hold
        X: the opponents hidden card
        P calculates the expected value of the hidden card, on condition that, when adding to the visible
        sum, it must give a value that is less than 21.
        Then if this Expected value is added to the visible sum and it exceeds S, then P hits

    3. P(X + S <= 21) >= p2
        P hits because she has a good chance of not going over 21
"""    

"""
MONTE CARLO STRATEGY: 
"""

with open('blackjack_params.json', 'r') as f:
    params = json.load(f)

p1 = params['p1']
p2 = params['p2']
M = params['M']
N = params['N']
cards = [1,2,3,4,5,6,7,8,9,10,10,10,10,11]


def sum_hand(player):
     player['sum'] = player['hidden'] + sum(player['upward'])

def setup_deck():
    deck = 4*cards
    dealing_deck = 4*deck
    random.shuffle(dealing_deck)
    return dealing_deck    

mc_wins = 0
naive_wins = 0
ties = 0
matches_played = 0

N_HIT = 0
N_HOLD = 0
N_NEUTRAL = 0
consecutive_neutral = 0
longest_cons_neutral = 0

simulation_over = False

while not simulation_over:

    dealing_deck = setup_deck()

    while len(dealing_deck) > 1 and not simulation_over:
        naive = {
            'hidden': 0,
            'upward': [],
            'sum': 0
        }

        mc = {
            'hidden': 0,
            'upward': [],
            'sum': 0
        }

        first_hand = True
        match_over = False
        mc_lose = False
        naive_bust = False
        mc_bust = False

        hold_subcase = 'WIN'
        hit_subcase = 'WIN'

        matches_played_this_deck = 0
        
        while not match_over:
            # END MATCH IF DECK IS EMPTY
            if len(dealing_deck) == 0:
                break

            # SUM NAIVE HAND
            sum_hand(naive)
            sum_hand(mc)

            naive_hit = False

            # NAIVE HIT CONDITIONS
            if naive['sum'] < 21 and len(dealing_deck) > 0:
                if len([x for x in dealing_deck if x <= 21 - naive['sum']])/len(dealing_deck) > p1:
                    # HIT CONDITION 1
                    naive_hit = True
                elif sum([x for x in cards if x < 21 - sum(mc['upward'])])/len(cards) > naive['sum']:
                    # HIT CONDITION 2
                    naive_hit = True
                elif len([x for x in dealing_deck if x <= 21 - naive['sum']])/len(dealing_deck) >= p2:
                    naive_hit = True

            if naive_hit:
                if first_hand:
                    naive['hidden'] = dealing_deck.pop()
                else:
                    naive['upward'].append(dealing_deck.pop())


            sum_hand(naive)
            sum_hand(mc)

            if naive['sum'] > 21:
                naive_bust = True
                mc_wins += 1
                hit_subcase = 'WIN'
                match_over = True
                matches_played += 1
                break


            # MONTE CARLO
            if len(dealing_deck) >= 20:
                sample = list(np.random.choice(dealing_deck, 20, replace=False))
            else:
                sample = dealing_deck[:]
                random.shuffle(sample)

            naive_sum = naive['sum']
            mc_sum = mc['sum'] # S
            mc_hit = False

            # IF OPPONENT IS HOLDING (NOT HITTING)
            if not naive_hit and len(dealing_deck) > 1:
                if mc_sum < naive_sum:
                    mc_hit = True
                elif naive_sum == mc_sum:
                    if mc_sum + sample[1] <= 21:
                        mc_hit = True
            else:
                if mc_sum < naive_sum:
                    mc_hit = True
                elif len(sample) > 1:
                    if mc_sum >= naive_sum and mc_sum + sample[1] == 21:
                        mc_hit = True
                    elif mc_sum >= naive_sum and mc_sum + sample[1] < 21:
                        for j in range(1, len(sample)):
                            cond = naive_sum + sum(sample[1:j])
                            if cond > mc_sum and cond <= 21:
                                hold_subcase = 'LOSE'
                                break

            if mc_hit and mc['sum'] < 21 and len(dealing_deck) > 0:
                if first_hand:
                    first_hand = False
                    mc['hidden'] = dealing_deck.pop()
                else:
                    mc['upward'].append(dealing_deck.pop())

            sum_hand(naive)
            sum_hand(mc)

            if 21 in [mc['sum'], naive['sum']]:
                if mc['sum'] == 21 and naive['sum'] == 21:
                    ties += 1
                    hit_subcase = 'TIE'
                elif mc['sum'] == 21:
                    mc_wins += 1
                    hit_subcase = 'WIN'
                else:
                    naive_wins += 1
                    hit_subcase = 'LOSE'
                match_over = True
            elif not naive_hit and not mc_hit:
                if mc['sum'] > naive['sum']:
                    mc_wins += 1
                    hit_subcase = 'WIN'
                elif naive['sum'] > mc['sum']:
                    naive_wins += 1
                    hit_subcase = 'LOSE'
                else:
                    ties += 1
                    hit_subcase = 'TIE'
                match_over = True
            elif mc['sum'] > 21:
                match_over = True
                naive_wins += 1
                hit_subcase = 'LOSE'      
            elif len(dealing_deck) == 0:
                match_over = True
                ties += 1
                hit_subcase = 'TIE'

            if match_over:
                matches_played += 1
                if N_HIT < N and N_HOLD < N and consecutive_neutral < 100:
                    if hold_subcase == 'WIN' and hit_subcase in ['LOST', 'TIE']:
                        N_HOLD += 1
                        if consecutive_neutral > longest_cons_neutral:
                            longest_cons_neutral = consecutive_neutral
                        consecutive_neutral = 0
                    elif hold_subcase == 'WIN' and hit_subcase == 'WIN':
                        N_NEUTRAL += 1
                        consecutive_neutral += 1
                    elif hold_subcase == 'LOSE' and hit_subcase == 'LOSE':
                        N_NEUTRAL += 1
                        consecutive_neutral += 1
                    else:
                        N_HIT += 1  
                        if consecutive_neutral > longest_cons_neutral:
                            longest_cons_neutral = consecutive_neutral
                        consecutive_neutral == 0
                else:
                    simulation_over = True

            if matches_played == M:
                simulation_over = True    
           

print('HIT: {}, HOLD: {}, Longest Consecutive NEUTRAL: {}'.format(N_HIT, N_HOLD, longest_cons_neutral))
print('Matches Played: {}'.format(matches_played))
print('Naive wins:{}, MC wins:{}, Ties:{}'.format(naive_wins, mc_wins, ties))
print('Percentages: {:.2%}, {:.2%}, {:.2%}'.format(
    naive_wins/matches_played,
    mc_wins/matches_played,
    ties/matches_played
))
