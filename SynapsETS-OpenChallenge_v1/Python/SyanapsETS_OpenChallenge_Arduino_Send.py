import serial, time
arduino = serial.Serial('COM22', 9600, timeout=.1)
time.sleep(1) #give the connection a second to settle

from socket import *

serverSocket = socket(AF_INET, SOCK_DGRAM)
serverSocket.bind(('', 8053))

print("Arduino waiting for UDP on port 8053...")

count = 0;
while True:
    message, address = serverSocket.recvfrom(1024)

#    count = count + 1;
#    
#    if(count == 4):
#        count = 0;
            
    if(message == "0"):
        
        msg = "0"; 
        arduino.write(msg)  
        print(str(count) + " : To arduino : " + msg)
        
        
    if(message == "1"):
        
        msg = "1"; 
        arduino.write(msg)  
        print(str(count) + " : To arduino : " + msg)

        
    time.sleep(0.05)
        
        
        
        