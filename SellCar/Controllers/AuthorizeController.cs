//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Options;
//using Microsoft.IdentityModel.Tokens;
//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;
//using System.Text;
//using SellCar.Models;

//namespace SellCar.Controllers
//{
//    [ApiController]
//    [Route("api/[controller]")]
//    public class AuthorizeController : Controller
//    {
        
//        private readonly UserManager<IdentityUser> _userManager;
//        private readonly SignInManager<IdentityUser> _signInManager;
//        private readonly JWTSettings _options;

//        public AuthorizeController(UserManager<IdentityUser> user, SignInManager<IdentityUser> signIn, IOptions<JWTSettings> optAcces)
//        {
//            _userManager = user;
//            _signInManager = signIn;
//            _options = optAcces.Value;
//        }

//        [HttpPost("Register")]
//        public async Task<IActionResult> Register(InfoClients infoClients)
//        {
//            var user = new IdentityUser { UserName = infoClients.UserName, Email = infoClients.Email };
//            var result = await _userManager.CreateAsync(user, infoClients.Password);
//            if (result.Succeeded)
//            {
//                await _signInManager.SignInAsync(user, isPersistent: false);
//                List<Claim> claims = new List<Claim>();
//                claims.Add(new Claim("Profession", infoClients.Profession.ToString()));
//                claims.Add(new Claim("SeniorManager", infoClients.SeniorManager.ToString()));
//                claims.Add(new Claim(ClaimTypes.Email, infoClients.Email));

//                await _userManager.AddClaimsAsync(user, claims);
//                return Ok();
//            }
//            else
//            {
//                return BadRequest();
//            }
            
//        }

//        private string GetToken(IdentityUser user, IEnumerable<Claim> prinicpal)
//        {
//            var claims = prinicpal.ToList();
//            claims.Add(new Claim(ClaimTypes.Name, user.UserName));
//            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey));

//            var jwt = new JwtSecurityToken(
//                issuer: _options.Issuer,
//                audience: _options.Audience,
//                claims: claims,
//                expires: DateTime.UtcNow.Add(TimeSpan.FromDays(1)),
//                notBefore: DateTime.UtcNow,
//                signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
//                );
//            return new JwtSecurityTokenHandler().WriteToken(jwt);
//        }

//        [HttpPost("SignIn")]
//        public async Task<IActionResult> SignIn(Clients clients)
//        {
//            var user = await _userManager.FindByEmailAsync(clients.Email);
//            var result = await _signInManager.PasswordSignInAsync(user, clients.Password, false, false);
//            if (result.Succeeded)
//            {
//                IEnumerable<Claim> claims = await _userManager.GetClaimsAsync(user);
//                var token = GetToken(user, claims);
//                return Ok(token);
//            }
//            return BadRequest();
//        }
//    }
//}

