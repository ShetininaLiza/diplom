using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

namespace UnitTestProject1.TestsForAdmin
{
    [TestClass]
    public class TestRegisterEditor
    {
        readonly IWebDriver Driver = new ChromeDriver();
        string url = "https://localhost:44319/Admin/RegisterEditor";
        [TestMethod]
        public void TestMethod_GoodRegisterNewEditor()
        {
            string login = "editor1";
            string pass = "Editor12345!";
            string last = "v";
            string name = "b";
            string otch = "n";
            string email = "editor1@gmail.com";
            string phone = "89028889702";
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
        public void TestMethod_RegisterOldEditor()
        {
            string login = "editor";
            string pass = "Shetinina12345!";
            string last = "Щетинина";
            string name = "Елизавета";
            string otch = "Вадимовна";
            string email = "editor@gmail.com";
            string phone = "89165658953";
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
                Assert.AreEqual("Регистрация редактора Научный журнал", Driver.Title);
                Console.WriteLine("All ok");
            }
            catch (Exception) { Console.WriteLine("Exception"); throw; }
        }
        [TestMethod]
        public void TestMethod_RegisterWithOnlyLogin()
        {
            string login = "editor1";
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
                Assert.AreEqual("Регистрация редактора Научный журнал", Driver.Title);
                Console.WriteLine("All ok");
            }
            catch (Exception) { Console.WriteLine("Exception"); throw; }
        }
        [TestMethod]
        public void TestMethod_RegisterWithFIOAndLogin()
        {
            string login = "editor1";
            string pass = "";
            string last = "v";
            string name = "b";
            string otch = "n";
            string email = "";
            string phone = "";
            string work = "";
            PrintData(login, pass, last, name, otch, email, phone, work);
            try
            {
                Assert.AreEqual("Регистрация редактора Научный журнал", Driver.Title);
                Console.WriteLine("All ok");
            }
            catch (Exception) { Console.WriteLine("Exception"); throw; }
        }
        [TestMethod]
        public void TestMethod_RegisterWithoutWork()
        {
            string login = "editor1";
            string pass = "editor1";
            string last = "v";
            string name = "b";
            string otch = "n";
            string email = "editor";
            string phone = "+7";
            string work = "";
            PrintData(login, pass, last, name, otch, email, phone, work);
            try
            {
                Assert.AreEqual("Регистрация редактора Научный журнал", Driver.Title);
                Console.WriteLine("All ok");
            }
            catch (Exception) { Console.WriteLine("Exception"); throw; }
        }
        [TestMethod]
        public void TestMethod_RegisterWithWrongPass()
        {
            string login = "editor1";
            string pass = "editor1";
            string last = "v";
            string name = "b";
            string otch = "n";
            string email = "editor1";
            string phone = "+7";
            string work = "ooo";
            PrintData(login, pass, last, name, otch, email, phone, work);
            try
            {
                Assert.AreEqual("Регистрация редактора Научный журнал", Driver.Title);
                Console.WriteLine("All ok");
            }
            catch (Exception) { Console.WriteLine("Exception"); throw; }
        }
        [TestMethod]
        public void TestMethod_RegisterWithWrongEmail()
        {
            string login = "editor1";
            string pass = "Editor12345!";
            string last = "v";
            string name = "b";
            string otch = "n";
            string email = "editor1";
            string phone = "+7";
            string work = "ooo";
            PrintData(login, pass, last, name, otch, email, phone, work);
            try
            {
                Assert.AreEqual("Регистрация редактора Научный журнал", Driver.Title);
                Console.WriteLine("All ok");
            }
            catch (Exception) { Console.WriteLine("Exception"); throw; }
        }
        [TestMethod]
        public void TestMethod_RegisterWithOldEmail()
        {
            string login = "editor1";
            string pass = "Editor12345!";
            string last = "v";
            string name = "b";
            string otch = "n";
            string email = "editor@gmail.com";
            string phone = "89028889702";
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
        public void TestMethod_RegisterWithWrongPhone()
        {
            string login = "editor1";
            string pass = "Editor12345!";
            string last = "v";
            string name = "b";
            string otch = "n";
            string email = "editr1@gmail.com";
            string phone = "+7";
            string work = "ooo";
            PrintData(login, pass, last, name, otch, email, phone, work);
            try
            {
                Assert.AreEqual("Регистрация редактора Научный журнал", Driver.Title);
                Console.WriteLine("All ok");
            }
            catch (Exception) { Console.WriteLine("Exception"); throw; }
        }
        [TestMethod]
        public void TestMethod_RegisterWithPhone()
        {
            string login = "editor";
            string pass = "Editor12345!";
            string last = "v";
            string name = "b";
            string otch = "n";
            string email = "editor1@gmail.com";
            string phone = "89165658953";
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
            Driver.Url = url;
            Driver.FindElement(By.Id("TextLastName")).SendKeys(last);
            Driver.FindElement(By.Id("TextFirstName")).SendKeys(name);
            Driver.FindElement(By.Id("TextOtch")).SendKeys(otch);
            Driver.FindElement(By.Id("TextLogin")).SendKeys(login);
            Driver.FindElement(By.Id("TextPass")).SendKeys(pass);
            Driver.FindElement(By.Id("TextEmail")).SendKeys(email);
            Driver.FindElement(By.Id("Tel")).SendKeys(phone);
            Driver.FindElement(By.Id("TextWork")).SendKeys(work);
            Driver.FindElement(By.Id("NextStep")).Click();
        }
    }
}
