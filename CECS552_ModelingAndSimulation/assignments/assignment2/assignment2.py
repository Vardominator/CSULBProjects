import subprocess

while True:
    print()
    print("Select one of the following: ")
    print("1. Integral Approximation")
    print("2. Blackjack Monte Carlo")
    print("3. Quit")
    option = input("\n")

    if option == "1":
        proc = subprocess.Popen(['python3', 'integralapproximation.py'])
        proc.wait()
    elif option == "2":
        proc = subprocess.Popen(['python3', 'blackjack_mc.py'])
        proc.wait()
    elif option == "3":
        print("Have a nice day!\n")
        break