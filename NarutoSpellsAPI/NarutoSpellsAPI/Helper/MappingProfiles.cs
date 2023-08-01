using AutoMapper;
using NarutoCharactersAPI.Dto;
using NarutoCharactersAPI.Models;

namespace NarutoCharactersAPI.Helper
{
    public class MappingProfiles: Profile
    {
        public MappingProfiles()
        {
            CreateMap<Character, CharacterDto>();
            CreateMap<CharacterDto, Character>();
            CreateMap<Affiliation, AffiliationDto>();
            CreateMap<AffiliationDto, Affiliation>();
            CreateMap<Models.Type, TypeDto>();
            CreateMap<TypeDto, Models.Type>();
            CreateMap<Spell, SpellDto>();
            CreateMap<SpellDto, Spell>();
            CreateMap<Review, ReviewDto>();
            CreateMap<ReviewDto, Review>();
            CreateMap<Reviewer, ReviewerDto>();
            CreateMap<ReviewerDto, Reviewer>();
        }
    }
}
