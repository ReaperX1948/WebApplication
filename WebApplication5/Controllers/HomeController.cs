using Microsoft.AspNetCore.Mvc;
using System;

namespace YourNamespace.Controllers
{
    public class HomeController : Controller
    {
        private const string CookieLastVisit = "LastVisit";

        public IActionResult Index()
        {
            string lastVisitMessage = "Това е първото ви посещение!";

            if (Request.Cookies.TryGetValue(CookieLastVisit, out string lastVisit))
            {
                lastVisitMessage = $"Последно посещение: {lastVisit}";
            }

            ViewBag.LastVisit = lastVisitMessage;
            return View();
        }
    }
}
