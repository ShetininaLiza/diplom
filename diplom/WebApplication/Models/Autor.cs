using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Models
{
    public class Autor
    {
        public int? Id { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        //Фамилия
        public string LastName { get; set; }
        //Имя
        public string Name { get; set; }
        //Отчество
        public string Otch { get; set; }
        //Место работы
        public string Work { get; set; }
        public bool IsConnect { get; set; }
    }
}
