#include "Ardunity.h"
#include "DigitalInput.h"

DigitalInput dInput0(0, 2, true);

void setup()
{
  ArdunityApp.attachController((ArdunityController*)&dInput0);
  ArdunityApp.resolution(256, 1024);
  ArdunityApp.timeout(5000);
  ArdunityApp.begin(115200);
}

void loop()
{
  ArdunityApp.process();
}
