using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Runtime.Serialization;
using System.Globalization; // для передачи точки вместо запятой  //https://metanit.com/sharp/tutorial/20.4.php
using Microsoft.EntityFrameworkCore;
using RazorPagesApp.Models;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Linq;

namespace RazorPagesApp.Pages
{


	public class IndexModel : PageModel
    {
		IFormatProvider formatter = new NumberFormatInfo { NumberDecimalSeparator = "." }; // для передачи точки вместо запятой
        public float temp_ = 0;
        public float hum_ = 0;
        public float num_ = 0;
        public string? name3 = ""; // for test only. delete in future. 
        public string? name25 = ""; // for test only. delete in future. 
        string? querystring = "";
        string searchTemp = "temp=";
        public int iSearch_01 = 0;
        public int iSearch_02 = 0;
        public int iSearch_03 = 0;
        public int iSearch_04 = 0;
        public int iSearch_05 = 0;
        public int dbCountString = 1000; //количество строк в каждой БД или грубина архива в строках другими словами
       

                public async Task OnGet()
                {
                    querystring = Request.QueryString.Value; //работающий аналог только для ввода в адресную строку.
                    //querystring = Request.GetEncodedPathAndQuery(); //работающий аналог только для ввода в адресную строку.// добавлено из версии app.Run(async (context)=>
                    
                    //подключение базы данных - 1начало

                    SensorData_01 = context.SensorData_01.AsNoTracking().ToList();
                    SensorData_02 = context.SensorData_02.AsNoTracking().ToList();
                    SensorData_03 = context.SensorData_03.AsNoTracking().ToList();
                    SensorData_04 = context.SensorData_04.AsNoTracking().ToList();
                    SensorData_05 = context.SensorData_05.AsNoTracking().ToList();

                    Users = context.Users.AsNoTracking().ToList();
                    int test = SensorData_01.Count;// for test only. delete in future.

                   //подключение базы данных - 1конец
                    if (querystring.IndexOf(searchTemp) > 0 && querystring != null) // без этой проверки значение вылетает исключение, т.к. temp бывает null в момент запуска приложения
                    {
                        temp_ = float.Parse(Request.Query["temp"], formatter);// для передачи точки вместо запятой  //https://metanit.com/sharp/tutorial/20.4.php
                        hum_ = float.Parse(Request.Query["hum"], formatter);
                        num_ = float.Parse(Request.Query["num"], formatter);
                        name25 = querystring;// for test only. delete in future.
                        
                        switch (num_)
                        {
                            case 11111: //your secret number
                                DateTimeOffset dateSearch_01 = DateTimeOffset.Now;
                                if (SensorData_01.Count < dbCountString) 
                                {
                                 //temp1 = float.Parse(temp, formatter);// для передачи точки вместо запятой  //https://metanit.com/sharp/tutorial/20.4.php
                                 bme280_01.temp = temp_;
                                 bme280_01.hum = hum_;
                                 bme280_01.num = num_;
                                 bme280_01.Name = name25;// for test only. delete in future.
                                 bme280_01.Age = SensorData_01.Count;// for test only. delete in future. 
                                 bme280_01.date = DateTimeOffset.Now;
                                 context.SensorData_01.Add(bme280_01);
                                }
                                 //обновляем самую старую строку начало
                                else 
                                {
                                 //var SensorData = context.SensorData_01.OrderBy(p=>p.date); // это ветка с сортировкой не работает https://metanit.com/sharp/efcore/5.10.php // потестить без context позже
                                 //Ниже организована сортировка которая не перезапишет в итоге всю таблицу заново, а только отдельную строку:
                                  for (int i = 0; i < (SensorData_01.Count); i++)
                                  {
                                        if (SensorData_01[i].date < dateSearch_01) 
                                        {
                                            dateSearch_01 = SensorData_01[i].date;
                                            iSearch_01 = i;
                                        }
                                  }
                                 Sensor_01? bme280_01S = SensorData_01[iSearch_01]; //приравниваем для дальнейшей коррекции
                                 bme280_01S.temp = temp_;
                                 bme280_01S.hum = hum_;
                                 bme280_01S.num = num_;
                                 bme280_01S.Name = name25;// for test only. delete in future.
                                 bme280_01S.Age = SensorData_01.Count;// for test only. delete in future. 
                                 bme280_01S.date = DateTimeOffset.Now;
                                 context.Update(bme280_01S); // РАЗОБРАТЬСЯ В ЭТОМ ОПЕРАТОРЕ: https://metanit.com/sharp/efcore/1.4.php

                                 //bme280_01test = SensorData_01[0];
                                 //context.SensorData_01.Add(bme280_01test);// добавление работает
                                 //context.SensorData_01.Remove(SensorData_01[0]);// удаление работает
                                 // await context.SaveChangesAsync();                            
                                }
                                await context.SaveChangesAsync();
                                //обновляем самую старую строку конец
                            break;
                            case 22222://your secret number
                            DateTimeOffset dateSearch_02 = DateTimeOffset.Now;
                            if (SensorData_02.Count < dbCountString)
                            {
                               bme280_02.temp = temp_;
                               bme280_02.hum = hum_;
                               bme280_02.num = num_;
                               bme280_02.Name = name25;// for test only. delete in future.
                               bme280_02.Age = SensorData_02.Count;// for test only. delete in future.
                               bme280_02.date = DateTimeOffset.Now;
                               context.SensorData_02.Add(bme280_02);

                            }
                            else
                            {
                                for (int i = 0; i < (SensorData_02.Count); i++)
                                {
                                    if (SensorData_02[i].date < dateSearch_02)
                                    {
                                        dateSearch_02 = SensorData_02[i].date;
                                        iSearch_02 = i;
                                    }
                                }
                                Sensor_02? bme280_02S = SensorData_02[iSearch_02]; //приравниваем для дальнейшей коррекции
                                bme280_02S.temp = temp_;
                                bme280_02S.hum = hum_;
                                bme280_02S.num = num_;
                                bme280_02S.Name = name25;// for test only. delete in future.
                                bme280_02S.Age = SensorData_02.Count;// for test only. delete in future. 
                                bme280_02S.date = DateTimeOffset.Now;
                                context.Update(bme280_02S); 
                            }
                            await context.SaveChangesAsync();
                            break;
                            
                            case 33333://your secret number
                            DateTimeOffset dateSearch_03 = DateTimeOffset.Now;
                            if (SensorData_03.Count < dbCountString)
                            {
                               bme280_03.temp = temp_;
                               bme280_03.hum = hum_;
                               bme280_03.num = num_;
                               bme280_03.Name = name25;// for test only. delete in future.
                               bme280_03.Age = SensorData_03.Count;// for test only. delete in future.
                               bme280_03.date = DateTimeOffset.Now;
                               context.SensorData_03.Add(bme280_03);
                            }
                            else
                            {
                                for (int i = 0; i < (SensorData_03.Count); i++)
                                {
                                    if (SensorData_03[i].date < dateSearch_03)
                                    {
                                        dateSearch_03 = SensorData_03[i].date;
                                        iSearch_03 = i;
                                    }
                                }
                                Sensor_03? bme280_03S = SensorData_03[iSearch_03]; //приравниваем для дальнейшей коррекции
                                bme280_03S.temp = temp_;
                                bme280_03S.hum = hum_;
                                bme280_03S.num = num_;
                                bme280_03S.Name = name25;// for test only. delete in future.
                                bme280_03S.Age = SensorData_03.Count;// for test only. delete in future. 
                                bme280_03S.date = DateTimeOffset.Now;
                                context.Update(bme280_03S); 
                            }
                            await context.SaveChangesAsync(); 
                            break;

                            case 44444://your secret number
                            DateTimeOffset dateSearch_04 = DateTimeOffset.Now;
                            if (SensorData_04.Count < dbCountString)
                            { 
                               bme280_04.temp = temp_;
                               bme280_04.hum = hum_;
                               bme280_04.num = num_;
                               bme280_04.Name = name25;// for test only. delete in future.
                               bme280_04.Age = SensorData_04.Count;// for test only. delete in future.
                               bme280_04.date = DateTimeOffset.Now;
                               context.SensorData_04.Add(bme280_04);
                            }
                            else
                            {
                                for (int i = 0; i < (SensorData_04.Count); i++)
                                {
                                    if (SensorData_04[i].date < dateSearch_04)
                                    {
                                        dateSearch_04 = SensorData_04[i].date;
                                        iSearch_04 = i;
                                    }
                                }
                                Sensor_04? bme280_04S = SensorData_04[iSearch_04]; //приравниваем для дальнейшей коррекции
                                bme280_04S.temp = temp_;
                                bme280_04S.hum = hum_;
                                bme280_04S.num = num_;
                                bme280_04S.Name = name25;// for test only. delete in future.
                                bme280_04S.Age = SensorData_04.Count;// for test only. delete in future. 
                                bme280_04S.date = DateTimeOffset.Now;
                                context.Update(bme280_04S); 
                            }
                            await context.SaveChangesAsync();
                            break;

                            case 55555://your secret number
                            DateTimeOffset dateSearch_05 = DateTimeOffset.Now;
                            if (SensorData_05.Count < dbCountString)
                            { 
                               bme280_05.temp = temp_;
                               bme280_05.hum = hum_;
                               bme280_05.num = num_;
                               bme280_05.Name = name25;// for test only. delete in future.
                               bme280_05.Age = SensorData_04.Count;// for test only. delete in future.
                               bme280_05.date = DateTimeOffset.Now;
                               context.SensorData_05.Add(bme280_05);
                            }
                            else
                            {
                                for (int i = 0; i < (SensorData_05.Count); i++)
                                {
                                    if (SensorData_05[i].date < dateSearch_05)
                                    {
                                        dateSearch_05 = SensorData_05[i].date;
                                        iSearch_05 = i;
                                    }
                                }
                                Sensor_05? bme280_05S = SensorData_05[iSearch_05]; //приравниваем для дальнейшей коррекции
                                bme280_05S.temp = temp_;
                                bme280_05S.hum = hum_;
                                bme280_05S.num = num_;
                                bme280_05S.Name = name25;// for test only. delete in future.
                                bme280_05S.Age = SensorData_04.Count;// for test only. delete in future. 
                                bme280_05S.date = DateTimeOffset.Now;
                                context.Update(bme280_05S); 
                            }
                            await context.SaveChangesAsync();
                            break;
                        }

                    }

                }
        

        public string PrintTime() => DateTimeOffset.Now.ToString();

        public Sensor_01 bme280_01 { get; set; } = new();//поле для записи с датчиков в базу данных
        //public Sensor_01 bme280_01test { get; set; } = new();//поле для записи с датчиков в базу данных
        public Sensor_02 bme280_02 { get; set; } = new();//поле для записи с датчиков в базу данных
        public Sensor_03 bme280_03 { get; set; } = new();//поле для записи с датчиков в базу данных
        public Sensor_04 bme280_04 { get; set; } = new();//поле для записи с датчиков в базу данных
        public Sensor_05 bme280_05 { get; set; } = new();//поле для записи с датчиков в базу данных
        public User Person { get; set; } = new();//поле для записи с датчиков в базу данных
        //подключение базы данных - 2начало
        
        ApplicationContext context;
        public List<Sensor_01> SensorData_01 { get; private set; } = new();
        //public List<Sensor_01> SensorData { get; set; } = new(); // for test only
        public List<Sensor_02> SensorData_02 { get; private set; } = new();
        public List<Sensor_03> SensorData_03 { get; private set; } = new();
        public List<Sensor_04> SensorData_04 { get; private set; } = new();
        public List<Sensor_05> SensorData_05 { get; private set; } = new();
        public List<User> Users { get; private set; } = new();

        public IndexModel(ApplicationContext db)
        {
            context = db;
        }

		public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var bme280 = await context.SensorData_01.FindAsync(id); 
            if (bme280 != null) 
            { 
                context.SensorData_01.Remove(bme280);
                await context.SaveChangesAsync();   
            }
            return RedirectToPage();
        }
        //подключение базы данных - 2конец

        public int iMaxSearch(List<Sensor_01> SensorData) //может в cshtml можно сделать сортировку проще?
        {
            int iSearchMax = 0;
            DateTimeOffset? dateMaxSearch = SensorData[0].date;
            for (int i = 0; i < (SensorData.Count); i++)
            {
                if (SensorData[i].date > dateMaxSearch)
                {
                    dateMaxSearch = SensorData[i].date;
                    iSearchMax = i;
                }
            }
            return iSearchMax;

        }
        public int iMaxSearch(List<Sensor_02> SensorData) //перегрузка для List<Sensor_02>
        {
            int iSearchMax = 0;
            DateTimeOffset? dateMaxSearch = SensorData[0].date;
            for (int i = 0; i < (SensorData.Count); i++)
            {
                if (SensorData[i].date > dateMaxSearch)
                {
                    dateMaxSearch = SensorData[i].date;
                    iSearchMax = i;
                }
            }
            return iSearchMax;

        }
        public int iMaxSearch(List<Sensor_03> SensorData) //перегрузка для List<Sensor_03>
        {
            int iSearchMax = 0;
            DateTimeOffset? dateMaxSearch = SensorData[0].date;
            for (int i = 0; i < (SensorData.Count); i++)
            {
                if (SensorData[i].date > dateMaxSearch)
                {
                    dateMaxSearch = SensorData[i].date;
                    iSearchMax = i;
                }
            }
            return iSearchMax;

        }
        public int iMaxSearch(List<Sensor_04> SensorData) //перегрузка для List<Sensor_04>
        {
            int iSearchMax = 0;
            DateTimeOffset? dateMaxSearch = SensorData[0].date;
            for (int i = 0; i < (SensorData.Count); i++)
            {
                if (SensorData[i].date > dateMaxSearch)
                {
                    dateMaxSearch = SensorData[i].date;
                    iSearchMax = i;
                }
            }
            return iSearchMax;

        }
        public int iMaxSearch(List<Sensor_05> SensorData) //перегрузка для List<Sensor_05>
        {
            int iSearchMax = 0;
            DateTimeOffset? dateMaxSearch = SensorData[0].date;
            for (int i = 0; i < (SensorData.Count); i++)
            {
                if (SensorData[i].date > dateMaxSearch)
                {
                    dateMaxSearch = SensorData[i].date;
                    iSearchMax = i;
                }
            }
            return iSearchMax;

        }


    }
    
}
    