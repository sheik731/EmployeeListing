using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeListing.Models
{
    public class Class
    {
        [Key]
        public int ClassId{get; set;}
        [Range(1,12)]
        public int className{get; set;}
        public bool isActive {get; set;}
        // public ICollection<Employee>? Employees {get; set;}
    }
}