namespace MyMauiAppAndroid;

public partial class App : Application
{
	public App()
	{
		// MainPage = new MainPage();
	}

	protected override Window CreateWindow(IActivationState? activationState)
	{
		return new Window(new AppShell());
	}
}