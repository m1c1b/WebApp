// Класс для заполнения базы данных значений из лаборатории
using System;
using System.IO;
using System.Linq;

namespace Test
{
    public class FillDb
    {
        public static void FillLabVals(string path)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(path); // Переменная класса для работы с файлами
            string[] lines = new string[3]; // Массив для записи строк из файла
            byte[] readArray;

            if (dirInfo.Exists)
            {
                using (FileStream read = File.OpenRead(path + $@"\{DateTime.Now.Day}.txt"))
                {
                    readArray = new byte[read.Length]; // Преобразуем строку в байты
                    read.Read(readArray, 0, readArray.Length); // Считываем данные
                    string textFromFile = System.Text.Encoding.Default.GetString(readArray); // Декодируем байты в строку

                    #region File strings of laboratory numbers into array of strings

                    for (int i = 0; i < 3; i++)
                    {
                        int indexofnewline = textFromFile.IndexOf('\n');
                        lines[i] = textFromFile.Substring(0, indexofnewline);
                        textFromFile = textFromFile.Remove(0, indexofnewline + 1);
                    }

                    #endregion

                    #region Write this array of strings into Data Base 

                    using (LabValuesContext dbL = new LabValuesContext()) //    Создание контекста данных 
                    {
                        int indexofnewnum;
                        string[] texttodb = new string[2];
                        for (int i = 0; i < 3; i++)
                        {
                            for (int j = 0; j < 2; j++)
                            {
                                indexofnewnum = lines[i].IndexOf(' ');
                                texttodb[j] = lines[i].Substring(0, indexofnewnum);
                                lines[i] = lines[i].Remove(0, indexofnewnum + 1);
                            }

                            if (texttodb[0].Contains(".")) // Если дробное число содержит точку, то запятая заменяется на запятую
                            {
                                texttodb[0] = texttodb[0].Replace(".", ",");
                            }

                            Value value = new Value {Val = Convert.ToDouble(texttodb[0]), Time = texttodb[1]};
                            dbL.Values.Add(value);
                            dbL.SaveChanges();
                        }
                    }

                    #endregion
                }

                #region Adding a new line in .txt file after writing data in data base 

                using (FileStream writing = new FileStream(path + $@"\{DateTime.Now.Day}.txt", FileMode.OpenOrCreate))
                {
                    for (int i = readArray.Length - 1; i >= 1; i--)
                    {
                        readArray[i] = readArray[i - 1];
                    }

                    readArray[0] = 10;
                    writing.Write(readArray, 0, readArray.Length);
                }

                #endregion
            }
        }

        public static bool WritingCheck(string path)
        {
            using (FileStream read = File.OpenRead(path + $@"\{DateTime.Now.Day}.txt"))
            {
                byte[] readArray = new byte[read.Length]; // Преобразуем строку в байты
                read.Read(readArray, 0, readArray.Length);

                return readArray[0] != 10;
            }
        }

        public static void FillSensorVals()
        {
            int id;
            double summ = 0;
            LabValuesContext dbL = new LabValuesContext(); // Создание контекста данных базы со значениями из лаборатории 
            SensorValuesContext dbS = new SensorValuesContext(); // Создание контекста данных базы со значениями датчика 


            for (int i = 0; i < 3; i++)
            {
                id = dbL.Values.Max(v => v.Id) - i;
                var value = dbL.Values.Find(id);
                Console.WriteLine($"{value.Id} - {value.Val} - {value.Time}");
                summ += value.Val;
            }
            Value sensVal = new Value{Val = Math.Round(summ/3,2), Time =Convert.ToString(DateTime.Now.TimeOfDay)};
            dbS.Values.Add(sensVal);
            dbS.SaveChanges();
        }
    }
}