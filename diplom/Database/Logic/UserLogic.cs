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
    public static class UserLogic
    {
        public static string AddUser = "INSERT INTO Users(" +
                           "Login," +
                           "Email," +
                           "Phone," +
                           "Password," +
                           "LastName," +
                           "Name," +
                           "Otch," +
                           "Work," +
                           "Role," +
                           "IsBlock)" +
                       "VALUES(" +
                           "@Login," +
                           "@Email," +
                           "@Phone," +
                           "@Password," +
                           "@LastName," +
                           "@Name," +
                           "@Otch," +
                           "@Work," +
                           "@Role," +
                           "@IsBlock);";

        public static string UpdateUser = "UPDATE Users SET " +
                "Id = @Id, "+
                "Login = @Login, " +
                "Email = @Email, "+
                "Phone = @Phone, " +
                "Password = @Password, " +
                "LastName = @LastName, " +
                "Name = @Name, " +
                "Otch = @Otch," +
                "Work = @Work, " +
                "Role = @Role, "+
                "IsBlock = @IsBlock " +
            "WHERE Id = @Id;";

        public static string GetUsers = "SELECT * FROM Users WHERE Id = :id OR Login = :login;";

        public static string GetFIOUser(IDbConnection db, int? id) 
        {
            string result = "";
            if (id.HasValue)
            {
                string[] model = new string[3];
                string text = "SELECT  LastName, Name, Otch FROM Users WHERE Id = :id;";
                var par = new DynamicParameters();
                par.Add("id", id.Value);
                var zn = db.Query<User>(text, par);
                model[0] = zn.First().LastName;
                model[1] = zn.First().Name;
                model[2] = zn.First().Otch;
                result = string.Join(' ', model);
            }
            return result;
        }

        public static List<User> GetAllUser(IDbConnection db)
        {
            string text = "SELECT * FROM Users;";
            var result = db.Query<User>(text, new DynamicParameters()).ToList();
            return result;
        }
    }
}
