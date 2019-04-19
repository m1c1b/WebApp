﻿using System;
using System.Collections.Generic;
using System.Web.Mvc;
using WebApp.Models;
namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        public LabValuesContext db = new LabValuesContext(); // Создание контекста данных
        public ActionResult Index()
        {
            
            string path = $@"C:\Users\viazn\RiderProjects\WebApp\FormForCreatingValues\Values from the lab\{DateTime.Now.Year}\{DateTime.Now.Month}";
            if(!FillDb.WritingCheck(path))
                FillDb.Fill(path);
            
            IEnumerable<Value> labValues = db.Values; // Получаем из бд все объекты Value
            ViewBag.Values = labValues;            // Передаем все объекты в динамическое свойство Labvalues в ViewBag
            return View();                            // Возвращаем представление
        }
    }
}