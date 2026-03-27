using SpecialOfferApp.ViewModels;

namespace SpecialOfferApp.Popups;

public partial class OfferPopupPage : ContentPage
{
    private readonly OfferPopupViewModel _viewModel;

    public OfferPopupPage(OfferPopupViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = viewModel;

        _viewModel.RequestClose += OnRequestClose;
    }

    private async void OnRequestClose(object? sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        _viewModel.RequestClose -= OnRequestClose;
        _viewModel.CancelIfNeeded();
    }
}

