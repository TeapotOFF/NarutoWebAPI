using NarutoCharactersAPI.Models;

namespace NarutoCharactersAPI.Interfaces
{
    public interface ITypeRepository
    {
        ICollection<Models.Type> GetTypes();
        Models.Type GetType(int id);
        ICollection<Spell> GetSpellsByType(int typeId);
        bool TypeExists(int id);
        bool CreateType(Models.Type type);
        bool UpdateType(Models.Type type);
        bool DeleteType(Models.Type type);
        bool Save();
    }
}
