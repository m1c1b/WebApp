using System.Web.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class GetDataController : Controller
    {
        // GET
        public string Index()
        {
            return Dot.Creator(); // Возвращает JSON строку
        }
    }
}