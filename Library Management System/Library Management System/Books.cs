using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Remoting.Messaging;

namespace Library_Management_System
{
    internal class Books
    {
        public string BookName { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }

        public int BookID { get; set; }

        public Books(string bookName, string author, string genre, int bookID)
        {
            BookName = bookName;
            Author = author;
            Genre = genre;
            BookID = bookID;
        }

        public static List<Books> bookList = new List<Books>
            {
                new Books("The Alchemist", "Paulo Coelho", "Fiction", 1000),
                new Books("Pride and Prejudice", "Jane Austen", "Romance", 1001),
                new Books("Harry Potter and the Philosopher's Stone", "J.K. Rowling", "Fantasy", 1002),
                new Books("The Hobbit", "J.R.R. Tolkien", "Fantasy", 1003),
                new Books("To Kill a Mockingbird", "Harper Lee", "Fiction", 1004),
                new Books("The Great Gatsby", "F. Scott Fitzgerald", "Fiction", 1005),
                new Books("The Catcher in the Rye", "J.D. Salinger", "Fiction", 1006),
                new Books("The Lord of the Rings", "J.R.R. Tolkien", "Fantasy", 1007),
                new Books("Mockingjay", "Suzanne Collins", "Science Fiction", 1008),
                new Books("The Hunger Games", "Suzanne Collins", "Science Fiction", 1009)
            };

        public static List<Books> BorrowedList = new List<Books>();

        static string FilePath = "Books.csv";
        public static void BooksInFile()
        {
            using (StreamWriter Writer = new StreamWriter(FilePath))
            {
                Writer.WriteLine("BookName,Author,Genre,ID Number");
                foreach (Books book in bookList)
                {
                    Writer.WriteLine($"{book.BookName},{book.Author},{book.Genre},{book.BookID}");
                }
            }

            using (StreamReader Reader = new StreamReader(FilePath))
            {
                string line;
                while ((line = Reader.ReadLine()) != null)
                {
                    Console.WriteLine(line);
                }
            }
        }
        public static void AddBookToList()
        {
            Console.WriteLine("Enter Book name :");
            string bookName = Console.ReadLine();
            Console.WriteLine("Enter Author name :");
            string author = Console.ReadLine();
            Console.WriteLine("Enter Genre");
            string genre = Console.ReadLine();
            Console.WriteLine("Enter BookID");
            int bookID = Convert.ToInt32(Console.ReadLine());

            Books book = new Books(bookName, author, genre, bookID);
            bookList.Add(book);
            using (StreamWriter Writer = new StreamWriter(FilePath, true))
            {
                Writer.WriteLine($"{bookName}, by {author}, Genre: {genre}, ID: {bookID}");
            }
        }
        public static void EditBook()
        {
            Console.WriteLine("Enter the name of the book you want to edit");
            string bookName = Console.ReadLine();
            Books book = bookList.Find(b => b.BookName == bookName);
            if (book != null)
            {
                bool edit = true;
                do 
                {
                    Console.WriteLine("What would you like to change?");
                    Console.WriteLine("1. Book Name");
                    Console.WriteLine("2. Author");
                    Console.WriteLine("3. Genre");
                    Console.WriteLine("4. BookID");
                    Console.WriteLine("5. Finished");
                    int choice = Convert.ToInt32(Console.ReadLine());
                    switch (choice)
                    {
                        case 1:
                            Console.WriteLine("Enter new book name");
                            book.BookName = Console.ReadLine();
                            break;
                        case 2:
                            Console.WriteLine("Enter new author name");
                            book.Author = Console.ReadLine();
                            break;
                        case 3:
                            Console.WriteLine("Enter new genre");
                            book.Genre = Console.ReadLine();
                            break;
                        case 4:
                            Console.WriteLine("Enter new BookID");
                            book.BookID = Convert.ToInt32(Console.ReadLine());
                            break;
                        case 5:
                            edit = false;
                            break;
                        default:
                            Console.WriteLine("Invalid choice");
                            break;
                    } 
                }while(edit);


                Console.WriteLine("Book details updated successfully");
            }
            else
            {
                Console.WriteLine("Book not found");
            }
        }
    }
}
