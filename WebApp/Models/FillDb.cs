// Класс для заполнения базы данных значений из лаборатории
using System;
using System.IO;
using System.Linq;

namespace WebApp.Models
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
                        lines[i] = textFromFile.Substring(0, indexofnewline-1);
                        textFromFile = textFromFile.Remove(0, indexofnewline + 1);
                    }

                    #endregion

                    #region Write this array of strings into Data Base 

                    using (ValuesContext dbV = new ValuesContext()) //    Создание контекста данных 
                    {
                        int lastindexofnewnum;
                        int startindexofnewnum;
                        string[] texttodb = new string[3];
                        for (int i = 0; i < 3; i++)
                        {
                            for (int j = 0; j < texttodb.Length; j++)
                            {
                                startindexofnewnum = lines[i].IndexOf('_');
                                lastindexofnewnum = lines[i].IndexOf(' ');
                                
                                if (j == texttodb.Length-1)
                                {
                                    lastindexofnewnum = lines[i].LastIndexOf(' ');
                                }
                                
                                texttodb[j] = lines[i].Substring(startindexofnewnum+1, lastindexofnewnum-1);
                                lines[i] = lines[i].Remove(0, lastindexofnewnum + 1);
                            }

                            Value value = new Value(Convert.ToDouble(texttodb[0]), Convert.ToDouble(texttodb[1]), Convert.ToDateTime(texttodb[2]));
                            dbV.Values.Add(value);
                            dbV.SaveChanges();
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
    }
}