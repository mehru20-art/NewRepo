using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics.Eventing.Reader;

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
            int choice;
            Console.WriteLine("\nWould you like to login as a user or an admin?");
            Console.WriteLine("1. User");
            Console.WriteLine("2. Admin");
            Console.WriteLine("3. Exit Program");
            Console.WriteLine("Enter your choice: ");
            string enteredchoice = Console.ReadLine();
            if (!int.TryParse(enteredchoice, out int result))
            {
                Console.WriteLine("Invalid choice");
                StartMenu();
            }
            choice = Convert.ToInt32(enteredchoice);


            if (choice == 1 || choice == 2 || choice == 3)
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
            Console.WriteLine("\nEnter AdminID");
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
            Console.WriteLine("\nLogin successful");
            Console.WriteLine("\n1. Add Book");
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
            Console.WriteLine("\n1. Register");
            Console.WriteLine("2. Login");
            Console.WriteLine("3. Return to main menu");
            Console.WriteLine("Enter your choice: ");
            string enteredchoice = Console.ReadLine();
            if (!int.TryParse(enteredchoice, out int result))
            {
                Console.WriteLine("Invalid choice");
                StartMenu();
            }
            int choice = Convert.ToInt32(enteredchoice);
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
            Console.WriteLine("\nEnter your StudentID (5 digits)");
            string studentIDInput = (Console.ReadLine());
            while (!int.TryParse(studentIDInput, out int studentID) || studentIDInput.Length != 5)
            {
                Console.WriteLine("Invalid StudentID. Please enter a 5-digit number:");
                studentIDInput = Console.ReadLine();
            }
            User.StudentID = Convert.ToInt32(studentIDInput);
            User.Username = GetValidInput("Enter your Username:");
            string passwordbeforehash = GetValidInput("Enter your password:");
            User.Password = User.HashingPassword(passwordbeforehash);
            User user = new User(User.Username, User.Password, User.StudentID);

            using (StreamWriter sw = new StreamWriter(FilePath, true))
            {
                sw.WriteLine(User.Username + "," + User.Password + "," + User.StudentID);
            }

            Console.WriteLine("\nUser registered successfully");
        }
        public static bool Login()
        {
            if (!File.Exists(FilePath))
            {
                Console.WriteLine("Error: User data file not found.");
                return false;
            }

            Console.WriteLine("\nEnter your username:");
            string username = Console.ReadLine();
            Console.WriteLine("Enter your password:");
            string passwordbeforehashcheck = Console.ReadLine();
            string hashedpasswordtocheck = User.HashingPassword(passwordbeforehashcheck);

            string[] lines = File.ReadAllLines(FilePath);
            foreach (string line in lines)
            {
                string[] parts = line.Split(',');

                if (parts[0] == username && parts[1] == hashedpasswordtocheck)
                {
                    Console.WriteLine("Correct Username *clap*");

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
            Console.WriteLine("\n1. View Books");
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
                    BookStartMenu();
                    break;
                case 3:
                    ReturnBook();
                    BookStartMenu();
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
            Console.WriteLine("");
            Books.BooksInFile();
            Console.WriteLine("\nWhich book would you like to borrow. Enter Exact serial number");
            int bookID = Convert.ToInt32(Console.ReadLine());
            Books book = Books.bookList.Find(b => b.BookID == bookID);
            if (book != null)
            {
                book.DateBorrowed = d1;
                book.StudentID = StudentIDLoggedIn;
                Books.BorrowedList.Add(book);
                using (StreamWriter writetofile = new StreamWriter(Books.FilePath2, true))
                {
                    writetofile.WriteLine($"{book.StudentID},{book.BookName},{book.Genre},{book.Author},{book.BookID},{d1}");
                }
                Books.bookList.Remove(book);
                Console.WriteLine($"{book.StudentID} has borrowed {book.BookName} by {book.Author},Genre :{book.Genre}, BookID: {book.BookID} at {d1}");
            }
            else
            {
                Console.WriteLine("Book not found.");
            }
        }
        static void ReturnBook()
        {
            if (!File.Exists(Books.FilePath2))
            {
                Console.WriteLine("Error: Borrowed books data file not found.");
                return;
            }

            List<Books> borrowedBooks = new List<Books>();
            string[] lines = File.ReadAllLines(Books.FilePath2);
            foreach (string line in lines.Skip(1)) 
            {
                string[] parts = line.Split(',');
                int studentID = Convert.ToInt32(parts[0]);
                if (studentID == StudentIDLoggedIn)
                {
                    string bookName = parts[1];
                    string author = parts[3];
                    string genre = parts[2];
                    int bookID = Convert.ToInt32(parts[4]);
                    DateTime dateBorrowed = DateTime.ParseExact(parts[5], "dd/MM/yyyy HH:mm:ss", null);
                    borrowedBooks.Add(new Books(bookName, author, genre, bookID, dateBorrowed, studentID));
                }
            }

            if (borrowedBooks.Count == 0)
            {
                Console.WriteLine("You have not borrowed any books yet");
                return;
            }

            Console.WriteLine("\nBooks you have borrowed:");
            foreach (Books book in borrowedBooks)
            {
                Console.WriteLine($"BookID: {book.BookID}, BookName: {book.BookName}, Author: {book.Author}, DateBorrowed: {book.DateBorrowed}");
            }

            Console.WriteLine("\nWhich book would you like to return? Enter the exact BookID:");
            int bookIDToReturn = Convert.ToInt32(Console.ReadLine());
            Books bookToReturn = borrowedBooks.Find(b => b.BookID == bookIDToReturn);
            if (bookToReturn != null)
            {
                DateTime d2 = DateTime.Now;
                if ((d2 - bookToReturn.DateBorrowed).Seconds > 7)
                {
                    DateTime d3 = bookToReturn.DateBorrowed;
                    int fine = (((d2 - d3).Seconds)-7) * 10;
                    //Console.WriteLine(d2);
                    Console.WriteLine($"\n{bookToReturn.StudentID} have returned the book late. You will be fined.");
                    //int fine = (d2 - bookToReturn.DateBorrowed).Days * 10;
                    Console.WriteLine($"You have been fined {fine} euros. Pay at front desk.");
                }
                else
                {
                    Console.WriteLine($"\nThank you for returning the book on time, {bookToReturn.StudentID}");
                }
                Books.bookList.Add(bookToReturn);

                using (StreamWriter Writer = new StreamWriter(Books.FilePath2))
                {
                    Writer.WriteLine("StudentID,BookName,Author,Genre,BookID,DateBorrowed");
                    foreach (Books b in Books.BorrowedList)
                    {
                        if (b.BookID != bookIDToReturn)
                        {
                            Writer.WriteLine($"{b.StudentID},{b.BookName},{b.Author},{b.Genre},{b.BookID},{b.DateBorrowed:yyyy-MM-dd HH:mm:ss}");
                        }
                    }
                }

                Books.BorrowedList.Remove(bookToReturn);
            }
            else
            {
                Console.WriteLine("Book not found.");
            }
        }
        static string GetValidInput(string question)
        {
            Console.WriteLine(question);
            string validate = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(validate) )
            {
                Console.WriteLine("Input cannot be empty. Try again:");
                validate = Console.ReadLine();
            }
            return validate;
        }
    }
}