using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

namespace UnitTestProject1.TestsForAutor
{
    [TestClass]
    public class TestRegisterAutor
    {
        readonly IWebDriver Driver = new ChromeDriver();
        [TestMethod]
        public void TestMethod_GoodRegisterNewAutor()
        {
            string login = "user";
            string pass = "User12345!";
            string last = "z";
            string name = "x";
            string otch = "c";
            string email = "user@gmail.com";
            string phone = "89027658899";
            string work = "ooo";
            PrintData(login, pass, last, name, otch, email, phone, work);
            string res = Driver.FindElement(By.Id("Mess")).Text.Split('!')[0];
            try
            {
                Assert.AreEqual("Поздравляем", res);
                Console.WriteLine("All ok");
            }
            catch (Exception) { Console.WriteLine("Exception"); throw; }

        }
        [TestMethod]
        public void TestMethod_RegisterOldAutor() 
        {
            string login = "liza";
            string pass = "Shetinina12345!";
            string last = "Щетинина";
            string name = "Елизавета";
            string otch = "Вадимовна";
            string email = "shetinina.l2000@gmail.com";
            string phone = "89176316294";
            string work = "qq";
            PrintData(login, pass, last, name, otch, email, phone, work);
            string res = Driver.FindElement(By.Id("Mess")).Text.Split('!')[0];
            try
            {
                Assert.AreEqual("Ошибка", res);
                Console.WriteLine("All ok");
            }
            catch (Exception) { Console.WriteLine("Exception"); throw; }
        }
        [TestMethod]
        public void TestMethod_RegisterEmptyAll()
        {
            string login = "";
            string pass = "";
            string last = "";
            string name = "";
            string otch = "";
            string email = "";
            string phone = "";
            string work = "";
            PrintData(login, pass, last, name, otch, email, phone, work);
            try
            {
                Assert.AreEqual("Регистрация автора Научный журнал", Driver.Title);
                Console.WriteLine("All ok");
            }
            catch (Exception) { Console.WriteLine("Exception"); throw; }
        }
        [TestMethod]
        public void TestMethod_RegisterWithOnlyLogin()
        {
            string login = "user";
            string pass = "";
            string last = "";
            string name = "";
            string otch = "";
            string email = "";
            string phone = "";
            string work = "";
            PrintData(login, pass, last, name, otch, email, phone, work);
            try
            {
                Assert.AreEqual("Регистрация автора Научный журнал", Driver.Title);
                Console.WriteLine("All ok");
            }
            catch (Exception) { Console.WriteLine("Exception"); throw; }
        }
        [TestMethod]
        public void TestMethod_RegisterWithFIOAndLogin()
        {
            string login = "user";
            string pass = "";
            string last = "z";
            string name = "x";
            string otch = "c";
            string email = "";
            string phone = "";
            string work = "";
            PrintData(login, pass, last, name, otch, email, phone, work);
            try
            {
                Assert.AreEqual("Регистрация автора Научный журнал", Driver.Title);
                Console.WriteLine("All ok");
            }
            catch (Exception) { Console.WriteLine("Exception"); throw; }
        }
        [TestMethod]
        public void TestMethod_RegisterAutorWithoutWork() 
        {
            string login = "user";
            string pass = "user";
            string last = "z";
            string name = "x";
            string otch = "c";
            string email = "user";
            string phone = "+7";
            string work = "";
            PrintData(login, pass, last, name, otch, email, phone, work);
            try
            {
                Assert.AreEqual("Регистрация автора Научный журнал", Driver.Title);
                Console.WriteLine("All ok");
            }
            catch (Exception) { Console.WriteLine("Exception"); throw; }
        }
        [TestMethod]
        public void TestMethod_RegisterAutorWithWrongPass() 
        {
            string login = "user";
            string pass = "user";
            string last = "z";
            string name = "x";
            string otch = "c";
            string email = "user";
            string phone = "+7";
            string work = "ooo";
            PrintData(login, pass, last, name, otch, email, phone, work);
            try
            {
                Assert.AreEqual("Регистрация автора Научный журнал", Driver.Title);
                Console.WriteLine("All ok");
            }
            catch (Exception) { Console.WriteLine("Exception"); throw; }
        }
        [TestMethod]
        public void TestMethod_RegisterAutorWithWrongEmail()
        {
            string login = "user";
            string pass = "User12345!";
            string last = "z";
            string name = "x";
            string otch = "c";
            string email = "user";
            string phone = "+7";
            string work = "ooo";
            PrintData(login, pass, last, name, otch, email, phone, work);
            try
            {
                Assert.AreEqual("Регистрация автора Научный журнал", Driver.Title);
                Console.WriteLine("All ok");
            }
            catch (Exception) { Console.WriteLine("Exception"); throw; }
        }
        [TestMethod]
        public void TestMethod_RegisterAutorWithEmail()
        {
            string login = "user";
            string pass = "User12345!";
            string last = "z";
            string name = "x";
            string otch = "c";
            string email = "shetinina.l2000@gmail.com";
            string phone = "89027658899";
            string work = "ooo";
            PrintData(login, pass, last, name, otch, email, phone, work);
            string res = Driver.FindElement(By.Id("Mess")).Text.Split('!')[0];
            try
            {
                Assert.AreEqual("Ошибка", res);
                Console.WriteLine("All ok");
            }
            catch (Exception) { Console.WriteLine("Exception"); throw; }
        }
        [TestMethod]
        public void TestMethod_RegisterAutorWithWrongPhone()
        {
            string login = "user";
            string pass = "User12345!";
            string last = "z";
            string name = "x";
            string otch = "c";
            string email = "user@gmail.com";
            string phone = "+7";
            string work = "ooo";
            PrintData(login, pass, last, name, otch, email, phone, work);
            try
            {
                Assert.AreEqual("Регистрация автора Научный журнал", Driver.Title);
                Console.WriteLine("All ok");
            }
            catch (Exception) { Console.WriteLine("Exception"); throw; }
        }
        [TestMethod]
        public void TestMethod_RegisterAutorWithPhone()
        {
            string login = "user";
            string pass = "User12345!";
            string last = "z";
            string name = "x";
            string otch = "c";
            string email = "user@gmail.com";
            string phone = "89176316294";
            string work = "ooo";
            PrintData(login, pass, last, name, otch, email, phone, work);
            string res = Driver.FindElement(By.Id("Mess")).Text.Split('!')[0];
            try
            {
                Assert.AreEqual("Ошибка", res);
                Console.WriteLine("All ok");
            }
            catch (Exception) { Console.WriteLine("Exception"); throw; }
        }
        
        private void PrintData(string login, string pass, string last, string name, string otch,
                                string email, string phone, string work) 
        {
            Driver.Url = "http://localhost:46452/User/Register";
            Driver.FindElement(By.Id("LastName")).SendKeys(last);
            Driver.FindElement(By.Id("Name")).SendKeys(name);
            Driver.FindElement(By.Id("Otch")).SendKeys(otch);
            Driver.FindElement(By.Id("Login")).SendKeys(login);
            Driver.FindElement(By.Id("Pass")).SendKeys(pass);
            Driver.FindElement(By.Id("Email")).SendKeys(email);
            Driver.FindElement(By.Id("Tel")).SendKeys(phone);
            Driver.FindElement(By.Id("Work")).SendKeys(work);
            Driver.FindElement(By.Id("buttonRegister")).Click();
            
        }
    }
}
