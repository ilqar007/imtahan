using imtahan.BLL.DomainModel.Entities;
using imtahan.BLL.ServiceLayer.DtoAndMessages;
using imtahan.BLL.ServiceLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace imtahan.web.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ExamController : ControllerBase
    {
        private readonly IService<Exam> _examService;

        public ExamController(IService<Exam> examService)
        {
            _examService = examService;
        }

        // GET: <ExamController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok((await _examService.GetAllAsync()).Select(e => new ExamDto { ClassCode = e.ClassCode, StudentNumber = e.StudentNumber, Date = e.Date, Grade = e.Grade }));
        }

        // GET api/<ExamController>/5
        [HttpGet("{classCode}/{studentNumber}")]
        public async Task<IActionResult> Get(string classCode, int studentNumber)
        {
            var entity = await _examService.GetSingleAsync(x => x.ClassCode == classCode && x.StudentNumber == studentNumber);
            if (entity == null)
            {
                return NoContent();
            }
            return Ok(entity);
        }

        // POST /<ExamController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ExamDto dto)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }
            var entity = new Exam { ClassCode = dto.ClassCode, StudentNumber = dto.StudentNumber, Date = dto.Date, Grade = dto.Grade };
            try
            {
                await _examService.AddAsync(entity);
            }
            catch (Exception ex)
            {
                //Should handle more gracefully
                return BadRequest(ex);
            }

            return CreatedAtAction(nameof(Get), new { classCode = entity.ClassCode, studentNumber = entity.StudentNumber }, entity);
        }

        // PUT <ExamController>/str
        [HttpPut("{classCode}/{studentNumber}")]
        public async Task<IActionResult> Put(string classCode, int studentNumber, [FromBody] ExamDto dto)
        {
            if (classCode != dto.ClassCode || studentNumber != dto.StudentNumber)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }
            var entity = new Exam { ClassCode = dto.ClassCode, StudentNumber = dto.StudentNumber, Date = dto.Date, Grade = dto.Grade };
            try
            {
                await _examService.UpdateAsync(entity);
            }
            catch (Exception ex)
            {
                //Should handle more gracefully
                return BadRequest(ex);
            }

            return Ok(entity);
        }

        // DELETE <ExamController>/str
        [HttpDelete("{classCode}/{studentNumber}")]
        public async Task<IActionResult> Delete(string classCode, int studentNumber)
        {
            var entity = await _examService.GetSingleAsync(x => x.ClassCode == classCode && x.StudentNumber == studentNumber);
            if (entity == null)
            {
                return BadRequest();
            }

            try
            {
                await _examService.DeleteAsync(entity);
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