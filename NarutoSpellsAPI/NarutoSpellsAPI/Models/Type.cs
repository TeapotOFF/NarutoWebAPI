namespace NarutoCharactersAPI.Models
{
    public class Type
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<SpellType> SpellTypes { get; set; }  
    }
}
