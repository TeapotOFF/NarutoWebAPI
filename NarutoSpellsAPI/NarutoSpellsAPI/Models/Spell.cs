namespace NarutoCharactersAPI.Models
{
    public class Spell
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<SpellCharacter> SpellCharacters { get; set; }
        public ICollection<SpellType> SpellTypes { get; set; }
    }
}
