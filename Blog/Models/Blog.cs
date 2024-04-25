using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blogs.Models
{
    public class Blog
    {
        [Key]
        public int Idblog { get; set; }
        public string Title { get; set; }
        public string Details { get;set; }
        public DateTime CreatedDate { get; set; }

        public int UserId { get; set; }
        /*[ForeignKey("UserId")]
        public User User { get; set; }*/

    }
}




