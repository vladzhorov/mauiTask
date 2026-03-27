namespace SpecialOfferApp.Models;

public sealed class OfferDisplayConfiguration
{
    public OfferTheme Theme { get; init; } = OfferTheme.Light;

    public bool ShowMedia { get; init; } = true;
}
