using System;
using System.Runtime.InteropServices;

namespace Arookas.Audio
{
	// Token: 0x0200005C RID: 92
	public sealed class Timer : IDisposable
	{
		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x0600029E RID: 670 RVA: 0x00009E64 File Offset: 0x00008064
		public bool IsRunning
		{
			get
			{
				if (this.isDisposed)
				{
					throw new ObjectDisposedException("Timer");
				}
				return this.isRunning;
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x0600029F RID: 671 RVA: 0x00009E81 File Offset: 0x00008081
		public static uint MaximumPeriod
		{
			get
			{
				return Timer.capabilities.maximumPeriod;
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x060002A0 RID: 672 RVA: 0x00009E8D File Offset: 0x0000808D
		public static uint MinimumPeriod
		{
			get
			{
				return Timer.capabilities.minimumPeriod;
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x060002A1 RID: 673 RVA: 0x00009E99 File Offset: 0x00008099
		// (set) Token: 0x060002A2 RID: 674 RVA: 0x00009EB8 File Offset: 0x000080B8
		public TimerMode Mode
		{
			get
			{
				if (this.isDisposed)
				{
					throw new ObjectDisposedException("Timer");
				}
				return this.mode;
			}
			set
			{
				if (this.isDisposed)
				{
					throw new ObjectDisposedException("Timer");
				}
				if (!value.IsDefined<TimerMode>())
				{
					throw new ArgumentOutOfRangeException("value", value, "The specified TimerMode value was not defined.");
				}
				if (this.isRunning)
				{
					throw new InvalidOperationException("The Timer is running and the mode may not be changed.");
				}
				this.mode = value;
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x060002A3 RID: 675 RVA: 0x00009F14 File Offset: 0x00008114
		// (set) Token: 0x060002A4 RID: 676 RVA: 0x00009F34 File Offset: 0x00008134
		public uint Period
		{
			get
			{
				if (this.isDisposed)
				{
					throw new ObjectDisposedException("Timer");
				}
				return this.period;
			}
			set
			{
				if (this.isDisposed)
				{
					throw new ObjectDisposedException("Timer");
				}
				if (value < Timer.MinimumPeriod || value > Timer.MaximumPeriod)
				{
					throw new ArgumentOutOfRangeException("value", value, "The specified Period was outside of the range specified by Timer.MinimumPeriod and Timer.MaximumPeriod.");
				}
				if (this.isRunning)
				{
					throw new InvalidOperationException("The Timer is running and the period may not be changed.");
				}
				this.period = value;
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x060002A5 RID: 677 RVA: 0x00009F98 File Offset: 0x00008198
		// (set) Token: 0x060002A6 RID: 678 RVA: 0x00009FB7 File Offset: 0x000081B7
		public uint Resolution
		{
			get
			{
				if (this.isDisposed)
				{
					throw new ObjectDisposedException("Timer");
				}
				return this.resolution;
			}
			set
			{
				if (this.isDisposed)
				{
					throw new ObjectDisposedException("Timer");
				}
				if (this.isRunning)
				{
					throw new InvalidOperationException("The Timer is running and the resolution may not be changed.");
				}
				this.resolution = value;
			}
		}

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x060002A7 RID: 679 RVA: 0x00009FEC File Offset: 0x000081EC
		// (remove) Token: 0x060002A8 RID: 680 RVA: 0x0000A024 File Offset: 0x00008224
		public event EventHandler Started;

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x060002A9 RID: 681 RVA: 0x0000A05C File Offset: 0x0000825C
		// (remove) Token: 0x060002AA RID: 682 RVA: 0x0000A094 File Offset: 0x00008294
		public event EventHandler Stopped;

		// Token: 0x14000009 RID: 9
		// (add) Token: 0x060002AB RID: 683 RVA: 0x0000A0CC File Offset: 0x000082CC
		// (remove) Token: 0x060002AC RID: 684 RVA: 0x0000A104 File Offset: 0x00008304
		public event EventHandler Tick;

		// Token: 0x060002AD RID: 685 RVA: 0x0000A139 File Offset: 0x00008339
		public Timer() : this(Timer.MinimumPeriod, 1U, TimerMode.Periodic)
		{
		}

		// Token: 0x060002AE RID: 686 RVA: 0x0000A148 File Offset: 0x00008348
		public Timer(uint period) : this(period, 1U, TimerMode.Periodic)
		{
		}

		// Token: 0x060002AF RID: 687 RVA: 0x0000A153 File Offset: 0x00008353
		public Timer(uint period, uint resolution) : this(period, resolution, TimerMode.Periodic)
		{
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x0000A160 File Offset: 0x00008360
		public Timer(uint period, uint resolution, TimerMode mode)
		{
			if (period < Timer.MinimumPeriod || period > Timer.MaximumPeriod)
			{
				throw new ArgumentOutOfRangeException("period", period, "The specified period was outside of the range specified by Timer.MinimumPeriod and Timer.MaximumPeriod.");
			}
			if (!mode.IsDefined<TimerMode>())
			{
				throw new ArgumentOutOfRangeException("mode", mode, "The specified TimerMode value was not defined.");
			}
			this.period = period;
			this.resolution = resolution;
			this.mode = mode;
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x0000A1D2 File Offset: 0x000083D2
		static Timer()
		{
			Timer.GetTimerCapabilities(out Timer.capabilities, MarshalHelper.SizeOf<Timer.TimerCapabilities>());
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x0000A1E4 File Offset: 0x000083E4
		public void Dispose()
		{
			if (!this.isDisposed)
			{
				if (this.isRunning)
				{
					this.Stop();
				}
				this.isDisposed = true;
			}
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x0000A207 File Offset: 0x00008407
		private void OnOneShotTick(uint timerId, uint message, IntPtr userData, IntPtr parameter1, IntPtr parameter2)
		{
			if (this.isDisposed || !this.isRunning)
			{
				return;
			}
			this.OnTick(EventArgs.Empty);
			this.Stop();
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x0000A22D File Offset: 0x0000842D
		private void OnPeriodicTick(uint timerId, uint message, IntPtr userData, IntPtr parameter1, IntPtr parameter2)
		{
			if (this.isDisposed || !this.isRunning)
			{
				return;
			}
			this.OnTick(EventArgs.Empty);
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x0000A250 File Offset: 0x00008450
		private void OnStarted(EventArgs eventArguments)
		{
			if (eventArguments == null)
			{
				throw new ArgumentNullException("eventArguments");
			}
			EventHandler started = this.Started;
			if (started != null)
			{
				started(this, EventArgs.Empty);
			}
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x0000A284 File Offset: 0x00008484
		private void OnStopped(EventArgs eventArguments)
		{
			if (eventArguments == null)
			{
				throw new ArgumentNullException("eventArguments");
			}
			EventHandler stopped = this.Stopped;
			if (stopped != null)
			{
				stopped(this, EventArgs.Empty);
			}
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x0000A2B8 File Offset: 0x000084B8
		private void OnTick(EventArgs eventArguments)
		{
			if (eventArguments == null)
			{
				throw new ArgumentNullException("eventArguments");
			}
			EventHandler tick = this.Tick;
			if (tick != null)
			{
				tick(this, eventArguments);
			}
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x0000A2E8 File Offset: 0x000084E8
		public void Start()
		{
			if (this.isDisposed)
			{
				throw new ObjectDisposedException("Timer");
			}
			if (this.isRunning)
			{
				throw new InvalidOperationException("The Timer is already running.");
			}
			switch (this.mode)
			{
			case TimerMode.OneShot:
				this.tickMethod = new Timer.TimerTick(this.OnOneShotTick);
				break;
			case TimerMode.Periodic:
				this.tickMethod = new Timer.TimerTick(this.OnPeriodicTick);
				break;
			default:
				throw new InvalidOperationException("Failed to start the Timer because the mode is invalid.");
			}
			this.timerID = Timer.StartTimer(this.period, this.resolution, this.tickMethod, IntPtr.Zero, (uint)this.mode);
			if (this.timerID == 0U)
			{
				throw new Exception("Failed to start the Timer.");
			}
			this.isRunning = true;
			this.OnStarted(EventArgs.Empty);
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x0000A3BC File Offset: 0x000085BC
		public void Stop()
		{
			if (this.isDisposed)
			{
				throw new ObjectDisposedException("Timer");
			}
			if (!this.isRunning)
			{
				throw new InvalidOperationException("The Timer has already been stopped.");
			}
			if (Timer.StopTimer(this.timerID) != Timer.TimerResult.NoError)
			{
				throw new Exception("Failed to stop the Timer.");
			}
			this.isRunning = false;
			this.OnStopped(EventArgs.Empty);
		}

		// Token: 0x060002BA RID: 698
		[DllImport("winmm.dll", EntryPoint = "timeSetEvent")]
		private static extern uint StartTimer(uint delay, uint resolution, Timer.TimerTick tickDelegate, IntPtr userData, uint flags);

		// Token: 0x060002BB RID: 699
		[DllImport("winmm.dll", EntryPoint = "timeKillEvent")]
		private static extern Timer.TimerResult StopTimer(uint timerID);

		// Token: 0x060002BC RID: 700
		[DllImport("winmm.dll", EntryPoint = "timeGetDevCaps")]
		private static extern Timer.TimerResult GetTimerCapabilities(out Timer.TimerCapabilities capabilities, int cbSize);

		// Token: 0x0400017F RID: 383
		private static Timer.TimerCapabilities capabilities;

		// Token: 0x04000180 RID: 384
		private volatile bool isDisposed;

		// Token: 0x04000181 RID: 385
		private bool isRunning;

		// Token: 0x04000182 RID: 386
		private volatile TimerMode mode;

		// Token: 0x04000183 RID: 387
		private volatile uint period;

		// Token: 0x04000184 RID: 388
		private volatile uint resolution;

		// Token: 0x04000188 RID: 392
		private Timer.TimerTick tickMethod;

		// Token: 0x04000189 RID: 393
		private uint timerID;

		// Token: 0x0200005D RID: 93
		// (Invoke) Token: 0x060002BE RID: 702
		private delegate void TimerTick(uint timerID, uint message, IntPtr userData, IntPtr parameter1, IntPtr parameter2);

		// Token: 0x0200005E RID: 94
		private struct TimerCapabilities
		{
			// Token: 0x0400018A RID: 394
			public uint minimumPeriod;

			// Token: 0x0400018B RID: 395
			public uint maximumPeriod;
		}

		// Token: 0x0200005F RID: 95
		private enum TimerResult
		{
			// Token: 0x0400018D RID: 397
			NoError,
			// Token: 0x0400018E RID: 398
			InvalidParameter = 11
		}
	}
}
