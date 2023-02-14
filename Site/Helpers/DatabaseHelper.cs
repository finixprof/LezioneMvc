using Dapper;
using MySql.Data.MySqlClient;
using Site.Models.Dtos;
using Site.Models.Entities;
using System.Linq.Expressions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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
                        "AND isMailConfermata = 1 ";
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
                        "AND isMailConfermata = 1 ";
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
                        "AND isMailConfermata = 1 ";
                    return connection.Query<Utente>(sql, new { username }).FirstOrDefault() != null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }



        public static Utente SalvaUtente(Utente utente)
        {
            if (utente.Id > 0)
            {
                //update
                return UpdateUtente(utente);
            }
            //insert
            return InsertUtente(utente);
        }

        private static Utente InsertUtente(Utente utente)
        {
            try
            {
                using (var connection = new MySqlConnection(ConnectionString))
                {
                    var sql = "INSERT INTO utente (username, email) " +
                        "VALUES (@username, @email); " +
                        "SELECT * " +
                        "FROM utente " +
                        "WHERE username= @username " +
                        "AND email=@email";

                    return connection.Query<Utente>(sql, utente).FirstOrDefault();
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

        private static Utente UpdateUtente(Utente utente)
        {
            try
            {
                utente.DataUltimaModifica = DateTime.Now;
                using (var connection = new MySqlConnection(ConnectionString))
                {
                    var sql = "UPDATE utente " +
                        "SET password = @password, " +
                        " username = @username, " +
                        " email = @email, " +
                        " dataultimamodifica = @dataultimamodifica, " +
                        " ismailconfermata = @ismailconfermata " +
                        "WHERE id = @id; " +
                        "SELECT * " +
                        "FROM utente " +
                        "WHERE id = @id; ";
                    return connection.Query<Utente>(sql, utente).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        //public static bool UpdateDataUltimaModificaUtente(int id, string email)
        //{
        //    try
        //    {
        //        using (var connection = new MySqlConnection(ConnectionString))
        //        {
        //            var sql = "SET @LastUpdateID := 0; " +
        //                "UPDATE utente " +
        //                "SET dataultimamodifica = @dataultimamodifica " +
        //                ",Rno = (SELECT @LastUpdateID:= Rno)" +
        //                "WHERE id = @id " +
        //                "AND email = @email; " +
        //                "SELECT @LastUpdateID AS LastUpdateID";

        //            var dataultimamodifica = DateTime.Now;
        //            return connection.Query<int>(sql, new { dataultimamodifica, id, email }).FirstOrDefault() == id;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return false;
        //    }
        //}
    }
}
