using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

namespace UnitTestProject1.TestsForAutor
{
    [TestClass]
    public class TestEnterAutor
    {
        readonly IWebDriver Driver = new ChromeDriver();
        [TestMethod]
        public void TestMethod_GoodEnterAutor()
        {
            Driver.Url = "http://localhost:46452/User/Enter";
            string login = "liza";
            string pass = "Shetinina2000!";
            //поле логина
            Driver.FindElement(By.Id("login")).SendKeys(login);
            //поле пароля
            Driver.FindElement(By.Id("pass")).SendKeys(pass);
            //поле роли
            Driver.FindElement(By.Id("validationCustom04")).SendKeys("Автор");
            //кнопка входа
            Driver.FindElement(By.Id("enter")).Click();
            try
            {
                Assert.AreEqual("Автор Научный журнал", Driver.Title);
                Console.WriteLine("All Ok");
            }
            catch (Exception) { Console.WriteLine("Exception"); throw; }
        }
        
        [TestMethod]
        public void TestMethod_BedEnterAutor()
        {
            Driver.Url = "http://localhost:46452/User/Enter";
            string login = "chjdvkjhxjv";
            string pass = "jhfVFTklgfb5657!";
            //поле логина
            Driver.FindElement(By.Id("login")).SendKeys(login);
            //поле пароля
            Driver.FindElement(By.Id("pass")).SendKeys(pass);
            //поле роли
            Driver.FindElement(By.Id("validationCustom04")).SendKeys("Автор");
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
            Driver.FindElement(By.Id("validationCustom04")).SendKeys("Автор");
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
            string login = "liza";
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
            string login = "liza";
            string pass = "";
            //поле логина
            Driver.FindElement(By.Id("login")).SendKeys(login);
            //поле пароля
            Driver.FindElement(By.Id("pass")).SendKeys(pass);
            //поле роли
            Driver.FindElement(By.Id("validationCustom04")).SendKeys("Автор");
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
            try{ PrintData("liza", "liza");}
            catch (Exception) { throw; }
            try{ PrintData("liza", "Liza");}
            catch (Exception) { throw; }
            try{PrintData("liza", "Liza12345");}
            catch (Exception) { throw; }
            try { PrintData("liza", "Fbmjhh12345!"); }
            catch (Exception) { throw; }
        }

        private void PrintData(string login, string pass)
        {
            //поле логина
            Driver.FindElement(By.Id("login")).SendKeys(login);
            //поле пароля
            Driver.FindElement(By.Id("pass")).SendKeys(pass);
            //поле роли
            Driver.FindElement(By.Id("validationCustom04")).SendKeys("Автор");
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
