using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicBookingApp.DataContext;
using MusicBookingApp.Model;
using MusicBookingApp.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace MusicBookingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "admin")]

    public class AdminController : ControllerBase
    {

        private readonly IMapper map;
        private readonly ILogger<AdminController> logger;
        private readonly Context context;

        public AdminController(IMapper mapper, ILogger<AdminController> logger, Context context)
        {
            this.map = mapper;
            this.logger = logger;
            this.context = context;
        }


        [HttpPost("Artist/Create")]
        public async Task<IActionResult> CreateArtist(CreateArtistDTO createArtistDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var comp = map.Map<Artists>(createArtistDTO);
                var result = await context.Artists.AddAsync(comp);
                var checkid = await context.Users.FindAsync(createArtistDTO.UserId);

                if (checkid == null)
                {
                    return BadRequest(new  { Status = "Failed", Message = "User cannot be found" });
                }


                await context.SaveChangesAsync();
                return Ok(new  { Status = "Success", Message = "Created Successfully" });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Somethiing went wrong in {nameof(CreateArtist)}");
                return Problem($"something went wrong in the {nameof(CreateArtist)}", statusCode: 500);
            }
        }


        [HttpPut("Artist/Update/{id}")]
        public async Task<IActionResult> UpdateArtist(int id, UpdateArtistDTO update)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var check = await context.Artists.FindAsync(id);
                if (check == null)
                {
                    return NotFound();
                }
                var connect = map.Map(update, check);
                context.Entry(check).State = EntityState.Modified;

                await context.SaveChangesAsync();
                return Ok(new  { Status = "Success" });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"something is wrong in the {nameof(UpdateArtist)}");
                return Problem($"something went wrong in the {nameof(UpdateArtist)}", statusCode: 500);
            }
        }

        [HttpDelete("Artist/Delete/{id}")]
        public async Task<IActionResult> DeleteArtist(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var check = await context.Artists.FindAsync(id);
                if(check == null)
                {
                    return BadRequest(new {Message = "User not found", Staus = "Failed"});
                }

                var result = context.Artists.Remove(check);

                await context.SaveChangesAsync();

                return Ok(new { Status = "success" });

            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"something went wrong in the {nameof(DeleteArtist)}");
                return Problem($"something went wrong in the {nameof(DeleteArtist)}", statusCode: 500);
            }
        }

        [HttpPost("Event/Create")]
        public async Task<IActionResult> CreateEvent(CreateEventDTO createEvent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var comp = map.Map<Events>(createEvent);
                var result = await context.Events.AddAsync(comp);
                var checkid = await context.Artists.FindAsync(createEvent.ArtistId);

                if (checkid == null)
                {
                    return BadRequest(new { Status = "Failed", Message = "artist not be found" });
                }


                await context.SaveChangesAsync();
                return Ok(new { Status = "Success", Message = "Event Created Successfully" });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Somethiing went wrong in {nameof(CreateEvent)}");
                return Problem($"something went wrong in the {nameof(CreateEvent)}", statusCode: 500);
            }
        }


        [HttpPut("Event/Update/{id}")]
        public async Task<IActionResult> UpdateEvent(int id, UpdateEventDTO update)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var check = await context.Events.FindAsync(id);
                if (check == null)
                {
                    return NotFound(new { Status = "Failed" });
                }
                var connect = map.Map(update, check);
                context.Entry(check).State = EntityState.Modified;

                await context.SaveChangesAsync();
                return Ok(new { Status = "Success" });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"something is wrong in the {nameof(UpdateEvent)}");
                return Problem($"something went wrong in the {nameof(UpdateEvent)}", statusCode: 500);
            }
        }

        [HttpDelete("Events/Delete/{id}")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var check = await context.Events.FindAsync(id);
                
                if(check == null)
                {
                    return NotFound();
                }

                var result = context.Events.Remove(check);

                await context.SaveChangesAsync();

                return Ok(new { Status = "success" });

            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"something went wrong in the {nameof(DeleteArtist)}");
                return Problem($"something went wrong in the {nameof(DeleteArtist)}", statusCode: 500);
            }
        }
    }
}
