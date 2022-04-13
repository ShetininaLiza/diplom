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
    public static class MagazineLogic
    {
        public static void AddMagazine(IDbConnection db, Magazine magazine)
        {
            string text = "";
            string textPublic = "UPDATE Publications SET JournalId = :jId WHERE Id = :id;";
            long ID;
            try
            {
                if (magazine.Id.HasValue)
                {
                    ID = magazine.Id.Value;
                    text = "INSERT INTO Journals (Id, Date) VALUES( @Id, @Date);";
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("Id", magazine.Id.Value);
                    parameters.Add("Date", magazine.Date);
                    db.ExecuteAsync(text, parameters);
                }
                else
                {
                    text = "INSERT INTO Journals (Date) VALUES(@Date); SELECT MAX(Id) FROM Publications;";
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("Date", magazine.Date);
                    ID = db.ExecuteScalar<long>(text, parameters);
                }
                for(int i=0; i<magazine.Publications.Count; i++)
                {
                    var publication = magazine.Publications.ElementAt(i);
                    //обновляем статус на Опубликована для указанных статей
                    PublicationLogic.UpdateStatusPublication(db, publication.Id.Value, Status.Опубликована.ToString());
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("jId", ID);
                    parameters.Add("id", publication.Id);
                    db.ExecuteAsync(textPublic, parameters);
                }
            }
            catch (Exception) { throw; }
        }

        public static List<Magazine> GetMagazines(IDbConnection db)
        {
            string text = "SELECT * FROM Journals;";
            var result = db.Query<Magazine>(text, new DynamicParameters()).ToList();
            return result;
        }
        public static Magazine GetMagazine(IDbConnection db, int id)
        {
            string textPublic = "SELECT * FROM Publications WHERE JournalId = :id";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("id", id);
            
            var publications = db.Query<Database.Models.Publication>(textPublic, parameters)
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
            
            string text = "SELECT * FROM Journals WHERE Id = :id;";
            var result = db.Query<Magazine>(text, parameters)
                .Select(rec => new Magazine
                {
                    Id = rec.Id,
                    Date = rec.Date,
                    Publications = publications
                }).First();
            return result;
        }
    }
}
