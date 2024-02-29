using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppServiceLayer.Models
{
    [Table("Employee")]
    public class EmpInfo
    {
        [Key]
        public int EmpId { get; set; }
        public string? EmailId { get; set; }
        public string? Name { get; set; }
        public DateTime DateOfJoining { get; set; }
        public int PassCode { get; set; }
    }
}
