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
    public class SpellController: Controller
    {
        private readonly ISpellRepository _spellRepository;
        private readonly IReviewRepository _reviewRepository;
        private readonly ICharacterRepository _characterRepository;
        private readonly IMapper _mapper;

        public SpellController(ISpellRepository spellRepository,
            IReviewRepository reviewRepository,
            ICharacterRepository characterRepository,
            IMapper mapper) 
        {
            _spellRepository = spellRepository;
            _reviewRepository = reviewRepository;
            _characterRepository = characterRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Spell>))]
        public IActionResult GetSpells()
        {
            var spells = _mapper.Map<List<SpellDto>>(_spellRepository.GetSpells());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(spells);
        }

        [HttpGet("{spellId}")]
        [ProducesResponseType(200, Type = typeof(Spell))]
        [ProducesResponseType(400)]
        public IActionResult GetSpell(int spellId)
        {
            if (!_spellRepository.SpellExists(spellId))
                return NotFound();

            var spell = _mapper.Map<SpellDto>(_spellRepository.GetSpell(spellId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(spell);
        }
        [HttpGet("{spellId}/character")]
        [ProducesResponseType(200, Type = typeof(Spell))]
        [ProducesResponseType(400)]
        public IActionResult GetCharacterBySpell(int spellId)
        {
            if (!_spellRepository.SpellExists(spellId))
                return NotFound();

            var character = _mapper.Map<List<CharacterDto>>(_spellRepository.GetCharacterBySpell(spellId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(character);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public IActionResult CraeteSpell([FromQuery] int characterId, [FromQuery] int typeId, [FromBody] SpellDto spellCreate)
        {
            if (spellCreate == null)
                return BadRequest(ModelState);

            var spell = _spellRepository.GetSpells()
                .Where(t => t.Name.Trim().ToUpper() == spellCreate.Name.Trim().ToUpper())
                .FirstOrDefault();

            if (spell != null)
            {
                ModelState.AddModelError("", "Spell already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var spellMap = _mapper.Map<Spell>(spellCreate);

            if (!_spellRepository.CreateSpell(characterId, typeId, spellMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("{spelId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateCharacter(int spelId, [FromQuery] int characterId, 
            [FromQuery] int typeId, [FromBody] SpellDto updateSpell)
        {
            if (updateSpell == null)
                return BadRequest(ModelState);

            if (spelId != updateSpell.Id)
                return BadRequest(ModelState);

            if (!_spellRepository.SpellExists(spelId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var spellMap = _mapper.Map<Spell>(updateSpell);

            if (!_spellRepository.UpdateSpell(characterId, typeId, spellMap))
            {
                ModelState.AddModelError("", "Something went wrong updating character");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{spellId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteType(int spellId)
        {
            if (!_spellRepository.SpellExists(spellId))
            {
                return NotFound();
            }

            var spellToDelte = _spellRepository.GetSpell(spellId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (_spellRepository.DeleteSpell(spellToDelte))
            {
                ModelState.AddModelError("", "Something went wrong deleting spell");
            }

            return NoContent();
        }
    }
}
