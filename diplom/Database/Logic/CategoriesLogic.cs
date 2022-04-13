using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Logic
{
    public static class CategoriesLogic
    {
        public static string GetNamesCategories = "select Name from Categories";

        public static long GetIdCategory(IDbConnection db, string name)
        {
            string text = "SELECT Id FROM Categories WHERE Name = @name";
            var parametr = new DynamicParameters();
            parametr.Add("name", name);

            var result=db.ExecuteScalarAsync<long>(text, parametr);
            return result.Result;
        }
    
        public static List<string> GetCategoriesForPublic(IDbConnection db, int publicId)
        {
            string text = "SELECT Name FROM Categories WHERE Id IN " +
                "(SELECT CategoryId FROM PublicCategories WHERE PublicId = :id);";
            
            var parametr = new DynamicParameters();
            parametr.Add("id", publicId);

            var result = db.Query<string>(text, parametr).ToList();
            return result;
        }
        public static List<string> GetCategories(IDbConnection md)
        {
            var result=md.Query<string>(CategoriesLogic.GetNamesCategories, new DynamicParameters());
            return result.ToList();
        }
    }
}
