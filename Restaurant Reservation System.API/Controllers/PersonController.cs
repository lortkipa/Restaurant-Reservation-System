using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurant_Reservation_System.Service;
using Restaurant_Reservation_System.Service.DTOs.Person;
using Restaurant_Reservation_System.Service.Interfaces;

namespace Restaurant_Reservation_System.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _service;

        public PersonController(IPersonService service)
        {
            _service = service;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<PersonDTO>>> GetAll()
        {
            var persons = await _service.GetAllAsync();
            return Ok(persons);
        }
        [HttpGet("GetById/{id:int}")]
        public async Task<ActionResult<PersonDTO>> GetById(int id)
        {
            var person = await _service.GetByIdAsync(id);
            if (person == null) return BadRequest();

            return Ok(person);
        }
        [HttpPost("Add")]
        public async Task<ActionResult<PersonDTO>> Add(CreatePersonDTO person)
        {
            if (ModelState.IsValid)
            {
                var model = await _service.CreateAsync(person);
                if (model == null) return BadRequest();
                return Ok(model);
            }

            return BadRequest();
        }
        [HttpPut("Update/{id:int}")]
        public async Task<ActionResult<bool>> Update(int id, UpdatePersonDTO model)
        {
            if (ModelState.IsValid)
            {
                var person = await _service.GetByIdAsync(id);
                if (person == null) return BadRequest();

                var result = await _service.UpdateAsync(id, model);
                if (!result) return BadRequest();

                return Ok();
            }

            return BadRequest();
        }
        [HttpDelete("Delete/{id:int}")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            var result = await _service.DeleteAsync(id);
            if (result == false) return BadRequest();
            return Ok();
        }
    }
}
