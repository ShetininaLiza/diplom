using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BisnessLogic.Helper
{
    public static class GenerPass
    {
        static string[] spec_symbols = new string[] 
        { "!", "@", "#", "$", "%", "^", "*", "(", ")", "<", ">", "+","-"};
        public static string GetPassword(int size) {
            string buf = "";
            var r = new Random();
            while (buf.Length < size+1)
            {
                Char c = (char)r.Next(33, 125);
                if (Char.IsLetterOrDigit(c))
                    buf += c;
            }
            var symbol= spec_symbols.ElementAt(r.Next(0, (spec_symbols.Length + 1)));
            int len = buf.Length;
            var pass = buf.Insert(r.Next(0, len), symbol);
            return pass;
        }
    }
}
