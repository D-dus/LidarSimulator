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

file=open("LidarData.txt",'w')
try:
	while(True):
		data=connection.recv(1000)
		#print(data.decode('utf-8'))
		file.write(data.decode('utf-8')+"\n")
		messageToSend="OK"
		connection.send(bytes(messageToSend,'utf-8'))
		
		##################################################################		
	connection.close
	file.close()
except KeyboardInterrupt:
	print("Interrupted")
	connection.close()
	file.close()
