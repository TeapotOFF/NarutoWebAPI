namespace NarutoCharactersAPI.Models
{
    public class Character
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public Affiliation Affiliation { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public ICollection<SpellCharacter> SpellCharacters { get; set; }
    }
}
