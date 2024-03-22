using rendezvousBistro.Domain.Common.Models;

namespace rendezvousBistro.Domain.Common.ValueObjects;

public sealed class AverageRating : ValueObject
{
    public double Value { get; private set; }

    public int NumRatings { get; private set; }

    private AverageRating(double value, int numRatings)
    {
        Value = value;
        NumRatings = numRatings;
    }

    public static AverageRating CreateNew(double rating = 0, int numRatings = 0)
    {
        return new AverageRating(rating, numRatings);
    }

    public void AddNewRating(Rating newRating)
    {
        Value = (Value * NumRatings + newRating.Value) / ++NumRatings;
    }

    public void RemoveRating(Rating ratingToRemove)
    {
        Value = (Value * NumRatings - ratingToRemove.Value) / --NumRatings;
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}