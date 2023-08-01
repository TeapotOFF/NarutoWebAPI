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
    public class AffiliationController: Controller
    {
        private readonly IAffiliationRepository _affiliationRepository;
        private readonly IMapper _mapper;

        public AffiliationController(IAffiliationRepository affiliationRepository, IMapper mapper) 
        {
            _affiliationRepository = affiliationRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Affiliation>))]
        public IActionResult GetAffiliations()
        {
            var affiliations = _mapper.Map<List<AffiliationDto>>(_affiliationRepository.GetAffiliations());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(affiliations);
        }

        [HttpGet("{affiliationId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Affiliation>))]
        [ProducesResponseType(400)]
        public IActionResult GetAffiliation(int affiliationId)
        {
            if (!_affiliationRepository.AffiliationExists(affiliationId))
                return NotFound();

            var affiliation = _mapper.Map<AffiliationDto>(_affiliationRepository.GetAffiliation(affiliationId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(affiliation);
        }

        [HttpGet("/characters/{characterId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Affiliation>))]
        [ProducesResponseType(400)]
        public IActionResult GetAffiliationByCharacters(int characterId)
        {
            var affiliation = _mapper.Map<AffiliationDto>(_affiliationRepository.GetAffiliationByCharacter(characterId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(affiliation);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public IActionResult CraeteAffiliation([FromBody] AffiliationDto affiliationCreate)
        {
            if (affiliationCreate == null)
                return BadRequest(ModelState);

            var affiliation = _affiliationRepository.GetAffiliations()
                .Where(a => a.Name.Trim().ToUpper() == affiliationCreate.Name.Trim().ToUpper())
                .FirstOrDefault();

            if (affiliation != null)
            {
                ModelState.AddModelError("", "Affiliation already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var affiliationMap = _mapper.Map<Affiliation>(affiliationCreate);

            if (!_affiliationRepository.CreateAffiliation(affiliationMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("{affilitaionId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateType(int affilitaionId, [FromBody] AffiliationDto updateAffilitaion)
        {
            if (updateAffilitaion == null)
                return BadRequest(ModelState);

            if (affilitaionId != updateAffilitaion.Id)
                return BadRequest(ModelState);

            if (!_affiliationRepository.AffiliationExists(affilitaionId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var affilitaionMap = _mapper.Map<Affiliation>(updateAffilitaion);

            if (!_affiliationRepository.UpdateAffiliation(affilitaionMap))
            {
                ModelState.AddModelError("", "Something went wrong updating affiliation");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{affiliationId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteType(int affiliationId)
        {
            if (!_affiliationRepository.AffiliationExists(affiliationId))
            {
                return NotFound();
            }

            var affilitaionToDelete = _affiliationRepository.GetAffiliation(affiliationId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (_affiliationRepository.DeleteAffiliation(affilitaionToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting affiliation");
            }

            return NoContent();
        }
    }
}
