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

        public Books(string bookName, string author, string genre)
        {
            BookName = bookName;
            Author = author;
            Genre = genre;
        }

        static List<Books> bookList = new List<Books>
            {
                new Books("The Alchemist", "Paulo Coelho", "Fiction"),
                new Books("Pride and Prejudice", "Jane Austen", "Romance"),
                new Books("Harry Potter and the Philosopher's Stone", "J.K. Rowling", "Fantasy"),
                new Books("The Hobbit", "J.R.R. Tolkien", "Fantasy"),
                new Books("To Kill a Mockingbird", "Harper Lee", "Fiction"),
                new Books("The Great Gatsby", "F. Scott Fitzgerald", "Fiction"),
                new Books("The Catcher in the Rye", "J.D. Salinger", "Fiction"),
                new Books("The Lord of the Rings", "J.R.R. Tolkien", "Fantasy"),
                new Books("Mockingjay", "Suzanne Collins", "Science Fiction"),
                new Books("The Hunger Games", "Suzanne Collins", "Science Fiction")
            };

        static string FilePath = "Books.csv";
        public static void WriteBooksToFile()
        {
            using (StreamWriter Writer = new StreamWriter(FilePath))
            {
                Writer.WriteLine("BookName,Author,Genre");
                foreach (Books book in bookList)
                {
                    Writer.WriteLine($"{book.BookName},{book.Author},{book.Genre}");
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

            Books book = new Books(bookName, author, genre);
            bookList.Add(book);
            using (StreamWriter Writer = new StreamWriter(FilePath, true))
            {
                Writer.WriteLine($"{bookName}, by {author}, Genre: {genre}");
            }
        }
    }
}
