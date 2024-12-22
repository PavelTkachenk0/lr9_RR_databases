using lr9_RR_databases.Models.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace lr9_RR_databases.Controllers;

public class PositionsController : Controller
{
    private readonly string _connectionString = "Host=localhost;Username=postgres;Password=root;Database=delivery_system";

    [HttpPost]
    [AllowAnonymous]
    [Route("api/positions")]
    public async Task<IActionResult> CreatePosition([FromBody] CreatePositionRequest req)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();

        using var command = new NpgsqlCommand("SELECT create_new_restaurant_position(@p_name, @p_kkal, @p_proteins, @p_fats, @p_carbohydrates, @p_restaurantid, @p_weight, @p_price)", connection);
        command.Parameters.AddWithValue("p_name", req.Name);
        command.Parameters.AddWithValue("p_kkal", req.Kkal is not null ? req.Kkal : DBNull.Value);
        command.Parameters.AddWithValue("p_proteins", req.Proteins is not null ? req.Proteins : DBNull.Value);
        command.Parameters.AddWithValue("p_fats", req.Fats is not null ? req.Fats : DBNull.Value);
        command.Parameters.AddWithValue("p_carbohydrates", req.Carbohydrates is not null ? req.Carbohydrates : DBNull.Value);
        command.Parameters.AddWithValue("p_restaurantid", req.RestaurantId);
        command.Parameters.AddWithValue("p_weight", req.Weight is not null ? req.Weight : DBNull.Value);
        command.Parameters.AddWithValue("p_price", req.Price);

        var result = await command.ExecuteScalarAsync();

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete]
    [AllowAnonymous]
    [Route("api/positions/{positionId}")]
    public async Task<IActionResult> DeletePosition([FromRoute] int positionId)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();

        using var command = new NpgsqlCommand("SELECT delete_position(@p_id)", connection);
        command.Parameters.AddWithValue("p_id", positionId);

        var result = await command.ExecuteScalarAsync();

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }
}
