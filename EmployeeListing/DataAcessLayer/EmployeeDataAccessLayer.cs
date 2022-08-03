using System.ComponentModel.DataAnnotations;
using EmployeeListing.Models;
using EmployeeListing.validation;
using Microsoft.EntityFrameworkCore;

namespace EmployeeListing.DataAccessLayer
{
    public class EmployeeDataAccessLayer
    {
        private EmployeeListingDBcontext _db;
        private ILogger _logger;

        public EmployeeDataAccessLayer(EmployeeListingDBcontext dbContext, ILogger<EmployeeDataAccessLayer> logger)
        {
            _db = dbContext;
            _logger = logger;
        }

        public bool RegisterNewEmployee(Employee employee)
        {
            Employeevalidation.isEmployeeValid(employee);
            if(_db.Employees!.Any(e => e.RegisterNumber == employee.RegisterNumber && e.IsActive == true && e.isAdminAccepted == true))throw new ValidationException("Register Number already exist");
            if(_db.Employees!.Any(e => e.MailId == employee.MailId && e.IsActive == true && e.isAdminAccepted == true))throw new ValidationException("Mail Id already exist");
            try
            {
                _db.Employees!.Add(employee);
                _db.SaveChanges();
                return true;
            }
            catch(Exception exception)
            {
                _logger.LogError($"Database : RegisterNewEmployee(Employee employee) : {exception.Message}");
                return false;
            }
        }

        public List<Employee>ViewEmployeeByClass(int ClassId)
        {
            try
            {
                var ViewEmployeesByClass = _db!.Employees.Where(x=>x.EmployeeClass == ClassId && x.IsActive == true).Include(c => c.EmpClass).Include(q => q.Qualifications).Include(s => s.Subject).ToList();
                return ViewEmployeesByClass;
            }
            catch (Exception IsClassValid)
            {
                _logger.LogError($"Exception on Employee DAL : IsClassValid(int departmnet) : {IsClassValid.Message} : {IsClassValid.StackTrace}");
                throw;
            }

        }

        public bool RemoveEmployee(int EmployeeId)
        {
            if(EmployeeId <= 0)throw new ValidationException("Enter a valid EmployeeId");
            try
            {
            var employee = _db.Employees!.Find(EmployeeId);
            if(employee == null)throw new ValidationException("No employee found with the given Employee ID");
            if(employee.IsActive == false)throw new ValidationException("There is no exixting employee for the Employee ID");
            else
            {
                employee.IsActive = false;
                _db.Employees.Update(employee);
                _db.SaveChanges();
                return true;
            }
            }
            catch (DbUpdateException exception)
            {
                _logger.LogError($"Employee DAL : RemoveEmployeeFromDatabase(int employeeId) : {exception.Message}");
                return false;
            }
            catch (OperationCanceledException exception)
            {
                _logger.LogError($"Employee DAL : RemoveEmployeeFromDatabase(int employeeId) : {exception.Message}");
                return false;
            }
            catch (ValidationException exception)
            {
                _logger.LogError($"Employee DAL : RemoveEmployeeFromDatabase(int employeeId) : {exception.Message}");
            
                throw exception;
            }
            catch (Exception exception)
            {
                _logger.LogError($"Employee DAL : RemoveEmployeeFromDatabase(int employeeId) : {exception.Message}");
                return false;
            }
        }

        public List<Employee> ViewEmployeeRequest()
        {
            try
            {
                return (from employee in _db.Employees!.Where(e => e.isAdminAccepted == false && e.IsActive == true).Include(c => c.EmpClass).Include(q => q.Qualifications).Include(s => s.Subject) select employee).ToList();
            }
                 catch (DbUpdateException exception)
            {
                _logger.LogError($"Employee DAL : GetEmployeesFromDatabase() : {exception.Message}");
                throw new DbUpdateException();
            }
            catch (OperationCanceledException exception)
            {
                _logger.LogError($"Employee DAL : GetEmployeesFromDatabase() : {exception.Message}");
                throw new OperationCanceledException();
            }
            catch (Exception exception)
            {
                _logger.LogError($"Employee DAL : GetEmployeesFromDatabase() : {exception.Message}");
                throw new Exception();
            }
        }

        public bool RespondEmployeeRequest(int employeeId, bool Response)
        {
            if(employeeId <= 0)throw new ValidationException("Enter a valid EmployeeId");
            try
            {
                if(!_db.Employees!.Any(e=>e.EmployeeId == employeeId))throw new ValidationException("No employee found with given employee Id");

                var employee = _db.Employees!.Find(employeeId);
                employee!.isAdminAccepted =Response;
                _db.Employees.Update(employee);
                _db.SaveChanges();
                return true;
            }
            catch (ValidationException exception)
            {
                _logger.LogError($"Employee DAL:RespondEmployeeRequest(int employee,bool response):{exception.Message}");
                throw exception;
            }
            catch(Exception exception)
            {
                _logger.LogError($"Employee DAL:ResponsEmployeeRequest(int emloyeeId,bool response):{exception.Message}");
                return false;
            }
        }
        
        public List<Employee> GetEmployeesFromDatabase()
        {
            try
            { var res =(from employee in _db.Employees!.Where(e => e.isAdminAccepted == true && e.IsActive == true).Include(c => c.EmpClass).Include(q => q.Qualifications).Include(s => s.Subject) select employee).ToList();
                 return res;
            }
               catch (DbUpdateException exception)
            {
                _logger.LogError($"Employee DAL : GetEmployeesFromDatabase() : {exception.Message}");
                throw new DbUpdateException();
            }
            catch (OperationCanceledException exception)
            {
                _logger.LogError($"Employee DAL : GetEmployeesFromDatabase() : {exception.Message}");
                throw new OperationCanceledException();
            }
            catch (Exception exception)
            {
                _logger.LogError($"Employee DAL : GetEmployeesFromDatabase() : {exception.Message}");
                throw new Exception();
            }
         
        }

        public Employee CheckLoginCrendentials(string employeeMail, string password)
        {
            try
            {
                if(!_db.Employees!.Any(x => x.MailId == employeeMail))
                    throw new ValidationException($"Invalid credentials");

                if(!_db.Employees!.Any(x => x.MailId == employeeMail && x.Password == password))
                    throw new ValidationException($"Invalid credentials");

                if(!_db.Employees!.Any(x => x.MailId == employeeMail && x.Password == password && x.IsActive==true))
                    throw new ValidationException($"Your account has been deactivated! Please Contact Adminstrator.");
                if(!_db.Employees!.Any(x =>x.MailId == employeeMail && x.Password == password && x.isAdminAccepted==true))
                    throw new ValidationException($"Wait untill you receive a mail!");
                var _employee = GetEmployeesFromDatabase().Where(employee => employee.MailId == employeeMail).FirstOrDefault();
                return _employee;
            }
            catch (Exception exception)
            {
                _logger.LogError($"Exception on Employee DAL : CheckLoginCrendentials(string employeeAceNumber, string password) : {exception.Message}");
                throw;
            }
        }
        public bool CreateNewSubject (Subject subject)
        {
            Employeevalidation.isSubjectValid(subject);
            try
            {
                bool SubjectnameAlreadyExists = _db.Subjects!.Any(x => x.SubjectName == subject.SubjectName && x.isActive == subject.isActive);
                if(!SubjectnameAlreadyExists)
                {
                    _db.Subjects!.Add(subject);
                    _db.SaveChanges();
                    return true;
                }
                else
                {
                    throw new ValidationException("Subject name already exist");
                }
            }
            catch (DbUpdateException exception)
            {
                _logger.LogError($"Employee DAL : CreateNewSubject(Subject subject) : {exception.Message}");
                return false;
            }
            catch (OperationCanceledException exception)
            {
                _logger.LogError($"Employee DAL : CreateNewSubject(Subject subject) : {exception.Message}");
                return false;
            }
            catch (ValidationException exception)
            {
                throw exception;
            }
            catch (Exception exception)
            {
                _logger.LogError($"Employee DAL : CreateNewSubject(Subject subject)) : {exception.Message}");
                return false;
            }
        }

        public bool RemoveSubject(Subject subject)
        {
            Employeevalidation.isSubjectValid(subject);
            bool isSubjectValid = _db.Subjects!.Any(x => x.SubjectId == subject.SubjectId && x.isActive == subject.isActive);
            if (isSubjectValid)
            {
                throw new ValidationException("Subject already deleted");
            }

            try
            {
                var subjects = _db.Subjects!.Find(subject.SubjectId);
                if (subjects == null)
                    throw new ValidationException("No location is found with given Location Id");

                subjects.isActive = false;
                _db.Subjects.Update(subjects);
                _db.SaveChanges();
                return true;
            }
            catch (DbUpdateException exception)
            {
                _logger.LogError($"Subject DAL : RemoveSubject(int Subjectid) : {exception.Message}");
                return false;
            }
            catch (OperationCanceledException exception)
            {
                _logger.LogError($"Subject DAL : RemoveSubject(int Subjectid) : {exception.Message}");
                return false;
            }
            catch (ValidationException SubjectNotFound)
            {
                throw SubjectNotFound;
            }
            catch (Exception exception)
            {
                _logger.LogError($"Subject DAL : RemoveSubject(int Subjectid) : {exception.Message}");
                return false;
            }

        }
        public List<Subject> ViewSubject()
        {
            try
            {
                return (from Subject in _db.Subjects where Subject.isActive == true select Subject).OrderBy(x => x.SubjectName).ToList();
            }
            catch (DbUpdateException exception)
            {
                _logger.LogError($"Subject DAL : ViewSubject() : {exception.Message}");
                throw new DbUpdateException();
            }
            catch (OperationCanceledException exception)
            {
                _logger.LogError($"Subject DAL : ViewSubject() : {exception.Message}");
                throw new OperationCanceledException();
            }
            catch (Exception exception)
            {
                _logger.LogError($"Subject DAL : ViewSubject() : {exception.Message}");
                throw new Exception();
            }
        }
    }
}