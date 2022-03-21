using BisnessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Models
{
    public class Publication
    {
        public int? Id { get; set; }
        public string Title { get; set; }
        public string Annotation { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime DatePublic { get; set; }
        public byte[] File { get; set; }
        public string Status { get; set; }
        public IEnumerable<BisnessLogic.Models.Autor> Autors { get; set; }
        public IEnumerable<string> KeyWords { get; set; }
        public IEnumerable<string> Categories { get; set; }
    }
}
