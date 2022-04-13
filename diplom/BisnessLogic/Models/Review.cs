using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BisnessLogic.Models
{
    public class Review
    {
        public int? Id { get; set; }
        public int PublicId { get; set; }
        public string Categories { get; set; }
        public string New { get; set; }
        public string CommentNew { get; set; }
        public string Correctness { get; set; }
        public string CommentCorrectness { get; set; }
        public string Znach { get; set; }
        public string CommentZnach { get; set; }
        public string Polnota { get; set; }
        public string CommentPolnota { get; set; }
        //Ясность
        public string Clarity { get; set; }
        public string CommentClarity { get; set; }
        public string Recomend { get; set; }
        public string CommentRecomend { get; set; }
        public string Result { get; set; }
        public string CommentResult { get; set; }
        public string HasBlocks { get; set; }
        public string Conclusion { get; set; }
    }
}
