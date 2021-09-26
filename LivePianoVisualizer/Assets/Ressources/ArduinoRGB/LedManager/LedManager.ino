#include <FastLED.h>

//----------------------- FastLED -------------------------//
#include <FastLED.h>

#define DATA_PIN 7
#define COLOR_ORDER GRB
#define LED_TYPE WS2812B
#define NUM_LEDS 176

uint8_t max_bright = 100;

struct CRGB ledList[NUM_LEDS];

//---------------------------------------------------------//

int noteStates[88];
int noteValue;
unsigned long lastPressed;

int r = 0;
int g = 0;
int b = 0;

#define TURN_OFF_SIGNAL 255

void setup() 
{
    //Let leds get recognized
    delay(500);
      //Set the baud rate for the unity-arduino comunication
    Serial.begin(250000);
    Serial.setTimeout(10);
    
    LEDS.addLeds<LED_TYPE, DATA_PIN, COLOR_ORDER>(ledList, NUM_LEDS);

    FastLED.setBrightness(10);

    //Little animation to know if the system is working
    for(int i = 0; i < NUM_LEDS; i++)
    {
        ledList[i].setRGB(255/(i/20), 0, 255);
        FastLED.show();
    }
    for(int i = NUM_LEDS; i >= -1; i--)
    {
        ledList[i].setRGB(0, 0, 0);
        FastLED.show();
    }
    
    FastLED.setBrightness(100);

    for(int i = 0; i < 88; i++)
    {
      noteStates[i] = 0;
    }
}

void loop()
{    
  //Check if received something from unity
  if (Serial.available() > 0)
  {
    noteValue = Serial.read();

/*
    //If the value is above 180 it means it starts to parse the color rgb values
    if(noteValue == 180)
    {
      r = Serial.read();
      g = Serial.read();
      b = Serial.read();

      noteValue = Serial.read();
    }
    */
    
    //This is a turn on signal
    if(noteValue < 88)
    {
      noteStates[noteValue] = 1;
      ledList[noteValue * 2].setRGB(150,150,150);
      ledList[noteValue * 2 + 1].setRGB(150,150,150);
      lastPressed = millis();
    }
    //If the value is above 87 it means it's a turn off signal
    else if(noteValue > 87 && noteValue < 180)
    {
      noteStates[noteValue - 88] = 0;
      ledList[(noteValue - 88) * 2].setRGB(0,0,0);
      ledList[(noteValue - 88) * 2 + 1].setRGB(0,0,0);
    }
  }
  //If we receive nothing from unity
  else
  {
    //Check if the time elapsed between the last press and now is above a threshold in (milliseconds)
    //If we don't do that, the leds blinks really fast and that's not what we want
    if((millis() - lastPressed) > 50)
    {
      //Then turn off all the leds because nothing is being pressed
      for(int i = 0; i < 88; i++)
      {
        noteStates[i] = 0;
        ledList[i * 2].setRGB(0,0,0);
        ledList[i * 2 + 1].setRGB(0,0,0);
      }
    }
  }

  //Apply the changes to the leds
  FastLED.show();
}
