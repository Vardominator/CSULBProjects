import random

cards = [1,2,3,4,5,6,7,8,9,10,10,10,10,11]
deck = 4*cards
dealing_deck = 4*deck
random.shuffle(dealing_deck)

p1 = 0.4
p2 = 0.5

def sum_hand(player):
     player['sum'] = player['hidden'] + sum(player['upward'])

def clear_hand(player):
    player['hidden'] = 0,
    player['upward'] = [],
    player['sum'] = 0


option = int(input("Choose one of the following options: "))


player1 = {
    'hidden': 0,
    'upward': [],
    'sum': 0,
    'wins': 0
}

player2 = {
    'hidden': 0,
    'upward': [],
    'sum': 0,
    'wins': 0
}

ties = 0

while len(dealing_deck) > 1:

    first_hand = True

    while True and len(dealing_deck) > 1:
        sum_hand(player1)
        sum_hand(player2)
        
        player1_hit = False

        # player1 HIT CONDITIONS
        if player1['sum'] < 21 and len(dealing_deck) > 0:
            if len([x for x in dealing_deck if x <= 21 - player1['sum']])/len(dealing_deck) > p1:
                # HIT CONDITION 1
                player1_hit = True
            elif sum([x for x in cards if x < 21 - sum(player2['upward'])])/len(cards) > player1['sum']:
                # HIT CONDITION 2
                player1_hit = True
            elif len([x for x in dealing_deck if x <= 21 - player1['sum']])/len(dealing_deck) >= p2:
                player1_hit = True

        if player1_hit:
            if first_hand:
                player1['hidden'] = dealing_deck.pop()
            else:
                player1['upward'].append(dealing_deck.pop())

        sum_hand(player1)

        if player1['sum'] == 21:
            player1['wins'] += 1
            break
        elif player1['sum'] > 21:
            player2['wins'] += 1
            break


        # player2 HIT CONDITIONS
        if player2['sum'] < 21 and len(dealing_deck) > 0:
            if len([x for x in dealing_deck if x <= 21 - player2['sum']])/len(dealing_deck) > p1:
                # HIT CONDITION 1
                player2_hit = True
            elif sum([x for x in cards if x < 21 - sum(player1['upward'])])/len(cards) > player2['sum']:
                # HIT CONDITION 2
                player2_hit = True
            elif len([x for x in dealing_deck if x <= 21 - player2['sum']])/len(dealing_deck) >= p2:
                player2_hit = True

        if player2_hit:
            if first_hand:
                player2['hidden'] = dealing_deck.pop()
            else:
                player2['upward'].append(dealing_deck.pop())

        sum_hand(player2)

        if player2['sum'] == 21:
            player2['wins'] += 1
            break
        elif player2['sum'] > 21:
            player1['wins'] += 1
            break

        if not player1_hit and not player2_hit:
            if player1['sum'] > player2['sum']:
                player1['wins'] += 1
            elif player2['sum'] > player1['sum']:
                player2['wins'] += 1
            else:
                ties += 1

    print(player1)
    print(player2)
    clear_hand(player1)
    clear_hand(player2)
