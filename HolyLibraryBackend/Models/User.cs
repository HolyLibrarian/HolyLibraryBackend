namespace HolyLibraryBackend.Models
{
    public abstract class User
    {
        public int Id { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }

        public abstract string GetIdentification();

        public virtual bool BorrowCollection(Collection collection)
        {
            return false;
        }

        public virtual bool ReturnCollection(Collection collection)
        {
            return false;
        }
    }
}
