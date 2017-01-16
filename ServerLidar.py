import socket 
import numpy as np
import matplotlib.pyplot as plt



################################# TCP IP Parameters #################################
port=4321
host="127.0.0.1"
#####################################################################################

sock=socket.socket(socket.AF_INET,socket.SOCK_STREAM)
sock.bind((host,port))
sock.listen(1)
print("Server is listening")
connection,addrClient=sock.accept()
#####################################################################################

try:
	while(True):
		data=connection.recv(1000)
		################# DO SOMETHING WITH THE DATA ########################
		# data is a string converted into a byte
		# decoded data looks like float iAngle, float iDistance
		# the angle is relative to the robot orientation
		####################################################################
		messageToSend="OK"
		connection.send(bytes(messageToSend,'utf-8'))		
	connection.close
except KeyboardInterrupt:
	print("Interrupted")
	connection.close()
