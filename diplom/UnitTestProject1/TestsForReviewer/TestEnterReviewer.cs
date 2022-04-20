using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace UnitTestProject1.TestsForReviewer
{
    [TestClass]
    public class TestEnterReviewer
    {
        readonly IWebDriver Driver = new ChromeDriver();
        
        [TestMethod]
        public void TestMethod_GoodEnterRev() 
        {
            Driver.Url = "http://localhost:46452/User/Enter";
            string login = "rev";
            string pass = "Rev12345!";
            //поле логина
            Driver.FindElement(By.Id("login")).SendKeys(login);
            //поле пароля
            Driver.FindElement(By.Id("pass")).SendKeys(pass);
            //поле роли
            Driver.FindElement(By.Id("validationCustom04")).SendKeys("Рецензент");
            //кнопка входа
            Driver.FindElement(By.Id("enter")).Click();
            try
            {
                Assert.AreEqual("Рецензент Научный журнал", Driver.Title);
                Console.WriteLine("All Ok");
            }
            catch (Exception) { Console.WriteLine("Exception"); throw; }
            //Driver.Close();
        }
        
        [TestMethod]
        public void TestMethod_BadEnterRev() 
        {
            Driver.Url = "http://localhost:46452/User/Enter";
            string login = "chjdvkjhxjv";
            string pass = "jhfVFTklgfb5657!";
            //поле логина
            Driver.FindElement(By.Id("login")).SendKeys(login);
            //поле пароля
            Driver.FindElement(By.Id("pass")).SendKeys(pass);
            //поле роли
            Driver.FindElement(By.Id("validationCustom04")).SendKeys("Рецензент");
            //кнопка входа
            Driver.FindElement(By.Id("enter")).Click();
            try
            {
                Assert.AreEqual("Вход Научный журнал", Driver.Title);
                Console.WriteLine("All Ok");
            }
            catch (Exception) { Console.WriteLine("Exception"); throw; }
            //Driver.Close();
        }

        [TestMethod]
        public void TestMethod_EmptyAll()
        {
            Driver.Url = "http://localhost:46452/User/Enter";
            string login = "";
            string pass = "";
            //поле логина
            Driver.FindElement(By.Id("login")).SendKeys(login);
            //поле пароля
            Driver.FindElement(By.Id("pass")).SendKeys(pass);
            //поле роли
            Driver.FindElement(By.Id("validationCustom04")).SendKeys("Рецензент");
            //кнопка входа
            Driver.FindElement(By.Id("enter")).Click();
            try
            {
                Assert.AreEqual("Вход Научный журнал", Driver.Title);
                Console.WriteLine("All Ok");
            }
            catch (Exception) { Console.WriteLine("Exception"); throw; }
        }

        [TestMethod]
        public void TestMethod_DifferentRole()
        {
            Driver.Url = "http://localhost:46452/User/Enter";
            string login = "rev";
            string pass = "";
            //поле логина
            Driver.FindElement(By.Id("login")).SendKeys(login);
            //поле пароля
            Driver.FindElement(By.Id("pass")).SendKeys(pass);
            //поле роли
            Driver.FindElement(By.Id("validationCustom04")).SendKeys("Рецензент");
            //кнопка входа
            Driver.FindElement(By.Id("enter")).Click();
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
            Driver.Url = "http://localhost:46452/User/Enter";
            string login = "rev";
            string pass = "";
            //поле логина
            Driver.FindElement(By.Id("login")).SendKeys(login);
            //поле пароля
            Driver.FindElement(By.Id("pass")).SendKeys(pass);
            //поле роли
            Driver.FindElement(By.Id("validationCustom04")).SendKeys("Рецензент");
            //кнопка входа
            Driver.FindElement(By.Id("enter")).Click();
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
            Driver.Url = "http://localhost:46452/User/Enter";
            try { PrintData("rev", "rev"); }
            catch (Exception) { throw; }
            try { PrintData("rev", "Rev"); }
            catch (Exception) { throw; }
            try { PrintData("rev", "Rev12345"); }
            catch (Exception) { throw; }
            try { PrintData("rev", "Fbmjhh12345!"); }
            catch (Exception) { throw; }
        }

        private void PrintData(string login, string pass)
        {
            //поле логина
            Driver.FindElement(By.Id("login")).SendKeys(login);
            //поле пароля
            Driver.FindElement(By.Id("pass")).SendKeys(pass);
            //поле роли
            Driver.FindElement(By.Id("validationCustom04")).SendKeys("Рецензент");
            //кнопка входа
            Driver.FindElement(By.Id("enter")).Click();
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
