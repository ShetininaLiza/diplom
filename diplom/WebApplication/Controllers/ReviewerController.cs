using BisnessLogic.Models;
using Database.Logic;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using WebApplication;

namespace ApplicationUsers.Controllers
{
    public class ReviewerController : Controller
    {
        private IDbConnection database = Program.database;
        List<string> categories;
        List<BisnessLogic.Models.Publication> publications;
        BisnessLogic.Models.Publication publication;
        public ReviewerController()
        {
            categories = CategoriesLogic.GetCategories(database);
            publications = PublicationLogic.GetPublications(database, null);
        }
        public IActionResult Index()
        {
            var model = publications.Where(rec => rec.ReviewerId == Program.user.Id).ToList();
            return View(model);
        }
        [HttpPost]
        public IActionResult GetPublicationList(string selectedStatus = "All")
        {
            publications = PublicationLogic.GetPublications(database, null)
                .Where(rec => rec.ReviewerId == Program.user.Id).ToList();
            var publics = publications;
            if (selectedStatus != "All")
            {
                publics = publications.Where(rec => rec.Status == selectedStatus).ToList();
            }
            return View("Index", publics);
        }
        [HttpGet]
        public IActionResult CreateReview()
        {
            int id = Convert.ToInt32(Request.Query["Id"]);
            publication = publications.First(rec => rec.Id == id);
            return View((publication, categories, new string[] { }));
        }

        public IActionResult Download(int Id)
        {
            var publication = PublicationLogic.GetAllFilePublication(database, Id).Last();
            var r = OnDowload(publication);
            return r;
        }
        public IActionResult DownloadFilePublication(int PublicId, int Id)
        {

            var publication = PublicationLogic.GetAllFilePublication(database, PublicId)
                .FirstOrDefault(rec => rec.Id == Id);
            if (publication != null)
            {
                var r = OnDowload(publication);
                return r;
            }
            else
                return new NotFoundResult();
        }
        public IActionResult OnDowload(FilePublication publication)
        {
            var text = publication.Text;
            if (publication.GetTypeFile().Length > 0)
            {
                string type = publication.GetTypeFile();
                return new FileContentResult(text, type)
                {
                    FileDownloadName = publication.Name
                };
            }
            else
                return new NotFoundResult();

        }
        public IActionResult GetToWork(int Id)
        {
            var model = publications.Where(rec => rec.ReviewerId == Program.user.Id).ToList();
            try
            {
                PublicationLogic.UpdatePublic(database, Id, Program.user.Id.Value,
                        BisnessLogic.Models.Status.Находится_на_рецензировании.ToString());
                model = publications.Where(rec => rec.ReviewerId == Program.user.Id).ToList();

                return View("Index", model);
                //{"Поздравляем!", "Изменения былы внесены успешно." }));
            }
            catch (Exception ex) { return View("Index", model); }
        }

        public IActionResult CenselPublic(int Id)
        {
            var model = publications.Where(rec => rec.ReviewerId == Program.user.Id).ToList();
            try
            {
                PublicationLogic.UpdatePublic(database, Id, null,
                        BisnessLogic.Models.Status.Поступила.ToString());

                model = publications.Where(rec => rec.ReviewerId == Program.user.Id).ToList();

                return View("Index", model);
            }
            catch (Exception ex) { return View("Index", model); }
        }
        [HttpPost]
        public IActionResult GetInformationForRev(string New, string CommentNew, string Corect, string CommentCorect,
                                            string Znachim, string CommentZnachim, string Polnota, string TextPolnita, string Jasnost, string TextJasnost, string Recomend,
                                            string TextRecomend, string Result,
                                            string CommentResult, string Blocks, string Zakl, int PublicId)
        {
            BisnessLogic.Models.Review review = new BisnessLogic.Models.Review()
            {
                New = New,
                CommentNew = CommentNew,
                Correctness = Corect,
                CommentCorrectness = CommentCorect,
                Znach = Znachim,
                CommentZnach = CommentZnachim,
                Polnota = Polnota,
                Clarity = Jasnost,
                Recomend = Recomend,
                Result = Result,
                CommentResult = CommentResult,
                HasBlocks = Blocks,
                Conclusion = Zakl,
                PublicId = PublicId
            };
            PublicationLogic.AddOrUpdateReviewToPublication(database, review);
            return new EmptyResult();
        }

        [HttpPost]
        [ActionName("CreateReview")]
        public IActionResult CreateReview(string ID, List<string> category, List<string> novizna, string textNew,
                                        List<string> corect, string textErorr, List<string> znach, string textZnach,
                                        List<string> polnota, string textPolnote, List<string> izloj, string textIzloj,
                                        List<string> recomends, string textRecomend, List<string> results,
                                        string textResult, List<string> blocks, List<string> zakls)
        {

            int id = Convert.ToInt32(ID);
            publication = publications.First(rec => rec.Id == id);
            if (category.Count == 0)
            {
                return View((publication, categories,
                    new string[] { "Ошибка!", "Выберите хотя бы один пункт из раздела Рубрика(и)." }));
            }
            if (novizna.Count == 0)
            {
                return View((publication, categories,
                    new string[] { "Ошибка!", "Выберите хотя бы один пункт из раздела Новизна." }));
            }
            if (zakls.Count == 0)
            {
                return View((publication, categories, new string[]
                { "Ошибка!", "Выберите хотя бы один пункт из раздела Заключение о публикации статьи." }));

                /*View((publication, categories,
                new string[] { "Ошибка!", "Выберите хотя бы один пункт из раздела Заключение о публикации статьи." }));
                */
            }
            BisnessLogic.Models.Review review = new BisnessLogic.Models.Review()
            {
                PublicId = id,
                Categories = string.Join(',', category.ToArray()),
                New = string.Join(',', novizna.ToArray()),
                CommentNew = textNew,
                Correctness = string.Join(',', corect.ToArray()),
                CommentCorrectness = textErorr,
                Znach = string.Join(',', znach.ToArray()),
                CommentZnach = textZnach,
                Polnota = string.Join(',', polnota),
                CommentPolnota = textPolnote,
                Clarity = string.Join(',', izloj.ToArray()),
                CommentClarity = textIzloj,
                Recomend = string.Join(',', recomends.ToArray()),
                CommentRecomend = textRecomend,
                Result = string.Join(',', results.ToArray()),
                CommentResult = textResult,
                HasBlocks = string.Join(',', blocks.ToArray()),
                Conclusion = string.Join(',', zakls.ToArray())
            };
            try
            {
                PublicationLogic.AddOrUpdateReviewToPublication(database, review);
                PublicationLogic.UpdateStatusPublication(database, id,
                    BisnessLogic.Models.Status.Рецензирование_окончено.ToString(), null);
                /*
                PublicationLogic.UpdatePublic(database, id, Program.user.Id.Value, 
                    BisnessLogic.Models.Status.Рецензирование_окончено.ToString());
                */
                return View((publication, categories, new string[] { "Поздравляем!", "Рецензия была успешно создана." }));
            }
            catch (Exception ex)
            {
                return View((publication, categories, new string[] { "Ошибка!", ex.Message }));
            }
            //return new EmptyResult();
        }
        [HttpGet]
        public IActionResult GetReview()
        {
            int Id = Convert.ToInt32(Request.Query["Id"]);
            var model = PublicationLogic.GetReviewByIDPublic(database, Id);
            return View(model);
        }

        [HttpGet]
        public IActionResult GetPublic()
        {
            var Id = Convert.ToInt32(Request.Query["Id"]);

            var publication = publications.First(rec => rec.Id == Id);
            var files = PublicationLogic.GetAllFilePublication(database, Id);
            var rev = PublicationLogic.GetReviewByIDPublic(database, Id);
            if (rev != null)
                return View("Publication", (publication, new string[] { }, files));
            else
                return View("Publication", (publication, new string[] { }, files));
        }

    }
}
