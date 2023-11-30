using Microsoft.Data.SqlClient;

/*
 * A basic custom SQL Server database class that can be used to execute queries or be inherited from
 */
namespace CarbeenoLibrary.Database
{
    public class SQLServerDb 
    {
        public string ConnectionString { get; set; }

        public SQLServerDb(string connectionString = "Data Source=(LocalDB)\\LocalDBDemo;Initial Catalog=PokemonTracker;Integrated Security=True;")
        {
            ConnectionString = connectionString;
        }

        public void PrintConnectionString()
        {
            Console.WriteLine(ConnectionString);
        }

        // ExecuteNonQuery is used for INSERT, UPDATE, DELETE, and SET statements
        public void ExecuteNonQuery(string query)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                command.ExecuteNonQuery();
            }
        }
       
        // ExecuteReader is used for SELECT statements
        public SqlDataReader ExecuteReader(string query)
        {
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();
            SqlCommand command = new SqlCommand(query, connection);
            return command.ExecuteReader(); // returns a SqlDataReader object
        }

    }
}
