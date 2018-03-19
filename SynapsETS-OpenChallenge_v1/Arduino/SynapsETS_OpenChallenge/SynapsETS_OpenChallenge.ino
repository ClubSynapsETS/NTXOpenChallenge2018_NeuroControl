
#include <Servo.h>

Servo servo_finger1;          // Define thumb servo
Servo servo_finger2;          // Define thumb servo
Servo servo_finger3;          // Define thumb servo
Servo servo_finger4;          // Define thumb servo
Servo servo_finger5;          // Define thumb servo

int VibrationPin = 11;      // Vibration connected to digital pin 9

int finger_digital_pin;

void setup() {
  Serial.begin(9600);
  
  // initialize digital pin 13 as an output.
  pinMode(13, OUTPUT);
  pinMode(VibrationPin, OUTPUT); 



  finger_digital_pin = 3;
  servo_finger1.attach(finger_digital_pin);  // Set thumb servo to digital pin 2

  finger_digital_pin = 5;
  servo_finger2.attach(finger_digital_pin);  // Set thumb servo to digital pin 2

  finger_digital_pin = 6;
  servo_finger3.attach(finger_digital_pin);  // Set thumb servo to digital pin 2
  
  finger_digital_pin = 9;
  servo_finger4.attach(finger_digital_pin);  // Set thumb servo to digital pin 2
  
  finger_digital_pin = 10;
  servo_finger5.attach(finger_digital_pin);  // Set thumb servo to digital pin 2
  
  
}

int action = 1;
// the loop function runs over and over again forever
void loop() {

  if(Serial.available() > 0) {
      char data = Serial.read();
      
      if(data == '0')
          action = 0;

      if(data == '1')
          action = 1;

    
  }

    if(action == 1)
    {

      digitalWrite(13, HIGH);  
          
      servo_finger1.write(60);
      servo_finger2.write(60);
      servo_finger3.write(60);
      servo_finger4.write(60);
      servo_finger5.write(60);


      analogWrite(VibrationPin, 153); // 3 Volt

      
      delay(200);           // Wait 2000 milliseconds (2 seconds)
      
    }

  
  if(action == 0)
  {
    digitalWrite(13, LOW);  
    
    servo_finger1.write(25);
    servo_finger2.write(25);
    servo_finger3.write(25);
    servo_finger4.write(25);
    servo_finger5.write(25);

    analogWrite(VibrationPin, 0); // 0 Volt
    
    delay(1000);           // Wait 2000 milliseconds (2 seconds)


  }

  /*

  if(action == 2)
  {
      digitalWrite(13, HIGH); 
  }

*/
}
