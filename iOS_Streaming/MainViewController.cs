using System;
using MonoTouch.UIKit;

namespace StreamingAudio
{
    public enum PlayerOption
    {
        Stream = 0,
        StreamAndSave
    }

    public partial class MainViewController : UIViewController
    {

		private const string LetsStopTheWarUrl = "http://video.kenhtin.net/music_file/files/audio/blue-jazz/39719/o71t2g8qxfsWTwzd/1a3a3ce8449036260215277b82c9471e.mp3";

        public MainViewController() : base("MainViewController", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            Title = "Streaming MP3";

            streamAndPlayButton.TouchUpInside += (sender, e) => OpenPlayerView(PlayerOption.Stream);

            urlTextbox.Text = LetsStopTheWarUrl;
            urlTextbox.EditingDidEnd += (sender, e) =>
            {
                urlTextbox.ResignFirstResponder();
            };
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            statusLabel.Text = string.Empty;
        }

        private void OpenPlayerView(PlayerOption option)
        {
            statusLabel.Text = "Starting HTTP request";
            var url = string.IsNullOrEmpty(urlTextbox.Text) ? LetsStopTheWarUrl : urlTextbox.Text;
            var playerViewController = new PlayerViewController(option, url);
            playerViewController.ErrorOccurred += HandleError;
            NavigationController.PushViewController(playerViewController, true);
        }

        private void HandleError(string message)
        {
            InvokeOnMainThread(delegate
            {
                statusLabel.Text = message;
            });
        }
    }
}

