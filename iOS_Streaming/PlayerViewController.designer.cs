// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;
using System.CodeDom.Compiler;

namespace StreamingAudio
{
	[Register ("PlayerViewController")]
	partial class PlayerViewController
	{
		[Outlet]
		MonoTouch.UIKit.UILabel playbackTime { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton playPauseButton { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIProgressView progressBar { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel timeLabel { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (playPauseButton != null) {
				playPauseButton.Dispose ();
				playPauseButton = null;
			}

			if (progressBar != null) {
				progressBar.Dispose ();
				progressBar = null;
			}

			if (timeLabel != null) {
				timeLabel.Dispose ();
				timeLabel = null;
			}

			if (playbackTime != null) {
				playbackTime.Dispose ();
				playbackTime = null;
			}
		}
	}
}
