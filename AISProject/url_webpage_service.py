import requests
import time
from contextlib import closing
import zipfile
import os
import xml.etree.ElementTree as ET
import tempfile
import MySQLdb

def getData():
	url = 'http://data.aishub.net/ws.php?username=AH_2228_DD08E66D&format=0&output=csv&compress=1'  # webservice url

	time.sleep(60)
	with closing(requests.get(url, stream=True)) as response:  # allows to safely open/closes connection to url
		if int(response.headers['content-length']) > 500:  # data is available to download
			time_stamp = time.strftime('%a_%d_%b_%Y_%H_%M_%S', time.localtime(
				time.time()))  # create time stamp to attach to each file name in a format that is kosher for path
			filePath = 'C:/Users/MSC/Desktop/NextProj'  # + time_stamp
			with open(filePath + '.zip', 'wb') as f:  # open/create new zip file to store downloaded zipped xml data
				if not response.ok:
					print ('something went wrong fetching raw data')
				for block in response.iter_content(1024):
					f.write(block)
			with zipfile.ZipFile(
							filePath + '.zip') as myzip:  # opens JUST ZIPFILE to show archive and look up what context managers are
				myzip.extract('data.csv', filePath)
			os.remove(filePath + '.zip')  # deletes the zip file

			#### access the database to download
			# opens connection to database in a way that ensures that the connection is closed automatically
				

		else:  # querying url too frequently only downloads a ~300 byte file warning
			print ('Querying Webservice more frequently than once per minute! ')