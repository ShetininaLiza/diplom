using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BisnessLogic.Models
{
    public class User
    {
        //Id может быть null, при регистрации нового пользователя
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? Id { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
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

        public bool IsBlock { get; set; }
    }
}
