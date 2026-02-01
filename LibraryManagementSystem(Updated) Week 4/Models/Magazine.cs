namespace LibraryManagementSystem.Models
{
    public class Magazine : LibraryItemBase
    {
        public int IssueNumber { get; set; }

        public Magazine(string title, string publisher, int year, int issue)
            : base(title, publisher, year)
        {
            IssueNumber = issue;
        }

        public override string ToFileString()
        {
            return $"Magazine,{Title},{Publisher},{PublicationYear},{IssueNumber}";
        }
    }
}