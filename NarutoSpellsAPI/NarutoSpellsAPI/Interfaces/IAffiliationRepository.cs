using NarutoCharactersAPI.Models;

namespace NarutoCharactersAPI.Interfaces
{
    public interface IAffiliationRepository
    {
        ICollection<Affiliation> GetAffiliations();
        Affiliation GetAffiliation(int id);
        Affiliation GetAffiliationByCharacter(int characterId);
        ICollection<Character> GetCharactersByAffiliation(int affiliationId);
        bool AffiliationExists(int id);
        bool CreateAffiliation(Affiliation affiliation);
        bool UpdateAffiliation(Affiliation affiliation);
        bool DeleteAffiliation(Affiliation affiliation);
        bool Save();
    }
}
