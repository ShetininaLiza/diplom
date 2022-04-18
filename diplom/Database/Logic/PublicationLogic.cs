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
        public static void AddPublication(IDbConnection db, BisnessLogic.Models.Publication publication, List<long> autorsId)
        {
            string text_addPublic = "INSERT INTO Publications (" +
                             "Title," +
                             "Annotation," +
                             "Status," +
                             "DateCreate," +
                             "DatePublic," +
                             "KeyWords" +
                         ")VALUES( " +
                             "@Title," +
                             "@Annotation," +
                             "@Status," +
                             "@DateCreate," +
                             "@DatePublic," +
                             "@KeyWords);" +
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

            string text_addFile = "INSERT INTO Files (Name,  Date, Text, PublicationId" +
                  ")VALUES( @Name, @Date,@Text,@PublicationId);";

            var par = new DynamicParameters();
            par.Add("Title", publication.Title);
            par.Add("Annotation", publication.Annotation);
            par.Add("Status", publication.Status);
            par.Add("DateCreate", publication.DateCreate);
            par.Add("DatePublic", publication.DatePublic);
            par.Add("KeyWords", string.Join(',', publication.KeyWords.ToArray()));
            //par.Add("Text", publication.Text);

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
                //вставляем связь с файлом
                var parametr = new DynamicParameters();
                parametr.Add("Name", publication.NameFile);
                parametr.Add("Date", publication.DateCreate);
                parametr.Add("Text", publication.Text);
                parametr.Add("PublicationId", publication_ID);
                db.ExecuteAsync(text_addFile, parametr);

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
                    Autors = AutorLogic.GetAutorsForPublication(db, rec.Id.Value),
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

        public static BisnessLogic.Models.Publication UpdatePublic(IDbConnection db, int publicId, int? revId, string status)
        {
            string text = "UPDATE Publications SET ReviewerId = :revId, Status = :status WHERE Id = :id;" +
                            "SELECT * FROM Publications WHERE Id = :id;";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("revId", revId);
            parameters.Add("status", status);
            parameters.Add("id", publicId);
            try
            {
                var data = db.Query<Database.Models.Publication>(text, parameters)
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
                }).First();
                return data;
            }
            catch (Exception) { throw; }
        }

        public static void AddOrUpdateReviewToPublication(IDbConnection db, Review review)
        {
            try
            {

                string publicsIds = "SELECT PublicId FROM Review;";
                var ids = db.QueryAsync<long>(publicsIds, new DynamicParameters()).Result.ToList();
                string text = "";
                if (ids.Contains(review.PublicId))
                {
                    text = "UPDATE Review SET  Categories = @Categories," +
                        "New = @New," +
                        "CommentNew = @CommentNew," +
                        "Correctness = @Correctness," +
                       "CommentCorrectness = @CommentCorrectness," +
                       "Znach = @Znach," +
                       "CommentZnach = @CommentZnach," +
                       "Polnota = @Polnota," +
                       "CommentPolnota = @CommentPolnota," +
                       "Clarity = @Clarity," +
                       "CommentClarity = @CommentClarity," +
                       "Recomend = @Recomend," +
                       "CommentRecomend = @CommentRecomend," +
                       "Result = @Result," +
                       "CommentResult = @CommentResult," +
                       "HasBlocks = @HasBlocks," +
                       "Conclusion = @Conclusion WHERE PublicId=@PublicId";
                }
                else
                {
                    text = "INSERT INTO Review (" +
                           "PublicId," +
                           "Categories," +
                           "New," +
                           "CommentNew," +
                           "Correctness," +
                           "CommentCorrectness," +
                           "Znach," +
                           "CommentZnach," +
                           "Polnota," +
                           "CommentPolnota," +
                           "Clarity," +
                           "CommentClarity," +
                           "Recomend, CommentRecomend, Result," +
                           "CommentResult," +
                           "HasBlocks," +
                           "Conclusion" +
                           ") VALUES(" +
                           "@PublicId," +
                           "@Categories," +
                           "@New," +
                           "@CommentNew," +
                           "@Correctness," +
                           "@CommentCorrectness," +
                           "@Znach," +
                           "@CommentZnach," +
                           "@Polnota," +
                           "@CommentPolnota," +
                           "@Clarity," +
                           "@CommentClarity," +
                           "@Recomend," +
                           "@CommentRecomend," +
                           "@Result," +
                           "@CommentResult," +
                           "@HasBlocks," +
                           "@Conclusion);";
                }
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("PublicId", review.PublicId);
                parameters.Add("Categories", review.Categories);
                parameters.Add("New", review.New);
                parameters.Add("CommentNew", review.CommentNew);
                parameters.Add("Correctness", review.Correctness);
                parameters.Add("CommentCorrectness", review.CommentCorrectness);
                parameters.Add("Znach", review.Znach);
                parameters.Add("CommentZnach", review.CommentZnach);
                parameters.Add("Polnota", review.Polnota);
                parameters.Add("CommentPolnota", review.CommentPolnota);
                parameters.Add("Clarity", review.Clarity);
                parameters.Add("CommentClarity", review.CommentClarity);
                parameters.Add("Recomend", review.Recomend);
                parameters.Add("CommentRecomend", review.CommentRecomend);
                parameters.Add("Result", review.Result);
                parameters.Add("CommentResult", review.CommentResult);
                parameters.Add("HasBlocks", review.HasBlocks);
                parameters.Add("Conclusion", review.Conclusion);

                db.Execute(text, parameters);
            }
            catch (Exception) { throw; }

        }
        public static BisnessLogic.Models.Publication UpdateStatusPublication(IDbConnection db, int publicId, string status, DateTime? datePublic)
        {
            string text = "";
            DynamicParameters parameters = new DynamicParameters();
            if (datePublic.HasValue)
            {
                text = "UPDATE Publications SET Status = :status AND  DatePublic = :date WHERE Id = :id;" +
                "SELECT * FROM Publications WHERE Id = :id;";
                parameters.Add("status", status);
                parameters.Add("date", datePublic.Value);
                parameters.Add("id", publicId);
            }
            else
            {
                text = "UPDATE Publications SET Status = :status WHERE Id = :id;" +
                    "SELECT * FROM Publications WHERE Id = :id;";

                parameters.Add("status", status);
                parameters.Add("id", publicId);
            }
            try
            {
                var data = db.Query<Database.Models.Publication>(text, parameters)
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
                }).First();
                return data;
            }
            catch (Exception) { throw; }
        }

        public static Review GetReviewByIDPublic(IDbConnection db, int publicId)
        {
            string text = "SELECT * FROM Review WHERE PublicId = :id;";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("id", publicId);
            try
            {
                var res = db.Query<BisnessLogic.Models.Review>(text, parameters)
                    /*
                .Select(rec => new BisnessLogic.Models.Review
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
                })*/
                    .ToList().First();
                return res;
            }
            catch (Exception)
            {
                return null;
                //throw;
            }
        }

        public static FilePublication GetFilePublication(IDbConnection db, int publicId)
        {
            string text = "SELECT * FROM Files WHERE PublicationId=:id";
            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("id", publicId);

            var result = db.Query<FilePublication>(text, parameter).ToList().Last();
            return result;
        }

        public static List<FilePublication> GetAllFilePublication(IDbConnection db, int publicId)
        {
            string text = "SELECT * FROM Files WHERE PublicationId=:id";
            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("id", publicId);

            var result = db.Query<FilePublication>(text, parameter).ToList();
            return result;
        }

        public static void AddFilePublication(IDbConnection db, FilePublication file)
        {
            string text = "INSERT INTO Files (Name," +
                      "Date," +
                      "Text," +
                      "PublicationId, Comment) VALUES(@Name," +
                      "@Date," +
                      "@Text," +
                      "@PublicationId, @Comment);";
            try
            {
                db.ExecuteAsync(text, file);
            }
            catch (Exception) { throw; }
        }
    }
}

