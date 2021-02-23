using System;

namespace Arookas.Audio.MusicalInstrumentDigitalInterface
{
	// Token: 0x0200002C RID: 44
	public sealed class MIDIClock : IDisposable
	{
		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000162 RID: 354 RVA: 0x000058D0 File Offset: 0x00003AD0
		// (set) Token: 0x06000163 RID: 355 RVA: 0x000058F1 File Offset: 0x00003AF1
		public uint BeatsPerMinute
		{
			get
			{
				if (this.isDisposed)
				{
					throw new ObjectDisposedException("MIDIClock");
				}
				return 60000000U / this.tempo;
			}
			set
			{
				if (this.isDisposed)
				{
					throw new ObjectDisposedException("MIDIClock");
				}
				if (value < 1U)
				{
					throw new ArgumentException("Beats per minute cannot be zero.", "BeatsPerMinute");
				}
				this.tempo = 60000000U / value;
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000164 RID: 356 RVA: 0x00005927 File Offset: 0x00003B27
		public static uint DefaultTempo
		{
			get
			{
				return 500000U;
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000165 RID: 357 RVA: 0x0000592E File Offset: 0x00003B2E
		public bool IsRunning
		{
			get
			{
				if (this.isDisposed)
				{
					throw new ObjectDisposedException("MIDIClock");
				}
				return this.timer.IsRunning;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000166 RID: 358 RVA: 0x0000594E File Offset: 0x00003B4E
		private uint PeriodResolution
		{
			get
			{
				return this.timer.Period * this.pulsesPerQuarterNote * 1000U;
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000167 RID: 359 RVA: 0x00005968 File Offset: 0x00003B68
		// (set) Token: 0x06000168 RID: 360 RVA: 0x00005983 File Offset: 0x00003B83
		public uint PulsesPerQuarterNote
		{
			get
			{
				if (this.isDisposed)
				{
					throw new ObjectDisposedException("MIDIClock");
				}
				return this.pulsesPerQuarterNote;
			}
			set
			{
				if (this.isDisposed)
				{
					throw new ObjectDisposedException("MIDIClock");
				}
				if (value < 1U)
				{
					throw new ArgumentException("PulsesPerQuarterNote", "Pulses per quarter note must be larger than zero.");
				}
				this.pulsesPerQuarterNote = value;
			}
		}

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000169 RID: 361 RVA: 0x000059B4 File Offset: 0x00003BB4
		// (remove) Token: 0x0600016A RID: 362 RVA: 0x000059EC File Offset: 0x00003BEC
		public event EventHandler Resumed;

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x0600016B RID: 363 RVA: 0x00005A24 File Offset: 0x00003C24
		// (remove) Token: 0x0600016C RID: 364 RVA: 0x00005A5C File Offset: 0x00003C5C
		public event EventHandler Started;

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x0600016D RID: 365 RVA: 0x00005A94 File Offset: 0x00003C94
		// (remove) Token: 0x0600016E RID: 366 RVA: 0x00005ACC File Offset: 0x00003CCC
		public event EventHandler Stopped;

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x0600016F RID: 367 RVA: 0x00005B01 File Offset: 0x00003D01
		// (set) Token: 0x06000170 RID: 368 RVA: 0x00005B1C File Offset: 0x00003D1C
		public uint Tempo
		{
			get
			{
				if (this.isDisposed)
				{
					throw new ObjectDisposedException("MIDIClock");
				}
				return this.tempo;
			}
			set
			{
				if (this.isDisposed)
				{
					throw new ObjectDisposedException("MIDIClock");
				}
				if (value < 1U)
				{
					throw new ArgumentException("Tempo cannot be zero.", "Tempo");
				}
				this.tempo = value;
			}
		}

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x06000171 RID: 369 RVA: 0x00005B4C File Offset: 0x00003D4C
		// (remove) Token: 0x06000172 RID: 370 RVA: 0x00005B84 File Offset: 0x00003D84
		public event EventHandler Tick;

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000173 RID: 371 RVA: 0x00005BB9 File Offset: 0x00003DB9
		// (set) Token: 0x06000174 RID: 372 RVA: 0x00005BD4 File Offset: 0x00003DD4
		public ulong Ticks
		{
			get
			{
				if (this.isDisposed)
				{
					throw new ObjectDisposedException("MIDIClock");
				}
				return this.ticks;
			}
			set
			{
				if (this.isDisposed)
				{
					throw new ObjectDisposedException("MIDIClock");
				}
				if (this.IsRunning)
				{
					throw new InvalidOperationException("The MIDIClock is running and the position may not be changed.");
				}
				this.ticks = value;
				this.fractionalTicks = 0UL;
			}
		}

		// Token: 0x06000175 RID: 373 RVA: 0x00005C0B File Offset: 0x00003E0B
		public MIDIClock() : this(960U, 120U)
		{
		}

		// Token: 0x06000176 RID: 374 RVA: 0x00005C1C File Offset: 0x00003E1C
		public MIDIClock(uint ppqn, uint bpm)
		{
			this.PulsesPerQuarterNote = ppqn;
			this.BeatsPerMinute = bpm;
			this.timer = new Timer();
			this.timer.Tick += this.Advance;
		}

		// Token: 0x06000177 RID: 375 RVA: 0x00005C6C File Offset: 0x00003E6C
		private void Advance(object sender, EventArgs e)
		{
			ulong num = ((ulong)this.PeriodResolution + this.fractionalTicks) / (ulong)this.tempo;
			this.fractionalTicks += (ulong)this.PeriodResolution - (ulong)this.tempo * num;
			ulong num2 = 0UL;
			while (num2 < num)
			{
				this.OnTick(EventArgs.Empty);
				num2 += 1UL;
				this.ticks += 1UL;
			}
		}

		// Token: 0x06000178 RID: 376 RVA: 0x00005CD6 File Offset: 0x00003ED6
		public void Dispose()
		{
			if (!this.isDisposed)
			{
				this.timer.Dispose();
				this.isDisposed = true;
			}
		}

		// Token: 0x06000179 RID: 377 RVA: 0x00005CF4 File Offset: 0x00003EF4
		private void OnResumed(EventArgs eventArguments)
		{
			if (eventArguments == null)
			{
				throw new ArgumentNullException("eventArguments");
			}
			EventHandler resumed = this.Resumed;
			if (resumed != null)
			{
				resumed(this, EventArgs.Empty);
			}
		}

		// Token: 0x0600017A RID: 378 RVA: 0x00005D28 File Offset: 0x00003F28
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

		// Token: 0x0600017B RID: 379 RVA: 0x00005D5C File Offset: 0x00003F5C
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

		// Token: 0x0600017C RID: 380 RVA: 0x00005D90 File Offset: 0x00003F90
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

		// Token: 0x0600017D RID: 381 RVA: 0x00005DBD File Offset: 0x00003FBD
		public void Resume()
		{
			if (this.isDisposed)
			{
				throw new ObjectDisposedException("MIDIClock");
			}
			if (this.IsRunning)
			{
				throw new InvalidOperationException("The MIDIClock is already running.");
			}
			this.timer.Start();
			this.OnResumed(EventArgs.Empty);
		}

		// Token: 0x0600017E RID: 382 RVA: 0x00005DFB File Offset: 0x00003FFB
		public void Start()
		{
			if (this.isDisposed)
			{
				throw new ObjectDisposedException("MIDIClock");
			}
			this.ticks = 0UL;
			this.fractionalTicks = 0UL;
			this.timer.Start();
			this.OnStarted(EventArgs.Empty);
		}

		// Token: 0x0600017F RID: 383 RVA: 0x00005E36 File Offset: 0x00004036
		public void Stop()
		{
			if (this.isDisposed)
			{
				throw new ObjectDisposedException("MIDIClock");
			}
			if (!this.IsRunning)
			{
				throw new InvalidOperationException("The MIDIClock has already been stopped.");
			}
			this.timer.Stop();
			this.OnStopped(EventArgs.Empty);
		}

		// Token: 0x0400006E RID: 110
		public const uint MicrosecondsPerMinute = 60000000U;

		// Token: 0x0400006F RID: 111
		public const uint MillisecondsPerSecond = 1000U;

		// Token: 0x04000070 RID: 112
		private bool isDisposed;

		// Token: 0x04000071 RID: 113
		private ulong fractionalTicks;

		// Token: 0x04000072 RID: 114
		private uint pulsesPerQuarterNote;

		// Token: 0x04000076 RID: 118
		private uint tempo = MIDIClock.DefaultTempo;

		// Token: 0x04000078 RID: 120
		private ulong ticks;

		// Token: 0x04000079 RID: 121
		private Timer timer;
	}
}
