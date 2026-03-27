namespace SpecialOfferApp.Models;

public sealed class OfferResult
{
    public bool Accepted { get; init; }

    public string Message { get; init; } = string.Empty;
}
