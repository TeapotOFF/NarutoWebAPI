using NarutoCharactersAPI.Models;

namespace NarutoCharactersAPI.Interfaces
{
    public interface IReviewRepository
    {
        ICollection<Review> GetReviews();
        Review GetReview(int id);
        ICollection<Review> GetReviewOfCharacter(int characterId);
        bool ReviewExists(int id);
        bool CreateReview(Review review);
        bool UpdateReview(Review review);
        bool DeleteReview(Review review);
        bool DeleteReviews(ICollection<Review> reviews);
        bool Save();
    }
}
