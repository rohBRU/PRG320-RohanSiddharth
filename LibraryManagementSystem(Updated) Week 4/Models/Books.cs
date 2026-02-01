namespace LibraryManagementSystem.Models
{
    public class Book : LibraryItemBase
    {
        public string Author { get; set; }

        public Book(string title, string publisher, int year, string author)
            : base(title, publisher, year)
        {
            Author = author;
        }

        public override string ToFileString()
        {
            return $"Book,{Title},{Publisher},{PublicationYear},{Author}";
        }
    }
}
