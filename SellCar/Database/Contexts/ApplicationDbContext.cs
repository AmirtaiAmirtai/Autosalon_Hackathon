using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SellCar.Database.Entities;

namespace SellCar.Database.Contexts;

public sealed class ApplicationDbContext : IdentityDbContext<IdentityUser>
{
    public DbSet<Clients> Clients => Set<Clients>();
    public DbSet<Car> Cars => Set<Car>();
    public DbSet<Seller> Sellers => Set<Seller>();
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
}