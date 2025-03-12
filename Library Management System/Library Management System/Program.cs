using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Library_Management_System
{
    internal class Program
    {
        private static string AdminUsername = "Admin130613";
        private static string AdminSetPassword = "OnlyIcaneditbookentries7";
        static string FilePath = "Users.csv";
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Library Management System");
            StartMenu();
        }

        private static void StartMenu()
        {
            Console.WriteLine("Would you like to login as a user or an admin?");
            Console.WriteLine("1. User");
            Console.WriteLine("2. Admin");
            Console.WriteLine("3. Exit");
            Console.WriteLine("4. View Available Books");
            Console.WriteLine("Enter your choice: ");
            int choice = Convert.ToInt32(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    UserRegisterorlogin();
                    break;
                case 2:
                    Admin();
                    break;
                case 3:
                    Environment.Exit(0);
                    break;
                case 4:
                    Books.BooksInFile();
                    StartMenu();
                    break;
                default:
                    Console.WriteLine("Invalid choice");
                    break;
            }
        }
        private static void Admin()
        {
            Console.WriteLine("Enter AdminID");
            string AdminID = Console.ReadLine();
            Console.WriteLine("Enter Admin Password");
            string AdminPassword = Console.ReadLine();
            if (AdminID == AdminUsername)
            {
                if(AdminPassword == AdminSetPassword)
                {
                    Console.WriteLine("Login successful");
                    Console.WriteLine("1. Add Book");
                    Console.WriteLine("2. Edit Book");
                    Console.WriteLine("3. Delete Book");
                    Console.WriteLine("4. Exit");
                    Console.WriteLine("Enter your choice: ");
                    int choice = Convert.ToInt32(Console.ReadLine());
                    switch (choice)
                    {
                        case 1:
                            Books.AddBookToList();
                            break;
                        case 2:
                            Books.EditBook();
                            break;
                        case 3:
                            //DeleteBook();
                            break;
                        case 4:
                            StartMenu();
                            break;
                        default:
                            Console.WriteLine("Invalid choice");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid Password");
                }

            }
            else
            {
                Console.WriteLine("Invalid AdminID");
            }
        }
        public static void UserRegisterorlogin()
        {
            Console.WriteLine("1. Register");
            Console.WriteLine("2. Login");
            Console.WriteLine("3. Return to main menu");
            Console.WriteLine("Enter your choice: ");
            int choice = Convert.ToInt32(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    Register();
                    StartMenu();
                    break;
                case 2:
                    Login();
                    StartMenu();
                    break;
                case 3:
                    StartMenu();
                    break;
                default:
                    Console.WriteLine("Invalid choice");
                    break;
            }
            void Register()
            {
                Console.WriteLine("Enter your StudentID");
                User.StudentID = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter your Username:");
                User.Username = Console.ReadLine();
                Console.WriteLine("Enter your password:");
                User.Password = Console.ReadLine();
                
                User user = new User(User.Username, User.Password, User.StudentID);
                
                using (StreamWriter sw = new StreamWriter(FilePath, true))
                {
                    sw.WriteLine("Mehreen, bulletproof7," + 13456);  
                    sw.WriteLine(User.Username + "," + User.Password + "," + User.StudentID);
                    Console.WriteLine("User saved to csv file");
                }

                Console.WriteLine("User registered successfully");

            }
            bool Login()
            {
                Console.WriteLine("Enter your username:");
                string username = Console.ReadLine();
                Console.WriteLine("Enter your password:");
                string password = Console.ReadLine();

                string[] lines = File.ReadAllLines(FilePath);
                foreach (string line in lines)
                {
                    string[] parts = line.Split(',');
                    if (parts[0] == username && parts[1] == password)
                    {
                        Console.WriteLine("Login successful");
                        BookStartMenu();
                        return true; // Username and password match
                    }
                }

                Console.WriteLine("Invalid username or password");
                return false; // Invalid credentials
            }
        }
        public static void BookStartMenu()
        {
            Console.WriteLine("1. View Books");
            Console.WriteLine("2. Borrow Book");
            Console.WriteLine("3. Return Book");
            Console.WriteLine("4. Return to main menu");
            Console.WriteLine("Enter your choice: ");
            int choice = Convert.ToInt32(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    Books.BooksInFile();
                    BookStartMenu();
                    break;
                case 2:
                    BorrowBook();
                    break;
                case 3:
                    //ReturnBook();
                    break;
                case 4:
                    StartMenu();
                    break;
                default:
                    Console.WriteLine("Invalid choice");
                    break;
            } 
        }

        static void BorrowBook()
        {
            Books.BooksInFile();
            Console.WriteLine("Which book would you like to borrow. Enter Exact serial number");

        }
    }
}