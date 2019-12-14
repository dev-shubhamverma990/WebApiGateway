namespace WebApi.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string NewPassword { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }
        public string Logo { get; set; }
        public string SchoolName { get; set; }
        public string Email { get; set; }
        public string Class { get; set; }
        public string FatherMobile { get; set; }
    }
}