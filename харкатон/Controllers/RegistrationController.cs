using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using харкатон.Models;

namespace харкатон.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RegistrationController : ControllerBase
{

    private readonly IConfiguration _configuration;

    public RegistrationController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpPost]
    [Route("registration")]
    public string registration(Clients clients)
    {
        SqlConnection con = new SqlConnection(_configuration.GetConnectionString("ClientsDB").ToString());
        SqlCommand cmd = new SqlCommand("INSERT INTO Registration(UserName,Password,Email,isActive) VALUES('" + clients.UserName + "','" + clients.Password + "','" + clients.Email + "','" + clients.IsActive + "' )", con);
        con.Open();
        int i = cmd.ExecuteNonQuery();
        con.Close();
        if (i > 0)
        {
            return "Data Inserted";
        }
        else
        {
            return "Error";
        }

    }

    [HttpPost]
    [Route("login")]
    public string login(Clients clients)
    {
        SqlConnection con = new SqlConnection(_configuration.GetConnectionString("ClientsDB").ToString());
        SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Registration WHERE Email = '" + clients.Email + "' AND Password = '" + clients.Password + "' AND isActive = 1 ", con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            return "Valid User";
        }
        else
        {
            return "Invalid User";
        }
    }
}