using System;
using System.Collections.Generic;

namespace MyLibraryApp
{
    public class Program
    {
        // Initialize the central data store
        public static Dictionary<int, Book> libraryBooks = new();
        // book indexer
        public static int currentId = 0;
        public static void Main(string[] args)
        {
            // Add a sample books for testing
            Book newBook = new()
            {
                Id = GetNextId(),
                Title = "Book 1",
                Author = "Author 1"
            };
            libraryBooks.Add(newBook.Id, newBook);
            Book newBook2 = new()
            {
                Id = GetNextId(),
                Title = "Book 2",
                Author = "Author 2"
            };
            libraryBooks.Add(newBook2.Id, newBook2);

            // Start the main loop
            RunMenu();
        }

        /// <summary>
        /// Generates and returns the next unique book ID in a sequential order.
        /// </summary>
        public static int GetNextId()
        {
            currentId++;
            return currentId;
        }

        /// <summary>
        /// Main menu logic
        /// </summary>
        public static void RunMenu()
        {
            bool isRunning = true;
            /// main loop until false to exit application
            while (isRunning)
            {
                Console.WriteLine("1. Add a new book");
                Console.WriteLine("2. Update an existing book");
                Console.WriteLine("3. Delete a book");
                Console.WriteLine("4. List all books");
                Console.WriteLine("5. View details of a specific book");
                Console.WriteLine("6. Exit Application");
                Console.Write("Enter 1 to 6: ");

                string? choice = Console.ReadLine();
                Console.Clear();
                Console.WriteLine("~~~");
                switch (choice)
                {
                    case "1":
                        HandleAdd();
                        break;
                    case "2":
                        HandleUpdate();
                        break;
                    case "3":
                        HandleDelete();
                        break;
                    case "4":
                        HandleList();
                        break;
                    case "5":
                        HandleView();
                        break;
                    case "6":
                        isRunning = false;
                        Console.WriteLine("Exiting Library Application. Goodbye!");
                        break;
                    default:
                        Console.WriteLine("Invalid. Enter 1 to 6.");
                        break;
                }
                Console.WriteLine("~~~\n");
            }
        }
        /// <summary>
        /// add new book. limited book count to 100 for testing purpose
        /// </summary>
        public static void HandleAdd()
        {
            Console.WriteLine("Add Book:");
            if (libraryBooks.Count >= 100)
            {
                Console.WriteLine("Library is full. Cannot add more books.");
                return;
            }
            else
            {
                /// get book title and author from user
                Console.Write("Enter Book Title: ");
                string title = Console.ReadLine() ?? string.Empty;
                Console.Write("Enter Book Author: ");
                string author = Console.ReadLine() ?? string.Empty;
                /// validate input. should not have empty title/author. return if invalid
                if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(author))
                {
                    Console.WriteLine("Title and/or Author cannot be empty.");
                    return;
                }
                else
                {
                    /// create new book and add to library
                    title = title.Trim();
                    author = author.Trim();
                    Book newBook = new()
                    {
                        Id = GetNextId(), /// assign unique id
                        Title = title,
                        Author = author
                    };
                    /// add book to library
                    libraryBooks.Add(newBook.Id, newBook);
                    /// confirm addition
                    Console.WriteLine($"Book '{newBook.Title}' by {newBook.Author} added with ID {newBook.Id}.");
                }

            }
        }

        /// <summary>
        /// update existing book. use id to find book
        /// </summary>
        public static void HandleUpdate()
        {
            Console.WriteLine("Update Book:");
            /// check if library has books
            if (libraryBooks.Count > 0)
            {
                /// get book id from user
                Console.Write("Enter Book ID to update: ");
                if (int.TryParse(Console.ReadLine(), out int bookId) && libraryBooks.ContainsKey(bookId))
                {
                    /// book found, get new title and author
                    var book = libraryBooks[bookId];
                    Console.Write($"Enter new title (current: {book.Title}): ");
                    string newTitle = Console.ReadLine() ?? string.Empty;
                    Console.Write($"Enter new author (current: {book.Author}): ");
                    string newAuthor = Console.ReadLine() ?? string.Empty;
                    /// validate input. should not have empty title/author. return if invalid
                    if (!string.IsNullOrWhiteSpace(newTitle) && !string.IsNullOrWhiteSpace(newAuthor))
                    {
                        book.Title = newTitle.Trim();
                        book.Author = newAuthor.Trim();
                        Console.WriteLine($"Book with ID \"{bookId}\" updated.");
                    }
                    else
                    {
                        Console.WriteLine("Title and/or Author cannot be empty. aborted.");
                        return;
                    }
                }
                else
                {
                    Console.WriteLine("Book not found.");
                }
            }
            else
            {
                Console.WriteLine("No books available in the library.");
            }
        }

        /// <summary>
        /// delete existing book
        /// </summary>
        public static void HandleDelete()
        {
            Console.WriteLine("Delete Book:");
            /// check if library has books
            if (libraryBooks.Count > 0)             {
                Console.Write("Enter Book ID to delete: ");
                /// get book id from user and delete if found
                if (int.TryParse(Console.ReadLine(), out int bookId) && libraryBooks.ContainsKey(bookId))
                {
                    // Get book title before deletion
                    var book = libraryBooks[bookId];
                    var booktitle = book.Title;
                    // Delete the book
                    libraryBooks.Remove(bookId);
                    Console.WriteLine($"Book with ID \"{bookId}\", title \"{booktitle}\" deleted.");
                }
                else
                {
                    Console.WriteLine("Book not found.");
                }
            }
            else
            {
                Console.WriteLine("No books available in the library.");
            }

        }

        /// <summary>
        /// list all books
        /// </summary>
        public static void HandleList()
        {
            Console.WriteLine("List of Books:");
            if (libraryBooks.Count > 0)
            {
                foreach (var book in libraryBooks.Values)
                {
                    Console.WriteLine($"ID: {book.Id}, Title: {book.Title}, Author: {book.Author}");
                }
            }
            else
            {
                Console.WriteLine("No books available in the library.");
            }
        }
        /// <summary>
        /// view book details
        /// </summary>
        public static void HandleView()
        {
            Console.WriteLine("View Book Details:");
            /// check if library has books
            if (libraryBooks.Count > 0)
            {
                /// get book id from user
                Console.Write("Enter Book ID for details: ");
                /// display book details if found
                if (int.TryParse(Console.ReadLine(), out int bookId) && libraryBooks.ContainsKey(bookId))
                {
                    /// book found, display details via book id
                    var book = libraryBooks[bookId];
                    Console.WriteLine($"ID: {book.Id}");
                    Console.WriteLine($"Title: {book.Title}");
                    Console.WriteLine($"Author: {book.Author}");
                }
                else
                {
                    Console.WriteLine("Book not found.");
                }
            }
            else
            {
                Console.WriteLine("No books available in the library.");
            }
        }
    }

    /// <summary>
    /// book class: identifier, title, and author.
    /// </summary>
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
    }
}