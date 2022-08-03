using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using EmployeeListing.Models;

namespace EmployeeListing.validation
{
    public static class Employeevalidation
    { 
    public static void isEmployeeValid(Employee employee)
    {
        if (employee == null)throw new ValidationException("Employee object can't be null");

        if(string.IsNullOrWhiteSpace(employee.EmployeeName) || string.IsNullOrEmpty(employee.EmployeeName))throw new ValidationException("Employee name can't be null or blank space");

        else if(!Regex.IsMatch(employee.EmployeeName, @"^(?!.*([ ])\1)(?!.*([A-Za-z])\2{2})\w[a-zA-Z\s]*$"))throw new ValidationException("Name can't contain Symbols or Number");

        if(string.IsNullOrWhiteSpace(employee.RegisterNumber))throw new ValidationException("Register number cannot be null or blank space");
        
        else if (employee.RegisterNumber == "PSNA0000" )throw new ValidationException("Register number cannot be 0");

        else if (!Regex.IsMatch(employee.RegisterNumber, @"^PSNA[0-9]{4,5}$"))throw new ValidationException ("Register number must be of PSNA domain");

        if (employee.DateOfBirth >= System.DateTime.Now) throw new ValidationException("Your age must be above 21");

        else if ((System.DateTime.Now.Year - employee.DateOfBirth.Year) >= 60 || (System.DateTime.Now.Year - employee.DateOfBirth.Year)<= 22)throw new ValidationException ("You are not meeting the age limits");

        if (String.IsNullOrEmpty(employee.MailId)) throw new ValidationException("EmailId cannot be null");

        else if (!Regex.IsMatch(employee.MailId,@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z")) throw new ValidationException ("Please enter valid email Id");

        if (String.IsNullOrEmpty(employee.Password)) throw new ValidationException("Password cannot be null"); 

        else if (!Regex.IsMatch(employee.Password,@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$")) throw new ValidationException ("Password must be between 8 and 15 characters and atleast contain one uppercase letter, one lowercase letter, one digit and one special character."); 
    }

    public static void IsClassValid(int ClassId)
        {
            if (ClassId <= 0 || ClassId >= 13) throw new ValidationException("Class with the given Id is Not found");
        }

    public static void isSubjectValid(Subject subject)
    {
        if(subject==null && string.IsNullOrEmpty(subject!.SubjectName) && string.IsNullOrWhiteSpace(subject!.SubjectName))throw new ValidationException("Subject can't be null");
        var res = Regex.IsMatch(subject!.SubjectName , @"^(?!.*([ ])\1)(?!.*([A-Za-z])\2{2})\w[a-zA-Z\s]{3,15}$");
        if(!res)
        {

            throw new ValidationException("subject Name must be alphabets and of lenght of 3 to 15.");
        }
    }
 }
}