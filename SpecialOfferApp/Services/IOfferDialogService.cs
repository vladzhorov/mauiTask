using SpecialOfferApp.Models;

namespace SpecialOfferApp.Services;

public interface IOfferDialogService
{
    Task<OfferResult?> ShowAsync(OfferConfiguration config);
}

