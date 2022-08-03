using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeListing.Models
{
    public class Role
    {
        [Key]
        public int RoleId {get; set;}
        [StringLength(20)]
        public String ? RoleName {get; set;}
        public bool isActive {get; set;}
        // public ICollection<Employee>? Employees{get; set;}
    }
    
}