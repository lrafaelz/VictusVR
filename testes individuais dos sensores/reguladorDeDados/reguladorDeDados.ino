  /*
      Curso de Arduino e AVR WR Kits Channel
      
      Aula 40 - PWM em todos os pinos
      
      
      Autor: Eng. Wagner Rambo  Data: Novembro de 2015
      
      www.wrkits.com.br | facebook.com/wrkits | youtube.com/user/canalwrkits
      
   */

   // --- Protótipo das Funções Auxiliares ---
   void setDuty_pin03(float value);    //Seleciona o duty cycle na saída digital  3
   void setDuty_pin11(float value);    //Seleciona o duty cycle na saída digital 11
   void setFrequency(char option);     //Seleciona a frequência de operação do PWM

    int BPM, EMG, vel;
    char texto[20];


   // --- Variáveis Globais ---
   int adc = 0;                     //Armazena grandeza analógica


   // --- Configurações Iniciais ---
   void setup()
   {
   Serial.begin(1200);
   pinMode( 9, OUTPUT);   //saída PWM pino 3
   pinMode(10, OUTPUT);   //saída PWM pino 11
   
   TCCR2A = 0xA3;         //Configura operação em fast PWM, utilizando registradores OCR2x para comparação
                           //1010 0011
   setFrequency(2);       //Seleciona opção 4 para frequência PWM (aprox 7.8kHz)
   
   setDuty_pin03(80);     //Saída  3 com duty de 80%
   setDuty_pin11(40);     //Saída 11 com duty de 20%

   } //end setup

   void loop()
   {
      adc = analogRead(A0);
      
      
      adc = map(adc,0,1023,1,100);
      
      setDuty_pin03(adc);
      // Serial.println(adc); // BPM#Vel#EMG
      BPM = map(adc, 1, 100, 50, 170);   
      vel = map(adc, 1, 100, 1, 20);
      EMG = map(adc, 1, 100, 300, 500);
      Serial.print(BPM);
      Serial.print("#");
      Serial.print(vel);
      Serial.print("#");
      Serial.println(EMG);


      // digitalWrite(3, HIGH);
   
   } //end loop

   void setDuty_pin03(float value)
   {
      int duty;
      
      value = value/100;
      
      duty = (value * 256) - 1;
      
      OCR2B = duty;

   } //end setDuty_pin3

   void setDuty_pin11(float value)
   {
      int duty;
      
      value = value/100;
      
      duty = (value * 256) - 1;
      
      OCR2A = duty;

   } //end setDuty_pin11

   void setFrequency(char option)
   {
   /*
   TABLE:
   
         option  frequency
         
         1      62.5  kHz
         2       7.81 kHz
         3       1.95 kHz
         4     976.56  Hz
         5     488.28  Hz
         6     244.14  Hz
         7      61.03  Hz   
   */
   TCCR2B = option;

   } //end setFrequency
