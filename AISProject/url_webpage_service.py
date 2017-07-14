import requests
import time
from contextlib import closing
import zipfile
import os
import xml.etree.ElementTree as ET
import tempfile
import MySQLdb


url = 'http://data.aishub.net/ws.php?username=AH_2228_DD08E66D&format=0&output=csv&compress=1'  # webservice url

while (1):
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
            with closing(MySQLdb.connect(host='localhost', user='ais_fraud',passwd='msctesting', unix_socket='/home/mysql/mysql.sock')) as db:  # db = 'aisFraudDetectionDb')
                with db as cursor:  # creates cursor to execute commands, commits automatically after execution if no error
                    cursor.execute("""DROP TABLE aisFraudDetectionDb.temp_bulk_import;""")
                    cmd = ("""CREATE TABLE aisFraudDetectionDb.temp_bulk_import (MMSI VARCHAR(45) PRIMARY KEY NOT NULL,TSTAMP VARCHAR(45),LATITUDE VARCHAR(45),
                        LONGITUDE VARCHAR(45),COG VARCHAR(45),SOG VARCHAR(45), HEADING VARCHAR(45),NAVSTAT VARCHAR(45),IMO VARCHAR(45),NAME VARCHAR(45),
                        CALLSIGN VARCHAR(45),TYPE VARCHAR(45), A VARCHAR(45),B VARCHAR(45),C VARCHAR(45),D VARCHAR(45),DRAUGHT VARCHAR(45),DEST VARCHAR(45),ETA VARCHAR(45));""")  # ETA IS VARCHAR bc some people leave it blank
                    cursor.execute(cmd)

            with closing(MySQLdb.connect(host='localhost', user='ais_fraud', passwd='msctesting',db='aisFraudDetectionDb', unix_socket='/home/mysql/mysql.sock')) as db:
                with db as c:
                    # bulk imports ais .csv file into a temporary table
                    cmd1 = ("""LOAD DATA LOCAL INFILE '/home/msc/ais/ais_/data.csv' INTO TABLE aisFraudDetectionDb.temp_bulk_import
                        FIELDS TERMINATED BY ','
                        ENCLOSED BY '"'
                        LINES TERMINATED BY '\n'
                        IGNORE 1 ROWS
                        (@vMMSI,@vTSTAMP,@vLATITUDE,@vLONGITUDE,@vCOG,@vSOG,@vHEADING,@vNAVSTAT,@vIMO,@vNAME,
                        @vCALLSIGN,@vTYPE,@vA,@vB,@vC,@vD,@vDRAUGHT,@vDEST,@vETA)
                        SET
                        MMSI=nullif(@vMMSI,''), TSTAMP=nullif(@vTSTAMP,''),LATITUDE=nullif(@vLATITUDE,''),
                        LONGITUDE=nullif(@vLONGITUDE,''),COG=nullif(@vCOG,''),SOG=nullif(@vSOG,''),
                        HEADING=nullif(@vHEADING,''),NAVSTAT=nullif(@vNAVSTAT,''),IMO=nullif(@vIMO,''),
                        NAME=nullif(@vNAME,''),CALLSIGN=nullif(@vCALLSIGN,''),TYPE=nullif(@vTYPE,''),A=nullif(@vA,''),
                        B=nullif(@vB,''),C=nullif(@vC,''),D=nullif(@vD,''),DRAUGHT=nullif(@vDRAUGHT,''),
                        DEST=nullif(@vDEST,''),ETA=nullif(@vETA,''); """)

                    c.execute(cmd1)

                    # transfers non-duplicate entries into static_vessel_information from temp_bulk_import table
                    cmd2 = ("""INSERT IGNORE INTO static_vessel_information
                        (MMSI,IMO,NAME,CALLSIGN,TYPE,A_DIM_BOW,B_DIM_STERN,C_DIM_PORT,D_DIM_Starboard,DRAUGHT)
                        SELECT
                        temp_bulk_import.MMSI, temp_bulk_import.IMO, temp_bulk_import.NAME, temp_bulk_import.CALLSIGN,
                        temp_bulk_import.TYPE,temp_bulk_import.A, temp_bulk_import.B, temp_bulk_import.C, temp_bulk_import.D,
                        temp_bulk_import.DRAUGHT
                        FROM
                        temp_bulk_import;""")
                    c.execute(cmd2)

                    # transfers all dynamic data into the dynamic_vessel_information table
                    cmd3 = ("""INSERT INTO dynamic_vessel_information
                        (MMSI, TSTAMP, LONGITUDE, LATITUDE, COG, SOG, HEADING,NAVSTAT, DEST, ETA)
                        SELECT
                        temp_bulk_import.MMSI, temp_bulk_import.TSTAMP, temp_bulk_import.LONGITUDE, temp_bulk_import.LATITUDE,
                        temp_bulk_import.COG, temp_bulk_import.SOG, temp_bulk_import.HEADING,temp_bulk_import.NAVSTAT,
                        temp_bulk_import.DEST, temp_bulk_import.ETA
                        FROM temp_bulk_import;""")
                    c.execute(cmd3)

        else:  # querying url too frequently only downloads a ~300 byte file warning
            print ('Querying Webservice more frequently than once per minute! ')
