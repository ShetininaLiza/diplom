using BisnessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Helper
{
    public class MultipleModel
    {
        public List<User> users { get; set; }
        public List<string> categories { get; set; }
        public List<Publication> publications { get; set; }
    }
}
