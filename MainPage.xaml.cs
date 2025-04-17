namespace MyMauiAppAndroid;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private void OnPlayClicked(object sender, EventArgs e)
    {
        videoPlayer.Play();
    }

    private void OnPauseClicked(object sender, EventArgs e)
    {
        videoPlayer.Pause();
    }
}