namespace HolyLibraryBackend.Dto
{
    public class CreateCollecitonDto
    {
        public string Name { get; set; }
        public string Isbn { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
        public int Price { get; set; }
        public string Location { get; set; }
    }
}
