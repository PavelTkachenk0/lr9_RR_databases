namespace lr9_RR_databases.Models.Requests;

public class CreatePositionRequest
{
    public string Name { get; set; } = null!;
    public short? Kkal {  get; set; }
    public short? Proteins { get; set; }
    public short? Fats { get; set; }
    public short? Carbohydrates { get; set; }
    public int RestaurantId { get; set; }
    public short? Weight { get; set; }
    public decimal Price { get; set; }
}
