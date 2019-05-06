using System;
using System.IO;
using System.Text;
using System.Linq;

namespace WebApp.Models
{
    public class DownloadFile
    {
        public static void Create(string start, string end, string path)
        {

            ValuesContext dbV = new ValuesContext();
            DateTime sTime, eTime;
            sTime = Convert.ToDateTime(start);
            eTime = Convert.ToDateTime(end);

            Value valuestart = dbV.Values.Where(v => v.Time >= sTime).FirstOrDefault(); // Начальное значение
            Value valueend = dbV.Values.Where(v => v.Time >= eTime).FirstOrDefault(); // Конечное значение
            if (valueend == null)
            {
                int id = dbV.Values.Max(v => v.Id);
                valueend = dbV.Values.Find(id);
            }

            Dot[] dots = new Dot[valueend.Id - valuestart.Id + 1]; // Массив объектов из БД
            int i = 0;

            for (int id = valuestart.Id; id <= valueend.Id; id++)
            {
                var value = dbV.Values.Find(id); // Нахождение строчки в БД по значению
                if (value == null)
                    break;

                Dot dot = new Dot(value.Id, value.LabVal, value.SensVal, value.Time); // Создаёт объект с параметрами из БД
                dots[i] = dot;
                i++;
            }

            using (FileStream fstream = new FileStream(path, FileMode.OpenOrCreate))
            {
                for (i = 0; i < dots.Length; i++)
                {
                    // преобразуем строку в байты
                    byte[] array = Encoding.Default.GetBytes
                        ($@"L_{dots[i].LabVal} S_{dots[i].SensVal} T_{dots[i].Time}.{dots[i].Time.Millisecond} L-S:{dots[i].LabVal - dots[i].SensVal}" + "\n\n");
                    // запись массива байтов в файл
                    fstream.Write(array, 0, array.Length);
                }
            }
        }
    }
}
