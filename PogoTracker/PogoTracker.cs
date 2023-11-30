// Main Tracker Application
namespace PogoTracker
{
    public class PogoTracker 
    {
        private readonly PoGoRequest _pogoRequest;
        private readonly PogoTrackerDb _pogoTrackerDb;

        public PogoTracker(PoGoRequest pogoRequest, PogoTrackerDb pogoTrackerDb)
        {
            this._pogoRequest = pogoRequest;
            this._pogoTrackerDb = pogoTrackerDb;
        }

        public void Run()
        {
            Console.WriteLine("Running PogoTracker");
            Thread.Sleep(500);
            while (true)
            {
                PrintCommands();
                Console.Write("Enter Command: ");
                string input = Console.ReadLine();
                Console.Clear();
                if (input == "0")
                {
                    Exit();
                    break;
                }
                else if (input == "1")
                {
                    ListAllPokemon();
                }
                else if (input == "2")
                {
                     AddPokemon().Wait();
                }
                else if (input == "3")
                {
                    DeletePokemon();
                }
                else
                {
                    Console.WriteLine("Invalid Command");
                }
            }
        }

        public async Task AddPokemon()
        {
            Console.WriteLine("Enter in DexId of pokemon you wish to add: ");
            int inputId;
            while(!int.TryParse(Console.ReadLine(), out inputId))
            {
                Console.WriteLine("Invalid Input");
            }

            // Check if pokemon already exists in database
            var pokemon = _pogoTrackerDb.GetPokemon(inputId);
            if (pokemon != null)
            {
                Console.WriteLine("Pokemon already exists in database");
                return;
            }

            var pokemonDTO = await _pogoRequest.ProcessDexIdAsync(inputId);
            if (pokemonDTO == null)
            {
                Console.WriteLine($"Pokemon with ID of {inputId} does not exist");
                return;
            }
            Console.WriteLine(pokemonDTO);
            var newPokemon = new Pokemon
            (
                pokemonDTO.DexId,
                pokemonDTO.Name.English,
                pokemonDTO.Generation,
                pokemonDTO.Stats.Attack,
                pokemonDTO.Stats.Defense,
                pokemonDTO.Stats.Stamina,
                pokemonDTO.PrimaryType.Name.English,
                pokemonDTO.SecondaryType?.Name.English
            );
            _pogoTrackerDb.AddPokemon(newPokemon);
            ListAllPokemon();
        }

        public void DeletePokemon()
        {
            ListAllPokemon();
            Console.WriteLine("Enter in DexId of pokemon you wish to delete: ");
            int input;
            while(!int.TryParse(Console.ReadLine(), out input))
            {
                Console.WriteLine("Invalid Input");
            }
            _pogoTrackerDb.DeletePokemon(input);
        }

        public void ListAllPokemon()
        {
            Console.WriteLine("Listing all Pokemon in Database");
            var pokemonList = _pogoTrackerDb.GetAllPokemons();
            Console.WriteLine("----------------------------");
            foreach (Pokemon pokemon in pokemonList)
            {
                Console.WriteLine(pokemon.ToString());
            }
        }

        public static void PrintCommands()
        {
            Console.WriteLine("Commands:");
            Console.WriteLine("----------------------------");
            Console.WriteLine("Type 0 to Close Application.");
            Console.WriteLine("Type 1 to List all Pokemon in Database.");
            Console.WriteLine("Type 2 to Add a new Pokemon");
            Console.WriteLine("Type 3 to Delete an existing pokemon");
            Console.WriteLine("----------------------------");
        }

        public static void Exit()
        {
            Console.WriteLine("Exiting PogoTracker");
        }
    }
}
