using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

namespace UnitTestProject1.TestsForAutor
{
    [TestClass]
    public class TestAllPublication
    {
        readonly IWebDriver Driver = new ChromeDriver();
        [TestMethod]
        public void TestMethod_GetAllPublication()
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
            Driver.FindElement(By.LinkText("Мои публикации")).Click();
            try
            {
                Assert.AreEqual("Мои статьи Научный журнал", Driver.Title);
                Console.WriteLine("All Ok");
            }
            catch (Exception)
            {
                Console.WriteLine("Exception");
                throw;
            }
        }

        [TestMethod]
        public void TestMethod_GetInformationPublication()
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
            Driver.FindElement(By.LinkText("Мои публикации")).Click();
            Driver.FindElement(By.Id("title")).Click();
            try
            {
                Assert.AreEqual("Информация о статье Научный журнал", Driver.Title);
                Console.WriteLine("All Ok");
            }
            catch (Exception)
            {
                Console.WriteLine("Exception");
                throw;
            }
        }

        [TestMethod]
        public void TestMethod_GetResultReviewPublication()
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
            Driver.FindElement(By.LinkText("Мои публикации")).Click();
            Driver.FindElement(By.Id("result")).Click();
            try
            {
                Assert.AreEqual("Рецензия на статью Научный журнал", Driver.Title);
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
