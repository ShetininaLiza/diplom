using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace UnitTestProject1
{
    [TestClass]
    public class TestEnter
    {
        readonly IWebDriver Driver = new ChromeDriver();
        string urlEnter = "http://localhost:46452/User/Enter";
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
            Console.WriteLine("End");
            //Driver.Close();
        }

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
            Console.WriteLine("End");
            //Driver.Close();
        }
        public void TestMethod_GoodEnterRev() 
        {
            Driver.Url = "http://localhost:46452/User/Enter";
            string login = "rev1";
            string pass = "Rev12345!";
            //поле логина
            Driver.FindElement(By.Id("login")).SendKeys(login);
            //поле пароля
            Driver.FindElement(By.Id("pass")).SendKeys(pass);
            //поле роли
            Driver.FindElement(By.Id("validationCustom04")).SendKeys("Автор");
            //кнопка входа
            Driver.FindElement(By.Id("enter")).Click();
            Console.WriteLine("End");
            //Driver.Close();
        }
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
            Driver.FindElement(By.Id("validationCustom04")).SendKeys("Автор");
            //кнопка входа
            Driver.FindElement(By.Id("enter")).Click();
            Console.WriteLine("End");
            //Driver.Close();
        }
    }
}
