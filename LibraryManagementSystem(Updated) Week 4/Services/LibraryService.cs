using LibraryManagementSystem.Exceptions;
using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Services
{
    // This class contains ALL business logic related to library items
    // Program.cs will only call methods from this class
    public class LibraryService
    {
        // File where all library data is stored
        private readonly string filePath = "libraryData.txt";

        // Adds a new item to the file after checking duplicates
        public void AddItem(ILibraryItem newItem)
        {
            // Check if the same item already exists in file
            if (CheckForDuplicate(newItem))
                throw new DuplicateItemException("Duplicate item found.");

            // Append item data to file in CSV format
            File.AppendAllText(filePath, newItem.ToFileString() + Environment.NewLine);
        }

        // Reads all items from file and returns them as a list
        public List<ILibraryItem> GetAllItems()
        {
            List<ILibraryItem> items = new List<ILibraryItem>();

            // If file does not exist, return empty list
            if (!File.Exists(filePath))
                return items;

            // Read file line by line
            foreach (var line in File.ReadAllLines(filePath))
            {
                var parts = line.Split(',');
                items.Add(CreateItemFromFile(parts));
            }

            return items;
        }

        // Searches items by title (case-insensitive)
        public List<ILibraryItem> SearchByTitle(string title)
        {
            return GetAllItems()
                .Where(i => i.Title.ToLower().Contains(title.ToLower()))
                .ToList();
        }

        // Sort items by title
        public List<ILibraryItem> SortByTitle()
        {
            return GetAllItems()
                .OrderBy(i => i.Title)
                .ToList();
        }

        // Sort items by publication year
        public List<ILibraryItem> SortByYear()
        {
            return GetAllItems()
                .OrderBy(i => i.PublicationYear)
                .ToList();
        }

        // Checks all stored items to detect duplicates
        private bool CheckForDuplicate(ILibraryItem newItem)
        {
            if (!File.Exists(filePath))
                return false;

            foreach (var line in File.ReadAllLines(filePath))
            {
                var parts = line.Split(',');
                var existingItem = CreateItemFromFile(parts);

                if (IsDuplicate(existingItem, newItem))
                    return true;
            }
            return false;
        }

        // Compares two items to see if they are identical
        private bool IsDuplicate(ILibraryItem existingItem, ILibraryItem newItem)
        {
            // If types differ (Book vs Magazine), not duplicate
            if (existingItem.GetType() != newItem.GetType())
                return false;

            // Compare common properties
            if (existingItem.Title != newItem.Title ||
                existingItem.Publisher != newItem.Publisher ||
                existingItem.PublicationYear != newItem.PublicationYear)
                return false;

            // Compare type-specific properties
            if (existingItem is Book b1 && newItem is Book b2)
                return b1.Author == b2.Author;

            if (existingItem is Magazine m1 && newItem is Magazine m2)
                return m1.IssueNumber == m2.IssueNumber;

            if (existingItem is Newspaper n1 && newItem is Newspaper n2)
                return n1.Editor == n2.Editor;

            return false;
        }

        // Converts CSV line back into an object
        private ILibraryItem CreateItemFromFile(string[] parts)
        {
            return parts[0] switch
            {
                "Book" => new Book(parts[1], parts[2], int.Parse(parts[3]), parts[4]),
                "Magazine" => new Magazine(parts[1], parts[2], int.Parse(parts[3]), int.Parse(parts[4])),
                "Newspaper" => new Newspaper(parts[1], parts[2], int.Parse(parts[3]), parts[4]),
                _ => null
            };
        }
    }
}
