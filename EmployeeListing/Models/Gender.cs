using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeListing.Models
{
    public class Gender
    {
        [Key]
        public int GenderId{get; set;}
        [StringLength(10)]
        public string ? GenderName {get; set;}
        public bool isActive {get; set;}
        // public ICollection<Employee>? Employees{get; set;}

    }
}