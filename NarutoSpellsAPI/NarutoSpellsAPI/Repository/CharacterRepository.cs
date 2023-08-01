using NarutoCharactersAPI.Data;
using NarutoCharactersAPI.Interfaces;
using NarutoCharactersAPI.Models;

namespace NarutoCharactersAPI.Repository
{
    public class CharacterRepository: ICharacterRepository
    {
        private readonly DataContext _context;

        public CharacterRepository(DataContext context) 
        {
            _context = context;
        }

        public bool CharacterExists(int chareId)
        {
            return _context.Characters.Any(p => p.Id == chareId);
        }

        public bool CreateCharacter(Character character)
        {
            _context.Add(character);
            return Save();
        }

        public bool DeleteCharacter(Character character)
        {
            _context.Remove(character);
            return Save();
        }

        public Character GetCharacter(int id)
        {
            return _context.Characters.Where(c => c.Id == id).FirstOrDefault();
        }

        public Character GetCharacter(string name)
        {
            return _context.Characters.Where(c => c.Name == name).FirstOrDefault();
        }

        public decimal GetCharacterRating(int chareId)
        {
            var review = _context.Reviews.Where(c => c.Character.Id == chareId);

            if (review.Count() <= 0)
                return 0;

            return ((decimal)review.Sum(r => r.Rating) / review.Count());
        }

        public ICollection<Character> GetCharacters()
        {
            return _context.Characters.OrderBy(c => c.Id).ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateCharacter(Character character)
        {
            _context.Update(character);
            return Save();
        }
    }
}
