using Attendance.Api.DbContexts;
using Attendance.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Attendance.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly AttendanceDBContext context;

        public EmployeeController(AttendanceDBContext context)
        {
            this.context = context;
        }

        [HttpPost]
        public ActionResult AddEmployee(Employees employees) 
        {
            employees.Id = Guid.NewGuid().ToString();
            employees.Active = Enums.RecordState.Active;
            context.Employees.Add(employees);
            context.SaveChanges();

            return Created("Created Succesfully", employees);
        }

        [HttpPut]
        public ActionResult PutEmployee(Employees employees)
        {
            context.Employees.Update(employees);
            context.SaveChanges();

            return Ok("Updated Succesfully");
        }

        [HttpDelete]
        public ActionResult DeleteEmployee(string Id)
        {
            var response = context.Employees.FirstOrDefault(e => e.Id == Id);

            if(response != null)
            {
                response.Active = 0;
                context.Employees.Update(response);
                context.SaveChanges();
            } else
            {
                return new JsonResult(new { message = "Employee not found" }) { StatusCode = StatusCodes.Status404NotFound };
            }

            return Ok("Deleted Succesfully");
        }

        [HttpGet]
        public ActionResult<IEnumerable<Employees>> GetEmployees()
        {
            var response = context.Employees.ToList();

            return Ok(response);
        }

        [HttpGet("{Id}")]
        public ActionResult<Employees> GetEmployee(string Id)
        {
            var response = context.Employees.FirstOrDefault(e => e.Id == Id);

            return Ok(response);
        }
    }
}
