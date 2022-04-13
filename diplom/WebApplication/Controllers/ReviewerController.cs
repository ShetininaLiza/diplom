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
            var model=publications.Where(rec => rec.ReviewerId == Program.user.Id).ToList();
            return View(model);
        }
        [HttpGet]
        public IActionResult CreateReview()
        {
            int id = Convert.ToInt32(Request.Query["Id"]);
            publication = publications.First(rec => rec.Id == id);
            return //View();
                View((publication, categories, new string[] { }));
        }
        
        public IActionResult DownloadAndUpdate(int ID)
        {
            var publication = publications.First(rec => rec.Id == ID);
            //обновление статьи (меняем статус)
            try
            {
                PublicationLogic.UpdatePublic(database, ID, Program.user.Id.Value, BisnessLogic.Models.Status.Находится_на_рецензировании.ToString());
                return File(publication.Text, "application/txt");
                //return File(publication.Text, "application/vnd.openxmlformats-officedocument.wordprocessingml.document");
            }
            catch (Exception ex) { return Content(ex.Message); } 
                
        }
        
        [HttpPost]
        public IActionResult GetInformationForRev(string New, string CommentNew, string Corect, string CommentCorect,
                                            string Znachim, string CommentZnachim, string Polnota, string TextPolnita, string Jasnost, string TextJasnost, string Recomend,
                                            string TextRecomend, string Result,
                                            string CommentResult, string Blocks, string Zakl, int PublicId)
        {
            BisnessLogic.Models.Review review = new BisnessLogic.Models.Review()
            {
                New=New,
                CommentNew=CommentNew,
                Correctness=Corect,
                CommentCorrectness=CommentCorect,
                Znach=Znachim,
                CommentZnach=CommentZnachim,
                Polnota=Polnota,
                Clarity=Jasnost,
                Recomend=Recomend,
                Result=Result,
                CommentResult=CommentResult,
                HasBlocks=Blocks,
                Conclusion=Zakl,
                PublicId= PublicId
            };
            PublicationLogic.AddReviewToPublication(database, review);
            return new EmptyResult();
        }

        [HttpPost]
        [ActionName("CreateReview")]
        public IActionResult CreateReview(string ID, List<string> category, List<string> novizna, string textNew,
                                        List<string> corect, string textErorr, List<string> znach, string textZnach,
                                        List<string> polnota, string textPolnote, List<string> izloj, string textIzloj,
                                        List<string> recomends, string textRecomend,List<string> results,
                                        string textResult,List<string> blocks, List<string> zakls)
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
                return PartialView("Mess", new string[] 
                { "Ошибка!", "Выберите хотя бы один пункт из раздела Заключение о публикации статьи." });
                    
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
                CommentClarity=textIzloj,
                Recomend = string.Join(',', recomends.ToArray()),
                CommentRecomend=textRecomend,
                Result = string.Join(',', results.ToArray()),
                CommentResult = textResult,
                HasBlocks = string.Join(',',blocks.ToArray()),
                Conclusion = string.Join(',', zakls.ToArray())
            };
            try
            {
                PublicationLogic.AddReviewToPublication(database, review);
                PublicationLogic.UpdatePublic(database, id, Program.user.Id.Value, BisnessLogic.Models.Status.Рецензирование_окончено.ToString());
                return View((publication, categories, new string[] { "Поздравляем!", "Рецензия была успешно создана." }));
            }
            catch (Exception ex) {
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
    }
}
