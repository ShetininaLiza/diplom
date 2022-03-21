using BisnessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicationEditor.Models
{
    public class Publication
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Annotation { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime DatePublic { get; set; }
        public byte[] Text { get; set; }
        public string Status { get; set; }
        public List<Autor> Autors { get; set; }
        //public List<int> AutorsId { get; set; }
        public List<string> KeyWords { get; set; }
        public List<string> Categories { get; set; }

        public int? ReviewerId { get; set; }
        public string ReviewerFIO { get; set; }
    }
}
