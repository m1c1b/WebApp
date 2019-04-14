// Класс для заполнения базы данных значений из лаборатории
using System;
using System.IO;

namespace WebApp.Models
{
    public class FillDb
    {
        static string path = $@"C:\Users\viazn\RiderProjects\WebApp\FormForCreatingValues\Values from the lab\{DateTime.Now.Year}\{DateTime.Now.Month}"; // Путь нахождения файла с данными
        DirectoryInfo dirInfo = new DirectoryInfo(path);
        string[] lines = new string[3]; // Массив для записи строк из файла
        public void Fill()
        {
            if (dirInfo.Exists)
            {
                using (FileStream fstream = File.OpenRead($@"C:\Users\viazn\RiderProjects\WebApp\FormForCreatingValues\Values from the lab\{DateTime.Now.Year}\{DateTime.Now.Month}\{DateTime.Now.Day}.txt"))
                {
                    byte[] array = new byte[fstream.Length];    // Преобразуем строку в байты
                    fstream.Read(array, 0, array.Length);    // Считываем данные
                    string textFromFile = System.Text.Encoding.Default.GetString(array);    // Декодируем байты в строку

                    #region File strings of laboratory numbers into array of strings
                    for (int i = 0; i < 3; i++)
                    {
                        int indexofnewline = textFromFile.IndexOf('\n');
                        lines[i] = textFromFile.Substring(0, indexofnewline);
                        textFromFile = textFromFile.Remove(0, indexofnewline+1);
                    }
                    #endregion
                    
                    #region Write this array of strings into Data Base 
                    using (LabValuesContext db = new LabValuesContext()) //    Создание контекста данных 
                    {
                        int indexofnewnum;
                        string[] texttodb = new string[2];
                        for (int i = 0; i < 3; i++)
                        {
                            for (int j = 0; j < 2; j++)
                            {
                                indexofnewnum = lines[i].IndexOf(' ');
                                texttodb[j] = lines[i].Substring(0, indexofnewnum);
                                lines[i] = lines[i].Remove(0, indexofnewnum+1);
                            }
                            
                            if (texttodb[0].Contains("."))    // Если дробное число содержит точку, то запятая заменяется на запятую
                            {
                                texttodb[0] = texttodb[0].Replace(".", ",");
                            }
                            
                            Value value = new Value{Val = Convert.ToDouble(texttodb[0]), Date = texttodb[1]};
                            db.Values.Add(value);
                            db.SaveChanges();
                        }   
                    }
                    #endregion
                    
                }
            }
        }
    }
}