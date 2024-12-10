using imtahan.BLL.DomainModel.Entities;
using imtahan.BLL.ServiceLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace imtahan.web.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IService<Student> _studentService;

        public StudentController(IService<Student> studentService)
        {
            _studentService = studentService;
        }

        // GET: <StudentController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _studentService.GetAllAsync());
        }

        // GET api/<StudentController>/5
        [HttpGet("{number}")]
        public async Task<IActionResult> Get(int number)
        {
            var entity = await _studentService.GetSingleAsync(x => x.Number == number);
            if (entity == null)
            {
                return NoContent();
            }
            return Ok(entity);
        }

        // POST /<StudentController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Student entity)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }
            try
            {
                await _studentService.AddAsync(entity);
            }
            catch (Exception ex)
            {
                //Should handle more gracefully
                return BadRequest(ex);
            }

            return CreatedAtAction(nameof(Get), new { number = entity.Number }, entity);
        }

        // PUT <StudentController>/str
        [HttpPut("{number}")]
        public async Task<IActionResult> Put(int number, [FromBody] Student entity)
        {
            if (number != entity.Number)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            try
            {
                await _studentService.UpdateAsync(entity);
            }
            catch (Exception ex)
            {
                //Should handle more gracefully
                return BadRequest(ex);
            }

            return Ok(entity);
        }

        // DELETE <StudentController>/str
        [HttpDelete("{number}")]
        public async Task<IActionResult> Delete(int number)
        {
            var entity = await _studentService.GetSingleAsync(x => x.Number == number);
            if (entity == null)
            {
                return BadRequest();
            }

            try
            {
                await _studentService.DeleteAsync(entity);
            }
            catch (Exception ex)
            {
                //Should handle more gracefully
                return new BadRequestObjectResult(ex.GetBaseException()?.Message);
            }

            return Ok();
        }
    }
}