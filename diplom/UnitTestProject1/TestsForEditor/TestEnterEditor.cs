using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

namespace UnitTestProject1.TestsForEditor
{
    [TestClass]
    public class TestEnterEditor
    {
        readonly IWebDriver Driver = new ChromeDriver();

        [TestMethod]
        public void TestMethod_GoodEnterEditor()
        {
            Driver.Url = "https://localhost:44317/Editor/Enter";
            string login = "editor";
            string pass = "Editor12345!";
            //поле логина
            Driver.FindElement(By.Id("TextLogin")).SendKeys(login);
            //поле пароля
            Driver.FindElement(By.Id("TextPass")).SendKeys(pass);
            //кнопка входа
            Driver.FindElement(By.Id("NextStep")).Click();
            try
            {
                Assert.AreEqual("Редактор Научный журнал", Driver.Title);
                Console.WriteLine("All Ok");
            }
            catch (Exception) { Console.WriteLine("Exception"); throw; }
            //Driver.Close();
        }

        [TestMethod]
        public void TestMethod_BadEnterEditor()
        {
            Driver.Url = "https://localhost:44317/Editor/Enter";
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
            //Driver.Close();
        }

        [TestMethod]
        public void TestMethod_EmptyAll()
        {
            Driver.Url = "https://localhost:44317/Editor/Enter";
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
            Driver.Url = "https://localhost:44317/Editor/Enter";
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
            Driver.Url = "https://localhost:44317/Editor/Enter";
            try { PrintData("editor", "editor"); }
            catch (Exception) { throw; }
            try { PrintData("editor", "Editor"); }
            catch (Exception) { throw; }
            try { PrintData("editor", "editor12345"); }
            catch (Exception) { throw; }
            try { PrintData("editor", "Fbmjhh12345!"); }
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
