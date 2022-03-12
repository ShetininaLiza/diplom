using Database;
using BisnessLogic.Models;
using BisnessLogic.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Helper;
//


namespace WebApplication.Controllers
{
    public class HomeController : Controller
    {
        private WorkFile wf;
        private MailLogic ml;
        IEnumerable<string> categories;
        MultipleModel model;
        List<string> cat;
        public HomeController()
        {
            wf = new WorkFile();
            ml = new MailLogic();
            
            cat = new List<string>();
            cat.Add("Автоматизированные системы управления");
            cat.Add("Архитектура корабельных систем");
            cat.Add("Информационные системы");
            cat.Add("Искусственный интеллект");
            cat.Add("Исследование операций и принятие решений");
            cat.Add("Математическое моделирование");
            cat.Add("Системы автоматизации проектирования");
            cat.Add("Электротехника и электронные устройства");
            
            var emails= wf.GetUsers().Select(rec => rec.Email).ToList();
            
            model = new MultipleModel();
            model.users = wf.GetUsers();
            model.categories = cat;

            categories = cat;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Autor()
        {
            return View("Autor");
        }
        [HttpGet]
        [ActionName("AddPublication")]
        public IActionResult AddPublic()
        {
            
            return View("AddPublication", categories);
        }
        [HttpPost]
        [ActionName("AddPublication")]
        public async Task<IActionResult> AddPublicationAsync(List<string> last, List<string> name, List<string> otch,
                                            List<string> work, List<string> email,
                                            string title, string annotation, string file, 
                                            string words, List<string> category, List<string> connection)
        {
            var users = wf.GetUsers();
            List<(User, string)> autors = new List<(User, string)>();
            List<int> autorsId = new List<int>();
            List<User> buf = new List<User>();
            for (int i = 0; i < last.Count; i++)
            {
                var user = new User
                {
                    LastName = last.ElementAt(i),
                    Name = name.ElementAt(i),
                    Otch = otch.ElementAt(i),
                    Work = work.ElementAt(i),
                    Email = email.ElementAt(i),
                    Role = Role.Автор.ToString(),
                    IsBlock = false
                };
                var check = users.FirstOrDefault(rec => rec.Email == email.ElementAt(i) 
                                                && rec.Role == Role.Автор.ToString());
                autors.Add((user, "false"));
                await SendMailAutor(user, title);
                
                /*
                //Если нет пользователя в системе
                if (check == null)
                {
                    await SendMailAutor(user, title);
                    //autors.Add((user, connection.ElementAt(i)));
                    //await RegisterAutor(user);
                    //await SendMailNewAutor(user, title);
                }
                else 
                {
                    await SendMailAutor(check, title);
                    //autors.Add((check, connection.ElementAt(i)));
                    //autorsId.Add(check.Id.Value);
                }
                */
            }
            for (int i = 0; i < connection.Count; i++)
            {
                //получили индекс
                int index = Convert.ToInt32(connection.ElementAt(i));
                var autor = autors.ElementAt(index - 1).Item1;
                autors.RemoveAt(index - 1);
                autors.Insert((index - 1), (autor, "true"));
            }

            var publication = new Publication
            {
                Title = title,
                DateCreate = DateTime.Now.Date,
                Annotation = annotation,
                File = file,
                Status = Status.Принята.ToString(),
                //Autors = autors,
                //AutorsId=autorsId,
                KeyWords = words.Split('\n').ToList(),
                Categories = category
            };
            await WritePublic(publication);
            await WriteAutorsPublic(autors);

            return Content("Статья подана.");
                //View("AddPublication");
        }

        
        async Task RegisterAutor(User user)
        {
            user.Login = "user" + (wf.GetLastId(wf.path + "users.xml", "User")+1);
            user.Password = GenerPass.GetPassword(8);
            await Task.Run(() => wf.WriteUser(user));
        }
        async Task SendMailNewAutor(User user, string title) 
        {
            string subject = "Регистрация в научном журнале";
            string mess = "Уважаемый(ая) " + user.LastName + " " + user.Name + " " + user.Otch
                            + ", Вы зарегистрированы в системе научного журнала.\n" +
                            "Ваш логин: " + user.Login + "\nВаш пароль: " + user.Password+"." +
                            "\nТакже уведомляем Вас, что Вы указаны как автор научной публикации "+title+".";
            await Task.Run(() => ml.Send(user.Email, subject, mess));
        }
        async Task SendMailAutor(User user, string title)
        {
            string subject = "Указание авторства статьи";
            string mess = "Уважаемый(ая) " + user.LastName + " " + user.Name + " " + user.Otch
                                            + ", уведомляем Вас, что Вы указаны как автор научной публикации " + title;
            await Task.Run(() => ml.Send(user.Email, subject, mess));
        }
        async Task WritePublic(Publication publication)
        {
            await Task.Run(() => wf.WritePublic(publication));
        }
        async Task WriteAutorsPublic(List<(User, string)> autors)
        {
            await Task.Run(() => wf.WriteAutorsPublic(autors));
        }
        
        [HttpPost]
        public string CheckEmail(string Email)
        {
            //string email = Request.Query["Email"];
            var val= wf.GetUsers().FirstOrDefault(rec => rec.Email == Email);
            /*
            if (val == null)
            {
               return View("AddPublication", categories);
            }
            else
            {
                // ничего не возвращаем
                return new EmptyResult();
            }
            */
            if (val == null)
                return "";
            else
                return val.Email + "," + val.LastName + "," + val.Name + "," + val.Otch + "," + val.Work;
        }
        
        [HttpGet]
        public IActionResult MyPublications()
        {
            var buf = wf.GetPublications(Program.user);
            
            var publications = buf
                .Select(rec => new Models.Publication
                {
                    Id = rec.Id,
                    Title = rec.Title,
                    Annotation = rec.Annotation,
                    DateCreate = rec.DateCreate,
                    DatePublic = rec.DatePublic,
                    File = rec.File,
                    Status = rec.Status,
                    //Autors = rec.Autors,
                    Autors = rec.Autors,
                    Categories = rec.Categories,
                    KeyWords = rec.KeyWords
                }).ToList();
            return View(publications);
        }
    
        [HttpGet]
        public IActionResult GetPublic(int Id)
        {
            var publication = wf.GetPublications(null).Where(rec => rec.Id == Id).ToList();
            return View("Publication", publication);
        }
    }
}
