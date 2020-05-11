namespace HolyLibraryBackend.Dto
{
    public class CreateUserDto
    {
        public string Account { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
