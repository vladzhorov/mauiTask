using SpecialOfferApp.Models;
using SpecialOfferApp.Popups;
using SpecialOfferApp.ViewModels;

namespace SpecialOfferApp.Services;

public sealed class OfferDialogService : IOfferDialogService
{
    private readonly IServiceProvider _services;

    public OfferDialogService(IServiceProvider services)
    {
        _services = services;
    }

    public async Task<OfferResult?> ShowAsync(OfferContentConfiguration content, OfferDisplayConfiguration display)
    {
        var vm = ActivatorUtilities.CreateInstance<OfferPopupViewModel>(_services, content, display);
        var page = ActivatorUtilities.CreateInstance<OfferPopupPage>(_services, vm);

        await Shell.Current.Navigation.PushModalAsync(page);
        return await vm.ResultTask;
    }
}

