using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeListing.Models
{
    [NotMapped]
    public class User
    {
        public string ?MailId { get; set; }
        public string ? Password { get; set; }
    }
}