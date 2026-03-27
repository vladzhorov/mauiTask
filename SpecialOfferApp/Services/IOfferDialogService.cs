using SpecialOfferApp.Models;

namespace SpecialOfferApp.Services;

public interface IOfferDialogService
{
    Task<OfferResult?> ShowAsync(OfferContentConfiguration content, OfferDisplayConfiguration display);
}

