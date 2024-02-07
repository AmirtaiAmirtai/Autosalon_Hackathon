namespace SellCar.Database.Entities;

public class Car
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; }
    
    public int SellerId { get; set; }
    
    public Seller Seller { get; set; }
}