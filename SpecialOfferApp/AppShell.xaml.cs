namespace SpecialOfferApp;

public partial class AppShell : Shell
{
	private readonly MainPage _mainPage;

	public AppShell(MainPage mainPage)
	{
		InitializeComponent();
		_mainPage = mainPage;
		Items.Clear();

		Items.Add(new ShellContent
		{
			Title = "Home",
			Route = "MainPage",
			ContentTemplate = new DataTemplate(() => _mainPage)
		});
	}
}
