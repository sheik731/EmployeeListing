using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeListing.Models
{
    public class Subject 
    {
        [Key]
        public int SubjectId {get; set;}
        
        public String ? SubjectName {get; set;}
        public bool isActive {get; set;}
        // public ICollection<Employee>? Employees{get;set;}

    }
}