using Microsoft.AspNetCore.Identity;

namespace victors.Models
{
    public class User:IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public  string Function { get;set; }
    }
}
