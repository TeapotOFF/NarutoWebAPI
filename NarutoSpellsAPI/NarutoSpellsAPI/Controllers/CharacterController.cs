using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NarutoCharactersAPI.Dto;
using NarutoCharactersAPI.Interfaces;
using NarutoCharactersAPI.Models;
using NarutoCharactersAPI.Repository;

namespace NarutoCharactersAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharacterController: Controller
    {
        private readonly ICharacterRepository _characterRepository;
        private readonly IReviewRepository _reviewRepository;
        private readonly IAffiliationRepository _affiliationRepository;
        private readonly IMapper _mapper;

        public CharacterController(ICharacterRepository characterRepository,
            IReviewRepository reviewRepository,
            IAffiliationRepository affiliationRepository, IMapper mapper)
        {
            _characterRepository = characterRepository;
            _reviewRepository = reviewRepository;
            _affiliationRepository = affiliationRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Character>))]
        public IActionResult GetCharacters()
        {
            var characters = _mapper.Map<List<CharacterDto>>(_characterRepository.GetCharacters());

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(characters);
        }

        [HttpGet("{chareId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Character>))]
        [ProducesResponseType(400)]
        public IActionResult GetCharacter(int chareId)
        {
            if(!_characterRepository.CharacterExists(chareId))
                return NotFound();

            var character = _mapper.Map<CharacterDto>(_characterRepository.GetCharacter(chareId));

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(character);
        }

        [HttpGet("{chareId}/rating")]
        [ProducesResponseType(200, Type = typeof(decimal))]
        [ProducesResponseType(400)]
        public IActionResult GetCharacterRating(int chareId)
        {
            if (!_characterRepository.CharacterExists(chareId))
                return NotFound();

            var rating = _characterRepository.GetCharacterRating(chareId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(rating);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public IActionResult CraeteSpell([FromQuery] int affilidationId, [FromBody] CharacterDto characterCreate)
        {
            if (characterCreate == null)
                return BadRequest(ModelState);

            var character = _characterRepository.GetCharacters()
                .Where(t => t.Name.Trim().ToUpper() == characterCreate.Name.Trim().ToUpper())
                .FirstOrDefault();

            if (character != null)
            {
                ModelState.AddModelError("", "Character already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var characterMap = _mapper.Map<Character>(characterCreate);

            characterMap.Affiliation = _affiliationRepository.GetAffiliation(affilidationId);

            if (!_characterRepository.CreateCharacter(characterMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("{characterId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateCharacter(int characterId, [FromBody] CharacterDto updateCharacter)
        {
            if (updateCharacter == null)
                return BadRequest(ModelState);

            if (characterId != updateCharacter.Id)
                return BadRequest(ModelState);

            if (!_characterRepository.CharacterExists(characterId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var characterMap = _mapper.Map<Character>(updateCharacter);

            if (!_characterRepository.UpdateCharacter(characterMap))
            {
                ModelState.AddModelError("", "Something went wrong updating character");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{characterId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteCharacters(int characterId)
        {
            if (!_characterRepository.CharacterExists(characterId))
            {
                return NotFound();
            }

            var reviewsToDelete = _reviewRepository.GetReviewOfCharacter(characterId);
            var characterToDelete = _characterRepository.GetCharacter(characterId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if(_reviewRepository.DeleteReviews(reviewsToDelete.ToList()))
            {
                ModelState.AddModelError("", "Somethiing went wrong when deleting reviews");
            }

            if (_characterRepository.DeleteCharacter(characterToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting character");
            }

            return NoContent();
        }
    }
}
