﻿using Database;
using BisnessLogic.Models;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace AppRegisterAdmin
{
    class Program
    { 
        static void Main(string[] args)
        {
            WorkFile wf = new WorkFile();
            
            Console.WriteLine("Введите логин:");
            string login = Console.ReadLine();
            var list = wf.GetUsers();
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
            //если все верно
            wf.WriteUser(new User
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
            Console.WriteLine("Администратор добавлен.");
            Console.ReadKey();
        }
    }
}