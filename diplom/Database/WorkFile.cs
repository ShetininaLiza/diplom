using BisnessLogic.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace Database
{
    public class WorkFile
    {
        string path = "C:\\Users\\sheti\\source\\repos\\diplom\\Database\\";
        public WorkFile()
        {
            if (!File.Exists(path+"users.xml"))
            {
                File.Create("users.xml").Close();
                var xElement = new XElement("Users");
                XDocument xDocument = new XDocument(xElement);
                xDocument.Save(path+"users.xml");
            }
            if (!File.Exists(path+"publications.xml"))
            {
                File.Create(path+"publications.xml").Close();
                var xElement = new XElement("Publications");
                XDocument xDocument = new XDocument(xElement);
                xDocument.Save(path+"publications.xml");
            }
        }
        public void WriteUser(User user)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(path+"users.xml");
            XmlElement xRoot = xmlDoc.DocumentElement;
            
            //Если нет такого логина
            //Создаем новый элемент
            XmlElement el = xmlDoc.CreateElement("User");
            int id = GetLastId(path + "users.xml", "User") + 1;
            el.SetAttribute("Id", id.ToString());
            el.SetAttribute("Login", user.Login);
            el.SetAttribute("Password", user.Password);
            el.SetAttribute("Email", user.Email);
            el.SetAttribute("LastName", user.LastName);
            el.SetAttribute("Name", user.Name);
            el.SetAttribute("Otch", user.Otch);
            el.SetAttribute("Work", user.Work);
            el.SetAttribute("Role", user.Role.ToString());
            el.SetAttribute("isBlock", user.IsBlock.ToString());
            xRoot.AppendChild(el);
            xmlDoc.Save(path+"users.xml");
        }
        private int GetLastId(string pathFile, string nameElement)
        {
            //Загрузили файл
            XDocument xDocument = XDocument.Load(pathFile);
            try
            {
                //Выбрали все элементы
                var xElementsUser = xDocument.Root.Elements(nameElement).ToList().Last();
                //Нашли с входящим логином
                var id = Convert.ToInt32(xElementsUser.Attribute("Id").Value);
                return id;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public bool CheakLogin(string login, string email)
        {
            //Загрузили файл
            XDocument xDocument = XDocument.Load(path+"users.xml");
            //Выбрали все элементы
            var xElementsUsers = xDocument.Root.Elements("User").ToList();
            //Нашли с входящим логином
            var user = xElementsUsers.FirstOrDefault(rec => rec.Attribute("Login").Value == login ||
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
            XDocument xDocument = XDocument.Load(path+"users.xml");
            //Выбрали все элементы
            var xElementsUsers = xDocument.Root.Elements("User").ToList();
            //Нашли с входящим логином
            var user = xElementsUsers.FirstOrDefault(rec => rec.Attribute("Login").Value == login);
            if (user != null)
                result = user.Attribute("Login").Value + " " + user.Attribute("Password").Value + " " + user.Attribute("Role").Value;
            return result;
        }

        public void WritePublic(Publication publication)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(path+"publications.xml");
            XmlElement xRoot = xmlDoc.DocumentElement;

            XmlElement el = xmlDoc.CreateElement("Publication");
            int id = GetLastId(path + "publications.xml", "Publication") + 1;
            el.SetAttribute("Id", id.ToString());
            el.SetAttribute("Title", publication.Title);
            el.SetAttribute("Annotation", publication.Annotation);
            el.SetAttribute("File", publication.File);
            el.SetAttribute("Status", publication.Status.ToString());
            var autors = publication.Autors;
            XmlElement el_Autors = xmlDoc.CreateElement("Autors");
            for (int i = 0; i < autors.Count; i++)
            {
                XmlElement autor = xmlDoc.CreateElement("Autor");
                autor.SetAttribute("LastName", autors.ElementAt(i).LastName);
                autor.SetAttribute("Name", autors.ElementAt(i).Name);
                autor.SetAttribute("Otch", autors.ElementAt(i).Otch);
                autor.SetAttribute("Email", autors.ElementAt(i).Email);
                autor.SetAttribute("Work", autors.ElementAt(i).Work);
                el_Autors.AppendChild(autor);
            }
            el.AppendChild(el_Autors);
            
            var categories = publication.Categories;
            XmlElement el_Categories = xmlDoc.CreateElement("Categories");
            for (int i = 0; i < categories.Count; i++)
            {
                XmlElement category = xmlDoc.CreateElement("Category");
                category.SetAttribute("Name", categories.ElementAt(i));
                el_Categories.AppendChild(category);
            }
            el.AppendChild(el_Categories);
            
            var words = publication.KeyWords;
            XmlElement el_Words = xmlDoc.CreateElement("KeyWords");
            for (int i = 0; i < words.Count; i++)
            {
                XmlElement word = xmlDoc.CreateElement("KeyWord");
                word.SetAttribute("Name", words.ElementAt(i));
                el_Words.AppendChild(word);
            }
            el.AppendChild(el_Words);

            xRoot.AppendChild(el);
            xmlDoc.Save(path+"publications.xml");
        }

        public List<User> GetUsers()
        {
            List<User> result = new List<User>();
            //Загрузили файл
            XDocument xDocument = XDocument.Load(path + "users.xml");
            //Выбрали все элементы
            //.Elements("Users")
            var xElementsUsers = xDocument.Root.Elements().ToList();
            for (int i = 0; i < xElementsUsers.Count; i++)
            {
                var user = xElementsUsers.ElementAt(i);
                result.Add(new User
                {
                    Id=Convert.ToInt32(user.Attribute("Id").Value),
                    Login = user.Attribute("Login").Value,
                    Email = user.Attribute("Email").Value,
                    Password=user.Attribute("Password").Value,
                    LastName = user.Attribute("LastName").Value,
                    Name=user.Attribute("Name").Value,
                    Otch=user.Attribute("Otch").Value,
                    Work=user.Attribute("Work").Value,
                    Role=user.Attribute("Role").Value,
                    IsBlock=Convert.ToBoolean(user.Attribute("isBlock").Value)
                }) ;
            }
            return result;
        }

        public void UpdateUser(User user) 
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(path + "users.xml");
            var root = doc.DocumentElement;
            var old = root.ChildNodes.Item((user.Id.Value)-1);
            XmlElement el = doc.CreateElement("User");
            el.SetAttribute("Id", user.Id.ToString());
            el.SetAttribute("Login", user.Login);
            el.SetAttribute("Password", user.Password);
            el.SetAttribute("Email", user.Email);
            el.SetAttribute("LastName", user.LastName);
            el.SetAttribute("Name", user.Name);
            el.SetAttribute("Otch", user.Otch);
            el.SetAttribute("Work", user.Work);
            el.SetAttribute("Role", user.Role.ToString());
            el.SetAttribute("isBlock", user.IsBlock.ToString());

            root.ReplaceChild(el, old);
            doc.Save(path + "users.xml");

        }
    }
}
