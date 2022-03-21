using BisnessLogic.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SQLite;
using System.Linq;
using System.Reflection;
using System.Text;
using System.IO;
using System.Data;
using Microsoft.Data.Sqlite;

namespace Database
{
    public class MyDatabase : DbContext
    {
        //sqlite
        static string[] buf = AppDomain.CurrentDomain.BaseDirectory
           .Split('\\').Reverse().ToArray()[5..].Reverse().ToArray();
        static string path = string.Join('\\',buf);
        //string nameDB = path + "\\Database\\database.db";
        string dbFileName = //path + "\\Database\\database.db";
        path+"\\Database\\diplomdb.db"; 
        //":memory:";

        private SQLiteConnection m_dbConn;
        private SQLiteCommand m_sqlCmd;
        public DbSet<User> Users { get; set; }

        public MyDatabase()
        {
            m_dbConn = new SQLiteConnection();
            m_sqlCmd = new SQLiteCommand();
            CreateTables();
            Connect();
        }
        /*
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //имя базы данных
            optionsBuilder.UseSqlite("FileName=" + dbFileName, option =>
             {
                 option.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
             });
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //DatabaseGenerated(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<User>().ToTable("Users", "diplom");
            //метим некоторые параметры модели
            modelBuilder.Entity<User>(entity =>
            {
                //устанавливаем ключ
                entity.HasKey(k => k.Email);
                entity.HasKey(p => p.Id);

                //отчество может быть null 
                entity.Property(p => p.Otch).IsRequired(true);
                entity.Property(p => p.LastName).IsRequired(false);
                entity.Property(p => p.Name).IsRequired(false);
                entity.Property(p => p.Login).IsRequired(false);
                entity.Property(p => p.Password).IsRequired(false);
                entity.Property(p => p.Phone).IsRequired(false);
                //entity.Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
                //???????????????????????????????
                entity.Property(p => p.Id).ValueGeneratedOnAdd();

                entity.HasIndex(i => i.Login).IsUnique();
                entity.HasIndex(i => i.Phone).IsUnique();
            });

            base.OnModelCreating(modelBuilder);
        }
        */
        
        public void CreateTables()
        {
            if (!File.Exists(dbFileName))
                SQLiteConnection.CreateFile(dbFileName);

            try
            {
                m_dbConn = new SQLiteConnection("Data Source=" + dbFileName + ";Mode=Memory;Cache=Shared");
                m_dbConn.Open();
                m_sqlCmd.Connection = m_dbConn;

                m_sqlCmd.CommandText = "CREATE TABLE IF NOT EXISTS Users " +
                    "(Id INTEGER PRIMARY KEY AUTOINCREMENT, " +
                    "Login TEXT UNIQUE NOT NULL, " +
                    "Email TEXT UNIQUE NOT NULL," +
                    "Phone TEXT UNIQUE NOT NULL," +
                    "Password TEXT NOT NULL," +
                    "LastName TEXT NOT NULL," +
                    "Name TEXT NOT NULL," +
                    "Otch TEXT," +
                    "Work TEXT," +
                    "Role TEXT," +
                    "IsBlock TEXT);";
                m_sqlCmd.ExecuteNonQuery();

                m_sqlCmd.CommandText = "CREATE TABLE IF NOT EXISTS Autors " +
                    "(Id INTEGER PRIMARY KEY AUTOINCREMENT, " +
                    "Email TEXT UNIQUE NOT NULL," +
                    "Phone TEXT UNIQUE DEFAULT ''," +
                    "LastName TEXT NOT NULL," +
                    "Name TEXT NOT NULL," +
                    "Otch TEXT," +
                    "Work TEXT," +
                    "IsConnect TEXT);";
                m_sqlCmd.ExecuteNonQuery();


                m_sqlCmd.CommandText = "CREATE TABLE IF NOT EXISTS Publics " +
                    "( Id INTEGER PRIMARY KEY AUTOINCREMENT," +
                    "Title TEXT NOT NULL," +
                    "Annotation TEXT NOT NULL," +
                    "Status TEXT," +
                    "DateCreate TEXT" +
                    "DatePublic TEXT" +
                    ");";
                m_sqlCmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                throw;
            }
        }
        public void Connect()
        {
           /*
            if (!File.Exists(dbFileName))
            {
                //MessageBox.Show("Please, create DB and blank table (Push \"Create\" button)");
                throw new Exception("Запрашиваемой базы данных или таблицы нет.");
            }
           */
            try
            {
                m_dbConn = new SQLiteConnection("Data Source=" + dbFileName + ";Mode=Memory;Cache=Shared");
                //";Version=3;");
                m_dbConn.Open();
                //m_sqlCmd.Connection = m_dbConn;
            }
            catch (SQLiteException ex)
            {
            }
        }
        
        public void WriteUser(User user)
        {
            //Connect();
            //убеждаемся что бд создана
            //await this.Database.EnsureCreatedAsync();
            
            if (m_dbConn.State != ConnectionState.Open)
            {
                throw new Exception("Откройте соединение с базой данных.");
            }
            try
            {
                m_sqlCmd.CommandText = "INSERT INTO Users " +
                "('Login', 'Email', 'Phone', 'Password', 'LastName', 'Name', 'Otch', 'Work', 'Role', 'IsBlock') values ('" +
                        user.Login + "' , '" +
                        user.Email + "' , '" +
                        user.Phone+ "' , '"+
                        user.Password+ "' , '" + 
                        user.LastName+ "' , '" +
                        user.Name+ "' , '" +
                        user.Otch+ "' , '" + 
                        user.Work+ "' , '" + 
                        user.Role+ "' , '" +
                        user.IsBlock.ToString()+"')";

                m_sqlCmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<User> GetUsers()
        {
            List<User> user = new List<User>();
            using (var connection = new SqliteConnection("Data Source="+ dbFileName))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText =
                @"
                    SELECT *
                    FROM Users
                ";
                //command.Parameters.AddWithValue("$id", id);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var id = reader.GetString(0);
                        var login = reader.GetString(1);
                        var email = reader.GetString(2);
                        var phone= reader.GetString(3);
                        var pas=reader.GetString(4);
                        var last=reader.GetString(5);
                        var name=reader.GetString(6);
                        var otch=reader.GetString(7);
                        var work= reader.GetString(8);
                        var role=reader.GetString(9);
                        var block=reader.GetString(10);
                        
                        user.Add(new User 
                        { 
                            Id=Convert.ToInt32(id),
                            Login= login,
                            Email=email,
                            Phone=phone,
                            Password=pas,
                            LastName=last,
                            Name=name,
                            Otch=otch,
                            Work=work,
                            Role=role,
                            IsBlock=Convert.ToBoolean(block)
                        });
                    }
                }
            }
            return user;
            /*
            Connect();
            if (m_dbConn.State != ConnectionState.Open)
            {
                //MessageBox.Show("Open connection with database");
                //return;
                throw new Exception("Откройте соединение с базой данных.");
            }
            try
            {
                var sqlQuery = "SELECT * FROM Users";
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(sqlQuery, m_dbConn);
                adapter.Fill(dTable);

                List<User> users = new List<User>();
                if (dTable.Rows.Count > 0)
                {
                    for (int i = 0; i < dTable.Rows.Count; i++)
                    {
                        //dgvViewer.Rows.Add(dTable.Rows[i].ItemArray);
                        var data= dTable.Rows[i].ItemArray[0];
                    }
                }
                return user;
            }
            catch (Exception)
            {
                return null;
            }
            */
            //return this.Users.ToList();
        }

        public List<string> GetCategory()
        {
            List<string> result = new List<string>();
            using (var connection = new SqliteConnection("Data Source=" + dbFileName))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText =
                @"
                    SELECT * FROM Category
                ";
                //command.Parameters.AddWithValue("$id", id);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        //var id = reader.GetString(0);
                        //var name = reader.GetString(1);
                        result.Add(reader.GetString(1));
                    }
                }
            }
            return result;
        }
        public List<User> UpdateUser(User user) 
        {
            if (m_dbConn.State != ConnectionState.Open)
            {
                throw new Exception("Откройте соединение с базой данных.");
            }
            try
            {
                m_sqlCmd.CommandText = "UPDATE Users " +
                    "SET " +
                    "Login='" +user.Login+"',"+
                    "Phone='" +user.Phone+"',"+
                    "Password='"+user.Password+"'," +
                   "LastName='"+user.LastName+"'," +
                   "Name='"+user.Name+"'," +
                   "Otch='"+user.Otch+"'," +
                   "Work='"+user.Work+"'," +
                   "IsBlock='" +user.IsBlock.ToString()+"' WHERE Id="+user.Id+"";
                m_sqlCmd.ExecuteNonQuery();
                return GetUsers();

            }
            catch (Exception)
            {
                return GetUsers();
            }
        }

        public void ExportDatabase()
        {
            
        }

        public bool CheakLoginAndEmail(string login, string email)
        {
            var user=Users.FirstOrDefault(rec => rec.Email == email || rec.Login == login);
            //если нет таких записей, то нет пользователя
            if (user == null)
                return false;
            else
                return true;
        }
        public void RemoveAllUsers()
        {
            Users.RemoveRange(Users.ToArray());
        }
    }
}
