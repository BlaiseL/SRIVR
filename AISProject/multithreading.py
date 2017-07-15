import threading
import time

class RepeatingThread(threading.Thread):
	def run(self):
		while (True):
			print ("auxilary thread")
			time.sleep(3)


thread = RepeatingThread()
thread.start()
while(True):
	print ("Main Thread")
	time.sleep(1)
