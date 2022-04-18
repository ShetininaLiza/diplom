using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BisnessLogic.Models
{
    public class FilePublication
    {
        public int? Id { get; set; }
        public int PublicationId { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public byte[] Text { get; set; }

        public string Comment { get; set; }

        public string GetTypeFile()
        {
            var ras = Name.Split('.')[1];
            if (ras == "pdf")
                return "application/pdf";
            else if (ras == "txt")
                return "application/txt";
            else if (ras == "doc")
                return "application/msword";
            else if (ras == "docx")
                return "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
            else
                return "";
        }
    }
}
