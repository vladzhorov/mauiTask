using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SpecialOfferApp.Constants;
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

        HeaderText = OfferConstants.DefaultHeaderText;
        SelectedOfferType = OfferType.PercentageDiscount;
        ShowMedia = true;

        ResultText = OfferConstants.ResultWaiting;
    }

    public ObservableCollection<OfferTheme> Themes { get; }

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsPercentageOffer))]
    [NotifyPropertyChangedFor(nameof(IsFixedAmountOffer))]
    private OfferType _selectedOfferType;

    [ObservableProperty]
    private string _headerText = string.Empty;

    [ObservableProperty]
    private OfferTheme _selectedTheme;

    [ObservableProperty]
    private bool _showMedia;

    [ObservableProperty]
    private string _resultText = string.Empty;

    public bool IsPercentageOffer
    {
        get => SelectedOfferType == OfferType.PercentageDiscount;
        set
        {
            if (value)
                SelectedOfferType = OfferType.PercentageDiscount;
        }
    }

    public bool IsFixedAmountOffer
    {
        get => SelectedOfferType == OfferType.FixedAmount;
        set
        {
            if (value)
                SelectedOfferType = OfferType.FixedAmount;
        }
    }

    [RelayCommand]
    private async Task PreviewAsync()
    {
        var content = new OfferContentConfiguration
        {
            Type = SelectedOfferType,
            HeaderText = HeaderText
        };
        var display = new OfferDisplayConfiguration
        {
            Theme = SelectedTheme,
            ShowMedia = ShowMedia
        };

        var result = await _offerDialogService.ShowAsync(content, display);
        ResultText = result?.Accepted == true
            ? $"Result: {result.Message}"
            : OfferConstants.ResultClosed;
    }
}

