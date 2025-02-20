using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;

namespace YourNamespace.Controllers
{
    public class AccountController : Controller
    {
        private const string CookieUsername = "RememberMeUser";
        private const string CookieLastVisit = "LastVisit";

        [HttpGet]
        public IActionResult Login()
        {
            // Проверка за съществуваща бисквитка с потребителското име
            string savedUsername = Request.Cookies[CookieUsername];

            ViewBag.Username = savedUsername; // Изпращаме го към изгледа
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, bool rememberMe)
        {
            if (!string.IsNullOrEmpty(username))
            {
                if (rememberMe)
                {
                    // Запазване на потребителското име в бисквитка за 30 дни
                    CookieOptions options = new CookieOptions
                    {
                        Expires = DateTime.Now.AddDays(30)
                    };
                    Response.Cookies.Append(CookieUsername, username, options);
                }
                else
                {
                    // Ако "Запомни ме" не е избрано, изтриваме бисквитката
                    Response.Cookies.Delete(CookieUsername);
                }

                // Запазване на последното посещение в бисквитка
                CookieOptions lastVisitOptions = new CookieOptions
                {
                    Expires = DateTime.Now.AddYears(1) // 1 година валидност
                };
                Response.Cookies.Append(CookieLastVisit, DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss"), lastVisitOptions);

                return RedirectToAction("Index", "Home"); // Пренасочване към началната страница
            }

            ViewBag.ErrorMessage = "Моля, въведете потребителско име!";
            return View();
        }
    }
}