using Database;
using BisnessLogic.Models;
using BisnessLogic.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Data;
using Dapper;
//using WebApplication.Models;
using Database.Logic;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using System.Web.Mvc;
using System.Text;
using Microsoft.Web.Helpers;

namespace WebApplication.Controllers
{
    public class HomeController : Controller
    {
        private MailLogic ml;
        private IDbConnection md = Program.database;
        IWebHostEnvironment _appEnvironment;

        IEnumerable<string> categories;
        
        public HomeController(IWebHostEnvironment appEnvironment)
        {
            _appEnvironment = appEnvironment;
            ml = new MailLogic();
            categories = md.Query<string>(CategoriesLogic.GetNamesCategories, new DynamicParameters());
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
            return View("AddPublication", (categories, new string[] { }));
        }
        
        [HttpPost]
        [ActionName("AddPublication")]
        public async Task<IActionResult> AddPublicationAsync(List<string> last, List<string> name, List<string> otch,
                                            List<string> work, List<string> email,
                                            string title, string annotation, IFormFile file, 
                                            string words, List<string> category, List<string> connection, List<string> tel)
        {
            //получили номера авторов для связи в списке
            List<int> autorsId = await ConvertIndexAutors(connection);
            //считываем статью
            var text = await ReadFileAsync(file);
            
            List<long> autors = new List<long>();
            //добавили всех авторов
            for (int i = 0; i < last.Count; i++)
            {
                bool connect = false;
                if (autorsId.Contains(i))
                    connect = true;
                
                var autor = new Autor
                { 
                    Email = email.ElementAt(i),
                    Phone=tel.ElementAt(i),
                    LastName = last.ElementAt(i),
                    Name = name.ElementAt(i),
                    Otch = otch.ElementAt(i),
                    Work = work.ElementAt(i),
                    IsConnect = connect
                };
                try
                {
                    var res=(long)md.ExecuteScalar(AutorLogic.AddAutor, autor);
                    //await SendMailAutor(autor, title);
                    autors.Add(res);
                }
                catch (Exception ex) { return View("AddPublication", (categories, new string[] { "Ошибка!", ex.Message })); }
            }
            

            var publication = new BisnessLogic.Models.Publication
            {
                Title = title,
                Annotation = annotation,
                DateCreate = DateTime.Now.Date,
                Status = Status.Принята.ToString(),
                Text = text,
                KeyWords = words.Split('\n').ToList(),
                Categories = category
            };
            try
            {
                PublicationLogic.AddPublication(md, publication, autors);
            }
            catch (Exception ex) { return View("AddPublication", (categories, new string[] { "Ошибка!", ex.Message })); }
            //ViewBag["Mess"] = "Статья подана.";
            return View("AddPublication", (categories, new string[] {"Поздравляем!", "Статья подана." }));
                //Content("Статья подана.");
        }

        private List<int> GetIndexAutors(List<string> connection)
        {
            List<int> autorsId = new List<int>();
            for (int i = 0; i < connection.Count; i++)
            {
                //получили индекс
                int index = Convert.ToInt32(connection.ElementAt(i));
                //минус 1, так как id в представлении начинаются с 1, а позиции с 0.
                autorsId.Add((index - 1));
            }
            return autorsId;
        }
        async Task<List<int>> ConvertIndexAutors(List<string> connection)
        {
            var result=await Task.Run(()=>GetIndexAutors(connection));
            return result;
        }
        
        private byte[] ReadFileToByteArrayAsync(IFormFile file)
        {
            //Guid.NewGuid
            /*
            //https://metanit.com/sharp/adonetcore/4.7.php
            // массив для хранения бинарных данных файла
            byte[] result = null;
            // считываем переданный файл в массив байтов
            using (var binaryReader = new BinaryReader(file.OpenReadStream()))
            {
                result = binaryReader.ReadBytes((Int32)file.Length);
            }
            */
            byte[] result = null;
            using (var target=new MemoryStream()) 
            {
                file.CopyTo(target);
                result = target.ToArray();
                target.Close();
            }
                /*
                var strim = file.OpenReadStream();
                BinaryReader fs = new BinaryReader(strim);

                //var buf = fs.ReadChars((Int32)strim.Length);
                byte[] result = //buf.Select(c => (byte)c).ToArray();
                    fs.ReadBytes((Int32)strim.Length);
                */
                return result;
        }
        //byte[]
        async Task<byte[]> ReadFileAsync (IFormFile file)
        {
            var result = await Task.Run(() => ReadFileToByteArrayAsync(file));
            return result;
        }
        async Task SendMailAutor(Autor user, string title)
        {
            string subject = "Указание авторства статьи";
            string mess = "Уважаемый(ая) " + user.LastName + " " + user.Name + " " + user.Otch
                                            + ", уведомляем Вас, что Вы указаны как автор научной публикации " + title;
            await Task.Run(() => ml.Send(user.Email, subject, mess));
        }
        
        [HttpPost]
        public string CheckEmail(string Email)
        {
            var val = md.Query<User>("select * from Users Where Email = '" + Email+"';", new DynamicParameters()).ToList();
            if (val == null || val.Count==0)
                return "";
            else
                return val.ElementAt(0).Email + "," 
                    + val.ElementAt(0).LastName + "," 
                    + val.ElementAt(0).Name + "," 
                    + val.ElementAt(0).Otch + ","
                    + val.ElementAt(0).Phone+","
                    + val.ElementAt(0).Work;
        }

        [HttpGet]
        public IActionResult MyPublications()
        {
            //var buf = wf.GetPublications(Program.user);

            var publications = PublicationLogic.GetPublicationList(md, Program.user.Email)
                //buf
                .Select(rec => new Models.Publication
                {
                    Id = rec.Id,
                    Title = rec.Title,
                    Annotation = rec.Annotation,
                    DateCreate = rec.DateCreate,
                    DatePublic = rec.DatePublic,
                    //File = rec.File,
                    Status = rec.Status,
                    //Autors = rec.Autors,
                    Autors = rec.Autors,
                    Categories = rec.Categories,//CategoriesLogic.GetCategoriesForPublic(md, rec.Id.Value),
                    KeyWords = rec.KeyWords
                }).ToList();
            return View(publications);
        }
    
        [HttpGet]
        public IActionResult GetPublic()
        {
            int Id =Convert.ToInt32(Request.Query["Id"]);
            var publication = PublicationLogic.GetPublications(md, Id)
                //wf.GetPublications(null).Where(rec => rec.Id == Id)
                .Select(rec => new Models.Publication
                {
                    Id = rec.Id,
                    Title = rec.Title,
                    Annotation = rec.Annotation,
                    DateCreate = rec.DateCreate,
                    DatePublic = rec.DatePublic,
                    //File = rec.File,
                    Status = rec.Status,
                    //Autors = rec.Autors,
                    Autors = rec.Autors,
                    Categories = rec.Categories,
                    KeyWords = rec.KeyWords
                }).ToList();
            return View("Publication", publication);
        }
        
        [HttpGet]
        public IActionResult GetReview()
        {
            int Id = Convert.ToInt32(Request.Query["Id"]);
            var model = PublicationLogic.GetReviewByIDPublic(md, Id);
            return View(model);
        }
    }
}
