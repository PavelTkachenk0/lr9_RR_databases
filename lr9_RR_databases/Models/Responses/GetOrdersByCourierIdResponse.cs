namespace lr9_RR_databases.Models.Responses;

public class GetOrdersByCourierIdResponse
{
    public int Id { get; set; }
    public short Paymenttypeid { get; set; }
    public int Clientid { get; set; }
    public DateTime Ordertime { get; set; }
    public DateTime Deliverytime { get; set; }
    public decimal Price { get; set; }
    public string Address { get; set; } = null!;
    public string? Comment {  get; set; }
}
