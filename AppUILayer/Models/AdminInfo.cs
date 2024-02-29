using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogTracker.Models
{
    [Table("Admin")]
    public class AdminInfo
    {
        [Key]
        public int AdminId { get; set; }
        public string? EmailId { get; set; }
        public string? Password { get; set; }
    }
}
