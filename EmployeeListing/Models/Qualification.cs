using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeListing.Models
{
    public class Qualification
    {
        [Key]
        public int QualificationId{get; set;}
        public string ? QualificationName{get; set;}
        public bool isActive{get; set;}
    }
}