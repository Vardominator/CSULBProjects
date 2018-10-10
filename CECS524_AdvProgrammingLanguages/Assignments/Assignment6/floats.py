from bokeh.io import output_file, show
from bokeh.plotting import figure
import math

buckets = []
counts = []
max_log = -10
with open('float_ranges.txt', 'r') as f:
    lines = f.readlines()

for line in lines:
    vals = line.split(',')
    buckets.append((float(vals[0]), float(vals[1])))
    counts.append(math.log2(int(vals[2])))

p = figure(title="IEEE Float Distribution", y_range=(15, 32), plot_width=1000, plot_height=800)
p.min_border_left = 5
p.xaxis.axis_label = "Bucket"
p.xaxis.axis_label_text_font_size = "20pt"
p.xaxis.major_label_text_font_size = "10pt"
p.yaxis.axis_label = "Log2 of Frequency"
p.yaxis.axis_label_text_font_size = "20pt"
p.yaxis.major_label_text_font_size = "10pt"

p.vbar(x=[(bucket[0] + bucket[1])/2 for bucket in buckets], top=counts, width=0.9)
p.title.text_font_size = "25pt"
output_file("bar.html")
show(p)