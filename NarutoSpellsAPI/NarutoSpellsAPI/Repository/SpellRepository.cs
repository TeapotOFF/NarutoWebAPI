using NarutoCharactersAPI.Data;
using NarutoCharactersAPI.Interfaces;
using NarutoCharactersAPI.Models;

namespace NarutoCharactersAPI.Repository
{
    public class SpellRepository : ISpellRepository
    {
        private readonly DataContext _context;

        public SpellRepository(DataContext context) 
        {
            _context = context;
        }

        public bool CreateSpell(int characterId, int typeId, Spell spell)
        {
            var spellCharacterEntity = _context.Characters.Where(c => c.Id == characterId).FirstOrDefault();
            var spellTypeEntity = _context.Types.Where(t => t.Id == typeId).FirstOrDefault();

            var spellCharacter = new SpellCharacter()
            {
                Character = spellCharacterEntity,
                Spell = spell,
            };

            _context.Add(spellCharacter);

            var spellType = new SpellType()
            {
                Type = spellTypeEntity,
                Spell = spell,
            };

            _context.Add(spellType);

            _context.Add(spell);

            return Save();
        }

        public bool DeleteSpell(Spell spell)
        {
            _context.Remove(spell);
            return Save();
        }

        public ICollection<Character> GetCharacterBySpell(int spellId)
        {
            return _context.SpellCharacters.Where(p => p.Spell.Id == spellId).Select(c => c.Character).ToList();
        }

        public Spell GetSpell(int spellId)
        {
            return _context.Spells.Where(s => s.Id == spellId).FirstOrDefault();
        }

        public ICollection<Spell> GetSpellOfCharacter(int characterId)
        {
            return _context.SpellCharacters.Where(c => c.Character.Id == characterId).Select(s => s.Spell).ToList();
        }

        public ICollection<Spell> GetSpells()
        {
            return _context.Spells.ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool SpellExists(int spellId)
        {
            return _context.Spells.Any(s => s.Id == spellId);
        }

        public bool UpdateSpell(int characterId, int typeId, Spell spell)
        {
            _context.Update(spell);
            return Save();
        }
    }
}
