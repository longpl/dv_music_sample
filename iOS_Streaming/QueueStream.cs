﻿using System;
using System.Diagnostics;
using System.Threading;
using System.IO;

namespace StreamingAudio
{
	public class QueueStream : Stream
	{
		private Stream writeStream;
		private Stream readStream;
		private long size;
		private bool done;
		private object plock = new object ();

		public QueueStream (string storage)
		{
			writeStream = new FileStream (storage, FileMode.Create, FileAccess.Write, FileShare.ReadWrite, 8192);
			readStream = new FileStream (storage, FileMode.Open, FileAccess.Read, FileShare.ReadWrite, 8192);
		}

		public override bool CanRead {
			get { return true; }
		}

		public override bool CanSeek {
			get { return false; }
		}

		public override bool CanWrite {
			get { return false; }
		}

		public override long Length {
			get { return readStream.Length; }
		}

		public override long Position {
			get { return readStream.Position; }
			set { throw new NotImplementedException (); }
		}

		public override int Read (byte[] buffer, int offset, int count)
		{
			lock (plock) {
				while (true) {
					if (Position <= size) {
						int n = readStream.Read (buffer, offset, count);
						return n;
					} else if (done)
						return 0;

					try {
						Debug.WriteLine ("Waiting for data");
						Monitor.Wait (plock);
						Debug.WriteLine ("Waking up, data available");
					} catch {
					}
				}
			}
		}

		public void Push (byte[] buffer, int offset, int count)
		{
			lock (plock) {
				writeStream.Write (buffer, offset, count);
				size += count;
				writeStream.Flush ();
				Monitor.Pulse (plock);
			}
		}

		public void Done ()
		{
			lock (plock) {
				Monitor.Pulse (plock);
				done = true;
			}
		}

		protected override void Dispose (bool disposing)
		{	
			if (disposing) {
				readStream.Close ();
				readStream.Dispose ();
				writeStream.Close ();
				writeStream.Dispose ();
			}
			base.Dispose (disposing);
		}

#region non implemented abstract members of Stream

		public override void Flush ()
		{
		}

		public override long Seek (long offset, SeekOrigin origin)
		{
			throw new NotImplementedException ();
		}

		public override void SetLength (long value)
		{
			throw new NotImplementedException ();
		}

		public override void Write (byte[] buffer, int offset, int count)
		{
			throw new NotImplementedException ();
		}

#endregion

	}
}

