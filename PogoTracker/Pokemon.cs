namespace PogoTracker
{
    public class Pokemon
    {
        public int DexId { get; set; }
        public string Name { get; set; }
        public int Generation { get; set; }
        public int BaseAttack { get; set; }
        public int BaseDefense { get; set; }
        public int BaseStamina { get; set; }
        public string PrimaryType { get; set; }
        public string SecondaryType { get; set; }

        public Pokemon(int dexId, string name, int generation, int baseAttack, int baseDefense, int baseStamina, string primaryType, string? secondaryType)
        {
            this.DexId = dexId;
            this.Name = name;
            this.Generation = generation;
            this.BaseAttack = baseAttack;
            this.BaseDefense = baseDefense;
            this.BaseStamina = baseStamina;
            this.PrimaryType = primaryType;
            this.SecondaryType = secondaryType;
        }

        public override string ToString()
        {
            return $"DexId: {DexId}, Name: {Name}, Generation: {Generation}, BaseAttack: {BaseAttack}, BaseDefense: {BaseDefense}, BaseStamina: {BaseStamina}, PrimaryType: {PrimaryType}, SecondaryType: {SecondaryType}";
        }
    }
}
