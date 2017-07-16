import csv

def retDict():
	dict={}
	file= open ('data.csv')
	reader= csv.reader(file)

	flag= 1
	for row in reader:
		if (flag==1):
			flag=0
			continue
		else:
			dict[row[0]]=row[11]
	reader.close()
	return dict		
