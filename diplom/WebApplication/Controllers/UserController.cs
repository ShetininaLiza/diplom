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
        public async Task<IActionResult> RegisterAutorAsync(string login, string email, string pass,
                                        string last, string first, string otch, string work, string tel)
        {
            //Проверяем наличие такого логина
            var cheak = file.CheakLogin(login, email);
            //Если нет такого логина
            if (!cheak)
            {
                var user = new User
                {
                    Login = login,
                    Email = email,
                    Password = pass,
                    LastName = last,
                    Name = first,
                    Otch = otch,
                    Phone=tel,
                    Work = work,
                    Role = Role.Автор.ToString(),
                    IsBlock = false
                };
                await WriteAutor(user);
                await SendMailNewAutor(user);

                return View();
            }
            return Content("Ошибка. Такой пользователь уже есть в системе.");
        }

        async Task WriteAutor(User autor)
        {
            await Task.Run(() => file.WriteUser(autor));
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
                    {
                        Program.user = user;
                        if(user.Role==Role.Автор.ToString())
                            return Redirect("../Home/Autor");
                        else if(user.Role==Role.Рецензент.ToString())
                            return Redirect("../Reviewer/Index");
                    }    
                }
                return View();
            }
        }

       
        [HttpGet]
        public IActionResult UpdateData() 
        {
            var user = file.GetUsers().Where(rec => rec.Id == Program.user.Id).ToList();
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
                LastName=Last,
                Name=First,
                Otch=Otch,
                Work=Work,
                Phone=Tel,
                Role=Program.user.Role,
                Email=Program.user.Email,
                IsBlock=Program.user.IsBlock
            };
            file.UpdateUser(user);
            return Content("All ok");
                //new EmptyResult();
        }
    }
}
