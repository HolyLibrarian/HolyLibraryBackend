namespace HolyLibraryBackend.Dto
{
    public class CreateReaderDto
    {
        public string Account { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int MaxBorrowNumber { get; set; }
    }
}
