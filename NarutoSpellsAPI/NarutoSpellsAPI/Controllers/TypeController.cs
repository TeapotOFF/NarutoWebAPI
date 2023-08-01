using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NarutoCharactersAPI.Dto;
using NarutoCharactersAPI.Interfaces;
using NarutoCharactersAPI.Models;

namespace NarutoCharactersAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypeController : Controller
    {
        private readonly ITypeRepository _typeRepository;
        private readonly IMapper _mapper;

        public TypeController(ITypeRepository typeRepository, IMapper mapper)
        {
            _typeRepository = typeRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Models.Type>))]
        public IActionResult GetTypes()
        {
            var types = _mapper.Map<List<TypeDto>>(_typeRepository.GetTypes());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(types);
        }

        [HttpGet("{typeId}")]
        [ProducesResponseType(200, Type = typeof(Models.Type))]
        [ProducesResponseType(400)]
        public IActionResult GetType(int typeId)
        {
            if (!_typeRepository.TypeExists(typeId))
                return NotFound();

            var type = _mapper.Map<TypeDto>(_typeRepository.GetType(typeId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(type);
        }

        [HttpGet("spell/{typeId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Spell>))]
        [ProducesResponseType(400)]
        public IActionResult GetSpellsByType(int typeId)
        {
            var spell = _mapper.Map<List<SpellDto>>(_typeRepository.GetSpellsByType(typeId));


            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(spell);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public IActionResult CraeteType([FromBody] TypeDto typeCreate)
        {
            if (typeCreate == null)
                return BadRequest(ModelState);

            var type = _typeRepository.GetTypes()
                .Where(t => t.Name.Trim().ToUpper() == typeCreate.Name.Trim().ToUpper())
                .FirstOrDefault();

            if (type != null)
            {
                ModelState.AddModelError("", "Type already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var typeMap = _mapper.Map<Models.Type>(typeCreate);

            if (!_typeRepository.CreateType(typeMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("{typeId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateType(int typeId, [FromBody] TypeDto updateType)
        {
            if (updateType == null)
                return BadRequest(ModelState);

            if (typeId != updateType.Id)
                return BadRequest(ModelState);

            if (!_typeRepository.TypeExists(typeId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var typeMap = _mapper.Map<Models.Type>(updateType);

            if (!_typeRepository.UpdateType(typeMap))
            {
                ModelState.AddModelError("", "Something went wrong updating type");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{typeId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteType(int typeId)
        {
            if(!_typeRepository.TypeExists(typeId))
            {
                return NotFound();
            }

            var typeToDelete = _typeRepository.GetType(typeId);

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            if(_typeRepository.DeleteType(typeToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting type");
            }

            return NoContent();
        }
    }
}
