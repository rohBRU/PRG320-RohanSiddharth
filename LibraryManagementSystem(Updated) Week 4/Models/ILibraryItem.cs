namespace LibraryManagementSystem.Models
{
    public interface ILibraryItem
    {
        string Title { get; set; }
        string Publisher { get; set; }
        int PublicationYear { get; set; }

        void DisplayInfo();
        string ToFileString();
    }
}
