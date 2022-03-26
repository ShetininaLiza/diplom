using Database.Models;
using BisnessLogic.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Logic
{
    public static class PublicationLogic
    {
        public static void AddPublication(IDbConnection db, BisnessLogic.Models.Publication publication, List<long>autorsId)
        {
            string text_addPublic= "INSERT INTO Publications ("+
                             "Title,"+
                             "Annotation,"+
                             "Status,"+
                             "DateCreate,"+
                             "DatePublic,"+
                             "KeyWords,"+
                             "Text"+
                         ")VALUES( "+
                             "@Title," +
                             "@Annotation,"+
                             "@Status,"+
                             "@DateCreate,"+
                             "@DatePublic,"+
                             "@KeyWords,"+
                             "@Text);" +
                             "SELECT last_insert_rowid();";
            
            string text_addAutorsPublic = "INSERT INTO PublicAitors (" +
                             "AutorId," +
                             "PublicId) VALUES(" +
                             "@AutorId," +
                             "@PublicId); ";
            
            string text_addCategoriesPublic = "INSERT INTO PublicCategories (" +
                                 "PublicId," +
                                 "CategoryId ) VALUES(" +
                                 "@PublicId," +
                                 "@CategoryId); ";
            
            var par = new DynamicParameters();
            par.Add("Title", publication.Title);
            par.Add("Annotation", publication.Annotation);
            par.Add("Status", publication.Status);
            par.Add("DateCreate", publication.DateCreate);
            par.Add("DatePublic", publication.DatePublic);
            par.Add("KeyWords", string.Join(',', publication.KeyWords.ToArray()));
            par.Add("Text", publication.Text);

            try
            {
                //вставляем статью и получаем ее id
                var publication_ID = db.ExecuteScalarAsync<long>(text_addPublic, par).Result;
                
                //вставляем связи между авторами и публикациями
                for (int i = 0; i < autorsId.Count; i++)
                {
                    var param = new DynamicParameters();
                    param.Add("AutorId", autorsId.ElementAt(i));
                    param.Add("PublicId", publication_ID);
                    db.ExecuteAsync(text_addAutorsPublic, param);
                }
                
                //вставляем связи между публикацией и категориями
                for (int i = 0; i < publication.Categories.Count; i++)
                {
                    string categoryName = publication.Categories.ElementAt(i);
                    var categoryId = CategoriesLogic.GetIdCategory(db, categoryName);
                    
                    var param = new DynamicParameters();
                    param.Add("PublicId", publication_ID);
                    param.Add("CategoryId", categoryId);
                    db.ExecuteAsync(text_addCategoriesPublic, param);

                }
            }
            catch (Exception ex) { throw; }
        }
    
        public static List<BisnessLogic.Models.Publication> GetPublicationList(IDbConnection db, string autorEmail)
        {
            //выдергиваем данные публикации (название, аннотацию, статус, дату создания и публикации
            //ключевые слова, текст)
            string text = "SELECT * FROM Publications WHERE Id IN " +
                "(SELECT PublicId FROM PublicAitors WHERE AutorId IN " +
                "(SELECT Id FROM Autors WHERE Email = :autor));";
            
            var parametr = new DynamicParameters();
            parametr.Add("autor", autorEmail);

            var result = db.Query<Database.Models.Publication>(text, parametr)
                .Select(rec => new BisnessLogic.Models.Publication
                {
                    Id = rec.Id,
                    Title = rec.Title,
                    Annotation = rec.Annotation,
                    DateCreate = rec.DateCreate,
                    DatePublic = rec.DatePublic,
                    Status = rec.Status,
                    Text = rec.Text,
                    Categories = CategoriesLogic.GetCategoriesForPublic(db, rec.Id.Value),
                    Autors=AutorLogic.GetAutorsForPublication(db, rec.Id.Value),
                    KeyWords = rec.KeyWords.Split(',').ToList()
                }).ToList();
            return result;
        }
    
        public static List<BisnessLogic.Models.Publication> GetPublications(IDbConnection db, int? id)
        {
            string text = "";
            DynamicParameters parameter = new DynamicParameters();
            if (id.HasValue)
            {
                text = "SELECT  * FROM Publications WHERE Id = :id;";
                parameter.Add("id", id.Value);
            }
            else
            {
                text = "SELECT  * FROM Publications;";
            }
            var data = db.Query<Database.Models.Publication>(text, parameter)
                .Select(rec => new BisnessLogic.Models.Publication
                {
                    Id = rec.Id,
                    Title = rec.Title,
                    Annotation = rec.Annotation,
                    DateCreate = rec.DateCreate,
                    DatePublic = rec.DatePublic,
                    Status = rec.Status,
                    Text = rec.Text,
                    Categories = CategoriesLogic.GetCategoriesForPublic(db, rec.Id.Value),
                    Autors = AutorLogic.GetAutorsForPublication(db, rec.Id.Value),
                    ReviewerId = rec.ReviewerId,
                    KeyWords = rec.KeyWords.Split(',').ToList()
                }).ToList();
            return data;
        }
    
        public static void UpdatePublic(IDbConnection db, int publicId, int revId, string status)
        {
            string text = "UPDATE Publications SET ReviewerId = :revId, Status = :status WHERE Id = :id;";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("revId", revId);
            parameters.Add("status", status);
            parameters.Add("id", publicId);
            try
            {
                db.ExecuteAsync(text, parameters);
            }
            catch (Exception) { throw; }
        }
    }
}
