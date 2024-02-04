// #include <String.h>
// variables to show data
int BPM, EMG, vel, dist, counter;
char texto[60];
char tex[24];
char tex1[24];
char tex2[24];
char tex3[24];

// variables to catch sensors information
const int pinX = A3;
const int pinY = A4;
int potentiometer; //Armazena grandeza analógica

int valueX = 0;
int valueY = 0;
int outputValueX = 0;
int outputValueY = 0;

// --- Configurações Iniciais ---
void setup(){
  Serial.begin(115200);
  pinMode(A2, INPUT);
}

void loop(){
  potentiometer = analogRead(A2);
  valueX = analogRead(pinX);
  
  potentiometer = map(potentiometer,0,1023,1,100);
  
  BPM = map(potentiometer, 1, 100, 50, 170);   
  vel = map(potentiometer, 1, 100, 0 , 30);
  EMG = map(potentiometer, 1, 100, 300, 500);
  outputValueX = map(valueX, 0, 1023, 0, 255);

  if(counter > 20 && vel != 0){
    dist++;
    counter = 0;
  }

  sprintf(texto, "%d", BPM);
  strcat(texto, "#");
  char *valor2 = dtostrf(vel,5,2,tex);  
  strcat(texto, valor2);
  strcat(texto, "#");
  sprintf(tex1, "%d", EMG);
  strcat(texto, tex1);
  strcat(texto, "#");
  sprintf(tex2, "%d", outputValueX);
  strcat(texto, tex2);
  strcat(texto, "#");
  sprintf(tex3, "%d", dist);
  strcat(texto, tex3);
  Serial.println(texto);
  delay(25);
  counter++;
} //end loop
