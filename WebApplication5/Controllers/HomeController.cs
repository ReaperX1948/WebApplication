using Microsoft.AspNetCore.Mvc;
using System;

namespace LastVisitCookie.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            string lastVisitMessage = "Това е първото ви посещение!";

            // Проверка дали има запазена бисквитка
            if (Request.Cookies.ContainsKey("LastVisit"))
            {
                lastVisitMessage = $"Последно посещение: {Request.Cookies["LastVisit"]}";
            }

            // Записване на текущата дата и час в бисквитка
            var cookieOptions = new Microsoft.AspNetCore.Http.CookieOptions
            {
                Expires = DateTime.Now.AddDays(30) // Бисквитката ще е валидна 30 дни
            };
            Response.Cookies.Append("LastVisit", DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss"), cookieOptions);

            ViewBag.LastVisitMessage = lastVisitMessage;

            return View();
        }
    }
}