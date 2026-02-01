// Importing model classes such as Book, Magazine, Newspaper
using LibraryManagementSystem.Models;

// Importing service class that contains business logic
using LibraryManagementSystem.Services;

// Main program class
class Program
{
    // Entry point of the application
    static void Main()
    {
        // Creating an object of LibraryService
        // This object will handle all operations like add, search, sort, etc.
        LibraryService service = new LibraryService();

        // Infinite loop so that menu keeps appearing
        // Program will exit only when user chooses Exit option
        while (true)
        {
            // Displaying menu options to the user
            Console.WriteLine("\n--- Library Management System ---");
            Console.WriteLine("1. Add Book");
            Console.WriteLine("2. Add Magazine");
            Console.WriteLine("3. Add Newspaper");
            Console.WriteLine("4. View All Items");
            Console.WriteLine("5. Search by Title");
            Console.WriteLine("6. Sort by Title");
            Console.WriteLine("7. Sort by Publication Year");
            Console.WriteLine("8. Exit");

            // Asking user to choose an option
            Console.Write("Choose an option: ");

            // Reading user input and converting it to integer
            int choice = int.Parse(Console.ReadLine());

            try
            {
                // Switch statement to execute selected menu option
                switch (choice)
                {
                    case 1:
                        // Calls method to add a new book
                        AddBook(service);
                        break;

                    case 2:
                        // Calls method to add a new magazine
                        AddMagazine(service);
                        break;

                    case 3:
                        // Calls method to add a new newspaper
                        AddNewspaper(service);
                        break;

                    case 4:
                        // Displays all library items
                        DisplayItems(service.GetAllItems());
                        break;

                    case 5:
                        // Takes title input and searches matching items
                        Console.Write("Enter title keyword: ");
                        DisplayItems(service.SearchByTitle(Console.ReadLine()));
                        break;

                    case 6:
                        // Displays items sorted by title
                        DisplayItems(service.SortByTitle());
                        break;

                    case 7:
                        // Displays items sorted by publication year
                        DisplayItems(service.SortByYear());
                        break;

                    case 8:
                        // Exits the program safely
                        Console.WriteLine("Exiting program...");
                        return;

                    default:
                        // Handles invalid menu choice
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
            catch (Exception ex)
            {
                // Catches all runtime errors such as:
                // - Invalid input
                // - Duplicate item
                // - Validation errors
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }

    // Method to add a new Book
    static void AddBook(LibraryService service)
    {
        // Taking book details from user
        Console.Write("Title: ");
        string title = Console.ReadLine();

        Console.Write("Publisher: ");
        string publisher = Console.ReadLine();

        Console.Write("Publication Year: ");
        int year = int.Parse(Console.ReadLine());

        Console.Write("Author: ");
        string author = Console.ReadLine();

        // Creating Book object and passing it to service layer
        service.AddItem(new Book(title, publisher, year, author));

        // Confirmation message
        Console.WriteLine("Book added successfully.");
    }

    // Method to add a new Magazine
    static void AddMagazine(LibraryService service)
    {
        Console.Write("Title: ");
        string title = Console.ReadLine();

        Console.Write("Publisher: ");
        string publisher = Console.ReadLine();

        Console.Write("Publication Year: ");
        int year = int.Parse(Console.ReadLine());

        Console.Write("Issue Number: ");
        int issue = int.Parse(Console.ReadLine());

        // Creating Magazine object
        service.AddItem(new Magazine(title, publisher, year, issue));

        Console.WriteLine("Magazine added successfully.");
    }

    // Method to add a new Newspaper
    static void AddNewspaper(LibraryService service)
    {
        Console.Write("Title: ");
        string title = Console.ReadLine();

        Console.Write("Publisher: ");
        string publisher = Console.ReadLine();

        Console.Write("Publication Year: ");
        int year = int.Parse(Console.ReadLine());

        Console.Write("Editor: ");
        string editor = Console.ReadLine();

        // Creating Newspaper object
        service.AddItem(new Newspaper(title, publisher, year, editor));

        Console.WriteLine("Newspaper added successfully.");
    }

    // Method to display list of library items
    static void DisplayItems(List<ILibraryItem> items)
    {
        // If no items are found, display message
        if (items.Count == 0)
        {
            Console.WriteLine("No items found.");
            return;
        }

        // Loop through each item and display its details
        foreach (var item in items)
        {
            item.DisplayInfo();   // Polymorphic call
            Console.WriteLine("---------------------------");
        }
    }
}
