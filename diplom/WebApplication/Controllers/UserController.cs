using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web;
using WebApplication.Models;
using Microsoft.AspNetCore.Session;
using System.Text;
using BisnessLogic.Helper;
using BisnessLogic.Models;
using Database;
using System.Linq;

namespace WebApplication.Controllers
{
    public class UserController : Controller
    {
        private MailLogic mLogic;
        private WorkFile file;
        public UserController()
        {
            mLogic = new MailLogic();
            file = new WorkFile();
        }

        // GET: /User/Register/
        // Регистрация пользователя
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Register")]
        public IActionResult Register(string login, string email, string pass)
        {
            //Проверяем наличие такого логина
            var cheak=file.CheakLogin(login, email);
            //Если нет такого логина
            if (!cheak)
            {
                InsertDataInTemp("Login", login);
                InsertDataInTemp("Email", email);
                InsertDataInTemp("Password", pass);
                TempData.Save();
                return View("NextRegister");
            }
            return Content("Ошибка. Такой пользователь уже есть в системе.");
        }
        
        //Метод для добавления значеня во временное хранилище для передачи между страницами
        private void InsertDataInTemp(string key, string value)
        {
            if (TempData.ContainsKey(key))
                TempData[key] = value;
            else
                TempData.Add(key, value);
        }
        //Вход пользователя
        [ActionName("Enter")]
        public IActionResult EnterUser()
        {
            return View("Enter");
        }
        [HttpPost]
        [ActionName("Enter")]
        public IActionResult Enter(string login, string pass, string role)
        {
            var user = file.GetUsers().FirstOrDefault(rec => rec.Login == login);
            //Если нет такого логина
            if (user==null)
                return Content("В системе нет пользователя с такими данными.");
            else
            {
                if (user.IsBlock)
                    return Content("Данный пользователь заблокирован администратором.");
                else
                {
                    if (user.Role != role)
                        return Content("Нет пользователя с такой ролью.");
                    //Если введен неправильный пароль
                    if (user.Password != pass)
                        return Content("Неправильно введен пароль.");
                    else
                        //return View("../Home/Autor");
                        return Redirect("../Home/Autor");
                }
            }
        }

        // GET: /User/NextStep/
        // Переход на вторую страницу регистрации
        [HttpGet]
        public IActionResult NextStep()
        {
            return View("NextRegister");
        }

        [HttpPost]
        [ActionName("NextStep")]
        //Завершение регистрации
        public IActionResult NextStep(string last, string first, string otch, string work)
        {
            string login=TempData.Peek("Login").ToString();
            string email = TempData.Peek("Email").ToString();
            string pass = TempData.Peek("Password").ToString();
            var user = new User
            {
                Login = login,
                Email = email,
                Password = pass,
                LastName = last,
                Name = first,
                Otch = otch,
                Work = work,
                Role = Role.Автор.ToString(),
                IsBlock=false
            };
            file.WriteUser(user);
            mLogic.Send(user.Email, "Регистрация в научном журнале", 
                            "Уважаемый(ая) " + user.LastName + " " + user.Name + " " + user.Otch
                            + ", благодарим за регистрацию в системе научного журнала.\n" +
                            "Ваш логин: " + user.Login + "\nВаш пароль: " + user.Password);
            return View("NextRegister");
        }
    }
}
