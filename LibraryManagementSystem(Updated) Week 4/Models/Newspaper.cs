namespace LibraryManagementSystem.Models
{
    public class Newspaper : LibraryItemBase
    {
        public string Editor { get; set; }

        public Newspaper(string title, string publisher, int year, string editor)
            : base(title, publisher, year)
        {
            Editor = editor;
        }

        public override string ToFileString()
        {
            return $"Newspaper,{Title},{Publisher},{PublicationYear},{Editor}";
        }
    }
}
