using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnitTestProject1.TestsForAdmin;
using UnitTestProject1.TestsForAutor;
using UnitTestProject1.TestsForEditor;
using UnitTestProject1.TestsForReviewer;
using System.Threading.Tasks;

namespace UnitTestProject1
{
    public class Start
    {
        public static int countOk = 0;
        public static int countError = 0;
        static void Main(string[] args)
        {
            Console.WriteLine("Автоматизированное тестирование проекта.");
            //TestEnterAutor();
            //TestEnterReviewer();
            //TestEnterEditor();
            //TestEnterAdmin();
            //TestRegisterAutor();
            //TestRegisterEditor();
            //TestBlockUser();
            TestGetAllPublicationByAutor();
            Console.WriteLine("Удачно пройдено тестов: "+ countOk);
            Console.WriteLine("Завалено тестов: " + countError);
        }

        private static void TestEnterAutor()
        {
            TestEnterAutor test = new TestEnterAutor();
            Console.WriteLine("Вход автора:");
            Console.WriteLine("Вход автора с неправильными данными:");
            try
            {
                test.TestMethod_BedEnterAutor();
                countOk++;
            }
            catch (Exception) { countError++; }
            
            Console.WriteLine("Вход автора с пустыми данными:");
            try
            {
                test.TestMethod_EmptyAll();
                countOk++;
            }
            catch (Exception) { countError++; }
            
            Console.WriteLine("Вход автора с пустым паролем:");
            try
            {
                test.TestMethod_EmptyPass();
                countOk++;
            }
            catch (Exception) { countError++; }
            
            Console.WriteLine("Вход автора с неправильным паролем:");
            try
            {
                test.TestMethod_WrongPass();
                countOk++;
            }
            catch (Exception) { countError++; }

            Console.WriteLine("Вход автора с неправильной ролью:");
            try 
            {
                test.TestMethod_DifferentRole();
                countOk++;
            }
            catch (Exception) { countError++; }
            Console.WriteLine("Вход автора:");
            try
            {
                test.TestMethod_GoodEnterAutor();
                countOk++;
            }
            catch (Exception) { countError++; }
        }
        private static void TestEnterReviewer() 
        {
            TestEnterReviewer test = new TestEnterReviewer();
            Console.WriteLine("Вход рецензента:");
            Console.WriteLine("Вход рецензента с неправильными данными:");
            try
            {
                test.TestMethod_BadEnterRev();
                countOk++;
            }
            catch (Exception) { countError++; }

            Console.WriteLine("Вход рецензента с пустыми данными:");
            try
            {
                test.TestMethod_EmptyAll();
                countOk++;
            }
            catch (Exception) { countError++; }

            Console.WriteLine("Вход рецензента с пустым паролем:");
            try
            {
                test.TestMethod_EmptyPass();
                countOk++;
            }
            catch (Exception) { countError++; }

            Console.WriteLine("Вход рецензента с неправильным паролем:");
            try
            {
                test.TestMethod_WrongPass();
                countOk++;
            }
            catch (Exception) { countError++; }

            Console.WriteLine("Вход рецензента с неправильной ролью:");
            try
            {
                test.TestMethod_DifferentRole();
                countOk++;
            }
            catch (Exception) { countError++; }
            Console.WriteLine("Вход рецензента:");
            try
            {
                test.TestMethod_GoodEnterRev();
                countOk++;
            }
            catch (Exception) { countError++; }
        }
        private static void TestEnterEditor() 
        {
            TestEnterEditor test = new TestEnterEditor();
            Console.WriteLine("Вход редактора:");
            Console.WriteLine("Вход редактора с неправильными данными:");
            try
            {
                test.TestMethod_BadEnterEditor();
                countOk++;
            }
            catch (Exception) { countError++; }

            Console.WriteLine("Вход редактора с пустыми данными:");
            try
            {
                test.TestMethod_EmptyAll();
                countOk++;
            }
            catch (Exception) { countError++; }

            Console.WriteLine("Вход редактора с пустым паролем:");
            try
            {
                test.TestMethod_EmptyPass();
                countOk++;
            }
            catch (Exception) { countError++; }

            Console.WriteLine("Вход редактора с неправильным паролем:");
            try
            {
                test.TestMethod_WrongPass();
                countOk++;
            }
            catch (Exception) { countError++; }

            Console.WriteLine("Вход рецензента:");
            try
            {
                test.TestMethod_GoodEnterEditor();
                countOk++;
            }
            catch (Exception) { countError++; }
        }
        private static void TestEnterAdmin()
        {
            TestEnterAdmin test = new TestEnterAdmin();
            Console.WriteLine("Вход администратора:");
            Console.WriteLine("Вход администратора с неправильными данными:");
            try
            {
                test.TestMethod_BadEnterAdmin();
                countOk++;
            }
            catch (Exception) { countError++; }

            Console.WriteLine("Вход администратора с пустыми данными:");
            try
            {
                test.TestMethod_EmptyAll();
                countOk++;
            }
            catch (Exception) { countError++; }

            Console.WriteLine("Вход администратора с пустым паролем:");
            try
            {
                test.TestMethod_EmptyPass();
                countOk++;
            }
            catch (Exception) { countError++; }

            Console.WriteLine("Вход администратора с неправильным паролем:");
            try
            {
                test.TestMethod_WrongPass();
                countOk++;
            }
            catch (Exception) { countError++; }

            Console.WriteLine("Вход администратора:");
            try
            {
                test.TestMethod_GoodEnterAdmin();
                countOk++;
            }
            catch (Exception) { countError++; }
        }
        private static void TestRegisterAutor() 
        {
            TestRegisterAutor test = new TestRegisterAutor();
            Console.WriteLine("Регистрация автора");
            Console.WriteLine("Регистрация существующего автора:");
            try
            {
                test.TestMethod_RegisterOldAutor();
                countOk++;
            }
            catch (Exception) { countError++; }
            Console.WriteLine("Регистрация автора со всеми пустыми полями:");
            try
            {
                test.TestMethod_RegisterEmptyAll();
                countOk++;
            }
            catch (Exception) { countError++; }

            Console.WriteLine("Регистрация автора только с логином");
            try
            {
                test.TestMethod_RegisterWithOnlyLogin();
                countOk++;
            }
            catch (Exception) { countError++; }
            
            Console.WriteLine("Регистрация автора с ФИО и логином");
            try 
            {
                test.TestMethod_RegisterWithFIOAndLogin();
                countOk++;
            } 
            catch (Exception) { countError++; }
            Console.WriteLine("Регистрация автора без места работы");
            try 
            {
                test.TestMethod_RegisterAutorWithoutWork();
                countOk++;
            } 
            catch (Exception) { countError++; }
            Console.WriteLine("Регистрация автора с неправильно введенным паролем");
            try 
            {
                test.TestMethod_RegisterAutorWithWrongPass();
                countOk++;
            } catch (Exception) { countError++; }
            Console.WriteLine("Регистрация автора с неправильной почтой");
            try 
            {
                test.TestMethod_RegisterAutorWithWrongEmail();
                countOk++;
            } catch (Exception) { countError++; }
            Console.WriteLine("Регистрация автора с уже имеющейся почтой");
            try 
            {
                test.TestMethod_RegisterAutorWithEmail();
                countOk++;
            } catch (Exception) { countError++; }
            Console.WriteLine("Регистрация автора с неправильным номером телефона");
            try 
            {
                test.TestMethod_RegisterAutorWithWrongPhone();
                countOk++;
            } catch (Exception) { countError++; }
            Console.WriteLine("Регистрация авора с уже имеющимся номером телефона");
            try 
            {
                test.TestMethod_RegisterAutorWithPhone();
                countOk++;
            } catch (Exception) { countError++; }
        }
        private static void TestRegisterEditor() 
        {
            TestRegisterEditor test = new TestRegisterEditor();
            Console.WriteLine("Регистрация редактора");
            Console.WriteLine("Регистрация существующего редактора:");
            try
            {
                test.TestMethod_RegisterOldEditor();
                countOk++;
            }
            catch (Exception) { countError++; }
            Console.WriteLine("Регистрация редактора со всеми пустыми полями:");
            try
            {
                test.TestMethod_RegisterEmptyAll();
                countOk++;
            }
            catch (Exception) { countError++; }

            Console.WriteLine("Регистрация редактора только с логином");
            try
            {
                test.TestMethod_RegisterWithOnlyLogin();
                countOk++;
            }
            catch (Exception) { countError++; }

            Console.WriteLine("Регистрация редактора с ФИО и логином");
            try
            {
                test.TestMethod_RegisterWithFIOAndLogin();
                countOk++;
            }
            catch (Exception) { countError++; }
            Console.WriteLine("Регистрация редактора без места работы");
            try
            {
                test.TestMethod_RegisterWithoutWork();
                countOk++;
            }
            catch (Exception) { countError++; }
            Console.WriteLine("Регистрация редактора с неправильно введенным паролем");
            try
            {
                test.TestMethod_RegisterWithWrongPass();
                countOk++;
            }
            catch (Exception) { countError++; }
            Console.WriteLine("Регистрация редактора с неправильной почтой");
            try
            {
                test.TestMethod_RegisterWithWrongEmail();
                countOk++;
            }
            catch (Exception) { countError++; }
            Console.WriteLine("Регистрация редактора с уже имеющейся почтой");
            try
            {
                test.TestMethod_RegisterWithOldEmail();
                countOk++;
            }
            catch (Exception) { countError++; }
            Console.WriteLine("Регистрация редактора с неправильным номером телефона");
            try
            {
                test.TestMethod_RegisterWithWrongPhone();
                countOk++;
            }
            catch (Exception) { countError++; }
            Console.WriteLine("Регистрация редактора с уже имеющимся номером телефона");
            try
            {
                test.TestMethod_RegisterWithPhone();
                countOk++;
            }
            catch (Exception) { countError++; }
        }
        private static void TestBlockUser() 
        {
            TestBlockUser test = new TestBlockUser();
            Console.WriteLine("Добавление и снятие блокировки пользователя:");
            Console.WriteLine("Добавление блокировки пользователя:");
            try 
            {
                test.TestMethod_AddBlock();
                countOk++;
            } 
            catch (Exception) 
            {
                countError++;
            }
            Console.WriteLine("Снятие блокировки пользователя:");
            try 
            {
                test.TestMethod_CloseBlock();
                countOk++;
            } 
            catch (Exception) 
            {
                countError++;
            }
        }
        private static void TestGetAllPublicationByAutor() 
        {
            TestAllPublication test = new TestAllPublication();
            Console.WriteLine("Получение списка всех статей автором:");
            try
            {
                test.TestMethod_GetAllPublication();
                countOk++;
            }
            catch (Exception)
            {
                countError++;
            }
            
            Console.WriteLine("Получение информации о статье:");
            try
            {
                test.TestMethod_GetInformationPublication();
                countOk++;
            }
            catch (Exception)
            {
                countError++;
            }

            Console.WriteLine("Получение результата рецензирования статьи:");
            try
            {
                test.TestMethod_GetResultReviewPublication();
                countOk++;
            }
            catch (Exception)
            {
                countError++;
            }
        }
    }
}
