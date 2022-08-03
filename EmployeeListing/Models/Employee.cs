using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeListing.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId {get; set;}
        [Required]
        public string ? RegisterNumber{get; set;}
        public string ? EmployeeName {get; set;}
        public DateTime DateOfBirth {get; set;}
        [Required]
        [ForeignKey("EmpClass")]
        public int EmployeeClass {get; set;}
        [ForeignKey("Qualifications")]
        public int Qualification {get; set;}
        [ForeignKey("Role")]
        public int RoleId {get; set;}
        [ForeignKey("Gender")]
        public int GenderId {get; set;}
        [ForeignKey("Subject")]
        [Required]
        public int  SubjectId {get; set;}
        [Required]
        public string ? MailId { get; set;}
        [Required]
        public string ? Password {get; set;}
        public bool IsActive{get; set;} = true;
        public bool isAdminAccepted{get; set;}
        public virtual Subject? Subject{get;set;}
        public virtual Gender? gender {get; set;}
        public virtual Class? EmpClass {get; set;}
        public virtual Role ? Role{get; set;}
        public virtual Qualification ? Qualifications{get; set;}

    }
}