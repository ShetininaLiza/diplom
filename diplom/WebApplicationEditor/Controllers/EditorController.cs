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
        IDbConnection database = Program.database;
        MailLogic mLogic;
        List<User> users;
        List<ApplicationEditor.Models.Publication> publications;
        public EditorController()
        {
            mLogic = new MailLogic();
            users = UserLogic.GetAllUser(database);
            publications = GetPublications();
        }

        [HttpGet]
        public IActionResult Enter()
        {
            return View("Enter", new string[] { });
        }

        [HttpPost]
        public IActionResult Enter(string login, string pass) 
        {
            var dp = new DynamicParameters();
            dp.Add("id", "%");
            dp.Add("login", login);

            var editor = database.Query<User>(UserLogic.GetUsers, dp).ToList();
            if (editor == null || editor.Count==0)
                return View("Enter", new string[] {"Ошибка!", "В системе нет пользователя с такими данными." });
            else
            {
                if (editor.ElementAt(0).IsBlock)
                    return View("Enter", new string[] { "Ошибка!", "Данный пользователь заблокирован администратором." });
                else
                {
                    //Если введен неправильный пароль
                    if (editor.ElementAt(0).Password != pass)
                        return View("Enter", new string[] { "Ошибка!", "Неправильно введен пароль." });
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
            
            var user = users.Where(rec => rec.Role == Role.Рецензент.ToString()).ToList();
            return View("Reviewers", user);
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View(new string[] { });
        }
        [HttpPost]
        public async Task <IActionResult> Register(string login, string email, string pass,
                                    string last, string first, string otch, string work, string tel)
        {
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
                //database.Execute(UserLogic.AddUser, user);
                UserLogic.AddUser(database, user);
                users = UserLogic.GetAllUser(database); 
                    //database.QueryAsync<User>(UserLogic.GetUsers, dp).Result.ToList();
                await SendMail(user);
                return View(new string[] {"Поздравляем!","Рецензент был удачно зарегистрирован в системе." });
            }
            catch (Exception) { return View(new string[] { "Ошибка!", "Пользователь с такими данными уже есть в системе." }); }
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
            return View((publications, "All"));
        }
        [HttpPost]
        public IActionResult GetPublicationList(string nameView, string selectedStatus = "All")
        {
            var publics = publications;
            if (selectedStatus != "All")
            {
                publics = publications.Where(rec => rec.Status == selectedStatus).ToList(); 
            }
            if (nameView == "CreateMagazine")
                return View(nameView, (publics, selectedStatus, new string[] { }));
            else
                return View(nameView, (publics, selectedStatus));
        }
        [HttpGet]
        public IActionResult GetPublic()
        {
            var Id = Convert.ToInt32(Request.Query["Id"]);

            var publication = publications.First(rec => rec.Id == Id);
            var user = users.Where(rec => rec.Role == Role.Рецензент.ToString()).ToList();

            return View("Publication", (publication, user, new string[] { }));
        }
        [HttpPost]
        [ActionName("DownloadFile")]
        public IActionResult DownloadFile(int id)
        {
            
            var publication = PublicationLogic.GetPublications(database, id).First();
            //обновление статьи (меняем статус)
            try
            {
                var byteArray = publication.Text;
                /*
                string mineType = "application/pdf";
                return new FileContentResult(byteArray, mineType)
                {
                    FileDownloadName = $"publication_" + id + ".pdf"
                };
                */
                //return new FileContentResult(byteArray, "application/pdf");
                return File(byteArray, "application/txt");
                //return File(publication.Text, "application/pdf");
                //return File(byteArray, "application/" +
                                //"vnd.openxmlformats-officedocument.wordprocessingml.document"
                                //"docx"
                                //"vnd.ms-word"
                                //"ms-word"
                                //"octet-stream");
                                //"x-zip-compressed"
                                //);
                
            }
            catch (Exception ex) { return Content(ex.Message); }

        }

        [HttpPost]
        public IActionResult Update(string publicId, string reviewerId)
        {
            int revId = Convert.ToInt32(reviewerId);
            int Id = Convert.ToInt32(publicId);
            var publication = publications.First(rec => rec.Id == Id);
            try
            {
                var res = PublicationLogic.UpdatePublic(database, Id, revId, Status.Отправлена_рецензенту.ToString());
                var model= new ApplicationEditor.Models.Publication
                    { 
                        Id = res.Id.Value,
                        Title = res.Title,
                        Annotation = res.Annotation,
                        DateCreate = res.DateCreate,
                        Status = res.Status,
                        KeyWords = res.KeyWords,
                        Categories = res.Categories,
                        Autors = res.Autors,
                        ReviewerId = res.ReviewerId,
                        ReviewerFIO = UserLogic.GetFIOUser(database, res.ReviewerId)
                    }; 
                //????????????????????????????????????
                return View("Publication", (model, new List<User>(), new string[] { "Поздравляем!", "Изменения были удачно внесены."}));
                    //new EmptyResult();
            }
            catch (Exception ex) 
            { 
                //??????????????????????????????????????????????????????
                return View("Publication", (publication, new List<User>(), new string[] { "Ошибка!", ex.Message})); 
            }
            
        }
        [HttpPost]
        public IActionResult ClosePublication(string publicId)
        {
            int id = Convert.ToInt32(publicId);
            var res=PublicationLogic.UpdateStatusPublication(database, id, BisnessLogic.Models.Status.Отклонена.ToString());
            return View("Publication", ((res, new List<User>(), new string[] { "Поздравляем!", "Изменения были удачно внесены." })));
                //new EmptyResult();
        }
        [HttpGet]
        public IActionResult CreateMagazine()
        {
            return View((publications, "All", new string[] { }));
        }
        [HttpGet]
        public IActionResult GetReview()
        {
            int Id = Convert.ToInt32(Request.Query["Id"]);
            var model = PublicationLogic.GetReviewByIDPublic(database, Id);
            return View((model, new string[] { }));
        }
        [HttpPost]
        public IActionResult UpdateStatus(string PublicId, string status)
        {
            int id = Convert.ToInt32(PublicId);
            var model = PublicationLogic.GetReviewByIDPublic(database, id);
            try
            {
                var data = PublicationLogic.UpdateStatusPublication(database, id, status);
                return View("GetReview", (model, new string[] {"Поздравляем!","Статус статьи успешно обновлен." }));
            }
            catch (Exception ex) { return View("GetReview", (model, new string[] {"Ошибка!",ex.Message})); }
        }

        [HttpPost]
        public IActionResult CreateMagazine(List<string>checkbox, string date)
        {
            var proverka=CheakDataMagazine(date);
            if (proverka == "false")
            {
                return View((publications, "All", new string[] { "Ошибка!", 
                    "В этом квартале уже выходил номер. Выберите другую дату."}));
            }
            try
            {
                List<Publication> buf = new List<Publication>();
                for(int i=0; i < checkbox.Count; i++)
                {
                    buf.Add(publications.Where(rec => rec.Id == Convert.ToInt32(checkbox.ElementAt(i)))
                        .Select(rec=>new BisnessLogic.Models.Publication 
                        { 
                            Id=rec.Id,
                            Title=rec.Title,
                            Annotation=rec.Annotation,
                            Text=rec.Text,
                            DateCreate = rec.DateCreate,
                            DatePublic = rec.DatePublic,
                            Status = rec.Status,
                            Categories = rec.Categories,
                            //CategoriesLogic.GetCategoriesForPublic(db, rec.Id.Value),
                            Autors = rec.Autors,
                            //AutorLogic.GetAutorsForPublication(db, rec.Id.Value),
                            ReviewerId = rec.ReviewerId,
                            KeyWords = rec.KeyWords,
                            //rec.KeyWords.Split(',').ToList()
                        }).First());
                }
                Magazine magazine = new Magazine
                {
                    //Id = 67,
                    Date = Convert.ToDateTime(date),
                    Publications=buf
                };
                //создаем выпуск журнала и обновляем статус статей указаных в журнале
                MagazineLogic.AddMagazine(database, magazine);
                //считываем все публикации
                var publicUpdate = PublicationLogic.GetPublications(database, null)
                    .Where(rec=>rec.Status==Status.Готова_к_публикации.ToString()).ToList();
                for (int i = 0; i < publicUpdate.Count; i++) 
                {
                    PublicationLogic.UpdateStatusPublication(database,
                    publicUpdate.ElementAt(i).Id.Value, Status.Публикуется_в_слудующем_номере.ToString());
                }
                publications = GetPublications();

                return View((publications, "All", new[] { "Поздравляем!", "Журнал успешно создан." }));
            }
            catch (Exception ex) { return View((publications, "All", new string[] {"Ошибка!" ,ex.Message })); }
            //return View((publications, "All", ""));
        }

        private List<ApplicationEditor.Models.Publication> GetPublications()
        {
            var res=PublicationLogic.GetPublications(database, null)
                .Select(rec => new ApplicationEditor.Models.Publication
                {
                    Id = rec.Id.Value,
                    Title = rec.Title,
                    Annotation = rec.Annotation,
                    DateCreate = rec.DateCreate,
                    Status = rec.Status,
                    KeyWords = rec.KeyWords,
                    Categories = rec.Categories,
                    Autors = rec.Autors,
                    ReviewerId = rec.ReviewerId,
                    ReviewerFIO = UserLogic.GetFIOUser(database, rec.ReviewerId)
                }).ToList();
            return res;
        }
        public IActionResult Magazines()
        {
            var model = MagazineLogic.GetMagazines(database);
            return View(model);
        }
        public IActionResult GetMagazine()
        {
            int Id = Convert.ToInt32(Request.Query["Id"]);
            var model = MagazineLogic.GetMagazine(database, Id);
            return View("Magazine", model);
        }

        private string CheakDataMagazine(string Date)
        {
            var lastDate = MagazineLogic.GetMagazines(database).Last().Date;
            //Если последняя дата плюс 3 месяца меньше или рана выбранной
            if (lastDate.AddMonths(3) <= Convert.ToDateTime(Date))
                return "true";
            else
                return "false";
        }
    }
}
