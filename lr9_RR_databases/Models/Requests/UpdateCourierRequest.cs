namespace lr9_RR_databases.Models.Requests;

public class UpdateCourierRequest
{
    public string? Name { get; set; } 
    public string? Surname { get; set; }
    public string? MiddleName { get; set; }
    public DateTime? Birthday { get; set; }
    public string? WorkSchedule { get; set; }
}
