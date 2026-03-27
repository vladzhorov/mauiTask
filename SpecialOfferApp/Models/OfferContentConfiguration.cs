namespace SpecialOfferApp.Models;

public sealed class OfferContentConfiguration
{
    public OfferType Type { get; init; } = OfferType.PercentageDiscount;

    public string HeaderText { get; init; } = string.Empty;
}
