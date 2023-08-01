using NarutoCharactersAPI.Data;
using NarutoCharactersAPI.Interfaces;
using NarutoCharactersAPI.Models;

namespace NarutoCharactersAPI.Repository
{
    public class AffiliationRepository : IAffiliationRepository
    {
        private DataContext _context;
        public AffiliationRepository(DataContext context)
        {
            _context = context;
        }
        public bool AffiliationExists(int id)
        {
            return _context.Affiliation.Any(a => a.Id == id);
        }

        public bool CreateAffiliation(Affiliation affiliation)
        {
            _context.Add(affiliation);
            return Save();
        }

        public bool DeleteAffiliation(Affiliation affiliation)
        {
            _context.Remove(affiliation);
            return Save();
        }

        public Affiliation GetAffiliation(int id)
        {
            return _context.Affiliation.Where(a => a.Id == id).FirstOrDefault();
        }

        public Affiliation GetAffiliationByCharacter(int characterId)
        {
            return _context.Characters.Where(c => c.Id == characterId).Select(a => a.Affiliation).FirstOrDefault();
        }

        public ICollection<Affiliation> GetAffiliations()
        {
            return _context.Affiliation.ToList();
        }

        public ICollection<Character> GetCharactersByAffiliation(int affiliationId)
        {
            return _context.Characters.Where(c => c.Affiliation.Id == affiliationId).ToList();
        }

        public bool Save()
        {
            var save = _context.SaveChanges();
            return save > 0 ? true : false;
        }

        public bool UpdateAffiliation(Affiliation affiliation)
        {
            _context.Update(affiliation);
            return Save();
        }
    }
}
