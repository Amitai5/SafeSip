# Write your code here :-)
import board
import digitalio
import time
import busio
import neopixel
from adafruit_circuitplayground import cp
zBase = 0
cp.pixels.brightness = 0.1

active = True
safe = True
triggered = False

thresh = 5

def reset():
    armed = False
    safe = True
    triggered = False

def getBaseRead():
    zSum = 0
    for i in range(5):
        zSum += cp.acceleration.z
    return zSum/5

def setColor(r,g,b):
    cp.pixels.fill((r,g,b))
    cp.pixels.show()
    
def checkState():
    # TODO check for 
    print("checking...")

def sendTriggered
    # TODO Visit site
    print("triggered")
    

zBase = getBaseRead()
setColor(0,255,0)

while True:
    if active:
        reading = cp.acceleration.z
        # print(reading)
        if abs(zBase - reading) > thresh:
            # Triggered one-time stuff
            setColor(255,0,0)
            safe = False
            active = False

    # time.sleep(0.1)
    print("test")
    # time.sleep(0.1)
