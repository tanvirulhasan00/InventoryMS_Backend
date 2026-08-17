using InventoryMS.Services.IServiceModels;
using Npgsql;

namespace InventoryMS.Services.ServiceModels
{
    public class Checker : IChecker
    {
        public async Task<bool> IsDatabaseConnectedAsync(string conStr)
        {
            try
            {
                await using var connection = new NpgsqlConnection(conStr);
                await connection.OpenAsync();
                return true;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Database connection failed: {ex.Message}");
                return false;
            }
        }
    }
}
