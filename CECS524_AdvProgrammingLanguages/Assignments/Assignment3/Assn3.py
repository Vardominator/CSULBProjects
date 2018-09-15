import re
import sys

hex_dec_exp = ("0[xX](([0-9a-fA-F])*)(\.([0-9a-fA-F])*)?"
               "(p|P)(\+|-|$)(\d\d*)((f|F)|(l|L)|$)")

with open(sys.argv[1]) as f:
    for value in f.readlines():
        match = re.match(hex_dec_exp, value.strip())
        if match:
            print("Matched: {}".format(match.group(0)))
        else:
            print("Not Matched: {}".format(value.strip()))