using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VirtualReality.Context;
using VirtualReality.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VirtualReality.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IdentityContext _roomDBContext;

        public RoomController(IdentityContext roomDBContext)
        {
            _roomDBContext = roomDBContext;
        }


        // GET: api/<RoomsControllers>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _roomDBContext.Rooms.ToListAsync());
        }

        // GET api/<RoomsControllers>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _roomDBContext.Rooms.FindAsync(id));
        }

        // POST api/<RoomsControllers>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Room room)
        {
            _roomDBContext.Rooms.Add(room);
            await _roomDBContext.SaveChangesAsync();
            return Ok(room);
        }

        // PUT api/<RoomsControllers>/5
        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody] Room room)
        {
            room.Id = id;
            _roomDBContext.Rooms.Update(room);
            await _roomDBContext.SaveChangesAsync();
        }

        // DELETE api/<RoomsControllers>/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            var item = await _roomDBContext.Rooms.FindAsync(id);
            _roomDBContext.Rooms.Remove(item);
            await _roomDBContext.SaveChangesAsync();

        }
    }
}
