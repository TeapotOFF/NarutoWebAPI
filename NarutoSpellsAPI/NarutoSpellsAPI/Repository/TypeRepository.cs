using AutoMapper;
using NarutoCharactersAPI.Data;
using NarutoCharactersAPI.Interfaces;
using NarutoCharactersAPI.Models;

namespace NarutoCharactersAPI.Repository
{
    public class TypeRepository : ITypeRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public TypeRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public bool CreateType(Models.Type type)
        {
            _context.Add(type);
            return Save();
        }

        public bool DeleteType(Models.Type type)
        {
            _context.Remove(type);
            return Save();
        }

        public ICollection<Spell> GetSpellsByType(int typeId)
        {
            return _context.SpellTypes.Where(e => e.TypeId == typeId).Select(s => s.Spell).ToList();
        }

        public Models.Type GetType(int id)
        {
            return _context.Types.Where(t => t.Id == id).FirstOrDefault();
        }
        public ICollection<Models.Type> GetTypes()
        {
            return _context.Types.ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool TypeExists(int id)
        {
            return _context.Types.Any(t => t.Id == id);
        }

        public bool UpdateType(Models.Type type)
        {
            _context.Update(type);
            return Save();
        }
    }
}
