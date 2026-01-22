using RazorPagesApp.Models;

namespace RazorPagesApp.Data
{
    public static class DbInitializer //а зачем static?
    {
        public static void Initialize(ApplicationContext context) 
        {
            if (context.SensorData_01.Any() && 
                context.SensorData_02.Any() &&
                context.SensorData_03.Any() &&
                context.SensorData_04.Any() &&
                context.SensorData_05.Any())
            {
                return;
            }

            if (!context.SensorData_01.Any())
            {
                var sensorData01 = new Sensor_01[]
                {
                    new Sensor_01 { Name = "temp1", Age = 0, temp = 0, hum = 0, num = 12020, date = DateTimeOffset.Now }
                };
                context.SensorData_01.AddRange(sensorData01);
                context.SaveChanges();
            }

            if (!context.SensorData_02.Any())
            {
                var sensorData02 = new Sensor_02[]
                {
                    new Sensor_02 { Name = "temp2", Age = 0, temp = 0, hum = 0, num = 22020, date = DateTimeOffset.Now }
                };
                context.SensorData_02.AddRange(sensorData02);
                context.SaveChanges();
            }
            
            if (!context.SensorData_03.Any())
            {
                var sensorData03 = new Sensor_03[]
                {
                    new Sensor_03 { Name = "temp3", Age = 0, temp = 0, hum = 0, num = 32020, date = DateTimeOffset.Now }
                };
                context.SensorData_03.AddRange(sensorData03);
                context.SaveChanges();
            }

            if (!context.SensorData_04.Any())
            {
                var sensorData04 = new Sensor_04[]
                {
                    new Sensor_04 { Name = "temp4", Age = 0, temp = 0, hum = 0, num = 42020, date = DateTimeOffset.Now }
                };
                context.SensorData_04.AddRange(sensorData04);
                context.SaveChanges();
            }
            
            if (!context.SensorData_05.Any())
            {
                var sensorData05 = new Sensor_05[]
                {
                    new Sensor_05 { Name = "temp5", Age = 0, temp = 0, hum = 0, num = 52020, date = DateTimeOffset.Now }
                };
                context.SensorData_05.AddRange(sensorData05);
                context.SaveChanges();
            }


        }
    }
}
