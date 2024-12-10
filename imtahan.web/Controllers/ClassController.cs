using imtahan.BLL.DomainModel.Entities;
using imtahan.BLL.ServiceLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace imtahan.web.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ClassController : ControllerBase
    {
        private readonly IService<Class> _classService;

        public ClassController(IService<Class> classService)
        {
            _classService = classService;
        }

        // GET: <ClassController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _classService.GetAllAsync());
        }

        // GET api/<ClassController>/5
        [HttpGet("{code}")]
        public async Task<IActionResult> Get(string code)
        {
            var entity = await _classService.GetSingleAsync(x => x.Code == code);
            if (entity == null)
            {
                return NoContent();
            }
            return Ok(entity);
        }

        // POST /<ClassController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Class entity)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }
            try
            {
                await _classService.AddAsync(entity);
            }
            catch (Exception ex)
            {
                //Should handle more gracefully
                return BadRequest(ex);
            }

            return CreatedAtAction(nameof(Get), new { code = entity.Code }, entity);
        }

        // PUT <ClassController>/str
        [HttpPut("{code}")]
        public async Task<IActionResult> Put(string code, [FromBody] Class entity)
        {
            if (code != entity.Code)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            try
            {
                await _classService.UpdateAsync(entity);
            }
            catch (Exception ex)
            {
                //Should handle more gracefully
                return BadRequest(ex);
            }

            return Ok(entity);
        }

        // DELETE <ClassController>/str
        [HttpDelete("{code}")]
        public async Task<IActionResult> Delete(string code)
        {
            var entity = await _classService.GetSingleAsync(x => x.Code == code);
            if (entity == null)
            {
                return BadRequest();
            }

            try
            {
                await _classService.DeleteAsync(entity);
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