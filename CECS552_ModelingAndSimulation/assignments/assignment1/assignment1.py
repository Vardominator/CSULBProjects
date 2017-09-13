import numpy as np

# on day 255, Markov selected episode 152 from Lucy

# input: number n of times program should simulate Markov's regimen
# process: simulate the regimen n times
# output: fraction of simulations that result in watching Lucy's episode 152 on day 255

while True:

    print("Select one of the following: ")
    print("1. Synchronicity of Lecture and Leisure")
    print("2. What/'s in a Name?")
    print("3. Quit")
    option = input("\n")

    # OPTION 1
    if option == "1":

        p_lucy = 0.6
        p_seinfeld = 0.4

        n_sims = int(input('Enter number of times to simulate Markov\'s regimen: '), 10)
        n_152 = 0

        for n in range(n_sims):
            choices = np.random.choice(2, 255, p=[p_lucy, p_seinfeld])
            current_lucy_episode = 0

            for choice in choices:
                if choice == 0:
                    current_lucy_episode += 1

            if current_lucy_episode == 152:
                n_152 += 1

        print("Fraction of simulations resulting in Lucy's episode 152 showing on day 255: {}\n\n".format(n_152/n_sims))


    # OPTION 2
    elif option == "2":
        p_tabatha = 0.001
        p_not_tabatha = 1 - p_tabatha
        p_girl = 0.5
        p_boy = 0.5

        n_families = int(input('Enter number of families to generate: \n'), 10)
        girl_only_families = 0
        relevant_families = 0
        girl_only_relevant_families = 0

        for n in range(n_families):
            family = np.random.choice(2, 2, p=[p_girl, p_boy])
            child1 = family[0]
            child2 = family[1]

            relevant_fam = False

            for child in family:
                # check if current child is a girl
                if child == 1:
                    tabatha = np.random.choice(2, 1, p=[p_tabatha, p_not_tabatha])
                    # check if girl is named tabatha
                    if(tabatha[0] == 0):
                        relevant_families += 1
                        relevant_fam = True
                        break

            if child1 == 1 and child2 == 1:
                girl_only_families += 1
                if relevant_fam:
                    girl_only_relevant_families += 1

        print("Girl only families: {}".format(girl_only_families))
        print("Relevant families: {}".format(relevant_families))
        print("Relevant families with two girls: {}".format(girl_only_relevant_families))
        print("Fraction of relevant families with two girls: {}\n\n".format(girl_only_relevant_families/relevant_families))

    else:
        print("Have a nice day!\n")
        break