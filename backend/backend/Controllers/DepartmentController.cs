using backend.Models;
using backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;
        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [HttpGet]
        public IActionResult Get() => Ok(_departmentService.GetAll());

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var department = _departmentService.GetById(id);
            if (department == null) return NotFound();
            return Ok(department);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Department department)
        {
            _departmentService.Add(department);
            return CreatedAtAction(nameof(Get), new { id = department.DepartmentID }, department);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Department department)
        {
            if (_departmentService.GetById(id) == null) return NotFound();

            department.DepartmentID = id;
            _departmentService.Update(department);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var department = _departmentService.GetById(id);
            if (department == null)
            {
                return NotFound();
            }

            try
            {
                _departmentService.Delete(id);
                return NoContent(); 
            }
            catch (InvalidOperationException ex)
            {
               
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while processing your request." });
            }
        }


    }
}
