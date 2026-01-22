#include <ESP8266WiFi.h>
//#include <Adafruit_Sensor.h>
//#include <DHT.h>
//#include <DHT_U.h>
#include <Wire.h>
#define SDA_PIN 0
#define SCL_PIN 2
#include <math.h>
#include <Adafruit_BME280.h>
#include <Adafruit_Sensor.h>
#define SEALEVELPRESSURE_HPA (1013.25)// Задаем высоту
Adafruit_BME280 bme;


const char* ssid = "Name";                   // Название WIFI сети
const char* password = "your password";                // Пароль от WIFI сети

//const char* host = "192.168.0.110";       // Локальный адрес - читается с#
const char* host = "88.201.252.176";        // глобальный адрес - читается с#
const uint16_t port = 80;                 // Порт

//float num = 11111; // для BME280 №01 your secret number
float num = 22222; // для BME280 №02 your secret number
//float num = 33333; // для BME280 №03 your secret number
//float num = 44444; // для BME280 №04 your secret number
//float num = 55555; // для BME280 №05 your secret number

float Ktemp = 1.5; // коэф.калибровки аддитивной погрешности

//#define DHTPIN 2 
//#define DHTTYPE    DHT11     // DHT 11
//#define DHTTYPE    DHT22     // DHT 22 (AM2302)
//#define DHTTYPE    DHT21     // DHT 21 (AM2301)
//DHT_Unified dht(DHTPIN, DHTTYPE);

void setup()
{
  Serial.begin(115200);
  
  //dht.begin();
  Wire.begin(SDA_PIN, SCL_PIN); 
  bme.begin(0x76);  // Установка связи по интерфейсу I2C
  
  sensor_t sensor;
  //dht.temperature().getSensor(&sensor);
  
  Serial.println();

  Serial.printf("Connecting to %s ", ssid);
  WiFi.begin(ssid, password);
  while (WiFi.status() != WL_CONNECTED)   // Ждём пока не соединится
  {
    delay(500);
    Serial.print(".");
  }
  Serial.println(" connected");
}

void loop() {

  delay(300000);
  
  // old code begin
  /*sensors_event_t event;
  dht.temperature().getEvent(&event); 
  float temp = event.temperature;
  dht.humidity().getEvent(&event);
  int hum = int(event.relative_humidity);*/
  // old code end
  
  float temp = bme.readTemperature();
  //float hum = bme.readPressure()*0.00750062;
  float hum = bme.readHumidity();

  WiFiClient client;
  if (client.connect(host, port))
  {
    //client.print("GET /insert.php?");
    client.print("GET /index?");//аналог строки выше, но для C#
    client.print("temp=");
    client.print(temp-Ktemp);
    client.print("&hum=");
    client.print(hum);
    client.print("&num=");
    client.print(num);    
    client.println(" HTTP/1.1"); 
    client.print( "Host: " );
    client.println(host);
    client.println( "Connection: close" );
    client.println();
    client.println();


    
    while (client.connected())                          //Ответ
    {
      if (client.available())
      {
        String line = client.readStringUntil('\n');
      }
    }
    client.stop();                                    // Остановить клиента
  }
  else
  {
    client.stop();                                    // Ошибка подключения
  }
  delay(10000);                         // Ждём 10 cекунд
}
