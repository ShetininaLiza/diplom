using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject1
{
    public class Start
    {
        static void Main(string[] args)
        {
            TestEnter test = new TestEnter();
            Console.WriteLine("Start Tests:");
            Console.WriteLine("Good Enter Autor:");
            test.TestMethod_GoodEnterAutor();
            Console.WriteLine("Bed Enter Autor:");
            test.TestMethod_BedEnterAutor();
            Console.WriteLine("Good Enter Reviewer:");
            test.TestMethod_GoodEnterRev();
            Console.WriteLine("Bad Enter Reviewer:");
            test.TestMethod_BadEnterRev();
        }
    }
}
