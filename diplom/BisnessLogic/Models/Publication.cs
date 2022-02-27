using System;
using System.Collections.Generic;

namespace BisnessLogic.Models
{
    public class Publication
    {
        public int? Id { get; set; }
        public string Title { get; set; }
        public string Annotation { get; set; }
        public string File { get; set; }
        public Status Status { get; set; }
        public List<User> Autors { get; set; }
        public List<string> KeyWords { get; set; }
        public List<string> Categories { get; set; }
    }
}
