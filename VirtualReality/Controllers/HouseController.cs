using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VirtualReality.Context;
using VirtualReality.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VirtualReality.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HouseController : ControllerBase
    {
        private readonly DBContext _houseDBContext;

        public HouseController(DBContext houseDBContext)
        {
            _houseDBContext = houseDBContext;
        }


        // GET: api/<HousesControllers>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _houseDBContext.Houses.ToListAsync());
        }

        // GET api/<HousesControllers>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _houseDBContext.Houses.FindAsync(id));
        }

        // POST api/<HousesControllers>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] House house)
        {
            _houseDBContext.Houses.Add(house);
            await _houseDBContext.SaveChangesAsync();
            return Ok(house);
        }

        // PUT api/<HousesControllers>/5
        [HttpPut("{id}")]
        public async Task Put(int id,[FromBody] House house)
        {
            house.Id = id;
            _houseDBContext.Houses.Update(house);
            await _houseDBContext.SaveChangesAsync();
        }

        // DELETE api/<HousesControllers>/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            var item = await _houseDBContext.Houses.FindAsync(id);            
             _houseDBContext.Houses.Remove(item);
             await _houseDBContext.SaveChangesAsync();
                      
        }
    }
}
