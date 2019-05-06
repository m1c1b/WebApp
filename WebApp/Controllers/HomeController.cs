using System;
using System.Collections.Generic;
using System.Web.Mvc;
using WebApp.Models;
namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        public ValuesContext dbV = new ValuesContext(); // Создание контекста данных
        public ActionResult Index()
        {
            string path = $@"C:\Users\viazn\RiderProjects\WebApp\FormForCreatingValues\Values from the lab\{DateTime.Now.Year}\{DateTime.Now.Month}";
            
            if (System.IO.File.Exists(path + $@"\{DateTime.Now.Day}.txt"))    // Существует ли файл
            {
                if (FillDb.WritingCheck(path))
                    FillDb.FillLabVals(path);
            }

            IEnumerable<Value> labValues = dbV.Values;        // Получаем из бд все объекты Value
            ViewBag.Values = labValues;                      // Передаем все объекты в динамическое свойство Labvalues в ViewBag
            return View();                                   // Возвращаем представление
        }
        
        public string Set(string start, string end) // Получает в качестве параметра JSON строку
        {
            return Dot.Creator(start, end);    // Возвращает строку от нужного отрезка времени
        }
        public FileResult GetFile(string start, string end)
        {
            string filepath = Server.MapPath("~/Files/YourData.txt"); // Путь к файлу
            string file_type="application/txt";  // Тип файла - content-type
            string file_name = "YourData.txt"; // Имя файла - необязательно
            DownloadFile.Create(start, end, filepath);
            return File(filepath,file_type,file_name);
        }
    }
}