namespace HolyLibraryBackend.Models
{
    public class Reader : User
    {
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int MaxBorrowNumber { get; set; }

        public override string GetIdentification()
        {
            return "Reader";
        }

        public override bool BorrowCollection(Collection collection)
        {
            if (collection.IsBorrowed())
            {
                return false;
            }
            collection.Borrower = this;
            return true;
        }

        public override bool ReturnCollection(Collection collection)
        {
            if (collection.Borrower != this)
            {
                return false;
            }
            collection.Borrower = null;
            return true;
        }
    }
}
