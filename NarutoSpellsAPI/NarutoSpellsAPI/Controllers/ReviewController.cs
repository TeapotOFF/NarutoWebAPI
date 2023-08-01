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
    public class ReviewController: Controller
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IMapper _mapper;
        private readonly ICharacterRepository _characterRepository;
        private readonly IReviewerRepository _reviewerRepository;

        public ReviewController(IReviewRepository reviewRepository, IMapper mapper, 
            ICharacterRepository characterRepository, IReviewerRepository reviewerRepository)
        {
            _reviewRepository = reviewRepository;
            _mapper = mapper;
            _characterRepository = characterRepository;
            _reviewerRepository = reviewerRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Review>))]
        public IActionResult GetReviews()
        {
            var reviews = _mapper.Map<List<ReviewDto>>(_reviewRepository.GetReviews());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(reviews);
        }

        [HttpGet("{reviewId}")]
        [ProducesResponseType(200, Type = typeof(Review))]
        [ProducesResponseType(400)]
        public IActionResult GetReview(int reviewId)
        {
            if (!_reviewRepository.ReviewExists(reviewId))
                return NotFound();

            var review = _mapper.Map<ReviewDto>(_reviewRepository.GetReview(reviewId));

            if (!ModelState.IsValid)
                return BadRequest(review);

            return Ok(review);
        }

        [HttpGet("character/{characterId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Review>))]
        [ProducesResponseType(400)]
        public IActionResult GetReviewOfCharacter(int characterId)
        {
            var review = _mapper.Map<List<ReviewDto>>(_reviewRepository.GetReviewOfCharacter(characterId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(review);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public IActionResult CraeteReview([FromQuery] int reviewerId, int characterId, [FromBody] ReviewDto reviewCreate)
        {
            if (reviewCreate == null)
                return BadRequest(ModelState);

            var review = _reviewRepository.GetReviews()
                .Where(t => t.Title.Trim().ToUpper() == reviewCreate.Title.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (review != null)
            {
                ModelState.AddModelError("", "Review already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var reviewMap = _mapper.Map<Review>(reviewCreate);

            reviewMap.Character = _characterRepository.GetCharacter(characterId);
            reviewMap.Reviewer = _reviewerRepository.GetReviewer(reviewerId);


            if (!_reviewRepository.CreateReview(reviewMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("{reviewId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateType(int reviewId, [FromBody] ReviewerDto updateReview)
        {
            if (updateReview == null)
                return BadRequest(ModelState);

            if (reviewId != updateReview.Id)
                return BadRequest(ModelState);

            if (!_reviewRepository.ReviewExists(reviewId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var reviewMap = _mapper.Map<Review>(updateReview);

            if (!_reviewRepository.UpdateReview(reviewMap))
            {
                ModelState.AddModelError("", "Something went wrong updating review");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{reviwId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteType(int reviwId)
        {
            if (!_reviewRepository.ReviewExists(reviwId))
            {
                return NotFound();
            }

            var reviewToDelte = _reviewRepository.GetReview(reviwId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (_reviewRepository.DeleteReview(reviewToDelte))
            {
                ModelState.AddModelError("", "Something went wrong deleting spell");
            }

            return NoContent();
        }
    }
}
