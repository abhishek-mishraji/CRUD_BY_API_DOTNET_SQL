using EmployeeAdminPortal.Data;
using EmployeeAdminPortal.Models;
using EmployeeAdminPortal.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeAdminPortal.Controllers
{    //localhost:Localhost_port/api/Employees
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext; 
        public EmployeesController(ApplicationDbContext dbContext)
        {
             this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAllEmployees()
        {
            var allEmployees = dbContext.Employees.ToList();
            return Ok(allEmployees);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult GetEmployee(Guid id)
        {
            var employee = dbContext.Employees.Find(id);
            if (employee == null)
            {
                return NotFound("Not Found Employee with given id : ERROR 404");
            }
            return Ok(employee);
        } 

        [HttpPost]
        public IActionResult AddEmployee(AddEmployeeDto AddEmployeeDto)
        {
            var employeeEntity = new Employee
            {
                Name = AddEmployeeDto.Name,
                Email = AddEmployeeDto.Email,
                Phone = AddEmployeeDto.Phone,
                Salary = AddEmployeeDto.Salary
            };

            dbContext.Employees.Add(employeeEntity);
            dbContext.SaveChanges();
            return Ok(employeeEntity);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public IActionResult UpdateEmployee(Guid id, UpdateEmployeeDto UpdateEmployeeDto)
        {
            var employee = dbContext.Employees.Find(id);
            if (employee == null)
            {
                return NotFound("Not Found Employee with given id : ERROR 404");
            }
            employee.Name = UpdateEmployeeDto.Name;
            employee.Email = UpdateEmployeeDto.Email;
            employee.Phone = UpdateEmployeeDto.Phone;
            employee.Salary = UpdateEmployeeDto.Salary;
            dbContext.SaveChanges();
            return Ok(employee);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public IActionResult DeleteEmployee(Guid id)
        {
            var employee = dbContext.Employees.Find(id);
            if (employee == null)
            {
                return NotFound("Not Found Employee with given id : ERROR 404");
            }
            dbContext.Employees.Remove(employee);
            dbContext.SaveChanges();
            return Ok("Employee Removed Successfully");
        }
    }
}
