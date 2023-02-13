using Dapper;
using MySql.Data.MySqlClient;
using Site.Models.Dtos;
using Site.Models.Entities;
using System.Linq.Expressions;

namespace Site.Helpers
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

        public static Personale GetPersonaleById(int id)
        {
            try
            {
                // Connect to the database
                using (var connection = new MySqlConnection(ConnectionString))
                {
                    // Create a query that retrieves all personale"    
                    var sql = "SELECT * " +
                        "FROM personale " +
                        "WHERE id=@id";
                    // Use the Query method to execute the query and return a list of objects
                    var personale = connection.Query<Personale>(sql, new { id = id }).FirstOrDefault();
                    return personale;
                }
            }
            catch (Exception ex)
            {
                // dovrei loggare un messaggio: problema di accesso al database
                return null;
            }
        }

        public static List<Paziente> GetAllPazienti()
        {
            try
            {
                // Connect to the database
                using (var connection = new MySqlConnection(ConnectionString))
                {
                    // Create a query that retrieves all pazienti"    
                    var sql = "SELECT * " +
                        "FROM paziente";
                    // Use the Query method to execute the query and return a list of objects
                    var listPazienti = connection.Query<Paziente>(sql).ToList();
                    return listPazienti;
                }
            }
            catch (Exception ex)
            {
                // dovrei loggare un messaggio: problema di accesso al database
                return null;
            }
        }

        public static Paziente GetPazienteById(int id)
        {
            try
            {
                // Connect to the database
                using (var connection = new MySqlConnection(ConnectionString))
                {
                    // Create a query that retrieves one paziente by id"    
                    var sql = "SELECT * " +
                        "FROM paziente " +
                        "WHERE id=@id";
                    // Use the Query method to execute the query and return a list of objects
                    var paziente = connection.Query<Paziente>(sql, new { id = id }).FirstOrDefault();
                    return paziente;
                }
            }
            catch (Exception ex)
            {
                // dovrei loggare un messaggio: problema di accesso al database
                return null;
            }
        }

        public static List<Visita> GetAllVisite()
        {
            try
            {
                // Connect to the database
                using (var connection = new MySqlConnection(ConnectionString))
                {
                    // Create a query that retrieves all visita"    
                    var sql = "SELECT * " +
                        "FROM visita";
                    // Use the Query method to execute the query and return a list of objects
                    var listVisite = connection.Query<Visita>(sql).ToList();
                    return listVisite;
                }
            }
            catch (Exception ex)
            {
                // dovrei loggare un messaggio: problema di accesso al database
                return null;
            }
        }

        public static Visita GetVisitaById(int id)
        {
            try
            {
                using (var connection = new MySqlConnection(ConnectionString))
                {
                    var sql = "SELECT * " +
                        "FROM visita " +
                        "WHERE id = @id";
                    var visita = connection.Query<Visita>(sql, new { id = id }).FirstOrDefault();
                    return visita;
                }


            }
            catch (Exception ex)
            {
                return null;
            }

        }


        public static Utente Login(string username, string password)
        {
            try
            {
                using (var connection = new MySqlConnection(ConnectionString))
                {
                    var sql = "SELECT * " +
                        "FROM utente " +
                        "WHERE username = @username " +
                        "AND password = @password " +
                        "AND dataUltimaModifica IS NOT NULL";
                    var utente = connection.Query<Utente>(sql, new { username, password }).FirstOrDefault();
                    return utente;
                }

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static Utente GetUtenteByUsername(string username)
        {
            try
            {
                using (var connection = new MySqlConnection(ConnectionString))
                {
                    var sql = "SELECT * " +
                        "FROM utente " +
                        "WHERE username = @username " +
                        "AND dataUltimaModifica is NOT NULL";
                    var utente = connection.Query<Utente>(sql, new { username }).FirstOrDefault();
                    return utente;
                }


            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static bool? ExistUtenteByUsername(string username)
        {
            try
            {
                using (var connection = new MySqlConnection(ConnectionString))
                {
                    var sql = "SELECT * " +
                        "FROM utente " +
                        "WHERE username = @username " +
                        //"OR email = @email " +
                        "AND dataultimamodifica IS NOT NULL ";
                    return connection.Query<Utente>(sql, new { username }).FirstOrDefault() != null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static Utente InsertUtente(RegistrazioneDto dto)
        {
            try
            {
                using (var connection = new MySqlConnection(ConnectionString))
                {
                    var sql = "INSERT INTO utente (username, email, password) " +
                        "VALUES (@username, @email, @password); " +
                        "SELECT * " +
                        "FROM utente " +
                        "WHERE username= @username " +
                        "AND email=@email " +
                        "AND password = @password";

                    var utente = connection.Query<Utente>(sql, new { dto.Username, dto.Email, dto.Password }).FirstOrDefault();
                    return utente;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static Utente GetUtenteByEmail(string email)
        {
            try
            {
                using (var connection = new MySqlConnection(ConnectionString))
                {
                    var sql = "SELECT * " +
                        "FROM utente " +
                        "WHERE email = @email ";
                    var utente = connection.Query<Utente>(sql, new { email }).FirstOrDefault();
                    return utente;
                }


            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static void UpdateUtente(Utente utente)
        {
            try
            {
                using (var connection = new MySqlConnection(ConnectionString))
                {
                    var sql = "UPDATE utente " +
                        "SET password = @password " +
                        "WHERE id = @id";
                    connection.Query(sql, new { utente.Password, utente.Id });
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
