using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Helper
{
    public class GeneralPassword
    {
        //длина пароля
        int length = 10;
        public  string GetPass()
        {
            string pass = "";
            Random r = new Random();
            while (pass.Length < length)
            {
                var c = (char)r.Next(33, 126);
                pass += c;
            }
            return pass;
        }
    }
}
