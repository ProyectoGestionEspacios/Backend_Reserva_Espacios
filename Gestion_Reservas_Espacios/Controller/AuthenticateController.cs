using DataAccess;
using DataAccess.Repos;
using Entities.Auth;
using Gestion_Reservas_Espacios.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Gestion_Reservas_Espacios.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        //definimos los parametros con los que queremos trabajar, son metodos de una api para trabajar con user y roles
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnitOfWork _uow;
        //private readonly RoleManager<IdentityUser> _roleManager;
        private readonly IConfiguration _configuration;//leer fichero de configuracion

        public AuthenticateController(
            UserManager<ApplicationUser> userManager,
            //RoleManager<IdentityUser> roleManager,
            IConfiguration configuration, IUnitOfWork uow)
        {
            _userManager = userManager;
            // _roleManager = roleManager;
            _configuration = configuration;
            _uow = uow;
        }

        [HttpGet("{id}")]
        public async Task<ApplicationUser> Get(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }

                
        [HttpPost]
        [Route("login")]
        // endpooint que espera usuario y contraseña .. Loginmodel
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            // preguntamos si existe usuario y comprobamos su clave
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                //asignamos roles
                var userRoles = await _userManager.GetRolesAsync(user);
                //listado de elementos dentro del token clain de tipo nombre usuario
                var authClaims = new List<Claim>
                {
                    new Claim("Id", user.Id),
                    new Claim(ClaimTypes.Name, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var token = GetToken(authClaims);

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }
            return Unauthorized();
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var userExists = await _userManager.FindByNameAsync(model.Username);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });

            ApplicationUser user = new()
            {
                Email = model.Email,
                UserName = model.Username,
                Name = model.Name,
                Surname = model.Surname,
                Company = model.Company,
                Avatar = model.Avatar

            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });

            return Ok(new Response { Status = "Success", Message = "User created successfully!" });
        }

        [HttpPut("Edit/{id}")]
        
        public async Task<IActionResult> Edit(string id, ApplicationUserDTO applicationUserDTO)
        {
            if (ModelState.IsValid)
            {

                try
                {
                    //await _uow.UsuariosRepository.Edit(usuario);
                    return Ok(await _uow.UserRepository.Edit(id, applicationUserDTO));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_uow.UserRepository.ApplicationUserExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            else
                return BadRequest(new { code = "InvalidModelState", message = "Error: ModelState inválido." });
        }



        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDTO resetPasswordDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var user = await _userManager.FindByEmailAsync(resetPasswordDTO.Email);
            if (user == null)
                return BadRequest("Invalid Request");
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var resetPassResult = await _userManager.ResetPasswordAsync(user, token, resetPasswordDTO.Password);
            if (!resetPassResult.Succeeded)
            {
                var errors = resetPassResult.Errors.Select(e => e.Description);

                return BadRequest(new { Errors = errors });
            }

            return Ok();
        }

        [HttpDelete("Delete/{id}")]
       
        public async Task<IActionResult> DeleteConfirmed(string id)
        {            
            return Ok(await _uow.UserRepository.DeleteConfirmed(id));
        }


        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(100),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }
    }
}
