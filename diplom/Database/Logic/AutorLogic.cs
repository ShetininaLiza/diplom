using Dapper;
using BisnessLogic.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Logic
{
    public static class AutorLogic
    {
        public static string AddAutor = "INSERT INTO Autors(" +
                           "Email," +
                           "Phone," +
                           "LastName," +
                           "Name," +
                           "Otch," +
                           "Work," +
                           "IsConnect)" +
                       "VALUES(" +
                           "@Email," +
                           "@Phone," +
                           "@LastName," +
                           "@Name," +
                           "@Otch," +
                           "@Work," +
                           "@IsConnect);" +
            "SELECT last_insert_rowid();";

        public static List<Autor> GetAutorsForPublication(IDbConnection db, int publicId)
        {
            string text = "SELECT * FROM Autors WHERE Id = "+
                    "(SELECT AutorId FROM PublicAitors WHERE PublicId = :id);";
            var parament = new DynamicParameters();
            parament.Add("id", publicId);
            var result = db.Query<Autor>(text, parament).ToList();
            return result;
        }
    }
}
