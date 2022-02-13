using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using WebApplication.Models;

namespace WebApplication.Helper
{
    public class WorkFile
    {
        public WorkFile()
        {
            if (!File.Exists("users.xml"))
            {
                File.Create("users.xml").Close();
                var xElement = new XElement("Users");
                XDocument xDocument = new XDocument(xElement);
                xDocument.Save("users.xml");
            }

        }
        public void WriteUser(User user)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("users.xml");
            XmlElement xRoot = xmlDoc.DocumentElement;

            XmlElement el = xmlDoc.CreateElement("User");
            el.SetAttribute("Login", user.GetLogin());
            el.SetAttribute("Password", user.GetPassword());
            el.SetAttribute("Email", user.GetEmail());
            el.SetAttribute("LastName", user.GetLastName());
            el.SetAttribute("Name", user.GetName());
            el.SetAttribute("Otch", user.GetOtch());
            el.SetAttribute("Work", user.GetWork());
            el.SetAttribute("Role", user.GetRole());
            xRoot.AppendChild(el);
            xmlDoc.Save("users.xml");
        }
        public bool CheakLogin(string login, string email)
        {
            //Загрузили файл
            XDocument xDocument = XDocument.Load("users.xml");
            //Выбрали все элементы
            var xElementsUsers = xDocument.Root.Elements("User").ToList();
            //Нашли с входящим логином
            var user= xElementsUsers.FirstOrDefault(rec => rec.Attribute("Login").Value == login ||
                                                    rec.Attribute("Email").Value == email);
            //если нет таких записей, то нет пользователя
            if (user == null)
                return false;
            else
                return true;
        }

        public string GetUserData(string login)
        {
            string result = "";
            //Загрузили файл
            XDocument xDocument = XDocument.Load("users.xml");
            //Выбрали все элементы
            var xElementsUsers = xDocument.Root.Elements("User").ToList();
            //Нашли с входящим логином
            var user = xElementsUsers.FirstOrDefault(rec => rec.Attribute("Login").Value == login);
            if(user!=null)
                result = user.Attribute("Login").Value + " " + user.Attribute("Password").Value;
            return result;
        }
    }
}
