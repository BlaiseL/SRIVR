import pandas 

file=pandas.read_csv("data.csv")
file = file.sort_values(['MMSI'], ascending=[True])
print (file)
