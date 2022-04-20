using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

namespace UnitTestProject1.TestsForAdmin
{
    [TestClass]
    public class TestBlockUser
    {
        readonly IWebDriver Driver = new ChromeDriver();
        string url = "https://localhost:44319/Admin/BlockUser";
        [TestMethod]
        public void TestMethod_AddBlock()
        {
            string id = "1";
            string login = "liza";
            Driver.Url = "https://localhost:44319/Admin/AddBlock?login=" + login;
            Driver.Url = url;
            var text=Driver.FindElement(By.Id("block")).Text;
            try
            {
                Assert.AreEqual("True", text);
                Console.WriteLine("All Ok");
            }
            catch (Exception)
            {
                Console.WriteLine("Exception");
                throw;
            }
        }
        [TestMethod]
        public void TestMethod_CloseBlock()
        {
            string id = "1";
            string login = "liza";
            Driver.Url = "https://localhost:44319/Admin/AddBlock?login=" + login;
            Driver.Url = url;
            var text = Driver.FindElement(By.Id("block")).Text;
            try
            {
                Assert.AreEqual("False", text);
                Console.WriteLine("All Ok");
            }
            catch (Exception)
            {
                Console.WriteLine("Exception");
                throw;
            }
        }
    }
}
