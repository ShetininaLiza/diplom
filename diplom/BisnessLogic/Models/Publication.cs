using System;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace BisnessLogic.Models
{
    public class Publication
    {
        public int? Id { get; set; }
        public string Title { get; set; }
        public string Annotation { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime DatePublic { get; set; }
        public IFormFile File { get; set; }

        public byte[] Text { get; set; }
        public string NameFile { get; set; }
        //public char[] Text { get; set; }
        public string Status { get; set; }
        public List<Autor> Autors { get; set; }
        //public List<int> AutorsId { get; set; }
        public List<string> KeyWords { get; set; }
        public List<string> Categories { get; set; }

        public int? ReviewerId { get; set; }
    }
}
