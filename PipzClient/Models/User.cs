using System.Collections.Generic;

namespace Pipz.Models
{
    public class User
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string JobTitle { get; set; }
        public string Phone { get; set; }
        public Company Company { get; set; }
        public string UserId { get; set; }
        public Dictionary<string, object> CustomFields { get; set; }
    }
}