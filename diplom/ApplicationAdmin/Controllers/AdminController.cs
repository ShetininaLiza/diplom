using Database;
using BisnessLogic.Models;
using BisnessLogic.Helper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Database.Logic;
using System.Threading.Tasks;
using System.Data;

namespace ApplicationAdmin.Controllers
{
    public class AdminController : Controller
    {
        MailLogic mLogic;
        private IDbConnection database = Program.database;
        List<User> list;
        public AdminController()
        {
            list = UserLogic.GetAllUser(database);
            mLogic = new MailLogic();
        }
        // GET: /Admin/Enter/
        // Вход админа
        [HttpGet]
        public IActionResult Enter()
        {
            return View(new string[] { });
        }

        [HttpPost]
        [ActionName("Enter")]
        public IActionResult Enter(string login, string pass)
        {
            var cheak = list.FirstOrDefault(rec => rec.Login == login && rec.Role == Role.Администратор.ToString());
            //Если нет такого логина
            if (cheak == null)
                return View(new string[] {"Ошибка!", "В системе нет пользователя с такими данными." });
            else
            {
                //Если введен неправильный пароль
                if (cheak.Password != pass)
                    return View(new string[] { "Ошибка!", "Неправильно введен пароль." });
                else
                    return View("../Home/Main");
            }
        }

        [HttpGet]
        public IActionResult RegisterEditor()
        {
            return View("RegisterEditor", new string[] { });
        }

        [HttpPost]
        public IActionResult RegisterEditor(string login, string email, string pass,
                                            string last, string first, string otch, string work, string tel)
        {
            var user = new User
            {
                Login = login,
                Email = email,
                Password = pass,
                LastName = last,
                Phone=tel,
                Name = first,
                Otch = otch,
                Work = work,
                Role = Role.Редактор.ToString()
            };
            try
            {
                UserLogic.AddUser(database, user);

                mLogic.Send(user.Email, "Регистрация в научном журнале",
                            "Уважаемый(ая) " + user.LastName + " " + user.Name + " " + user.Otch
                            + ",сообщаем Вам, что Вы зарегистрированы в системе научного журнала как редактор.\n" +
                            "Ваш логин: " + user.Login + "\nВаш пароль: " + user.Password);
                return View("RegisterEditor", new string[] {"Поздравляем!","Редактор был успешно зарегистрирован в системе." });
            }
            catch (Exception ex)
            {
                return View("RegisterEditor", new string[] { "Ошибка!", ex.Message });
            }
        }


        [HttpGet]
        public IActionResult BlockUser()
        {
            list = UserLogic.GetAllUser(database);
            var users = list.Where(rec => rec.Role != Role.Администратор.ToString()).ToList();
            List<int> ids = new List<int>();
            for (int i = 0; i < users.Count; i++)
            {
                ViewData[users.ElementAt(i).Id.Value.ToString()] = i + 1;
            }
            IEnumerable<User> data = users;
            
            return View("BlockUser", data);
        }

        [HttpGet]
        public PartialViewResult Update()
        {
            list = UserLogic.GetAllUser(database);
            var users = list.Where(rec => rec.Role != Role.Администратор.ToString()).ToList();
            List<int> ids = new List<int>();
            for (int i = 0; i < users.Count; i++)
            {
                ViewData[users.ElementAt(i).Id.Value.ToString()] = i + 1;
            }
            IEnumerable<User> data = users;
            return PartialView(data);
        }
        public IActionResult AddBlock(string login)
        {
            var user = list.FirstOrDefault(rec => rec.Login == login);
            bool zn = false;
            //Если пользователь не заблокирован, то метку переводим в значение блока
            if (!user.IsBlock)
                zn = true;
            try
            {
                User buf = new User
                {
                    Id = user.Id,
                    Login = user.Login,
                    Password = user.Password,
                    Phone = user.Phone,
                    Email = user.Email,
                    LastName = user.LastName,
                    Name = user.Name,
                    Otch = user.Otch,
                    Work = user.Work,
                    Role = user.Role,
                    IsBlock = zn
                };

                UserLogic.UpdateUser(database, buf);
                
                list = UserLogic.GetAllUser(database);
                var users = list.Where(rec => rec.Role != Role.Администратор.ToString()).ToList();
                List<int> ids = new List<int>();
                for (int i = 0; i < users.Count; i++)
                {
                    ViewData[users.ElementAt(i).Id.Value.ToString()] = i + 1;
                }
                IEnumerable<User> data = users;
                return //PartialView(users);
                    View("BlockUser", data);
            }
            catch (Exception) 
            {
                return PartialView(list);
            }
            
        }
    }
}
