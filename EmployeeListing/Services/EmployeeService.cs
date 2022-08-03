using System.ComponentModel.DataAnnotations;
using EmployeeListing.Models;
using EmployeeListing.DataAccessLayer;
using EmployeeListing.validation;
using Microsoft.EntityFrameworkCore;

namespace EmployeeListing.Services
{
    public class EmployeeService
    {
        private EmployeeDataAccessLayer _employeeDataAccessLayer;
        private ILogger _logger;

        public EmployeeService (EmployeeDataAccessLayer employeeDataAccessLayer, ILogger<EmployeeService> logger)
        {
            _employeeDataAccessLayer= employeeDataAccessLayer;
            _logger = logger;
        }
    public bool RegisterNewEmployee(Employee employee)
    {
        Employeevalidation.isEmployeeValid(employee);
        try 
        {
            return _employeeDataAccessLayer.RegisterNewEmployee(employee)?true : false;
        }
        catch(ValidationException invalidEmployee)
        {
            _logger.LogError($"Employee DAL : RegisterNewEmployee(Employee employee) : {invalidEmployee.Message}");
            throw invalidEmployee;
        }
        catch(Exception exception)
        {   
            _logger.LogError($"Employee DAL : RegisterNewEmployee(Employee employee) : {exception.Message}");
            throw;
        }
    }
    public object ViewEmployeeByClass(int ClassId)
    {
        Employeevalidation.IsClassValid(ClassId);

        try
        {
            List<object> employees = new List<object>();
            var employee = _employeeDataAccessLayer.ViewEmployeeByClass(ClassId)
            .Select (e => new
            {
                TeacherRegisterNumber = e.RegisterNumber,
                TeacherName = e.EmployeeName,
                TeacherClass = e.EmpClass!.className,
                TeacherQualification = e.Qualifications!.QualificationName,
                TeacherDateOfBirth = e.DateOfBirth.ToShortDateString(),
                TeacherSubject = e.Subject!.SubjectName,
                TeacherMailID = e.MailId,
            }
            );
            foreach (var item in employee)
            {
                employees.Add(item);
            }
            return employees;
        }
        catch (Exception exception)
            {
                _logger.LogError($"Employee service : ViewEmployeesByClass(int classId) : Exception occured in DAL :{exception.Message}");
                throw new Exception();
            }
    }

        public bool RemoveEmployee(int EmployeeId)
        {
            if(EmployeeId <= 0)throw new ValidationException("Enter a valid EmployeeId");
            try
            {
                return _employeeDataAccessLayer.RemoveEmployee(EmployeeId) ? true : false;
            }
            catch (ArgumentException exception)
            {
                _logger.LogError($"Employee service : RemoveEmployee(int employeeId) : {exception.Message}");
                throw exception;
            }
            catch (ValidationException employeeNotFound)
            {
                _logger.LogError($"Employee service : CreateEmployee(Employee employee) : {employeeNotFound.Message}");
                throw employeeNotFound;
            }
            catch (Exception exception)
            {
                _logger.LogError($"Employee service : RemoveEmployee(int employeeId) : {exception.Message}");
                throw ;
            }
        }

        public object ViewEmployeeRequest()
        {
            try
            {
            return _employeeDataAccessLayer.ViewEmployeeRequest()
            .Select(
                e => new{
                    employeeRegisterNumber = e.RegisterNumber,
                    employeeName = e.EmployeeName,
                    employeeClass = e.EmpClass!.className,
                    employeeQualification = e.Qualifications!.QualificationName,
                    employeeSubject = e.Subject!.SubjectName,
                    employeeMailID = e.MailId,
                }
            );
            }
            catch (Exception exception)
            {
                _logger.LogError($"Employee service : ViewTACRequest : Exception occured in DAL :{exception.Message}");
                throw new Exception();
            }
        }

        public bool RespondEmployeeRequest(int employeeId, bool Response)
        {
            if(employeeId <= 0)throw new ValidationException("Enter a valid EmployeeId");
            try
            {
                return _employeeDataAccessLayer.RespondEmployeeRequest(employeeId, Response);
            }
            catch (ValidationException exception)
            {
                _logger.LogError($"Employee service:RespondEmployeeRequest(int employeeId,bool response) : {exception.Message}");
                throw exception;
            }
            catch (Exception exception)
            {
                _logger.LogError($"Employee DAL : CheckLoginCredentails throwed an exception : {exception.Message}");
                throw ;
            }
        }

        public Object ViewEmployees()
        {
            try
            {
                return  _employeeDataAccessLayer.GetEmployeesFromDatabase().
                Select(
                    e => new{
                         TeacherRegisterNumber = e.RegisterNumber,
                         TeacherName = e.EmployeeName,
                         TeacherClass = e.EmpClass!.className,
                         TeacherQualification = e.Qualifications!.QualificationName,
                         TeacherDateOfBirth = e.DateOfBirth.ToShortDateString(),
                         TeacherSubject = e.Subject!.SubjectName,
                         TeacherMailID = e.MailId,
                    }
                );
            }
            catch (Exception exception)
            {   
                _logger.LogError($"Employee service : RemoveEmployee(int employeeId) : Exception occured in DAL :{exception.Message}");
                throw new Exception();
            }
        }

        public bool CreateNewSubject(Subject subject)

        {
            Employeevalidation.isSubjectValid(subject);
            try
            {
                return _employeeDataAccessLayer.CreateNewSubject(subject) ? true : false; // LOG Error in DAL;
            }
            catch (ArgumentException exception)
            {
                _logger.LogError($"Employee service : CreateSubject(string  locationName) : {exception.Message}");
                return false;
            }
            catch (ValidationException exception)
            {
             _logger.LogError($"Employee service : CreateSubject(string  locationName) : {exception.Message}");
              throw exception;
            }
            catch (Exception exception)
            {
                _logger.LogError($"Employee service : CreateSubject(string locationName) : {exception.Message}");
                return false;
            }
        }

        public bool RemoveSubject(Subject subject)
        {
            Employeevalidation.isSubjectValid(subject);
            try
            {
                return _employeeDataAccessLayer.RemoveSubject(subject) ? true : false; // LOG Error in DAL;
            }
           catch (ArgumentException exception)
            {
                _logger.LogError($"Employee service : RemoveEmployee(int SubjectId) : {exception.Message}");
                return false;
            }
            catch (ValidationException SubjectNotFound)
            {
                _logger.LogError($"Employee service : RemoveEmployee(int SubjectId) : {SubjectNotFound.Message}");
                throw SubjectNotFound;
            }
            catch (Exception exception)
            {
                _logger.LogError($"Employee service : RemoveEmployee(int SubjectId) :{exception.Message}");
                return false;
            }
        }

        public IEnumerable<Subject> ViewSubject()
        {
            try
            {
                IEnumerable<Subject> subjects = new List<Subject>();
                return subjects = _employeeDataAccessLayer.ViewSubject() ;
            }
            catch (Exception exception)
            {
                _logger.LogError($"Location service:ViewLocations(): {exception.Message}");
                throw new Exception();
            }

        }

    }
}