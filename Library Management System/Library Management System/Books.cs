﻿using System;
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

        public DateTime DateBorrowed { get; set; }
        public int StudentID { get; set; }

        public Books(string bookName, string author, string genre, int bookID)
        {
            BookName = bookName;
            Author = author;
            Genre = genre;
            BookID = bookID;
        }
        
        public Books(string bookName, string author, string genre, int bookID, DateTime dateBorrowed, int studentID) : this(bookName, author, genre, bookID)
        {
            DateBorrowed = dateBorrowed;
            StudentID = studentID;
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

        public static string FilePath = "BooksAvailable.csv";
        public static string FilePath2 = "BooksBorrowed.csv";

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
            Console.WriteLine("\nEnter Book name :");
            string bookName = Console.ReadLine();
            Console.WriteLine("\nEnter Author name :");
            string author = Console.ReadLine();
            Console.WriteLine("\nEnter Genre");
            string genre = Console.ReadLine();
            Console.WriteLine("\nEnter BookID");
            int bookID = Convert.ToInt32(Console.ReadLine());

            Books book = new Books(bookName, author, genre, bookID);
            bookList.Add(book);
            using (StreamWriter Writer = new StreamWriter(FilePath, true))
            {
                Writer.WriteLine($"{bookName}, {author}, {genre}, {bookID}");
            }
        }
        public static void EditBook()
        {
            Console.WriteLine("Enter the BookID of the book you want to edit");
            int LookupbookID = Convert.ToInt32(Console.ReadLine());
            Books book = bookList.Find(b => b.BookID == LookupbookID);
            if (book == null)
            {
                Console.WriteLine("Book may be borrowed or does not exist.");
                return;
            }
            Console.WriteLine($"Is the book you want to edit {book.BookName}, BookID: {book.BookID}");
            Console.WriteLine("Enter 1 to confirm or 2 to cancel");
            int confirm = Convert.ToInt32(Console.ReadLine());
            if (confirm == 1)
            {
                bool edit = true;
                do
                {
                    Console.WriteLine("\nWhat would you like to change?");
                    Console.WriteLine("1. Book Name");
                    Console.WriteLine("2. Author");
                    Console.WriteLine("3. Genre");
                    Console.WriteLine("4. BookID");
                    Console.WriteLine("5. Finished");
                    int choice = Convert.ToInt32(Console.ReadLine());
                    switch (choice)
                    {
                        case 1:
                            Console.WriteLine("\nEnter new book name");
                            book.BookName = Console.ReadLine();
                            break;
                        case 2:
                            Console.WriteLine("\nEnter new author name");
                            book.Author = Console.ReadLine();
                            break;
                        case 3:
                            Console.WriteLine("\nEnter new genre");
                            book.Genre = Console.ReadLine();
                            break;
                        case 4:
                            Console.WriteLine("\nEnter new BookID");
                            book.BookID = Convert.ToInt32(Console.ReadLine());
                            break;
                        case 5:
                            edit = false;
                            break;
                        default:
                            Console.WriteLine("Invalid choice");
                            break;
                    }
                } while (edit);

                using (StreamWriter updatebookentry = new StreamWriter(FilePath))
                {
                    updatebookentry.WriteLine("BookName,Author,Genre,ID Number");
                    foreach (Books books in bookList)
                    {
                        updatebookentry.WriteLine($"{books.BookName},{books.Author},{books.Genre},{books.BookID}");
                    }
                }

                Console.WriteLine("Book details updated successfully");
                
            }
            else
            {
                Console.WriteLine("Edit cancelled");
            }
        }

        public static void DeleteBook()
        {
            Console.WriteLine("\nEnter the BookID of the book you want to delete");
            int DeleteBookID = Convert.ToInt32(Console.ReadLine());
            Books book = bookList.Find(b => b.BookID == DeleteBookID);
            if (book == null)
            {
                Console.WriteLine("Book may be borrowed or does not exist.");
            }
            Console.WriteLine($"Is the book you want to delete {book.BookName}, by {book.Author} BookID: {book.BookID}");
            Console.WriteLine("Enter 1 to confirm or 2 to cancel");
            int confirm = Convert.ToInt32(Console.ReadLine());
            if (confirm == 1)
            {
                bookList.Remove(book);
                using (StreamWriter updatebookentry = new StreamWriter(FilePath))
                {
                    updatebookentry.WriteLine("BookName,Author,Genre,ID Number");
                    foreach (Books books in bookList)
                    {
                        updatebookentry.WriteLine($"{books.BookName},{books.Author},{books.Genre},{books.BookID}");
                    }
                }
                Console.WriteLine("Book deleted successfully");
            }
            else
            {
                Console.WriteLine("Delete cancelled");
            }
        }
    }
}
