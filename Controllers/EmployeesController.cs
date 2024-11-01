using EmployeeAdminPortal.Data;
using EmployeeAdminPortal.Model;
using EmployeeAdminPortal.Model.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeAdminPortal.Controllers
{
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
        public IActionResult GetEmployeeById(Guid id)
        {
           var employeeId =  dbContext.Employees.Find(id);
            if (employeeId == null)
            {
                return NotFound();
            }
            return Ok(employeeId);
            
        }
        [HttpPut]
        [Route("{id:guid}")]
        public IActionResult UpdateEmployee(Guid id, UpdateEmployeeDTO updateEmployeeDTO)
        {
            var Employee = dbContext.Employees.Find(id);
            if (Employee == null)
            {
                return NotFound();
            }
            Employee.Name = updateEmployeeDTO.Name;
            Employee.Email = updateEmployeeDTO.Email;
            Employee.Email = updateEmployeeDTO.Email;
            Employee.Salary = updateEmployeeDTO.Salary;
            dbContext.SaveChanges();
            return Ok(Employee);
        }
        [HttpPost]
        public IActionResult AddEmployee(AddEmployeeDTO addEmployeeDTO)
        {
            var EmployeeEntity = new Employee
            {
                Name = addEmployeeDTO.Name,
                Email = addEmployeeDTO.Email,
                Phone = addEmployeeDTO.Phone,
                Salary = addEmployeeDTO.Salary,
            };
            dbContext.Employees.Add(EmployeeEntity);
            dbContext.SaveChanges();
            return Ok(EmployeeEntity);
        }
        [HttpDelete]
        public IActionResult DeleteEmployee(Guid id) 
        {
            var employee = dbContext.Employees.Find(id);
            if (employee == null)
            {
                return NotFound();
            }
            dbContext.Employees.Remove(employee);
            dbContext.SaveChanges();
            return Ok();
        }
    }
}
