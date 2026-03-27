using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SpecialOfferApp.Models;
using SpecialOfferApp.Services;

namespace SpecialOfferApp.ViewModels;

public partial class MainViewModel : ObservableObject
{
    private readonly IOfferDialogService _offerDialogService;

    public MainViewModel(IOfferDialogService offerDialogService)
    {
        _offerDialogService = offerDialogService;

        Themes = new ObservableCollection<OfferTheme>(Enum.GetValues<OfferTheme>());
        SelectedTheme = OfferTheme.Light;

        HeaderText = "Spring Promo";
        OfferType = OfferType.PercentageDiscount;
        ShowMedia = true;

        ResultText = "Result: waiting for user action";
    }

    public ObservableCollection<OfferTheme> Themes { get; }

    [ObservableProperty]
    private OfferType offerType;

    [ObservableProperty]
    private string headerText = string.Empty;

    [ObservableProperty]
    private OfferTheme selectedTheme;

    [ObservableProperty]
    private bool showMedia;

    [ObservableProperty]
    private string resultText = string.Empty;

    [RelayCommand]
    private async Task PreviewAsync()
    {
        var config = new OfferConfiguration
        {
            Type = OfferType,
            HeaderText = HeaderText,
            Theme = SelectedTheme,
            ShowMedia = ShowMedia
        };

        var result = await _offerDialogService.ShowAsync(config);
        ResultText = result?.Accepted == true
            ? $"Result: {result.Message}"
            : "Result: popup was closed without claim.";
    }
}

