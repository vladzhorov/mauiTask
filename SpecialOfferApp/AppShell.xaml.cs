namespace SpecialOfferApp;

public partial class AppShell : Shell
{
	public AppShell(MainPage mainPage)
	{
		InitializeComponent();
		Items.Clear();

		Items.Add(new ShellContent
		{
			Title = "Home",
			Route = "MainPage",
			ContentTemplate = new DataTemplate(() => mainPage)
		});
	}
}
