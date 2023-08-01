namespace NarutoCharactersAPI.Models
{
    public class SpellCharacter
    {
        public int SpellId { get; set; }
        public int CharacterId { get; set; }
        public Spell Spell { get; set; }
        public Character Character { get; set; }
    }
}
