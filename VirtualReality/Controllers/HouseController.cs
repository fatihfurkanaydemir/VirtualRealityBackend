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
    public class HouseController : ControllerBase
    {
        private readonly IdentityContext _houseDBContext;
        private readonly IAuthenticatedUserService _authenticatedUserService;

        public HouseController(IdentityContext houseDBContext, IAuthenticatedUserService authenticatedUserService)
        {
            _houseDBContext = houseDBContext;
            _authenticatedUserService = authenticatedUserService;
        }


        // GET: api/<HousesControllers>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var data = await _houseDBContext.Houses.AsNoTracking().ToListAsync();
            return Ok(new Response<List<House>>() { Succeeded = true, Data = data });
        }

        // GET api/<HousesControllers>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var data = await _houseDBContext.Houses.AsNoTracking().SingleOrDefaultAsync(x => x.Id == id);

            return Ok(new Response<House>() { Succeeded = true, Data = data });
        }

        // POST api/<HousesControllers>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] HouseDTO houseDTO)
        {
            if(_authenticatedUserService.UserId == null) throw new ApiException($"Not authenticated");
            House house = new House()
            {
                AssetLink = houseDTO.AssetLink,
                Furnished = houseDTO.Furnished,
                Floor = houseDTO.Floor,
                SquareMeter = houseDTO.SquareMeter,
                DateCreated = DateTime.UtcNow,
                Description = houseDTO.Description,
                ImageLink = houseDTO.ImageLink,
                Latitude = houseDTO.Latitude,
                Longitude = houseDTO.Longitude,
                Price = houseDTO.Price,
                User = new ApplicationUser()
                {
                    Id = _authenticatedUserService.UserId,
                },
            };

            _houseDBContext.Entry(house.User).State = EntityState.Unchanged;
            _houseDBContext.Houses.Add(house);
            await _houseDBContext.SaveChangesAsync();
            return Ok(new Response<int>() { Succeeded = true, Data = house.Id });
        }

        // PUT api/<HousesControllers>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id,[FromBody] HouseDTO houseDTO)
        {
            if (_authenticatedUserService.UserId == null) throw new ApiException($"Not authenticated");

            var house = await _houseDBContext.Houses.Include(x => x.User).SingleOrDefaultAsync(x => x.Id == id);
            if (house == null) throw new ApiException($"House not found: {id}");

            house.Id = id;
            house.AssetLink = houseDTO.AssetLink;
            house.Furnished = houseDTO.Furnished;
            house.Floor = houseDTO.Floor;
            house.SquareMeter = houseDTO.SquareMeter;
            house.Description = houseDTO.Description;
            house.ImageLink = houseDTO.ImageLink;
            house.Latitude = houseDTO.Latitude;
            house.Longitude = houseDTO.Longitude;
            house.Price = houseDTO.Price;

            _houseDBContext.Entry(house.User).State = EntityState.Unchanged;
            _houseDBContext.Houses.Update(house);
            await _houseDBContext.SaveChangesAsync();
            return Ok(new Response<int>() { Succeeded = true, Data = house.Id });
        }

        // DELETE api/<HousesControllers>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (_authenticatedUserService.UserId == null) throw new ApiException($"Not authenticated");

            var item = await _houseDBContext.Houses.Include(x => x.User).SingleOrDefaultAsync(x => x.Id == id);
            _houseDBContext.Entry(item.User).State = EntityState.Unchanged;
            _houseDBContext.Houses.Remove(item);
            await _houseDBContext.SaveChangesAsync();

            return Ok(new Response<int>() { Succeeded = true, Data = item.Id });
        }
    }
}
