using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppServiceLayer.Models
{
    [Table("Blogs")]
    public class BlogInfo
    {
        [Key]
        public int BlogId { get; set; }
        public string? Title { get; set; }
        public string? Subject { get; set; }
        public DateTime DateOfCreation { get; set; }
        public string? BlogUrl { get; set; }
        public string? EmpEmailId { get; set; }
    }
}
