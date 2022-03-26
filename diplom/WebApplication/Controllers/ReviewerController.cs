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
        List<BisnessLogic.Models.Publication> publications;
        public ReviewerController()
        {
            publications = PublicationLogic.GetPublications(database, null);
        }
        public IActionResult Index()
        {
            var model=publications.Where(rec => rec.ReviewerId == Program.user.Id).ToList();
            return View(model);
        }

        public IActionResult CreateReview()
        {
            int id = Convert.ToInt32(Request.Query["Id"]);
            var model = publications.First(rec => rec.Id == id);
            return //View();
                View(model);
        }
        
        [HttpGet]
        public IActionResult GetPublic(int Id)
        {
            //int Id = Convert.ToInt32(Request.Query["Id"]);
            var model = publications.First(rec => rec.Id == Id); //PublicationLogic.GetPublications(database, Id).ToList();
            return View("CreateReview", model);
        }

        public IActionResult DownloadAndUpdate(int ID)
        {
            var publication = publications.First(rec => rec.Id == ID);
            //обновление статьи (меняем статус)
            try
            {
                PublicationLogic.UpdatePublic(database, ID, Program.user.Id.Value, BisnessLogic.Models.Status.Находится_на_рецензировании.ToString());
                return File(publication.Text, "application/txt");
            }
            catch (Exception ex) { return Content(ex.Message); } 
                
        }
    }
}
