using System;
using System.Text.Json.Serialization;
namespace PogoTracker
{
    public record class PokemonDTO(
        [property: JsonPropertyName("dexNr")] int DexId,
        [property: JsonPropertyName("names")] Name Name,
        [property: JsonPropertyName("generation")] int Generation,
        [property: JsonPropertyName("stats")] Stats Stats,
        [property: JsonPropertyName("primaryType")] PrimaryType PrimaryType,
        [property: JsonPropertyName("secondaryType")] SecondaryType SecondaryType
    );
    
    public record class Name(
        [property: JsonPropertyName("English")] string English
    );

    public record class Stats(
        [property: JsonPropertyName("attack")] int Attack,
        [property: JsonPropertyName("defense")] int Defense,
        [property: JsonPropertyName("stamina")] int Stamina
    );

    public record class PrimaryType(
               [property: JsonPropertyName("names")] Name Name
    );

    public record class SecondaryType(
               [property: JsonPropertyName("names")] Name Name
    );

}
