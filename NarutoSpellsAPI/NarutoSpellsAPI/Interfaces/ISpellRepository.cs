using NarutoCharactersAPI.Models;

namespace NarutoCharactersAPI.Interfaces
{
    public interface ISpellRepository
    {
        ICollection<Spell> GetSpells();
        Spell GetSpell(int spellId);
        ICollection<Spell> GetSpellOfCharacter(int characterId);
        ICollection<Character> GetCharacterBySpell(int spellId);
        bool SpellExists(int spellId);
        bool CreateSpell(int characterId, int typeId, Spell spell);
        bool UpdateSpell(int characterId, int typeId, Spell spell);
        bool DeleteSpell(Spell spell);
        bool Save();
    }
}
