namespace NarutoCharactersAPI.Models
{
    public class SpellType
    {
        public int SpellId { get; set; }
        public int TypeId { get; set; }
        public Spell Spell { get; set; }
        public Type Type { get; set; }
    }
}
