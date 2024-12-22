using lr9_RR_databases.Models.Requests;
using lr9_RR_databases.Models.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Npgsql;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace lr9_RR_databases.Controllers;

public class CouriersController : Controller
{
    private readonly string _connectionString = "Host=localhost;Username=postgres;Password=root;Database=delivery_system";

    [HttpPut]
    [AllowAnonymous]
    [Route("api/couriers/{courierId}")]
    public async Task<IActionResult> UpdateCourier([FromRoute] int courierId, [FromBody] UpdateCourierRequest req)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();

        using var command = new NpgsqlCommand("SELECT update_courier_data(@p_id, @p_name, @p_surname, @p_middlename, @p_birthday, @p_workschedule)", connection);
        command.Parameters.AddWithValue("p_id", courierId);
        command.Parameters.AddWithValue("p_name", req.Name is not null ? req.Name : DBNull.Value);
        command.Parameters.AddWithValue("p_surname", req.Surname is not null ? req.Surname : DBNull.Value);
        command.Parameters.AddWithValue("p_middlename", req.MiddleName is not null ? req.MiddleName : DBNull.Value);
        command.Parameters.AddWithValue("p_birthday", req.Birthday is not null ? req.Birthday : DBNull.Value);
        command.Parameters.AddWithValue("p_workschedule", req.WorkSchedule is not null ? req.WorkSchedule : DBNull.Value);

        var result = await command.ExecuteScalarAsync();

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpGet]
    [AllowAnonymous]
    [Route("api/couriers/{courierId}/total-revenue")]
    public async Task<IActionResult> GetTotalRevenueForDay([FromRoute] int courierId, [FromQuery] DateOnly date)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();

        using var command = new NpgsqlCommand("SELECT get_total_revenue_for_day(@p_courier_id, @p_date)", connection);
        command.Parameters.AddWithValue("p_courier_id", courierId);
        command.Parameters.AddWithValue("p_date", date);

        var result = await command.ExecuteScalarAsync();

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpGet]
    [AllowAnonymous]
    [Route("api/couriers/{courierId}/orders")]
    public async Task<IActionResult> GetOrdersById([FromRoute] int courierId)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();

        using var command = new NpgsqlCommand("SELECT get_orderds_by_courierid(@p_id)", connection);
        command.Parameters.AddWithValue("p_id", courierId);

        var result = await command.ExecuteScalarAsync() as string;

        if (result == null)
        {
            return NotFound();
        }

        var response = JsonConvert.DeserializeObject<List<GetOrdersByCourierIdResponse>>(result);

        return Ok(response);
    }
}
