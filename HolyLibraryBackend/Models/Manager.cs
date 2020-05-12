namespace HolyLibraryBackend.Models
{
    public class Manager : User
    {
        public override string GetIdentification()
        {
            return "Manager";
        }
    }
}
