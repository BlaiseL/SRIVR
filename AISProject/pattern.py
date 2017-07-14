import re

string= "\'x\': -74.1076431274414, \'y\': 40.647701263427734," 
string=str(string)
b = re.compile(r"\'x\': -+\d+\.\d*")
c = re.compile(r"\'y\': +\d+\.\d*")
#d = re.compile(r"\'mmsi\': +\d*")
return (re.search(b,string).group() + "," + re.search(c,string).group())

