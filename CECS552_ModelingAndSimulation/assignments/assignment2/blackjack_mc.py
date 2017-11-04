import random
import numpy as np
import scipy.stats as st
import json
import sys

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


with open('blackjack_params.json', 'r') as f:
    params = json.load(f)

p11 = params['p11']
p12 = params['p12']
p21 = params['p21']
p22 = params['p22']
M = params['M']
cards = [1,2,3,4,5,6,7,8,9,10,10,10,10,11]

player1_wins = []
player2_wins = []

def sum_hand(player):
     player['sum'] = player['hidden'] + sum(player['upward'])

def setup_deck():
    deck = 4*cards
    dealing_deck = 4*deck
    random.shuffle(dealing_deck)
    return dealing_deck    

def prob(s, deck):
    return len([x for x in deck if x + s <= 21])/len(deck)

def exp(sv):
    X = [x for x in cards if x + sv < 21]
    return np.mean(X)

option = int(input("Choose one of the following options: "))
original_deck = setup_deck()
dealing_deck = setup_deck()
count_cards = False

if option == 1:
    s = int(input("Enter S to calculate P(X + S <= 21): "))
    print(prob(s, dealing_deck))
    sys.exit()
elif option == 2:
    sv = int(input("Enter Sv to calculate E[X|X + Sv < 21]: "))
    print(exp(sv))
    sys.exit()
elif option == 3:
    print("Playing until deck exhaustion...")
elif option == 4:
    print("Playing until deck exhaustion, but Naive1 is counting cards...")
    count_cards = True

mc_wins = 0
naive_wins = 0
ties = 0
matches_played = 0

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

        naive_first_hand = True
        mc_first_hand = True
        match_over = False
        naive_hit = True
        mc_hit = True
        turn = True

        matches_played_this_deck = 0
        
        while not match_over:
            # END MATCH IF DECK IS EMPTY
            if len(dealing_deck) == 0:
                break

            # SUM NAIVE HAND
            sum_hand(naive)
            sum_hand(mc)
            
            if turn:            
                # NAIVE HIT CONDITIONS
                if naive['sum'] < 21 and len(dealing_deck) > 0:
                    if count_cards:
                        p = prob(naive['sum'], dealing_deck)
                    else:
                        p = prob(naive['sum'], original_deck)

                    if mc_hit and p > p11:
                        # HIT CONDITION 1
                        naive_hit = True
                    elif not mc_hit and exp(sum(mc['upward'])) + sum(mc['upward']) > naive['sum']:
                        # HIT CONDITION 2
                        naive_hit = True
                    elif not mc_hit and p >= p12:
                        naive_hit = True
                    else:
                        naive_hit = False

                if naive_hit:
                    if naive_first_hand:
                        naive_first_hand = False
                        naive['hidden'] = dealing_deck.pop()
                    else:
                        naive['upward'].append(dealing_deck.pop())


                sum_hand(naive)
                # sum_hand(mc)


                if naive['sum'] > 21:
                    naive_bust = True
                    mc_wins += 1
                    match_over = True
                    matches_played += 1
                    break


                # mc HIT CONDITIONS
                if mc['sum'] < 21 and len(dealing_deck) > 0:
                    p = prob(mc['sum'], original_deck)

                    if naive_hit and p > p21:
                        # HIT CONDITION 1
                        mc_hit = True
                    elif not naive_hit and exp(sum(naive['upward'])) + sum(naive['upward']) > mc['sum']:
                        # HIT CONDITION 2
                        mc_hit = True
                    elif not naive_hit and p >= p22:
                        mc_hit = True
                    else:
                        mc_hit = False


                if mc_hit and len(dealing_deck) > 0:
                    if mc_first_hand:
                        mc_first_hand = False
                        mc['hidden'] = dealing_deck.pop()
                    else:
                        mc['upward'].append(dealing_deck.pop())

                # sum_hand(naive)
                sum_hand(mc)

                # if mc['sum'] > 21:
                #     naive_bust = True
                #     naive_wins += 1
                #     match_over = True
                #     matches_played += 1
                #     break
                
            else:
                # mc HIT CONDITIONS
                if mc['sum'] < 21 and len(dealing_deck) > 0:
                    p = prob(mc['sum'], original_deck)

                    if naive_hit and p > p21:
                        # HIT CONDITION 1
                        mc_hit = True
                    elif not naive_hit and exp(sum(naive['upward'])) + sum(naive['upward']) > mc['sum']:
                        # HIT CONDITION 2
                        mc_hit = True
                    elif not naive_hit and p >= p22:
                        mc_hit = True
                    else:
                        mc_hit = False


                if mc_hit and len(dealing_deck) > 0:
                    if mc_first_hand:
                        mc_first_hand = False
                        mc['hidden'] = dealing_deck.pop()
                    else:
                        mc['upward'].append(dealing_deck.pop())

                # sum_hand(naive)
                sum_hand(mc)

                if mc['sum'] > 21:
                    naive_bust = True
                    naive_wins += 1
                    match_over = True
                    matches_played += 1
                    break

                # NAIVE HIT CONDITIONS
                if naive['sum'] < 21 and len(dealing_deck) > 0:
                    if count_cards:
                        p = prob(naive['sum'], dealing_deck)
                    else:
                        p = prob(naive['sum'], original_deck)

                    if mc_hit and p > p11:
                        # HIT CONDITION 1
                        naive_hit = True
                    elif not mc_hit and exp(sum(mc['upward'])) + sum(mc['upward']) > naive['sum']:
                        # HIT CONDITION 2
                        naive_hit = True
                    elif not mc_hit and p >= p12:
                        naive_hit = True
                    else:
                        naive_hit = False

                if naive_hit and len(dealing_deck) > 0:
                    if naive_first_hand:
                        naive_first_hand = False
                        naive['hidden'] = dealing_deck.pop()
                    else:
                        naive['upward'].append(dealing_deck.pop())


                sum_hand(naive)
                # sum_hand(mc)



            if 21 in [mc['sum'], naive['sum']]:
                if mc['sum'] == 21 and naive['sum'] == 21:
                    ties += 1
                elif mc['sum'] == 21:
                    mc_wins += 1
                else:
                    naive_wins += 1
                match_over = True
            elif not naive_hit and not mc_hit:
                if mc['sum'] > naive['sum']:
                    mc_wins += 1
                elif naive['sum'] > mc['sum']:
                    naive_wins += 1
                else:
                    ties += 1
                match_over = True 
            elif len(dealing_deck) == 0:
                match_over = True
                ties += 1

            if match_over:
                matches_played += 1

            if matches_played == M:
                simulation_over = True    

            turn = not turn



player1_wins = [1]*naive_wins
player1_wins.extend([0]*mc_wins)
player1_wins.extend([0]*ties)

player2_wins = [1]*mc_wins
player2_wins.extend([0]*naive_wins)
player2_wins.extend([0]*ties)

player1_prob = naive_wins/matches_played
player2_prob = mc_wins/matches_played


conf_int_player1 = st.t.interval(0.90, M - 1, loc=np.mean(player1_wins), scale=st.sem(player1_wins))
conf_int_player2 = st.t.interval(0.90, M - 1, loc=np.mean(player2_wins), scale=st.sem(player2_wins))

# print('90% Confidence interval for player 1: {}'.format([x[0] for x in conf_int_player1]))
# print('90% Confidence interval for player 2: {}'.format([x[0] for x in conf_int_player2]))
print(conf_int_player1)
print(conf_int_player2)
print('Matches Played: {}'.format(matches_played))
print('Naive1 wins:{}, Naive2 wins:{}, Ties:{}'.format(naive_wins, mc_wins, ties))
print('Percentages: {:.2%}, {:.2%}, {:.2%}'.format(
    naive_wins/matches_played,
    mc_wins/matches_played,
    ties/matches_played
))
print('Overall winner: {}'.format('Naive1' if naive_wins > mc_wins else 'Naive2'))
