namespace HolyLibraryBackend.Models
{
    public class Reader : User
    {
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int MaxBorrowNumber { get; set; }
    }
}
