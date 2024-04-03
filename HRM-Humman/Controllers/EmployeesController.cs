using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HRM_Humman.Data;
using HRM_Humman.Models;

namespace HRM_Humman.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly HRM_HummanContext _context;

        public EmployeesController(HRM_HummanContext context)
        {
            _context = context;
        }

        // GET: api/Employees
        [HttpGet]
        public async Task<ActionResult<List<Employee>>> GetAllEmployee()
        {
             var employees = await _context.Employee.ToListAsync();
             return Ok(employees);
        }

        // GET: api/Employees/search?name={name}
        [HttpGet("search")]
        public async Task<ActionResult<List<Employee>>> SearchEmployeeByName(string name)
        {
            var employees = await _context.Employee.Where(e => e.Name.Contains(name)).ToListAsync();

            if (employees == null || employees.Count == 0)
            {
                return NotFound("No employees found with the provided name.");
            }

            return Ok(employees);
        }

        // GET: api/Employees/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            var employees = await _context.Employee.FindAsync(id);

            if (employees == null)
            {
              return NotFound("Employee Not Found");
            }
            return Ok(employees);
        }

        // PUT: api/Employees/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(Employee updateEmployee)
        {
            var employees = await _context.Employee.FindAsync(updateEmployee.Id);

            if (employees == null)
            {
                return NotFound("Employee not found.");
            }

            employees.Name = updateEmployee.Name;
            employees.Age = updateEmployee.Age;
            employees.Email = updateEmployee.Email;
            employees.Phone = updateEmployee.Phone;

            await _context.SaveChangesAsync();

            return Ok(await _context.Employee.ToListAsync());
        }

        // POST: api/Employees
        [HttpPost]
        public async Task<ActionResult<Employee>> PostEmployee(Employee employee)
        {
          if (_context.Employee == null)
          {
              return Problem("Entity set 'HRM_HummanContext.Employee'  is null.");
          }
            _context.Employee.Add(employee);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEmployee", new { id = employee.Id }, employee);
        }

        // DELETE: api/Employees/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var employee = await _context.Employee.FindAsync(id);
            if (_context.Employee == null)
            {
                return NotFound();
            }
            if (employee == null)
            {
                return NotFound("Employee Not Found");
            }

            _context.Employee.Remove(employee);
            await _context.SaveChangesAsync();

            return Ok(await _context.Employee.ToListAsync());
        }

        private bool EmployeeExists(int id)
        {
            return (_context.Employee?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
