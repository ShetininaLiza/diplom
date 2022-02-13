using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WebApplication.Models
{
    public class User
    {
        //Id может быть null, при регистрации нового пользователя
        public int? Id { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        //Фамилия
        public string LastName { get; set; }
        //Имя
        public string Name { get; set; }
        //Отчество
        public string Otch { get; set; }
        //Место работы
        public string Work { get; set; }
        public string Role { get; set; }

        public User(string l, string e, string p, string ln, string fn, string o, string w, string r)
        {
            Login = l;
            Email = e;
            Password = p;
            LastName = ln;
            Name = fn;
            Otch = o; 
            Work = w; 
            Role = r;
    }
        public void SetLogin(string l){ Login = l; }
        public void SetEmail(string e) { Email = e; }
        public void SetPassword(string p){ Password = p; }
        public void SetRole(string r){ Role = r; }
        public void SetLastName(string l) { LastName = l; }
        public void SetName(string n) { Name=n; }
        public void SetOtch(string o) { Otch = o; }
        public void SetWork(string w) { Work = w; }

        public string GetLogin(){ return Login; }
        public string GetEmail() { return Email; }
        public string GetPassword() { return Password; }
        public string GetRole() { return Role; }
        public string GetLastName() { return LastName; }
        public string GetName() { return Name; }
        public string GetOtch() { return Otch; }
        public string GetWork() { return Work; }

    }
}
