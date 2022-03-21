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
using System.Threading.Tasks;
using System.Data;
using Dapper;
using Microsoft.Data.Sqlite;
using Database.Logic;

namespace WebApplication.Controllers
{
    public class UserController : Controller
    {
        private MailLogic mLogic;
        private WorkFile file;
        //private MyDatabase md = Program.database;
        private IDbConnection md = Program.database;
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
        public async Task<IActionResult> RegisterAutor(string Login, string Email, string Pass,
                                        string Last, string First, string Otch, string Work, string Tel)
        {
            var user = new User
                {
                    Login = Login,
                    Email = Email,
                    Password = Pass,
                    LastName = Last,
                    Name = First,
                    Otch = Otch,
                    Phone = Tel,
                    Work = Work,
                    Role = Role.Автор.ToString(),
                    IsBlock = false
                };
            try
            {
                md.Execute(UserLogic.AddUser, user);
                await SendMailNewAutor(user);
                return View();
            }
            catch (Exception) { return Content("Ошибка. Пользователь с такими данными уже есть в системе."); }
        }

        async Task WriteAutor(User autor)
        {
            try
            {
                await Task.Run(() => md.Execute(UserLogic.AddUser, autor));
            }
            catch(Exception) { throw new Exception("Ошибка. Пользователь с такими данными уже есть в системе."); }
            //.WriteUser(autor));
            //file.WriteUser(autor));
        }
        async Task SendMailNewAutor(User autor)
        {
            await Task.Run(() => mLogic.Send(autor.Email,
                                "Регистрация в научном журнале",
                                "Уважаемый(ая) " + autor.LastName + " " + autor.Name + " " + autor.Otch
                                + ", благодарим за регистрацию в системе научного журнала.\n" +
                                "Ваш логин: " + autor.Login + "\nВаш пароль: " + autor.Password));
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
            var dp = new DynamicParameters();
            dp.Add("id", "");
            dp.Add("login", login);
            
            var user = md.Query<User>(UserLogic.GetUsers, dp).ToList();
            //Если нет такого логина
            if (user==null || user.Count==0)
                return Content("В системе нет пользователя с такими данными.");
            else
            {
                if (user.ElementAt(0).IsBlock)
                    return Content("Данный пользователь заблокирован администратором.");
                else
                {
                    if (user.ElementAt(0).Role != role)
                        return Content("Нет пользователя с такой ролью.");
                    //Если введен неправильный пароль
                    if (user.ElementAt(0).Password != pass)
                        return Content("Неправильно введен пароль.");
                    else
                    {
                        Program.user = user.ElementAt(0);
                        if(user.ElementAt(0).Role==Role.Автор.ToString())
                            return Redirect("../Home/Autor");
                        else if(user.ElementAt(0).Role==Role.Рецензент.ToString())
                            return Redirect("../Reviewer/Index");
                    }    
                }
                return View();
            }
        }

       
        [HttpGet]
        public IActionResult UpdateData() 
        {
            var dp = new DynamicParameters();
            dp.Add("id", Program.user.Id);
            dp.Add("login", "");

            var user = md.Query<User>(UserLogic.GetUsers, dp);
            return View(user);
        }
        [HttpPost]
        public IActionResult UpdateData(string Login, string Pass,
                                        string Last, string First, string Otch, string Work, string Tel)
        {
            User user = new User
            {
                Id = Program.user.Id,
                Login = Login,
                Password = Pass,
                LastName = Last,
                Name = First,
                Otch = Otch,
                Work = Work,
                Phone = Tel,
                Role = Program.user.Role,
                Email = Program.user.Email,
                IsBlock = Program.user.IsBlock,
            };
            
            var dp = new DynamicParameters();
            
            dp.Add("login", Login);
            dp.Add("tel", Tel);
            dp.Add("pass", Pass);
            dp.Add("last", Last);
            dp.Add("name", First);
            dp.Add("otch", Otch);
            dp.Add("work", Work);
            dp.Add("block", Program.user.IsBlock);
            dp.Add("id", Program.user.Id);
            
            md.Execute(UserLogic.UpdateUser, user);
            
            dp = new DynamicParameters();
            dp.Add("id", Program.user.Id);
            dp.Add("login", "");
            
            return View(md.Query<User>(UserLogic.GetUsers,dp));
                //Content("All ok");
                //new EmptyResult();
        }
    }
}
