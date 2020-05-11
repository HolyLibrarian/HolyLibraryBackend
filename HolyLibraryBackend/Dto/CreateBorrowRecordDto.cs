namespace HolyLibraryBackend.Dto
{
    public class CreateBorrowRecordDto
    {
        public int UserId { get; set; }
        public int CollectionId { get; set; }
        public int ExpireDays { get; set; }
    }
}
