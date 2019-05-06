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
    }
}