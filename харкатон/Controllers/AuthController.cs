using Dapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Security.Claims;
using харкатон.Models;

namespace харкатон.Controllers;

[AllowAnonymous]
[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{

    private readonly IConfiguration _configuration;

    public AuthController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpPost]
    [Route("registration")]
    public IActionResult registration(Clients clients)
    {
        SqlConnection con = new SqlConnection(_configuration.GetConnectionString("ClientsDB").ToString());
        SqlCommand cmd = new SqlCommand("INSERT INTO Registration(UserName,Password,Email,isActive) VALUES('" + clients.UserName + "','" + clients.Password + "','" + clients.Email + "','" + clients.IsActive + "' )", con);
        con.Open();
        int i = cmd.ExecuteNonQuery();
        con.Close();
        if (i > 0)
        {
            return Ok("Data Inserted");
        }

        return BadRequest("Error");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(Clients clients)
    {
        SqlConnection con = new(_configuration.GetConnectionString("ClientsDB"));
        var rows = con.Query<int>("SELECT 1 FROM Registration WHERE Email = '" + clients.Email + "' AND Password = '" + clients.Password + "' AND isActive = 1 ");
        
        if (rows.Count() > 0)
        {
            var claims = new Claim[]
            {
                new Claim(ClaimTypes.Email, clients.Email),
                new Claim(ClaimTypes.Name, clients.UserName),
            };

            var claimIdentity = new ClaimsIdentity(claims, "cookie");
            var claimPrincipal = new ClaimsPrincipal(claimIdentity);

            await HttpContext.SignInAsync(claimPrincipal);

            return Ok("Valid User");
        }

        return Unauthorized("Invalid User");
    }

    [HttpDelete("Delete-User")]
    public string DeleteUser(string username, string password, string email)
    {
        // Проверяем соответствие пароля и электронной почты пользователя
        if (!IsValidCredentials(username, password, email))
        {
            return "Invalid credentials";
        }

        SqlConnection con = new SqlConnection(_configuration.GetConnectionString("ClientsDB").ToString());
        SqlCommand cmd = new SqlCommand("DELETE FROM Registration WHERE UserName = @UserName", con);
        cmd.Parameters.AddWithValue("@UserName", username);
        con.Open();
        int i = cmd.ExecuteNonQuery();
        con.Close();
        if (i > 0)
        {
            return "User Deleted";
        }
        else
        {
            return "User Not Found";
        }
    }

    private bool IsValidCredentials(string username, string password, string email)
    {
        SqlConnection con = new SqlConnection(_configuration.GetConnectionString("ClientsDB").ToString());
        SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Registration WHERE UserName = @UserName AND Password = @Password AND Email = @Email", con);
        cmd.Parameters.AddWithValue("@UserName", username);
        cmd.Parameters.AddWithValue("@Password", password);
        cmd.Parameters.AddWithValue("@Email", email);
        con.Open();
        int count = (int)cmd.ExecuteScalar();
        con.Close();
        return count > 0;
    }
}
