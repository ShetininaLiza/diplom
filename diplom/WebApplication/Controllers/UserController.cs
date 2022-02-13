using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web;
using WebApplication.Helper;
using WebApplication.Models;
using Microsoft.AspNetCore.Session;
using System.Text;

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
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Register")]
        public IActionResult Register(string login, string email, bool isRedactor, string pass)
        {
            //Проверяем наличие такого логина
            var cheak=file.CheakLogin(login, email);
            //Если нет такого логина
            if (!cheak)
            {
                /*
                //GeneralPassword pas = new GeneralPassword();
                //string pass = pas.GetPass();
                file.WriteUser(login, email, pass);
                mLogic.Send(email, login, pass);
                string authData = $"Регистрация прошла успешно. Login: {login}   Email: {email}   Password: {pass}";
                //return Content(authData);
                */
                InsertDataInTemp("Login", login);
                InsertDataInTemp("Email", email);
                InsertDataInTemp("Password", pass);
                InsertDataInTemp("Role", "Автор");
                TempData.Save();
                return View("NextRegister");
            }
            return Content("Ошибка. Такой пользователь уже есть в системе.");
        }
        private void InsertDataInTemp(string key, string value)
        {
            if (TempData.ContainsKey(key))
                TempData[key] = value;
            else
                TempData.Add(key, value);
        }
        
        [ActionName("Enter")]
        public IActionResult EnterUser()
        {
            return View("Enter");
        }
        [HttpPost]
        [ActionName("Enter")]
        public IActionResult Enter(string login, string pass)
        {
            var cheak=file.GetUserData(login);
            //Если нет такого логина
            if (cheak.Length == 0)
                return Content("В системе нет пользователя с такими данными.");
            else
            { 
                //Если введен неправильный пароль
                if(cheak.Split(' ')[1]!=pass)
                    return Content("Неправильно введен пароль.");
                else
                    return View("../Home/Index");
            }
        }

        // GET: /User/NextStep/ 
        [HttpGet]
        public IActionResult NextStep()
        {
            return View("NextRegister");
        }

        [HttpPost]
        [ActionName("NextStep")]
        public IActionResult NextStep(string last, string first, string otch, string work)
        {
            string login=TempData.Peek("Login").ToString();
            string email = TempData.Peek("Email").ToString();
            string pass = TempData.Peek("Password").ToString();
            string role = TempData.Peek("Role").ToString();
            User user = new Models.User(login, email, pass, last, first, otch, work, role);
            file.WriteUser(user);
            mLogic.Send(user);
            //string authData = $"Регистрация прошла успешно. Login: {user.Login}   Email: {user.Email}   Password: {user.Password}";
            //return Content(authData);
            return View("NextRegister");
        }
    }
}
