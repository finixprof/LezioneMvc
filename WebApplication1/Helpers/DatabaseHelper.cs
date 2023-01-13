using Dapper;
using MySql.Data.MySqlClient;
using WebApplication1.Models.Entities;

namespace WebApplication1.Helpers
{
    public static class DatabaseHelper
    {
        public static string ConnectionString { get; set; }
        public static List<Personale> GetAllPersonale()
        {
            try
            {
                // Connect to the database
                using (var connection = new MySqlConnection(ConnectionString))
                {
                    // Create a query that retrieves all personale"    
                    var sql = "SELECT * " +
                        "FROM personale";
                    // Use the Query method to execute the query and return a list of objects
                    var listPersonale = connection.Query<Personale>(sql).ToList();
                    return listPersonale;
                }
            }
            catch (Exception ex)
            {
                // dovrei loggare un messaggio: problema di accesso al database
                return null;
            }
        }

    }
}
