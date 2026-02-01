// Importing the namespace where custom exceptions are defined
using LibraryManagementSystem.Exceptions;

// Defining the namespace for model classes
namespace LibraryManagementSystem.Models
{
    // This is an abstract class, so objects of this class cannot be created directly
    // It serves as a base class for Book, Magazine, and Newspaper
    public abstract class LibraryItemBase : ILibraryItem
    {
        // Private backing field for Title
        private string title;

        // Private backing field for Publisher
        private string publisher;

        // Private backing field for PublicationYear
        private int publicationYear;

        // Public property for Title with validation
        public string Title
        {
            get => title; // Returns the value of title
            set
            {
                // Validation to ensure title is not empty or null
                if (string.IsNullOrWhiteSpace(value))
                    throw new InvalidItemDataException("Title cannot be empty");

                // Assigning value if validation passes
                title = value;
            }
        }

        // Public property for Publisher with validation
        public string Publisher
        {
            get => publisher; // Returns publisher value
            set
            {
                // Publisher must not be empty
                if (string.IsNullOrWhiteSpace(value))
                    throw new InvalidItemDataException("Publisher cannot be empty");

                publisher = value;
            }
        }

        // Public property for PublicationYear with validation
        public int PublicationYear
        {
            get => publicationYear; // Returns publication year
            set
            {
                // Publication year must be a positive number
                if (value <= 0)
                    throw new InvalidItemDataException("Invalid publication year");

                publicationYear = value;
            }
        }

        // Constructor used by derived classes (Book, Magazine, Newspaper)
        // It ensures all common properties are initialized with validation
        protected LibraryItemBase(string title, string publisher, int year)
        {
            Title = title;               // Calls Title setter with validation
            Publisher = publisher;       // Calls Publisher setter with validation
            PublicationYear = year;      // Calls PublicationYear setter with validation
        }

        // Virtual method to display common information of library items
        // Derived classes can override this if needed
        public virtual void DisplayInfo()
        {
            Console.WriteLine($"Title: {Title}");
            Console.WriteLine($"Publisher: {Publisher}");
            Console.WriteLine($"Year: {PublicationYear}");
        }

        // Abstract method that forces derived classes
        // to define how their data is saved to a file
        public abstract string ToFileString();
    }
}
