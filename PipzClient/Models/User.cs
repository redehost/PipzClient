namespace Pipz.Models
{
    public class User
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string JobTitle { get; set; }
        public long Phone { get; set; }
        public Company Company { get; set; }
        public string UserId { get; set; }
    }
}
