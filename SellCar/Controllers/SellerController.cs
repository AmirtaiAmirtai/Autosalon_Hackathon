using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SellCar.Database.Contexts;
using SellCar.Database.Entities;
using SellCar.Models;

namespace SellCar.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SellerController : ControllerBase
{
    private readonly ApplicationDbContext _dbContext;

    public SellerController(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost]
    public async Task<IActionResult> CreateSeller(SellerCreateModel model)
    {
        var existSeller = await _dbContext.Sellers.FirstOrDefaultAsync(x => x.Name == model.Name);

        if (existSeller is not null) 
            return BadRequest("Seller exist allready.");
        
        var entity = new Seller
        {
            Name = model.Name
        };

        await _dbContext.Sellers.AddAsync(entity);

        await _dbContext.SaveChangesAsync();
        
        return Ok("Seller created successfully");
    }

    [HttpGet]
    public async Task<IActionResult> GetSellers()
    {
        var sellers = await _dbContext.Sellers.ToListAsync();

        return Ok(sellers);
    }

    [HttpPut]
    public async Task<IActionResult> EditSellers(int sellerId, EditSellerModel model)
    {
        var existSeller = await _dbContext.Sellers.FirstOrDefaultAsync(x => x.Id == sellerId);

        if (existSeller is null)
            return BadRequest("Seller not found.");

        existSeller.Name = model.NewName;

        _dbContext.Sellers.Update(existSeller);
        await _dbContext.SaveChangesAsync();

        return Ok("Success updated");
    }
}