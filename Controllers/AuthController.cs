using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MusicBookingApp.TokenRepository;
using MusicBookingApp.Model;
using MusicBookingApp.DataContext;
using MusicBookingApp.DTOs;

namespace MusicBookingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<Users> userManager;
        private readonly IMapper map;
        private readonly ILogger<AuthController> logger;
        private readonly ITokenRepo tokenRepo;
        private readonly Context context;

        public AuthController(UserManager<Users> userManager, IMapper mapper, ILogger<AuthController> logger, ITokenRepo tokenRepo, Context context)
        {
            this.userManager = userManager;
            this.map = mapper;
            this.logger = logger;
            this.tokenRepo = tokenRepo;
            this.context = context;
        }

        [HttpPost]
        [Route("{Role}/Registration")]
        public async Task<IActionResult> Register([FromBody] RegistrationDTO registrationDTO, [FromRoute] string Role)
        {
            logger.LogInformation($"Registration attempt for {registrationDTO.UserName}");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (Role.Equals("admin", StringComparison.CurrentCultureIgnoreCase) || Role.Equals("user", StringComparison.CurrentCultureIgnoreCase))
                {
                    //.............mapping DTO to Model class
                    var identityuser = map.Map<Users>(registrationDTO);

                    //.............User Registration attempt
                    var identityresult = await userManager.CreateAsync(identityuser, registrationDTO.Password);

                    //.............returns error if user is not created successfully
                    if (!identityresult.Succeeded)
                    {
                        foreach (var item in identityresult.Errors)
                        {
                            ModelState.AddModelError(item.Code, item.Description);
                        }

                        return BadRequest(ModelState);

                    }
                    //.............Add Role
                    if (registrationDTO.Roles != null && registrationDTO.Roles.Contains(Role))
                    {
                        identityresult = await userManager.AddToRolesAsync(identityuser, registrationDTO.Roles);
                    }
                    else
                    {
                        return BadRequest(new { Status = "failed", Message = $"invalid role {Role}" });
                    }
                    

                    return Ok(new  { Status = "success", Message = "User created successfully" });

                }
                return BadRequest(new  { Status = "Failed", Message = "Invalid Role" });

            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Something went wrong in the {nameof(Register)}");
                return Problem($"Something went wrong in the {nameof(Register)}", statusCode: 500);
            }

        }


        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            logger.LogInformation($"Login attempt for {loginDTO.UserName}");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var user = await userManager.FindByNameAsync(loginDTO.UserName);
                if (user != null)
                {
                    var checkpassword = await userManager.CheckPasswordAsync(user, loginDTO.Password);
                    if (checkpassword)
                    {
                        //get roles
                        var userrole = await userManager.GetRolesAsync(user);
                        //create token
                        if (userrole != null)
                        {
                            var jwttoken = tokenRepo.CreateJWTToken(user, userrole.ToList());
                            return Ok(new { Message = jwttoken, Status = "success" });
                        }
                    }
                }

                return Unauthorized(new { Message = "Username or Password incorrect", Status = "Failed" });

            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Something went wrong in the {nameof(Login)}");
                return Problem($"Something went wrong in the {nameof(Login)}", statusCode: 500);
            }
        }

    }
}
