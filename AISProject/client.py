import socket
import ais
import re
import os
import threading 
import time
from url_webpage_serive import getData
from parsecsv import retDict
 
dict={}
host = ""
port = 5002
"""
Code needs to read Data.csv and parse it into a dictionary
Dictinaries are efficient, they should use bin search and therefore
will be great to use to check and update MMSI data
Threads are efficient, create another to run the input data to a dictionary 
when they recieve signal that data is done
"""
#return the number of whatever string you want
def seperate(string):
	spl= string.split(' ')
	return (spl[1])
	

def parse(string):
#regular expression grab the string
	b = re.compile(r"\'x\': -+\d+\.\d*") 
	c = re.compile(r"\'y\': +\d+\.\d*")
	d = re.compile(r"\'mmsi\': +\d*")
	#return the string
	partb=re.search(b,string).group()  
	partc= re.search(c,string).group()
	partd= re.search(d,string).group()
	#seperate to only get the number 
	splitb= seperate(partb)
	splitc= seperate(partc)
	splitd= seperate(partd)
	return (splitd+ ","+ splitb+ "," +splitc)

#Thread class that runs the data every 4 minutes, getData takes 60 sec
class RepeatingThread(threading.Thread):
	def run(self):
		while(True):
			getData()
			dict=retDict()
			time.sleep(240)

thread= RepeatingThread()
thread.start()


sock = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
sock.bind((host, port))
file = open("Merica.txt","a")
while True:
	try:
		#sock.settimeout(1)
		data, addr = sock.recvfrom(4096)
		data=str(data)
		arr= data.split(',')
		line = ais.decode(arr[5],0)
		line=str(line)
		#learn try to set arr to line
		string=parse(line)
		arr=string.split(',')
		x= (float)(arr[1])
		y= (float)(arr[2])
		if (y> 40.720721 and y<40.752849 and x> -74.024422 and x<-74.008198):
			strn= str(x)+","+str(y)+","+str(arr[0])
			ret=dict.get(arr[0])
			if(ret!=none):
				strn=strn+","+ ret
			else 
				strn= strn+ ",None"
			print (strn)
			file.write(strn+"\n")
	except Exception:
		pass
file.close()
sock.close()
