# Testes dos sensores individuais

# ❤️ Pulso Amped Sensor

- 📚
    
    [Pulse Sensor Amped Getting Started Guide](https://www.generationrobots.com/media/DetecteurDePoulsAmplifie/PulseSensorAmpedGettingStartedGuide.pdf)
    

### Para melhor ligar ao corpo é necessário:

- Velcro para prender o sensor no dedo conforme as imagens abaixo
    - 📷
        
        
        ![Untitled](Testes%20dos%20sensores%20individuais%201a34425ee6f248f3ab6dcc903598756c/Untitled.png)
        
        ![Untitled](Testes%20dos%20sensores%20individuais%201a34425ee6f248f3ab6dcc903598756c/Untitled%201.png)
        
        ![Untitled](Testes%20dos%20sensores%20individuais%201a34425ee6f248f3ab6dcc903598756c/Untitled%202.png)
        
- Adesivos transparentes para proteger o sensor da oleosidade dos dedos
    - 📷
        
        ![Untitled](Testes%20dos%20sensores%20individuais%201a34425ee6f248f3ab6dcc903598756c/Untitled%203.png)
        

### Conexão dos fios:

- 📷
    
    ![Untitled](Testes%20dos%20sensores%20individuais%201a34425ee6f248f3ab6dcc903598756c/Untitled%204.png)
    

Fio amarelo/**preto** Analogico

Fio azul/vermelho GND

Fio verde/marrom 5V

### Código:

```cpp
/******Necessário instalar biblioteca PulseSensor Playground******
   Code to detect pulses from the PulseSensor,
   using an interrupt service routine.

   Here is a link to the tutorial\
   https://pulsesensor.com/pages/getting-advanced

   Copyright World Famous Electronics LLC - see LICENSE
   Contributors:
     Joel Murphy, https://pulsesensor.com
     Yury Gitman, https://pulsesensor.com
     Bradford Needham, @bneedhamia, https://bluepapertech.com

   Licensed under the MIT License, a copy of which
   should have been included with this software.

   This software is not intended for medical use.
*/

/*
   Every Sketch that uses the PulseSensor Playground must
   define USE_ARDUINO_INTERRUPTS before including PulseSensorPlayground.h.
   Here, #define USE_ARDUINO_INTERRUPTS true tells the library to use
   interrupts to automatically read and process PulseSensor data.

   See ProcessEverySample.ino for an example of not using interrupts.
*/
#define USE_ARDUINO_INTERRUPTS true
#include <PulseSensorPlayground.h>

/*
   The format of our output.

   Set this to PROCESSING_VISUALIZER if you're going to run
    the Processing Visualizer Sketch.
    See https://github.com/WorldFamousElectronics/PulseSensor_Amped_Processing_Visualizer

   Set this to SERIAL_PLOTTER if you're going to run
    the Arduino IDE's Serial Plotter.
*/
const int OUTPUT_TYPE = SERIAL_PLOTTER;

/*
   Pinout:
     PULSE_INPUT = Analog Input. Connected to the pulse sensor
      purple (signal) wire.
     PULSE_BLINK = digital Output. Connected to an LED (and 220 ohm resistor)
      that will flash on each detected pulse.
     PULSE_FADE = digital Output. PWM pin onnected to an LED (and resistor)
      that will smoothly fade with each pulse.
      NOTE: PULSE_FADE must be a pin that supports PWM. Do not use
      pin 9 or 10, because those pins' PWM interferes with the sample timer.
*/
const int PULSE_INPUT = A0;
const int PULSE_BLINK = 13;    // Pin 13 is the on-board LED
const int PULSE_FADE = 5;
const int THRESHOLD = 550;   // Adjust this number to avoid noise when idle

/*
   All the PulseSensor Playground functions.
*/
PulseSensorPlayground pulseSensor;

void setup() {
  /*
     Use 115200 baud because that's what the Processing Sketch expects to read,
     and because that speed provides about 11 bytes per millisecond.

     If we used a slower baud rate, we'd likely write bytes faster than
     they can be transmitted, which would mess up the timing
     of readSensor() calls, which would make the pulse measurement
     not work properly.
  */
  Serial.begin(115200);

  // Configure the PulseSensor manager.

  pulseSensor.analogInput(PULSE_INPUT);
  pulseSensor.blinkOnPulse(PULSE_BLINK);
  pulseSensor.fadeOnPulse(PULSE_FADE);

  pulseSensor.setSerial(Serial);
  pulseSensor.setOutputType(OUTPUT_TYPE);
  pulseSensor.setThreshold(THRESHOLD);

  // Now that everything is ready, start reading the PulseSensor signal.
  if (!pulseSensor.begin()) {
    /*
       PulseSensor initialization failed,
       likely because our particular Arduino platform interrupts
       aren't supported yet.

       If your Sketch hangs here, try PulseSensor_BPM_Alternative.ino,
       which doesn't use interrupts.
    */
    for(;;) {
      // Flash the led to show things didn't work.
      digitalWrite(PULSE_BLINK, LOW);
      delay(50);
      digitalWrite(PULSE_BLINK, HIGH);
      delay(50);
    }
  }
}

void loop() {
  /*
     Wait a bit.
     We don't output every sample, because our baud rate
     won't support that much I/O.
  */
  delay(20);

  // write the latest sample to Serial.
 pulseSensor.outputSample();

  /*
     If a beat has happened since we last checked,
     write the per-beat information to Serial.
   */
  if (pulseSensor.sawStartOfBeat()) {
   pulseSensor.outputBeat();
  }
}
```

---

# 💪🏼 EMG

- 📚
    
    [Myoware Muscle Sensor Interfacing with Arduino](https://theorycircuit.com/myoware-muscle-sensor-interfacing-arduino/)
    

### Para ligar o sensor ao corpo é necessário:

- Posicionar no musculo de forma que os sensores fiquem no meio do musculo que se deseja coletar a força realizada conforme demonstrado na figura 1.
- Posicionar o aterramento longe do musculo a ser analisado.

![Figura 1 - posição do sensor](Testes%20dos%20sensores%20individuais%201a34425ee6f248f3ab6dcc903598756c/Untitled%205.png)

Figura 1 - posição do sensor

![Figura 2 - Sensor de Eletromiografia](Testes%20dos%20sensores%20individuais%201a34425ee6f248f3ab6dcc903598756c/Sensor_EMG.png)

Figura 2 - Sensor de Eletromiografia

### Conexão dos fios:

Fio azul Analogico

Fio verde GND

Fio vermelho 5V

### Código:

```cpp
const int analogInPin = A0;  // Analog input pin that the potentiometer is attached to
const int analogOutPin = 9; // Analog output pin that the LED is attached to

int sensorValue = 0;        // value read from the pot
int outputValue = 0;        // value output to the PWM (analog out)

void setup() {
  // initialize serial communications at 9600 bps:
  Serial.begin(9600);
}

void loop() {
	// read the analog in value:
	sensorValue = analogRead(analogInPin);
	// map it to the range of the analog out:
	outputValue = map(sensorValue, 0, 1023, 0, 255);
	// change the analog out value:
	analogWrite(analogOutPin, outputValue);
	
	// print the results to the Serial Monitor:
	Serial.print("sensor = ");
	Serial.print(sensorValue);
	Serial.print("\t output = ");
	Serial.println(outputValue);
	
	// wait 200 milliseconds before the next loop for the analog-to-digital
	// converter to settle after the last reading:
	delay(200);
}
// code from Arduino IDE: File > Examples > 03.Analogic > AnalogicInOutSerial
```

---