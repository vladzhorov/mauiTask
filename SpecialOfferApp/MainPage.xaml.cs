using SpecialOfferApp.ViewModels;

namespace SpecialOfferApp;

public partial class MainPage : ContentPage
{
	public MainPage(MainViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}
