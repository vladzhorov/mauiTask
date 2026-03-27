using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SpecialOfferApp.Models;

namespace SpecialOfferApp.ViewModels;

public partial class OfferPopupViewModel : ObservableObject
{
    private readonly TaskCompletionSource<OfferResult?> _tcs = new();

    public OfferPopupViewModel(OfferConfiguration config)
    {
        Config = config;

        HeaderText = string.IsNullOrWhiteSpace(config.HeaderText) ? "Special Offer" : config.HeaderText.Trim();
        ShowMedia = config.ShowMedia;

        var symbol = config.Type == OfferType.PercentageDiscount ? "%" : "$";
        var amount = config.Type == OfferType.PercentageDiscount ? "25" : "15";

        ValueText = $"{amount}{symbol}";
        DescriptionText = config.Type == OfferType.PercentageDiscount
            ? "You get a percentage discount today."
            : "You get a fixed amount discount today.";
    }

    public OfferConfiguration Config { get; }

    public Task<OfferResult?> ResultTask => _tcs.Task;

    public string HeaderText { get; }
    public bool ShowMedia { get; }
    public string ValueText { get; }
    public string DescriptionText { get; }

    public event EventHandler? RequestClose;

    [RelayCommand]
    private void Claim()
    {
        _tcs.TrySetResult(new OfferResult
        {
            Accepted = true,
            Message = "Offer was claimed by user."
        });
        RequestClose?.Invoke(this, EventArgs.Empty);
    }

    public void CancelIfNeeded()
    {
        _tcs.TrySetResult(null);
    }
}

