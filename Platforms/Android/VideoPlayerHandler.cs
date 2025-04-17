using Microsoft.Maui.Handlers;
using AndroidX.AppCompat.App;
using AndroidX.Core.Content;
using AndroidX.Media3.DataSource;
using AndroidX.Media3.ExoPlayer.Util;
using AndroidX.Media3.ExoPlayer.Dash;
using AndroidX.Media3.ExoPlayer.Source;
using AndroidX.Media3.UI;
using AndroidX.Media3.Common;
using AndroidX.Media3.ExoPlayer.Upstream;
using CommunityToolkit.Mvvm.Messaging;
using Com.Newrelic.Videoagent.Core;
using Com.Newrelic.Videoagent.Exoplayer.Tracker;
using Android.Util;
using Android.Content;
using AndroidX.Media3.ExoPlayer;

namespace MyMauiAppAndroid.Platforms.Android
{
    public class VideoPlayerHandler : ViewHandler<VideoPlayerView, PlayerView>
    {
       private SimpleExoPlayer _player;

        public VideoPlayerHandler() : base(ViewMapper)
        {
        }

        protected override PlayerView CreatePlatformView()
        {
            var playerView = new PlayerView(Context);
            InitializePlayer(playerView);
            SubscribeToMessages();
            return playerView;
        }

        private void InitializePlayer(PlayerView playerView)
        {
             _player = new SimpleExoPlayer.Builder(Context).Build();
            var tracker = new NRTrackerExoPlayer();
            var trackerId = NewRelicVideoAgent.Instance.Start(tracker); 
            NewRelicVideoAgent.Instance.SetUserId("MauiAviAndroid");
            playerView.Player = _player;
            tracker.SetPlayer(_player);
            // Create a DataSource.Factory
            var dataSourceFactory = new DefaultDataSource.Factory(Context);

            // Create a DashMediaSource using MediaSource.Factory
            var dashMediaSource = new DashMediaSource.Factory(dataSourceFactory)
                .CreateMediaSource(MediaItem.FromUri(global::Android.Net.Uri.Parse("https://turtle-tube.appspot.com/t/t2/dash.mpd")));
            
            _player.SetMediaSource(dashMediaSource);
            _player.Prepare();
            _player.PlayWhenReady = true;
        }

        private void SubscribeToMessages()
        {
            WeakReferenceMessenger.Default.Register<PlayVideoMessage>(this, (r, message) =>
            {
                _player.PlayWhenReady = message.Value;
            });

            WeakReferenceMessenger.Default.Register<PauseVideoMessage>(this, (r, message) =>
            {
                _player.PlayWhenReady = !message.Value;
            });
        }

        protected override void DisconnectHandler(PlayerView platformView)
        {
            WeakReferenceMessenger.Default.Unregister<PlayVideoMessage>(this);
            WeakReferenceMessenger.Default.Unregister<PauseVideoMessage>(this);

            _player.Release();
            _player = null;
        }

        // Removed incorrect Exoplayer inner class
    }
}