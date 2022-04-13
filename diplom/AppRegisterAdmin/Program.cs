using Database.Logic;
using BisnessLogic.Models;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Data;
using System.Data.SQLite;
using System.Configuration;

namespace AppRegisterAdmin
{
    class Program
    {
        //public static MyDatabase database;
        public static IDbConnection database;
        static void Main(string[] args)
        {
            database = new SQLiteConnection(LoadConnectionString());
            database.Open();
            //database = new MyDatabase();
            //database.Connect();
            //WorkFile wf = new WorkFile();

            var list = UserLogic.GetAllUser(database);
                //database.GetUsers();
            
            Console.WriteLine("Введите логин:");
            string login = Console.ReadLine();
            
                //wf.GetUsers();
            var res =list.FirstOrDefault(rec=>rec.Login==login);
            while (res != null)
            {
                Console.WriteLine("Такой логин уже занят.\nВведите логин:");
                login = Console.ReadLine();
                //res =wf.GetUserData(login);
                res= list.FirstOrDefault(rec => rec.Login == login);
                //Console.WriteLine("Res: " + res);
            }
            Console.WriteLine("Введите пароль:");
            string pas = Console.ReadLine();
            string pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[^a-zA-Z0-9])\S{8,}";
            while (!Regex.IsMatch(pas, pattern))
            {
                Console.WriteLine("Папроль не соответствует требованиям." +
                    "\nПароль должен содержать от 8 символов (английские строчные, заглавные, цифры и спец символы)." +
                    "\nВведите пароль:");
                pas = Console.ReadLine();
            }
            try
            {
                //если все верно
                //wf
                /*
                database.WriteUser(new User
                {
                    Login = login,
                    Password = pas,
                    Email = "",
                    LastName = "",
                    Name = "",
                    Otch = "",
                    Work = "",
                    Role = Role.Администратор.ToString()
                });
                */
                User user = new User
                {
                    Login = login,
                    Password = pas,
                    Email = "",
                    LastName = "",
                    Name = "",
                    Otch = "",
                    Work = "",
                    Role = Role.Администратор.ToString()
                };
                UserLogic.AddUser(database, user);
                Console.WriteLine("Администратор добавлен.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadKey();
        }
        private static string LoadConnectionString(string id = "DiplomDB")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }
    }
}
