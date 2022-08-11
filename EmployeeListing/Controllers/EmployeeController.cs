using Microsoft.AspNetCore.Mvc;
using EmployeeListing.Models;
using System.ComponentModel.DataAnnotations;
using EmployeeListing.Services;
using Microsoft.AspNetCore.Authorization;

namespace EmployeeListing.Controller
{

[ApiController]
[Route("[controller]/[action]")] 
public class EmployeeController : ControllerBase
{
    private EmployeeService _employeeService;
    private readonly ILogger _logger;

    public EmployeeController(EmployeeService employeeService, ILogger<EmployeeController>logger)
    {
        _employeeService = employeeService;
        _logger = logger;
    }

    [HttpPost]
    [AllowAnonymous]
    public IActionResult RegisterNewEmployee(Employee employee)
    {
        try
        {
            if(employee == null) throw new ValidationException("Employee Can't be null");

            if( _employeeService.RegisterNewEmployee(employee))
            {
                return Ok("You have registered successfully. Thankyou!");
            }
            return Problem("Sorry internal error occured");
        }
        catch (ValidationException invalidEmployee)
        {
            _logger.LogError($"Employee Service : RegisterNewEmployee(Employee employee) : {invalidEmployee.Message}");
            return BadRequest(invalidEmployee.Message);
        }
        catch(Exception exception)
        {
            _logger.LogError($"Employee Service : RegisterNewEmployee(Employee employee) : {exception.Message}");
            return Problem ("Sorry internal error occured");
        }
    }

    [HttpGet]
    [Authorize(Roles="2")]
    public IActionResult ViewEmployeeByClass(int ClassId)
    {
        try
        {
            return Ok(_employeeService.ViewEmployeeByClass(ClassId));
        }
        catch (ValidationException exception1)
        {
            _logger.LogError($"Employee controller:ViewEmployeesByClass(int departmentId):{exception1.Message}:{exception1.StackTrace}");
            return BadRequest(exception1.Message);
        }
        catch (Exception exception)
        {
            _logger.LogError($"Service throwed exception while fetching employeesbyclass : {exception.Message}:{exception.StackTrace}");
            return Problem("Sorry some internal error occured");
        }
    }

    [HttpPatch]
    [Authorize(Roles="1")]
    public IActionResult RemoveEmployee(int EmployeeId)
    {
        try
        {
            if(EmployeeId <= 0)throw new ValidationException("Enter a valid EmployeeId");
            return _employeeService.RemoveEmployee(EmployeeId) ? Ok("Employee Removed Successfully") : Problem("Sorry internal error occured");
        }
        catch(ValidationException employeeNotFound)
        {
            _logger.LogError($"Employee Service : RemoveEmployee(int employeeId) : {employeeNotFound.Message}");
            return BadRequest(employeeNotFound.Message);
        }
        catch(Exception exception)
        {
             _logger.LogError($"Employee Service : RemoveEmployee throwed an exception : {exception.Message}");
            return Problem("Sorry some internal error occured");
        }
    }

    [HttpGet]
    [Authorize(Roles="2")]
    public IActionResult ViewEmployeeRequest()
    {
        try
        {
            return Ok(_employeeService.ViewEmployeeRequest());
        }
        catch(Exception exception)
        {
            _logger.LogError($"Service throwed exception while fetching employees : {exception.Message}");
            return Problem("Sorry some internal error occured");
        }
    }

    [HttpPatch]
    [Authorize(Roles="2")]
    public IActionResult RespondEmployeeRequest(int employeeId, bool Response)
    {
        if(employeeId <=0) BadRequest("Enter a valid EmployeeId");

        try
        {
            if(Response)
            {
            return _employeeService.RespondEmployeeRequest(employeeId, Response)? Ok("Request accepted successfully") : Problem("Sorry internal error occured");
            }
            else
            {
                return _employeeService.RespondEmployeeRequest(employeeId, Response)?Ok("Request rejected successfully") : Problem ("Sorry internal error occured");
            }
        }
        catch (ValidationException exception)
        {
            _logger.LogError($"Employee Controller:RespondEmployeeRequest(int employeeId,bool response): {exception.Message}");
            return BadRequest(exception.Message);
        }
        catch (Exception exception)
        {
            _logger.LogError($"Service throwed exception while fetching employees : {exception.Message}");
            return Problem("Sorry some internal error occured");
        }
    }

    [HttpGet]
    public IActionResult ViewEmployees()
    {
        try
        {
            return Ok(_employeeService.ViewEmployees());
        }
        catch (Exception exception)
        {
            _logger.LogError($"Service throwed exception while fetching employees : {exception.Message}");
            return Problem("Sorry some internal error occured");
        }
    }

    [HttpPost]
    [Authorize(Roles="1")]
    public IActionResult CreateNewSubject(Subject subject)
    {
        if(string.IsNullOrEmpty(subject.SubjectName) || string.IsNullOrWhiteSpace(subject.SubjectName))
              return BadRequest("Subject name can't be Null or Empty");
        try
        {
            return _employeeService.CreateNewSubject(subject) ? Ok("Subject added successfully") : Problem("Sorry internal error occured");
        }
         catch (ValidationException exception)
        {
            _logger.LogError($"Employee Service : CreateNewSubject(string SubjectName) : {exception.Message}");
            return BadRequest(exception.Message);
        }
        catch (Exception exception)
        {
            _logger.LogError($"Subject Service : CreateSubject throwed an exception : {exception.Message}");
            return Problem("Sorry some internal error occured");
        }
    }

    [HttpPatch]
    [Authorize(Roles="1")]
    public IActionResult RemoveSubject(Subject subject)
    {
        if (subject.SubjectId <= 0)
           return BadRequest("Subject id cannot be negative or null");
        try
        {            
            int currentUser=Convert.ToInt32(User.FindFirst("UserId")?.Value);
            return _employeeService.RemoveSubject(subject) ? Ok("Subject Removed Successfully") : Problem("Sorry internal error occured");
        }
        catch (ValidationException SubjectNotFound)
        {
            _logger.LogError($"Subject Service : RemoveSubject(int Subject) : {SubjectNotFound.Message}");
            return BadRequest(SubjectNotFound.Message);
        }
        catch (Exception exception)
        {
            _logger.LogError($"Subject Service : RemoveSubject throwed an exception : {exception}");
            return Problem("Sorry some internal error occured");
        }

    }

    [HttpGet]
    public IActionResult ViewSubject()
    {
        try
        {
            return Ok(_employeeService.ViewSubject());
        }
        catch (Exception exception)
        {
            _logger.LogError("Service throwed exception while fetching Subjects ", exception);
            return Problem("Sorry some internal error occured");
        }
    }
  }
}