namespace NarutoCharactersAPI.Models
{
    public class Affiliation
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Character> Characters { get; set; }
    }
}
