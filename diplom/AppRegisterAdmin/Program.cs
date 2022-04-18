using Database.Logic;
using BisnessLogic.Models;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Data;
using System.Data.SQLite;
using System.Configuration;
using System.IO;

namespace AppRegisterAdmin
{
    class Program
    {
        //public static MyDatabase database;
        static void Main(string[] args)
        {
            string connectionString= ConfigurationManager.ConnectionStrings["DiplomDB"].ConnectionString;
            IDbConnection database = new SQLiteConnection(connectionString);
            database.Open();
            
            var list = UserLogic.GetAllUser(database);
            
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
            Console.WriteLine("Введите email:");
            string email = Console.ReadLine();
            while (!Regex.IsMatch(email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"))
            {
                Console.WriteLine("Неверно введен адрес электронной почты!\nВведите email:");
                email = Console.ReadLine();
            }
            var checkEmail = list.FirstOrDefault(rec => rec.Email == email);
            while (checkEmail != null)
            {
                Console.WriteLine("Такая почта уже есть в системе.\nВведите email:");
                email = Console.ReadLine();
                checkEmail = list.FirstOrDefault(rec => rec.Email == email);
            }
            
            Console.WriteLine("Введите контактный номер телефона:");
            string tel = Console.ReadLine();
            while (!Regex.IsMatch(tel, @"^89\d{9}$"))
            {
                Console.WriteLine("Формат телефона 89.........!\nВведите контактный номер телефона:");
                tel = Console.ReadLine();
            }
            var checkTel = list.FirstOrDefault(rec => rec.Phone == tel);
            while (checkTel != null)
            {
                Console.WriteLine("Такой номер телефона уже есть в системе.\nВведите контактный номер телефона:");
                tel = Console.ReadLine();
                checkTel = list.FirstOrDefault(rec => rec.Phone == tel);
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
                User user = new User
                {
                    Login = login,
                    Email = email,
                    Phone = tel,
                    Password = pas,
                    LastName = "",
                    Name = "",
                    Otch = "",
                    Work = "",
                    Role = Role.Администратор.ToString(),
                    IsBlock=false
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
    }
}
