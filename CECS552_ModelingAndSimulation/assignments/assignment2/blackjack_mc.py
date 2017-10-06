import random


cards = [1,2,3,4,5,6,7,8,9,10,10,10,10,11]
deck = 4*cards
dealing_deck = 4*deck
random.shuffle(dealing_deck)

# P(X=i) = 16/224 = 1/14 for all i=1,...,9,11
# P(X=10)= 64/224 = 2/7

def check_cards(player):
     player['sum'] = player['hidden'] + sum(player['upward'])



while len(dealing_deck) > 1:
    player1 = {
        'hidden': 0,
        'upward': [],
        'sum': 0
    }

    player2 = {
        'hidden': 0,
        'upward': [],
        'sum': 0
    }

    first_hand = True
    match_over = False

    while not match_over:
        if first_hand:
            player1['hidden'] = dealing_deck.pop()
            player2['hidden'] = dealing_deck.pop()
            first_hand = False
        else:
            player1['upward'].append(dealing_deck.pop())
            player2['upward'].append(dealing_deck.pop())

        check_cards(player1)
        check_cards(player2)

        if player1['sum'] >= 21 or player2['sum'] >= 21:
            match_over = True
        
        if len(dealing_deck) == 0:
            break

    print('match results:')
    print(player1)
    print(player2)
    print('length of deck: {}'.format(len(dealing_deck)))