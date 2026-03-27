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
    private OfferType _offerType;
    private string _headerText = string.Empty;
    private OfferTheme _selectedTheme;
    private bool _showMedia;
    private string _resultText = string.Empty;

    public MainViewModel(IOfferDialogService offerDialogService)
    {
        _offerDialogService = offerDialogService;

        Themes = new ObservableCollection<OfferTheme>(Enum.GetValues<OfferTheme>());
        SelectedTheme = OfferTheme.Light;

        HeaderText = OfferConstants.DefaultHeaderText;
        OfferType = OfferType.PercentageDiscount;
        ShowMedia = true;

        ResultText = OfferConstants.ResultWaiting;
    }

    public ObservableCollection<OfferTheme> Themes { get; }

    public OfferType OfferType
    {
        get => _offerType;
        set
        {
            if (!SetProperty(ref _offerType, value))
                return;

            OnPropertyChanged(nameof(IsPercentageOffer));
            OnPropertyChanged(nameof(IsFixedAmountOffer));
        }
    }

    public string HeaderText
    {
        get => _headerText;
        set => SetProperty(ref _headerText, value);
    }

    public OfferTheme SelectedTheme
    {
        get => _selectedTheme;
        set => SetProperty(ref _selectedTheme, value);
    }

    public bool ShowMedia
    {
        get => _showMedia;
        set => SetProperty(ref _showMedia, value);
    }

    public string ResultText
    {
        get => _resultText;
        set => SetProperty(ref _resultText, value);
    }

    public bool IsPercentageOffer
    {
        get => OfferType == OfferType.PercentageDiscount;
        set
        {
            if (value)
                OfferType = OfferType.PercentageDiscount;
        }
    }

    public bool IsFixedAmountOffer
    {
        get => OfferType == OfferType.FixedAmount;
        set
        {
            if (value)
                OfferType = OfferType.FixedAmount;
        }
    }

    [RelayCommand]
    private async Task PreviewAsync()
    {
        var content = new OfferContentConfiguration
        {
            Type = OfferType,
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

