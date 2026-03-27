namespace SpecialOfferApp.Models;

public enum OfferType
{
    PercentageDiscount,
    FixedAmount
}

public enum OfferTheme
{
    Light,
    Dark
}

public sealed class OfferConfiguration
{
    public OfferType Type { get; init; } = OfferType.PercentageDiscount;

    public string HeaderText { get; init; } = "Special Offer";

    public OfferTheme Theme { get; init; } = OfferTheme.Light;

    public bool ShowMedia { get; init; } = true;
}
