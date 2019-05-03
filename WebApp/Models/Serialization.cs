using System.Linq;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace WebApp.Models
{
    [DataContract]
    public class Dot 
    {
        [DataMember]
         int Id { get; set; }
        [DataMember]
         double Val { get; set; }
        [DataMember]
         string Time { get; set; }

        private Dot(int id, double val, string time)
        {
            Id = id;
            Val = val;
            Time = time;
        }

        public static string Creator()
        {
            SensorValuesContext dbS = new SensorValuesContext();
            Dot[] dots = new Dot[dbS.Values.Max(v => v.Id) - dbS.Values.Min(v => v.Id)+1]; // Массив объектов из БД
            int i = 0;
            
            for (int id = dbS.Values.Min(v => v.Id); id <= dbS.Values.Max(v => v.Id); id++)
            {
                var value = dbS.Values.Find(id); // Нахождение строчки в БД по значению
                Dot dot = new Dot(value.Id, value.Val, value.Time); // Создаёт объект с параметрами из БД
                
                dots[i] = dot;
                i++;
            }
            
            return JsonConvert.SerializeObject(dots);
        }
    }
}