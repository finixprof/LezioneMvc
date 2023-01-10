using Dapper;
using MySql.Data.MySqlClient;
using WebApplication1.Models.Entities;

namespace WebApplication1.Helpers
{
    public static class DatabaseHelper
    {
        public static List<Personale> GetAllPersonale()
        {
            // Connect to the database
            using (var connection = new MySqlConnection(connectionString))
            {
                // Create a query that retrieves all personale"    
                var sql = "SELECT * FROM personale";
                // Use the Query method to execute the query and return a list of objects
                var listPersonale = connection.Query<Personale>(sql).ToList();
                return listPersonale;
            }
        }

    }
}
