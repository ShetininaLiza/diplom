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
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

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
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        // GET: /User/Register/
        // Регистрация пользователя
        [HttpGet]
        public IActionResult Register()
        {
            return View(new string[] { });
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
                //md.Execute(UserLogic.AddUser, user);
                UserLogic.AddUser(md, user);
                //await Authenticate(user);
                await SendMailNewAutor(user);
                return View(new string[] {"Поздравляем!", "Регистрация в системе прошла успешно." });
            }
            catch (Exception) { return View(new string[] { "Ошибка!", "Пользователь с такими данными уже есть в системе." }); }
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
            return View("Enter", new string[] { });
        }
        
        [HttpPost]
        [ActionName("Enter")]
        public async Task<IActionResult> EnterAsync(string login, string pass, string role)
        {
            
            var dp = new DynamicParameters();
            dp.Add("id", "");
            dp.Add("login", login);
            
            var user = md.Query<User>(UserLogic.GetUsers, dp).ToList();
            //Если нет такого логина
            if (user==null || user.Count==0)
                return View("Enter", new string[] {"Ошибка!", "В системе нет пользователя с такими данными." });
            else
            {
                if (user.ElementAt(0).IsBlock)
                    return View("Enter", new string[] { "Ошибка!", "Данный пользователь заблокирован администратором." });
                else
                {
                    if (user.ElementAt(0).Role != role)
                        return View("Enter", new string[] { "Ошибка!", "Нет пользователя с такой ролью." });
                    //Если введен неправильный пароль
                    if (user.ElementAt(0).Password != pass)
                        return View("Enter", new string[] { "Ошибка!", "Неправильно введен пароль." });
                    else
                    {

                        if (user.ElementAt(0).Role == Role.Автор.ToString())
                        {
                            Program.user = user.ElementAt(0);
                            await Authenticate(user.ElementAt(0));
                            return Redirect("../Home/Autor");
                        }
                        else if (user.ElementAt(0).Role == Role.Рецензент.ToString())
                        {
                            Program.user = user.ElementAt(0);
                            await Authenticate(user.ElementAt(0));
                            return Redirect("../Reviewer/Index");
                        }
                            
                    }    
                }
                return View(new string[] { });
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

            UserLogic.UpdateUser(md, user);
            //md.Execute(UserLogic.UpdateUser, user);
            
            dp = new DynamicParameters();
            dp.Add("id", Program.user.Id);
            dp.Add("login", "");
            
            return View(md.Query<User>(UserLogic.GetUsers,dp));
                //Content("All ok");
                //new EmptyResult();
        }
        private async Task Authenticate(User user)
        {
            //https://metanit.com/sharp/aspnet5/15.7.php
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim("Email", user.Email),
                new Claim("Login", user.Login),
                new Claim("Role", user.Role)
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
    }
}
