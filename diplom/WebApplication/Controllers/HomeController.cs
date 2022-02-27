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
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class HomeController : Controller
    {
        private WorkFile wf;
        private MailLogic ml;
        IEnumerable<string> categories;
        public HomeController()
        {
            wf = new WorkFile();
            ml = new MailLogic();
            List<string> cat = new List<string>();
            cat.Add("Автоматизированные системы управления");
            cat.Add("Архитектура корабельных систем");
            cat.Add("Информационные системы");
            cat.Add("Искусственный интеллект");
            cat.Add("Исследование операций и принятие решений");
            cat.Add("Математическое моделирование");
            cat.Add("Системы автоматизации проектирования");
            cat.Add("Электротехника и электронные устройства");
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
        public IActionResult AddPublication(List<string> last, List<string> name, List<string> otch,
                                            List<string> work, List<string> email,
                                            string title, string annotation, string file, 
                                            string words, List<string> category)
        {
            List<User> autors = new List<User>();
            for (int i = 0; i < last.Count; i++)
            {
                autors.Add(new User
                {
                    LastName = last.ElementAt(i),
                    Name=name.ElementAt(i),
                    Otch=otch.ElementAt(i),
                    Work=work.ElementAt(i),
                    Email=email.ElementAt(i)
                });
                ml.Send(email.ElementAt(i), "Указание авторства статьи",
                                            "Уважаемый(ая) " + last.ElementAt(i) + " " + name.ElementAt(i) + " " + otch.ElementAt(i)
                                            + ", уведомляем Вас, что Вы указаны как автор научной публикации " + title);
            }
            wf.WritePublic(new Publication
            {
                Title=title,
                Annotation=annotation,
                File=file,
                Status= Status.Принята,
                Autors=autors,
                KeyWords=words.Split('\n').ToList(),
                Categories=category
            });
            return Content("Статья подана.");
                //View("AddPublication");
        }
    }
}
