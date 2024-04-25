using System.Numerics;
using System.Xml.Linq;

namespace Blogs.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string? Img { get; set; }
        public string UserType { get; set; }
        public string Phone { get; set; }


    }
    
}
