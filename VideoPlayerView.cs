using Microsoft.Maui.Controls;
using CommunityToolkit.Mvvm.Messaging;

namespace MyMauiAppAndroid
{
    public class VideoPlayerView : View
    {
        public void Play()
        {
            WeakReferenceMessenger.Default.Send(new PlayVideoMessage(true));
        }

        public void Pause()
        {
            WeakReferenceMessenger.Default.Send(new PauseVideoMessage(true));
        }
    }

    public class PlayVideoMessage
    {
        public bool Value { get; }

        public PlayVideoMessage(bool value)
        {
            Value = value;
        }
    }

    public class PauseVideoMessage
    {
        public bool Value { get; }

        public PauseVideoMessage(bool value)
        {
            Value = value;
        }
    }
}