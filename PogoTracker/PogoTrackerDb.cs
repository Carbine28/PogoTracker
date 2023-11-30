using CarbeenoLibrary.Database;
using Microsoft.Data.SqlClient;

// Database
namespace PogoTracker
{
    public class PogoTrackerDb : SQLServerDb
    {

        private string yourOwnConnectionString = "Data Source[Replace with SQL Server name, includign the square brackets];Initial Catalog=PokemonTracker;Integrated Security=True;";
        public PogoTrackerDb(string yourOwnConnectionString) : base(yourOwnConnectionString)
        {

        }




        public void AddPokemon(Pokemon pokemon)
        {
            string query = @$"INSERT INTO pokemons
                VALUES ({pokemon.DexId}, '{pokemon.Name}', {pokemon.Generation}, {pokemon.BaseAttack}, {pokemon.BaseDefense}, {pokemon.BaseStamina}, '{pokemon.PrimaryType}', '{pokemon.SecondaryType}')";
            ExecuteNonQuery(query);
        }

        public Pokemon? GetPokemon(int dexId)
        {
            using (SqlDataReader sr = ExecuteReader(@$"SELECT * FROM pokemons
                WHERE DexId = {dexId} 
            "))
            {
                while (sr.Read())
                {
                    return new Pokemon(
                        sr.GetInt32(0),
                        sr.GetString(1),
                        sr.GetInt32(2),
                        sr.GetInt32(3),
                        sr.GetInt32(4),
                        sr.GetInt32(5),
                        sr.GetString(6),
                        sr.GetString(7)
                    );
                }
                sr.Close();
            }
            return null;
        }

        public List<Pokemon> GetAllPokemons() 
        {
            using (SqlDataReader sr = ExecuteReader(@"SELECT * FROM pokemons"))
            {
                List<Pokemon> pokemons = [];
                while(sr.Read())
                {
                    var pokemon = new Pokemon(
                        sr.GetInt32(0),
                        sr.GetString(1),
                        sr.GetInt32(2),
                        sr.GetInt32(3),
                        sr.GetInt32(4),
                        sr.GetInt32(5),
                        sr.GetString(6),
                        sr.GetString(7)
                    );
                    pokemons.Add(pokemon);
                }
                sr.Close();
                return pokemons;
            }
        }

        public void DeletePokemon(int dexId)
        {
            string query = @$"DELETE FROM pokemons
                WHERE DexId = {dexId}";
            ExecuteNonQuery(query);
        }

    }
}
