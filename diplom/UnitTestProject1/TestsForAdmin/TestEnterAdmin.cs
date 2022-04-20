using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

namespace UnitTestProject1.TestsForAdmin
{
    [TestClass]
    public class TestEnterAdmin
    {
        readonly IWebDriver Driver = new ChromeDriver();
        [TestMethod]
        public void TestMethod_GoodEnterAdmin()
        {
            Driver.Url = "https://localhost:44319/Admin/Enter";
            string login = "admin";
            string pass = "Admin12345!";
            //поле логина
            Driver.FindElement(By.Id("TextLogin")).SendKeys(login);
            //поле пароля
            Driver.FindElement(By.Id("TextPass")).SendKeys(pass);
            //кнопка входа
            Driver.FindElement(By.Id("NextStep")).Click();
            try
            {
                Assert.AreEqual("Главная Научный журнал", Driver.Title);
                Console.WriteLine("All Ok");
            }
            catch (Exception) { Console.WriteLine("Exception"); throw; }
        }

        [TestMethod]
        public void TestMethod_BadEnterAdmin() 
        {
            Driver.Url = "https://localhost:44319/Admin/Enter";
            string login = "chjdvkjhxjv";
            string pass = "jhfVFTklgfb5657!";
            //поле логина
            Driver.FindElement(By.Id("TextLogin")).SendKeys(login);
            //поле пароля
            Driver.FindElement(By.Id("TextPass")).SendKeys(pass);
            //кнопка входа
            Driver.FindElement(By.Id("NextStep")).Click();
            try
            {
                Assert.AreEqual("Вход Научный журнал", Driver.Title);
                Console.WriteLine("All Ok");
            }
            catch (Exception) { Console.WriteLine("Exception"); throw; }
        }
        [TestMethod]
        public void TestMethod_EmptyAll()
        {
            Driver.Url = "https://localhost:44319/Admin/Enter";
            string login = "";
            string pass = "";
            //поле логина
            Driver.FindElement(By.Id("TextLogin")).SendKeys(login);
            //поле пароля
            Driver.FindElement(By.Id("TextPass")).SendKeys(pass);
            //кнопка входа
            Driver.FindElement(By.Id("NextStep")).Click();
            try
            {
                Assert.AreEqual("Вход Научный журнал", Driver.Title);
                Console.WriteLine("All Ok");
            }
            catch (Exception) { Console.WriteLine("Exception"); throw; }
        }


        [TestMethod]
        public void TestMethod_EmptyPass()
        {
            Driver.Url = "https://localhost:44319/Admin/Enter";
            string login = "editor";
            string pass = "";
            //поле логина
            Driver.FindElement(By.Id("TextLogin")).SendKeys(login);
            //поле пароля
            Driver.FindElement(By.Id("TextPass")).SendKeys(pass);
            //кнопка входа
            Driver.FindElement(By.Id("NextStep")).Click();
            try
            {
                Assert.AreEqual("Вход Научный журнал", Driver.Title);
                Console.WriteLine("All Ok");
            }
            catch (Exception) { Console.WriteLine("Exception"); throw; }
        }

        [TestMethod]
        public void TestMethod_WrongPass()
        {
            Driver.Url = "https://localhost:44319/Admin/Enter";
            try { PrintData("admin", "admin"); }
            catch (Exception) { throw; }
            try { PrintData("admin", "Admin"); }
            catch (Exception) { throw; }
            try { PrintData("admin", "Admin12345"); }
            catch (Exception) { throw; }
            try { PrintData("admin", "Fbmjhh12345!"); }
            catch (Exception) { throw; }
        }

        private void PrintData(string login, string pass)
        {
            //поле логина
            Driver.FindElement(By.Id("TextLogin")).SendKeys(login);
            //поле пароля
            Driver.FindElement(By.Id("TextPass")).SendKeys(pass);
            //кнопка входа
            Driver.FindElement(By.Id("NextStep")).Click();
            try
            {
                Assert.AreEqual("Вход Научный журнал", Driver.Title);
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
