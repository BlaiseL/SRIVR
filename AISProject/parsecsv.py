import csv

def retDict():
	dictionary={}
	file= open ('C:/Users/MSC/Desktop/NextProj/data.csv')
	reader= csv.reader(file)
	flag= 1
	for row in reader:
		if (flag==1):
			flag=0
			continue
		else:
			dictionary[row[0]]=row[11]
	file.close()
	return dictionary	