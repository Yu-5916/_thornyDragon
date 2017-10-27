#include "Ardunity.h"
#include "DigitalInput.h"
#include "AnalogInput.h"

DigitalInput dInput0(0, 2, true);
AnalogInput aInput1(1, A0);

void setup()
{
  ArdunityApp.attachController((ArdunityController*)&dInput0);
  ArdunityApp.attachController((ArdunityController*)&aInput1);
  ArdunityApp.resolution(256, 1024);
  ArdunityApp.timeout(5000);
  ArdunityApp.begin(115200);
}

void loop()
{
  ArdunityApp.process();
}
