using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicBookingApp.DataContext;
using MusicBookingApp.DTOs;
using MusicBookingApp.Model;

namespace MusicBookingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IMapper map;
        private readonly ILogger<UserController> logger;
        private readonly Context context;

        public UserController(IMapper mapper, ILogger<UserController> logger, Context context)
        {
            this.map = mapper;
            this.logger = logger;
            this.context = context;
        }


        [HttpGet("Artist/GetAll")]
        public async Task<IActionResult> GetallArtist()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await context.Artists.ToListAsync();
                if (result == null)
                {
                    return BadRequest();
                }
                var mapresult = map.Map<List<GetArtistDTO>>(result);
                return Ok(mapresult);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Somethiing went wrong in {nameof(GetallArtist)}");
                return Problem($"something went wrong in the {nameof(GetallArtist)}", statusCode: 500);
            }
        }

        [HttpGet("Get/Artist/id")]
        public async Task<IActionResult> Getbyid(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await context.Artists.Where(c => c.Id == id).ToListAsync();
                var mapresult = map.Map<List<GetArtistDTO>>(result);
                return Ok(mapresult);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Somethiing went wrong in {nameof(Getbyid)}");
                return Problem($"something went wrong in the {nameof(Getbyid)}", statusCode: 500);
            }
        }

        [HttpGet("Event/GetAll")]
        public async Task<IActionResult> GetallEvents()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await context.Events.ToListAsync();
                if (result == null)
                {
                    return BadRequest();
                }
                var mapresult = map.Map<List<GetEventDTO>>(result);
                return Ok(mapresult);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Somethiing went wrong in {nameof(GetallArtist)}");
                return Problem($"something went wrong in the {nameof(GetallArtist)}", statusCode: 500);
            }
        }

        [HttpGet("Get/Event/id")]
        public async Task<IActionResult> GetEventid(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await context.Events.Where(c => c.Id == id).ToListAsync();
                var mapresult = map.Map<List<GetEventDTO>>(result);
                return Ok(mapresult);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Somethiing went wrong in {nameof(Getbyid)}");
                return Problem($"something went wrong in the {nameof(Getbyid)}", statusCode: 500);
            }
        }

        [HttpPost("Bookings")]
        public async Task<IActionResult> CreateBookings(CreateBookingsDTO createBookingsDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var comp = map.Map<Bookings>(createBookingsDTO);
                var result = await context.Bookings.AddAsync(comp);
                var checkid = context.Users.Where(o => o.Id == createBookingsDTO.userId);
                var checkeventid = await context.Events.FindAsync(createBookingsDTO.EventId);

                if (checkid == null)
                {
                    return BadRequest(new { Status = "Failed", Message = "User cannot be found" });
                }
                if (checkeventid == null)
                {
                    return BadRequest(new { Status = "Failed", Message = "Event not found" });
                }


                await context.SaveChangesAsync();
                return Ok(new { Status = "Success", Message = "Created Successfully" });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Somethiing went wrong in {nameof(CreateBookings)}");
                return Problem($"something went wrong in the {nameof(CreateBookings)}", statusCode: 500);
            }
        }
        [HttpGet("Bookings/GetAll")]
        public async Task<IActionResult> GetallBookings()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await context.Bookings.ToListAsync();
                if (result == null)
                {
                    return BadRequest();
                }
                var mapresult = map.Map<List<GetBookingsDTO>>(result);
                return Ok(mapresult);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Somethiing went wrong in {nameof(GetallBookings)}");
                return Problem($"something went wrong in the {nameof(GetallBookings)}", statusCode: 500);
            }
        }

        [HttpGet("Get/Bookings/Event/{id}")]
        public async Task<IActionResult> GetBookingsbyid(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await context.Bookings.Where(c => c.EventId == id).ToListAsync();
                var mapresult = map.Map<List<GetBookingsByIDDTO>>(result);
                return Ok(mapresult);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Somethiing went wrong in {nameof(GetBookingsbyid)}");
                return Problem($"something went wrong in the {nameof(GetBookingsbyid)}", statusCode: 500);
            }
        }

    }
}
