using System;
using System.Linq;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace WebApp.Models
{
   // [DataContract]
    public class Dot 
    {
        [DataMember]
        public int Id { get; set; }      // Id значения
        [DataMember]
        public double LabVal { get; set; }  // Значение
        [DataMember]
        public double SensVal { get; set; }  // Значение
        [DataMember]
        public DateTime Time { get; set; } // Дата создания значения

        public Dot(int id, double labVal, double sensVal, DateTime time)
        {
            Id = id;
            LabVal = labVal;
            SensVal = sensVal;
            Time = time;
        }
        /*
        public static string Creator()
        {
            ValuesContext dbV = new ValuesContext();
            Dot[] dots = new Dot[dbV.Values.Max(v => v.Id) - dbV.Values.Min(v => v.Id)+1]; // Массив объектов из БД
            int i = 0;
            
            
            for (int id = dbV.Values.Min(v => v.Id); id <= dbV.Values.Max(v => v.Id); id++)
            {
                var value = dbV.Values.Find(id); // Нахождение строчки в БД по значению
                Dot dot = new Dot(value.Id, value.LabVal, value.SensVal, value.Time); // Создаёт объект с параметрами из БД
                
                dots[i] = dot;
                i++;
            }
            
            
            foreach (var value in dbV.Values)    // Все объекты бд в массив объектов
            {
                Dot dot = new Dot(value.Id, value.LabVal, value.SensVal, value.Time); // Создаёт объект с параметрами из БД
                dots[i] = dot;
                i++;
            }
            
            return JsonConvert.SerializeObject(dots);
        }
        */
        public static string Creator(string start, string end)
        {
            ValuesContext dbV = new ValuesContext();
            DateTime sTime, eTime;
            sTime = Convert.ToDateTime(start);
            eTime = Convert.ToDateTime(end);
            
            Value valuestart = dbV.Values.Where(v => v.Time >= sTime).FirstOrDefault(); // Начальное значение
            Value valueend = dbV.Values.Where(v => v.Time >= eTime).FirstOrDefault();   // Конечное значение
            if (valueend == null)
            {
                int id = dbV.Values.Max(v => v.Id);
                valueend = dbV.Values.Find(id);
            }
            Dot[] dots = new Dot[valueend.Id-valuestart.Id+1]; // Массив объектов из БД
            int i = 0;
            
            for (int id = valuestart.Id; id <= valueend.Id; id++)
            {
                var value = dbV.Values.Find(id); // Нахождение строчки в БД по значению
                Dot dot = new Dot(value.Id, value.LabVal, value.SensVal, value.Time); // Создаёт объект с параметрами из БД
                dots[i] = dot;
                i++;
            }
            
            /*
            foreach (var value in dbV.Values)    // Все объекты бд в массив объектов
            {
                Dot dot = new Dot(value.Id, value.LabVal, value.SensVal, value.Time); // Создаёт объект с параметрами из БД
                dots[i] = dot;
                i++;
            }
            */
            return JsonConvert.SerializeObject(dots);
        }
    }
}