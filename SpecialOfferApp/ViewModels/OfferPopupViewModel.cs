using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SpecialOfferApp.Constants;
using SpecialOfferApp.Models;

namespace SpecialOfferApp.ViewModels;

public partial class OfferPopupViewModel : ObservableObject
{
    private readonly TaskCompletionSource<OfferResult?> _tcs = new();

    public OfferPopupViewModel(OfferContentConfiguration content, OfferDisplayConfiguration display)
    {
        Theme = display.Theme;

        HeaderText = string.IsNullOrWhiteSpace(content.HeaderText)
            ? OfferConstants.FallbackHeaderText
            : content.HeaderText.Trim();
        ShowMedia = display.ShowMedia;

        var symbol = content.Type == OfferType.PercentageDiscount
            ? OfferConstants.PercentageSymbol
            : OfferConstants.CurrencySymbol;
        var amount = content.Type == OfferType.PercentageDiscount
            ? OfferConstants.PercentageValue
            : OfferConstants.FixedAmountValue;

        ValueText = $"{amount}{symbol}";
        DescriptionText = content.Type == OfferType.PercentageDiscount
            ? OfferConstants.PercentageDescription
            : OfferConstants.FixedDescription;
    }

    public Task<OfferResult?> ResultTask => _tcs.Task;

    public string HeaderText { get; }
    public bool ShowMedia { get; }
    public string ValueText { get; }
    public string DescriptionText { get; }
    public OfferTheme Theme { get; }

    public event EventHandler? RequestClose;

    [RelayCommand]
    private void Claim()
    {
        _tcs.TrySetResult(new OfferResult
        {
            Accepted = true,
            Message = OfferConstants.ResultClaimed
        });
        RequestClose?.Invoke(this, EventArgs.Empty);
    }

    public void CancelIfNeeded()
    {
        _tcs.TrySetResult(null);
    }
}

