using NarutoCharactersAPI.Models;

namespace NarutoCharactersAPI.Interfaces
{
    public interface ICharacterRepository
    {
        ICollection<Character> GetCharacters();
        Character GetCharacter(int id);
        Character GetCharacter(string name);
        decimal GetCharacterRating(int chareId);
        bool CharacterExists(int chareId);
        bool CreateCharacter(Character character);
        bool UpdateCharacter(Character character);
        bool DeleteCharacter(Character character);
        bool Save();
    }
}
