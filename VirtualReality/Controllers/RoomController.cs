using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VirtualReality.Context;
using VirtualReality.DTOs;
using VirtualReality.Exceptions;
using VirtualReality.Interfaces;
using VirtualReality.Models;
using VirtualReality.Wrappers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VirtualReality.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IdentityContext _roomDBContext;
        private readonly IAuthenticatedUserService _authenticatedUserService;


        public RoomController(IdentityContext roomDBContext, IAuthenticatedUserService authenticatedUserService)
        {
            _roomDBContext = roomDBContext;
            _authenticatedUserService = authenticatedUserService;
        }


        // GET: api/<RoomsControllers>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var data = await _roomDBContext.Rooms.AsNoTracking().ToListAsync();
            return Ok(new Response<List<Room>>() { Succeeded = true, Data = data });
        }

        // GET api/<RoomsControllers>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var data = await _roomDBContext.Rooms.AsNoTracking().Include(x => x.House).SingleOrDefaultAsync(x => x.Id == id);
            if (data == null) throw new ApiException($"Room not found: {id}");

            return Ok(new Response<Room>() { Succeeded = true, Data = data });
        }

        // GET api/<RoomsControllers>/5
        [HttpGet("GetByHouseId/{id}")]
        public async Task<IActionResult> GetByHouseId(int id)
        {
          var data = await _roomDBContext.Rooms.AsNoTracking()
            .Include(x => x.House)
            .Where(x => x.House.Id == id)
            .ToListAsync();
          if (data == null) throw new ApiException($"Rooms not found for house: {id}");

          var rooms = new List<RoomOutDTO>();
          foreach(var room in data)
          {
            rooms.Add(new RoomOutDTO()
            {
              RoomNumber = room.RoomNumber,
              AssetLink = room.House.AssetLink,
              HouseID = room.House.Id,
              Id = room.Id
            });
          }

          return Ok(new Response<List<RoomOutDTO>>() { Succeeded = true, Data = rooms });
        }

        [HttpGet("GetByRoomNumber/{roomNumber}")]
        public async Task<IActionResult> GetByRoomNumber(int roomNumber)
        {
          var data = await _roomDBContext.Rooms.AsNoTracking()
            .Include(x => x.House)
            .SingleOrDefaultAsync(x => x.RoomNumber == roomNumber);

          if (data == null) throw new ApiException($"Room not found for room number: {roomNumber}");

          var room = new RoomOutDTO()
          {
            RoomNumber = data.RoomNumber,
            AssetLink = data.House.AssetLink,
            HouseID = data.House.Id,
            Id = data.Id
          };

          return Ok(new Response<RoomOutDTO>() { Succeeded = true, Data = room });
        }

        // POST api/<RoomsControllers>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] RoomDTO roomDTO)
        {
            if (_authenticatedUserService.UserId == null) throw new ApiException($"Not authenticated");

            var house = await _roomDBContext.Houses.AsNoTracking().SingleOrDefaultAsync(x => x.Id == roomDTO.HouseID);
            if (house == null) throw new ApiException($"House not found");


            Room room = new Room()
            {
                House = new House()
                {
                    
                    Id = roomDTO.HouseID

                },
                RoomNumber = roomDTO.RoomNumber,
            };

            _roomDBContext.Entry(room.House).State = EntityState.Unchanged;
            _roomDBContext.Rooms.Add(room);
            await _roomDBContext.SaveChangesAsync();
            return Ok(new Response<int>() { Succeeded = true, Data = room.Id });
        }

        // PUT api/<RoomsControllers>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] RoomDTO roomDTO)
        {
            if (_authenticatedUserService.UserId == null) throw new ApiException($"Not authenticated");

            var room = await _roomDBContext.Rooms.Include(x => x.House).SingleOrDefaultAsync(x => x.Id == id);
            if (room == null) throw new ApiException($"Room not found: {id}");

            room.Id = id;
            room.RoomNumber = roomDTO.RoomNumber;

            _roomDBContext.Entry(room.House).State = EntityState.Unchanged;
            _roomDBContext.Rooms.Update(room);
            await _roomDBContext.SaveChangesAsync();
            return Ok(new Response<int>() { Succeeded = true, Data = room.Id });

        }

        // DELETE api/<RoomsControllers>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (_authenticatedUserService.UserId == null) throw new ApiException($"Not authenticated");


            var item = await _roomDBContext.Rooms.Include(x => x.House).SingleOrDefaultAsync(x => x.Id == id);
            if (item == null) throw new ApiException($"Room not found: {id}");

            _roomDBContext.Entry(item.House).State = EntityState.Unchanged;
            _roomDBContext.Rooms.Remove(item);
            await _roomDBContext.SaveChangesAsync();

            return Ok(new Response<int>() { Succeeded = true, Data = item.Id });
        }
    }
}
