using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurant_Reservation_System.Data;
using Restaurant_Reservation_System.Data.Entities;

namespace Restaurant_Reservation_System.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeveloperController : ControllerBase
    {
        private readonly RestaurantContext _context;

        public DeveloperController(RestaurantContext context)
        {
            _context = context;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult> GetAll()
        {
            var developers = await _context.DeveloperInfos
                .Include(d => d.Person)
                .Select(d => new
                {
                    d.Id,
                    d.Role,
                    d.GithubLink,
                    d.LinkedinLink,
                    d.PortfolioLink,

                    Person = new
                    {
                        d.Person.Id,
                        d.Person.FirstName,
                        d.Person.LastName,
                        d.Person.Phone,
                        d.Person.Address
                    }
                })
                .ToListAsync();

            return Ok(developers);
        }
    }
}
