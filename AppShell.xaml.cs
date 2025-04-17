namespace MyMauiAppAndroid;

public partial class AppShell : Shell
{
    public AppShell()
    {
        // Define routes and navigation
        Items.Add(new ShellContent
        {
            Content = new MainPage()
        });
    }
}