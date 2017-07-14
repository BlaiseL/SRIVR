import socket
import os

host = ""
port = 5001

sock = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
sock.bind((host, port))
while true