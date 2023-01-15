
#include <WiFi.h>
#include <HTTPClient.h>

#define armpin 16
#define trigpin 4

const char* ssid = "AmitaiPhone";
const char* password = "8189169000";

//Your Domain name with URL path or IP address with path
String serverName = "https://safesipapi.azurewebsites.net/";

unsigned long lastTime = 0;
unsigned long timerDelay = 500;

bool armed = false;
bool prevState = false;

bool trigd;


void setup() {
  Serial.begin(115200); 
  Serial1.begin(115200);

  pinMode(armpin,OUTPUT);
  pinMode(trigpin, INPUT);

  WiFi.begin(ssid, password);
  Serial.println("Connecting");
  while(WiFi.status() != WL_CONNECTED) {
    delay(500);
    Serial.print(".");
  }
  Serial.println("");
  Serial.print("Connected to WiFi network with IP Address: ");
  Serial.println(WiFi.localIP());
 
  Serial.println("Timer set to 5 seconds (timerDelay variable), it will take 5 seconds before publishing the first reading.");
  coasterMoved();
}

void loop() {
  trigd = digitalRead(trigpin);
  if(trigd){
    coasterMoved();
  }
  
  if ((millis() - lastTime) > timerDelay) {
    if(armed){
     digitalWrite(armpin,HIGH);
      
    }else{
      digitalWrite(armpin,LOW);
    }
    //Check WiFi connection status
    if(WiFi.status()== WL_CONNECTED){
      HTTPClient http;
      String serverPath = serverName + "Coaster?coasterID=3";
      http.begin(serverPath.c_str());
      // Send HTTP GET
      int httpResponseCode = http.GET();
      if (httpResponseCode>0) {
        Serial.print("HTTP Response code: ");
        Serial.println(httpResponseCode);
        String payload = http.getString();
        Serial.println(payload);
        if(payload == "true"){
          armed = true;
          Serial1.println("Arm");
        }
      }
      else {
        Serial.print("Error code: ");
        Serial.println(httpResponseCode);
      }
      http.end();
    }
    else {
      Serial.println("WiFi Disconnected");
    }
    lastTime = millis();
  }
  }


void coasterMoved() {
  Serial.println("Coaster moved");
  if(WiFi.status()== WL_CONNECTED){
      HTTPClient http;
      String serverPath = serverName + "Tamper?coasterID=3";
      http.begin(serverPath.c_str());
      int httpResponseCode = http.GET();
           
      Serial.print("HTTP Response code: ");
      Serial.println(httpResponseCode);

      http.end();
    }
    else {
      Serial.println("WiFi Disconnected");
    }
}
