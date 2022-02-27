using Database;
using BisnessLogic.Models;
using BisnessLogic.Helper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicationAdmin.Controllers
{
    public class AdminController : Controller
    {
        WorkFile wf;
        MailLogic mLogic;
        List<User> list;
        public AdminController()
        {
            wf = new WorkFile();
            list = wf.GetUsers();
            mLogic = new MailLogic();
        }
        // GET: /Admin/Enter/
        // Вход админа
        [HttpGet]
        public IActionResult Enter()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Enter")]
        public IActionResult Enter(string login, string pass)
        {
            var cheak = list.FirstOrDefault(rec => rec.Login == login && rec.Role == Role.Администратор.ToString());
            //wf.GetAdminData(login);
            //Если нет такого логина
            //if (cheak.Length == 0)
            if (cheak == null)
                return Content("В системе нет пользователя с такими данными.");
            else
            {
                /*
                //Если введен неправильный пароль
                if (cheak.Split(' ')[1] != pass)
                    return Content("Неправильно введен пароль.");
                else
                    return View("../Home/Main");
                */
                //Если введен неправильный пароль
                if (cheak.Password != pass)
                    return Content("Неправильно введен пароль.");
                else
                    return View("../Home/Main");
            }
        }

        [HttpGet]
        public IActionResult RegisterEditor()
        {
            return View("RegisterEditor");
        }

        [HttpPost]
        public IActionResult RegisterEditor(string login, string email, string pass,
                                            string last, string first, string otch, string work)
        {
            var user = new User
            {
                Login = login,
                Email = email,
                Password = pass,
                LastName = last,
                Name = first,
                Otch = otch,
                Work = work,
                Role = Role.Редактор.ToString()
            };
            wf.WriteUser(user);
            mLogic.Send(user.Email, "Регистрация в научном журнале",
                            "Уважаемый(ая) " + user.LastName + " " + user.Name + " " + user.Otch
                            + ",сообщаем Вам, что Вы зарегистрированы в системе научного журнала как редактор.\n" +
                            "Ваш логин: " + user.Login + "\nВаш пароль: " + user.Password);
            return View("RegisterEditor");
        }


        [HttpGet]
        public IActionResult BlockUser()
        {
            list = wf.GetUsers();
            var users = list.Where(rec => rec.Role != Role.Администратор.ToString()).ToList();
            List<int> ids = new List<int>();
            for (int i = 0; i < users.Count; i++)
            {
                ViewData[users.ElementAt(i).Id.Value.ToString()] = i + 1;
            }
            IEnumerable<User> data = users;
            
            /*
            ViewData["Count"] =users.Count;
            for (int i = 0; i < users.Count; i++)
            {
                ViewData["Login" + i] = users.ElementAt(i).Login;
                ViewData["Last" + i] = users.ElementAt(i).LastName;
                ViewData["Name" + i] = users.ElementAt(i).Name;
                ViewData["Otch" + i] = users.ElementAt(i).Otch;
                ViewData["Work" + i] = users.ElementAt(i).Work;
                ViewData["Email" + i] = users.ElementAt(i).Email;
                ViewData["Role" + i] = users.ElementAt(i).Role;
                ViewData["Block" + i] = users.ElementAt(i).IsBlock;
                ViewData["Id" + i] = i+1;
            }
            */
            //return PartialView(users);
            return View("BlockUser", data);
        }

        [HttpGet]
        public PartialViewResult Update()
        {
            list = wf.GetUsers();
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

            wf.UpdateUser(new User
            { 
                Id=user.Id,
                Login=user.Login,
                Password=user.Password,
                Email=user.Email,
                LastName=user.LastName,
                Name=user.Name,
                Otch=user.Otch,
                Work=user.Work,
                Role=user.Role,
                IsBlock=zn
            });
            list= wf.GetUsers();
            var users = list.Where(rec => rec.Role != Role.Администратор.ToString()).ToList();
            List<int> ids = new List<int>();
            for (int i = 0; i < users.Count; i++)
            {
                ViewData[users.ElementAt(i).Id.Value.ToString()] = i + 1;
            }
            IEnumerable<User> data = users;
            return PartialView(users);
        }
    }
}
