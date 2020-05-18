namespace HolyLibraryBackend.Models
{
    public class Manager : User
    {
        public override string GetIdentification()
        {
            return "Manager";
        }

        public override bool CreateBorrowRecord(User user, Collection collection)
        {
            return user.BorrowCollection(collection);
        }

        public override bool MarkBorrowRecordAsReturned(Collection collection)
        {
            if (!collection.IsBorrowed())
            {
                return false;
            }
            collection.Borrower = null;
            return true;
        }
    }
}
