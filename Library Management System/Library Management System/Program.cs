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
        static int StudentIDLoggedIn;
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
            if (choice == 1|| choice == 2|| choice == 3|| choice == 4)
            {
                switch (choice)
                {
                    case 1:
                        UserRegisterorLoginMenu();
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
            else
            {
                Console.WriteLine("Invalid choice");
                StartMenu();
            }
            
        }
        private static void Admin()
        {
            Console.WriteLine("Enter AdminID");
            string AdminID = Console.ReadLine();
            Console.WriteLine("Enter Admin Password");
            string AdminPassword = Console.ReadLine();
            if (AdminID == AdminUsername && AdminPassword == AdminSetPassword)
            {
                AdminBookEditMenu();
            }
            else
            {
                Console.WriteLine("Invalid AdminID");
            }
        }

        private static void AdminBookEditMenu()
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
                    AdminBookEditMenu();
                    break;
                case 2:
                    Books.EditBook();
                    AdminBookEditMenu();
                    break;
                case 3:
                    Books.DeleteBook();
                    AdminBookEditMenu();
                    break;
                case 4:
                    StartMenu();
                    break;
                default:
                    Console.WriteLine("Invalid choice");
                    break;
            }
        }

        public static void UserRegisterorLoginMenu()
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
        }
        public static void Register()
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
        public static bool Login()
        {
            Console.WriteLine("Enter your username:");
            string username = Console.ReadLine();
            Console.WriteLine("Enter your password:");
            string password = Console.ReadLine();
            if (!File.Exists(FilePath))
            {
                Console.WriteLine("Error: User data file not found.");
                return false;
            }
            string[] lines = File.ReadAllLines(FilePath);
            foreach (string line in lines)
            {
                string[] parts = line.Split(',');
                if (parts[0] == username && parts[1] == password)
                {
                    StudentIDLoggedIn = Convert.ToInt16(parts[2]);
                    Console.WriteLine($"Welcome back {StudentIDLoggedIn}");
                    BookStartMenu();
                    return true;
                }
            }
            Console.WriteLine("Invalid username or password");
            return false; 
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
                    ReturnBook();
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
            DateTime d1 = DateTime.Now;
            Books.BooksInFile();
            Console.WriteLine("Which book would you like to borrow. Enter Exact serial number");
            int bookID = Convert.ToInt32(Console.ReadLine());
            Books book = Books.bookList.Find(b => b.BookID == bookID);
            if (book != null)
            {
                book.DateBorrowed = d1;
                book.StudentID = StudentIDLoggedIn;
                Books.BorrowedList.Add(book);
                Books.bookList.Remove(book);
                Console.WriteLine($"{book.StudentID} has borrowed {book.BookName} by {book.Author}, BookID: {book.BookID} at {d1}");
            }
            else
            {
                Console.WriteLine("Book not found.");
            }
        }
        static void ReturnBook()
        {
            
            DateTime d2 = DateTime.Now;
            Console.WriteLine("Which book would you like to return. Enter Exact serial number");
            int bookID = Convert.ToInt32(Console.ReadLine());
            Books book = Books.BorrowedList.Find(b => b.BookID == bookID);
            Books.BorrowedList.Remove(book);
            if ((d2 - book.DateBorrowed).Seconds > 7)
            {
                Console.WriteLine($"{book.StudentID} have returned the book late. You will be fined.");
                int fine = (d2 - book.DateBorrowed).Seconds * 5;
                Console.WriteLine($"You have been fined {fine} euros. Pay at front desk.");
            }
            else
            {
                Console.WriteLine($"Thank you for returning the book on time, {book.StudentID}");
            }
            Books.bookList.Add(book);
        }
    }
}