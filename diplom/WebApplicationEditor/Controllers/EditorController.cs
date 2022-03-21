using BisnessLogic.Helper;
using BisnessLogic.Models;
using Dapper;
using Database;
using Database.Logic;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicationEditor.Controllers
{
    public class EditorController : Controller
    {
        //WorkFile wf;
        IDbConnection database = Program.database;
        MailLogic mLogic;
        List<User> users;
        int publicId = 0;
        public EditorController()
        {
            //wf = new WorkFile();
            mLogic = new MailLogic();
            
            users = UserLogic.GetAllUser(database);
        }

        [HttpGet]
        public IActionResult Enter()
        {
            return View("Enter");
        }

        [HttpPost]
        public IActionResult Enter(string login, string pass) 
        {
            var dp = new DynamicParameters();
            dp.Add("id", "%");
            dp.Add("login", login);

            var editor = //wf
                             //database.GetUsers().FirstOrDefault(rec => rec.Login == login && rec.Role == Role.Редактор.ToString());
                    database.Query<User>(UserLogic.GetUsers, dp).ToList();
            if (editor == null || editor.Count==0)
                return Content("В системе нет пользователя с такими данными.");
            else
            {
                if (editor.ElementAt(0).IsBlock)
                    return Content("Данный пользователь заблокирован администратором.");
                else
                {
                    //Если введен неправильный пароль
                    if (editor.ElementAt(0).Password != pass)
                        return Content("Неправильно введен пароль.");
                    else
                    {
                        Program.editor = editor.ElementAt(0);
                        return Redirect("../Home/Main");
                    }
                }
            }
        }
        
        [ActionName("Reviewers")]
        public IActionResult GetReviwers()
        {
            
            var user = //wf
                       users.Where(rec => rec.Role == Role.Рецензент.ToString()).ToList();
            return View("Reviewers", user);
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task <IActionResult> Register(string login, string email, string pass,
                                    string last, string first, string otch, string work, string tel)
        {
            /*
            var cheak = //wf
                        database.GetUsers().FirstOrDefault(rec => rec.Login == login && rec.Role == Role.Рецензент.ToString());
            //Если нет такого логина
            if (cheak==null)
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
                    Role = Role.Рецензент.ToString(),
                    IsBlock = false
                };
                try
                {
                    //wf.WriteUser(user);
                    database.WriteUser(user);
                    mLogic.Send(user.Email, "Регистрация в научном журнале",
                                    "Уважаемый(ая) " + user.LastName + " " + user.Name + " " + user.Otch
                                    + ", Вы были зарегистрированы в системе научного журнала как рецензент.\n" +
                                    "Ваш логин: " + user.Login + "\nВаш пароль: " + user.Password);
                    return View();
                }
                catch (Exception ex) { return Content(ex.Message); }
            }
            return Content("Ошибка. Такой пользователь уже есть в системе.");
            */
            var user = new User
            {
                Login = login,
                Email = email,
                Password = pass,
                LastName = last,
                Phone = tel,
                Name = first,
                Otch = otch,
                Work = work,
                Role = Role.Рецензент.ToString(),
                IsBlock = false
            };
            try
            {
                database.Execute(UserLogic.AddUser, user);
                users = UserLogic.GetAllUser(database); 
                    //database.QueryAsync<User>(UserLogic.GetUsers, dp).Result.ToList();
                await SendMail(user);
                return View();
            }
            catch (Exception) { return Content("Ошибка. Пользователь с такими данными уже есть в системе."); }
        }

        async Task SendMail(User user)
        {
            await Task.Run(() => mLogic.Send(user.Email,
                                "Регистрация в научном журнале",
                                 "Уважаемый(ая) " + user.LastName + " " + user.Name + " " + user.Otch
                                    + ", Вы были зарегистрированы в системе научного журнала как рецензент.\n" +
                                    "Ваш логин: " + user.Login + "\nВаш пароль: " + user.Password));
        }
    
        [HttpGet]
        public IActionResult Publications()
        {
            var publications = PublicationLogic.GetPublications(database, null)
                .Select(rec => new ApplicationEditor.Models.Publication
                {
                    Id=rec.Id.Value,
                    Title=rec.Title,
                    Annotation=rec.Annotation,
                    DateCreate=rec.DateCreate,
                    Status=rec.Status,
                    KeyWords=rec.KeyWords,
                    Categories=rec.Categories,
                    Autors=rec.Autors,
                    ReviewerId=rec.ReviewerId,
                    ReviewerFIO=UserLogic.GetFIOUser(database, rec.ReviewerId)
                }).ToList();
            
            
            
            return View(publications);
        }

        [HttpGet]
        public IActionResult GetPublic()
        {
            var Id = Convert.ToInt32(Request.Query["Id"]);
            
            var publication = PublicationLogic.GetPublications(database, Id)
                //wf.GetPublications(null).Where(rec => rec.Id == Id)
                .Select(rec => new Models.Publication
                {
                    Id = rec.Id.Value,
                    Title = rec.Title,
                    Annotation = rec.Annotation,
                    DateCreate = rec.DateCreate,
                    DatePublic = rec.DatePublic,
                    Status = rec.Status,
                    Autors = rec.Autors,
                    Categories = rec.Categories,
                    KeyWords = rec.KeyWords,
                    ReviewerId = rec.ReviewerId,
                    ReviewerFIO = UserLogic.GetFIOUser(database, rec.ReviewerId)
                }).ToList();
            var user = users.Where(rec => rec.Role == Role.Рецензент.ToString()).ToList();

            return View("Publication", (publication, user));
        }
    
        [HttpPost]
        public IActionResult Update(string publicId, string reviewerId)
        {
            int revId = Convert.ToInt32(reviewerId);
            int Id = Convert.ToInt32(publicId);
            try
            {
                PublicationLogic.UpdatePublic(database, Id, revId, Status.Отправлена_рецензенту.ToString());
                return Content("All Ok");
                    //new EmptyResult();
            }
            catch (Exception ex) { return Content(ex.Message); }
            
        }
    }
}
