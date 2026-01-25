using System;
using System.Collections.Generic;

// Namespace is used to group related classes
namespace LibraryManagementSystem
{
    // Custom exception class to handle invalid data
    public class InvalidItemDataException : Exception
    {
        // Constructor to pass custom error message
        public InvalidItemDataException(string message) : base(message)
        {
        }
    }

    // Base class representing a general library item
    public class Item
    {
        // Private variables (Encapsulation)
        private string title;
        private string publisher;
        private int publicationYear;

        // Public property for Title with validation
        public string Title
        {
            get { return title; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new InvalidItemDataException("Title cannot be empty.");
                title = value;
            }
        }

        // Public property for Publisher with validation
        public string Publisher
        {
            get { return publisher; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new InvalidItemDataException("Publisher cannot be empty.");
                publisher = value;
            }
        }

        // Public property for Publication Year with validation
        public int PublicationYear
        {
            get { return publicationYear; }
            set
            {
                if (value <= 0)
                    throw new InvalidItemDataException("Publication year must be valid.");
                publicationYear = value;
            }
        }

        // Constructor to initialize Item object
        public Item(string title, string publisher, int publicationYear)
        {
            Title = title;
            Publisher = publisher;
            PublicationYear = publicationYear;
        }

        // Virtual method to support polymorphism
        public virtual void DisplayInfo()
        {
            Console.WriteLine("Title: " + Title);
            Console.WriteLine("Publisher: " + Publisher);
            Console.WriteLine("Publication Year: " + PublicationYear);
        }
    }

    // Book class inherits from Item class (Inheritance)
    public class Book : Item
    {
        // Private variable for author name
        private string author;

        // Public property for Author with validation
        public string Author
        {
            get { return author; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new InvalidItemDataException("Author cannot be empty.");
                author = value;
            }
        }

        // Constructor for Book class
        public Book(string title, string publisher, int publicationYear, string author)
            : base(title, publisher, publicationYear) // Calling base class constructor
        {
            Author = author;
        }

        // Overriding DisplayInfo method (Polymorphism)
        public override void DisplayInfo()
        {
            base.DisplayInfo(); // Calls base class method
            Console.WriteLine("Author: " + Author);
            Console.WriteLine("Item Type: Book");
        }
    }

    // Magazine class inherits from Item class
    public class Magazine : Item
    {
        // Private variable for issue number
        private int issueNumber;

        // Public property for Issue Number with validation
        public int IssueNumber
        {
            get { return issueNumber; }
            set
            {
                if (value <= 0)
                    throw new InvalidItemDataException("Issue number must be greater than zero.");
                issueNumber = value;
            }
        }

        // Constructor for Magazine class
        public Magazine(string title, string publisher, int publicationYear, int issueNumber)
            : base(title, publisher, publicationYear)
        {
            IssueNumber = issueNumber;
        }

        // Overriding DisplayInfo method
        public override void DisplayInfo()
        {
            base.DisplayInfo();
            Console.WriteLine("Issue Number: " + IssueNumber);
            Console.WriteLine("Item Type: Magazine");
        }
    }

    // Library class to manage collection of items
    public class Library
    {
        // List to store Item objects
        private List<Item> items = new List<Item>();

        // Method to add item to library
        public void AddItem(Item item)
        {
            // Check for duplicate item based on title
            foreach (Item i in items)
            {
                if (i.Title == item.Title)
                    throw new InvalidItemDataException("Duplicate item cannot be added.");
            }

            // Add item if no duplicate found
            items.Add(item);
            Console.WriteLine("Item added successfully.\n");
        }

        // Method to display all library items
        public void DisplayAllItems()
        {
            foreach (Item item in items)
            {
                item.DisplayInfo(); // Polymorphic call
                Console.WriteLine("----------------------------");
            }
        }
    }

    // Main class where program execution starts
    class Program
    {
        static void Main(string[] args)
        {
            // Creating Library object
            Library library = new Library();

            try
            {
                // Creating Book object
                Book book1 = new Book(
                    "C# Programming",
                    "Tech Press",
                    2023,
                    "John Smith"
                );

                // Creating Magazine object
                Magazine magazine1 = new Magazine(
                    "Tech Monthly",
                    "Innovation Hub",
                    2024,
                    5
                );

                // Adding items to library
                library.AddItem(book1);
                library.AddItem(magazine1);

                // Displaying all library items
                library.DisplayAllItems();
            }
            catch (InvalidItemDataException ex)
            {
                // Handles custom exceptions
                Console.WriteLine("Error: " + ex.Message);
            }
            catch (Exception ex)
            {
                // Handles unexpected exceptions
                Console.WriteLine("Unexpected Error: " + ex.Message);
            }
            finally
            {
                // This block always executes
                Console.WriteLine("Program execution completed.");
            }
        }
    }
}
