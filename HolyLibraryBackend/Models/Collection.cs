namespace HolyLibraryBackend.Models
{
    public class Collection
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Isbn { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
        public int Price { get; set; }
        public string Location { get; set; }
        public User Borrower { get; set; }

        public bool DeleteFlag { get; set; }

        public bool IsBorrowed()
        {
            return Borrower != null;
        }
    }
}
