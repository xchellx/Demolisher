using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Arookas.Audio.MusicalInstrumentDigitalInterface
{
	// Token: 0x02000061 RID: 97
	public sealed class Track : IEnumerable<MIDIEvent>, IEnumerable
	{
		// Token: 0x170000DA RID: 218
		// (get) Token: 0x060002C1 RID: 705 RVA: 0x0000A41B File Offset: 0x0000861B
		public IEnumerable<ChannelEvent> ChannelEvents
		{
			get
			{
				return this.GetAllOfType<ChannelEvent>();
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x060002C2 RID: 706 RVA: 0x0000A423 File Offset: 0x00008623
		public IEnumerable<ControllerEvent> ControllerEvents
		{
			get
			{
				return this.GetAllOfType<ControllerEvent>();
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x060002C3 RID: 707 RVA: 0x0000A42B File Offset: 0x0000862B
		public int Count
		{
			get
			{
				return this.events.Count;
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x060002C4 RID: 708 RVA: 0x0000A438 File Offset: 0x00008638
		public ulong Duration
		{
			get
			{
				if (this.events.Count == 0)
				{
					return 0UL;
				}
				return this.events.Last<MIDIEvent>().AbsoluteTime;
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x060002C5 RID: 709 RVA: 0x0000A45A File Offset: 0x0000865A
		public bool IsTerminated
		{
			get
			{
				return this.events.Last<MIDIEvent>() is EndOfTrackEvent;
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x060002C6 RID: 710 RVA: 0x0000A46F File Offset: 0x0000866F
		public IEnumerable<MetaEvent> MetaEvents
		{
			get
			{
				return this.GetAllOfType<MetaEvent>();
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x060002C7 RID: 711 RVA: 0x0000A484 File Offset: 0x00008684
		// (set) Token: 0x060002C8 RID: 712 RVA: 0x0000A4E4 File Offset: 0x000086E4
		public string Name
		{
			get
			{
				if (this.events.Count == 0)
				{
					return null;
				}
				TrackNameEvent trackNameEvent = (TrackNameEvent)this.events.FirstOrDefault((MIDIEvent midiEvent) => midiEvent is TrackNameEvent);
				if (trackNameEvent == null)
				{
					return null;
				}
				return trackNameEvent.Text;
			}
			set
			{
				TrackNameEvent trackNameEvent = (TrackNameEvent)this.events.FirstOrDefault((MIDIEvent midiEvent) => midiEvent is TrackNameEvent);
				if (trackNameEvent == null)
				{
					this.Insert(new TrackNameEvent(value), 0UL);
					return;
				}
				this.events.Remove(trackNameEvent);
				this.Insert(new TrackNameEvent(value), trackNameEvent.AbsoluteTime);
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x060002C9 RID: 713 RVA: 0x0000A550 File Offset: 0x00008750
		public IEnumerable<NoteOffEvent> NoteOffEvents
		{
			get
			{
				return this.GetAllOfType<NoteOffEvent>();
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x060002CA RID: 714 RVA: 0x0000A558 File Offset: 0x00008758
		public IEnumerable<NoteOnEvent> NoteOnEvents
		{
			get
			{
				return this.GetAllOfType<NoteOnEvent>();
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x060002CB RID: 715 RVA: 0x0000A560 File Offset: 0x00008760
		public IEnumerable<SystemExclusiveEvent> SystemExclusiveEvents
		{
			get
			{
				return this.GetAllOfType<SystemExclusiveEvent>();
			}
		}

		// Token: 0x170000E4 RID: 228
		public MIDIEvent this[int index]
		{
			get
			{
				if (index < 0 || index >= this.events.Count)
				{
					throw new IndexOutOfRangeException();
				}
				return this.events[index];
			}
		}

		// Token: 0x170000E5 RID: 229
		public IEnumerable<MIDIEvent> this[ulong absoluteTime]
		{
			get
			{
				foreach (MIDIEvent midiEvent2 in from midiEvent in this.events
				where midiEvent.AbsoluteTime == absoluteTime
				select midiEvent)
				{
					yield return midiEvent2;
				}
				yield break;
			}
		}

		// Token: 0x060002CE RID: 718 RVA: 0x0000A7A0 File Offset: 0x000089A0
		public Track()
		{
			this.events = new List<MIDIEvent>(100);
		}

		// Token: 0x060002CF RID: 719 RVA: 0x0000A7B5 File Offset: 0x000089B5
		public Track(params MIDIEvent[] midiEvents) : this((IEnumerable<MIDIEvent>)midiEvents)
		{
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x0000A7C4 File Offset: 0x000089C4
		public Track(IEnumerable<MIDIEvent> midiEvents) : this()
		{
			if (midiEvents == null)
			{
				throw new ArgumentNullException("midiEvents");
			}
			foreach (MIDIEvent midievent in midiEvents)
			{
				if (midievent == null)
				{
					throw new ArgumentException("The specified collection of MIDIEvents contains a null element.", "midiEvents");
				}
				this.Add(midievent, midievent.AbsoluteTime);
			}
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x0000A83C File Offset: 0x00008A3C
		public void Add(MIDIEvent midiEvent, ulong absoluteTime)
		{
			if (midiEvent == null)
			{
				throw new ArgumentNullException("midiEvent");
			}
			int num = 0;
			while (num < this.events.Count && this.events[num].AbsoluteTime <= absoluteTime)
			{
				num++;
			}
			ulong deltaTime = (num == 0) ? absoluteTime : (absoluteTime - this.events[num - 1].AbsoluteTime);
			MIDIEvent item = midiEvent.SetTime(absoluteTime, deltaTime);
			if (num >= this.events.Count)
			{
				this.events.Add(item);
				return;
			}
			this.events[num] = this.events[num].SetTime(this.events[num].AbsoluteTime, this.events[num].AbsoluteTime - absoluteTime);
			this.events.Insert(num, item);
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x0000A910 File Offset: 0x00008B10
		public void AddRange(IEnumerable<MIDIEvent> midiEvents, ulong absoluteTime)
		{
			if (midiEvents == null)
			{
				throw new ArgumentNullException("midiEvents");
			}
			foreach (MIDIEvent midievent in midiEvents)
			{
				if (midievent == null)
				{
					throw new ArgumentException("The specified collection of MIDIEvents contains a null element.", "midiEvents");
				}
				this.Add(midievent, absoluteTime);
			}
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x0000A97C File Offset: 0x00008B7C
		public void Clear()
		{
			this.events.Clear();
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x0000A989 File Offset: 0x00008B89
		public bool Contains(MIDIEvent midiEvent)
		{
			if (midiEvent == null)
			{
				throw new ArgumentNullException("midiEvent");
			}
			return this.events.Contains(midiEvent);
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x0000A9A8 File Offset: 0x00008BA8
		public void ForEach<TMIDIEvent>(Action<TMIDIEvent> action) where TMIDIEvent : MIDIEvent
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			foreach (TMIDIEvent obj in this.GetAllOfType<TMIDIEvent>())
			{
				action(obj);
			}
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x0000AA1C File Offset: 0x00008C1C
		public void ForEach<TMIDIEvent>(Predicate<TMIDIEvent> predicate, Action<TMIDIEvent> action) where TMIDIEvent : MIDIEvent
		{
			if (predicate == null)
			{
				throw new ArgumentNullException("predicate");
			}
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			foreach (TMIDIEvent obj in from midiEvent in this.GetAllOfType<TMIDIEvent>()
			where predicate(midiEvent)
			select midiEvent)
			{
				action(obj);
			}
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x0000AAA8 File Offset: 0x00008CA8
		public void ForEach(Action<MIDIEvent> action)
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			foreach (MIDIEvent obj in this)
			{
				action(obj);
			}
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x0000AB18 File Offset: 0x00008D18
		public void ForEach(Predicate<MIDIEvent> predicate, Action<MIDIEvent> action)
		{
			if (predicate == null)
			{
				throw new ArgumentNullException("predicate");
			}
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			foreach (MIDIEvent obj in from midiEvent in this
			where predicate(midiEvent)
			select midiEvent)
			{
				action(obj);
			}
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x0000ABA0 File Offset: 0x00008DA0
		public IEnumerator<MIDIEvent> GetEnumerator()
		{
			return this.events.GetEnumerator();
		}

		// Token: 0x060002DA RID: 730 RVA: 0x0000ABB2 File Offset: 0x00008DB2
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x060002DB RID: 731 RVA: 0x0000ACF8 File Offset: 0x00008EF8
		public IEnumerable<TMIDIEvent> GetAllOfType<TMIDIEvent>() where TMIDIEvent : MIDIEvent
		{
			for (int midiEvent = 0; midiEvent < this.events.Count; midiEvent++)
			{
				TMIDIEvent e = this.events[midiEvent] as TMIDIEvent;
				if (e != null)
				{
					yield return e;
				}
			}
			yield break;
		}

		// Token: 0x060002DC RID: 732 RVA: 0x0000AD15 File Offset: 0x00008F15
		public int IndexOf(MIDIEvent midiEvent)
		{
			if (midiEvent == null)
			{
				throw new ArgumentNullException("midiEvent");
			}
			return this.events.IndexOf(midiEvent);
		}

		// Token: 0x060002DD RID: 733 RVA: 0x0000AD34 File Offset: 0x00008F34
		public void Insert(MIDIEvent midiEvent, ulong absoluteTime)
		{
			if (midiEvent == null)
			{
				throw new ArgumentNullException("midiEvent");
			}
			int num = 0;
			while (num < this.events.Count && this.events[num].AbsoluteTime < absoluteTime)
			{
				num++;
			}
			ulong deltaTime = (num == 0) ? absoluteTime : (absoluteTime - this.events[num - 1].AbsoluteTime);
			MIDIEvent item = midiEvent.SetTime(absoluteTime, deltaTime);
			if (num >= this.events.Count)
			{
				this.events.Add(item);
				return;
			}
			this.events[num] = this.events[num].SetTime(this.events[num].AbsoluteTime, this.events[num].AbsoluteTime - absoluteTime);
			this.events.Insert(num, item);
		}

		// Token: 0x060002DE RID: 734 RVA: 0x0000AE08 File Offset: 0x00009008
		public bool Replace(MIDIEvent replacee, MIDIEvent replacer)
		{
			if (replacee == null)
			{
				throw new ArgumentNullException("replacee");
			}
			if (replacer == null)
			{
				throw new ArgumentNullException("replacer");
			}
			if (replacee == replacer)
			{
				throw new InvalidOperationException("Cannot replace a MIDIEvent with itself.");
			}
			int num = this.events.IndexOf(replacee);
			if (num < 0)
			{
				return false;
			}
			this.events[num] = replacer.SetTime(replacee.AbsoluteTime, replacee.DeltaTime);
			return true;
		}

		// Token: 0x060002DF RID: 735 RVA: 0x0000AE74 File Offset: 0x00009074
		public bool Remove(MIDIEvent midiEvent)
		{
			if (midiEvent == null)
			{
				throw new ArgumentNullException("midiEvent");
			}
			int num = this.events.IndexOf(midiEvent);
			if (num < 0)
			{
				return false;
			}
			if (num < this.events.Count - 1)
			{
				this.events.RemoveAt(num);
				this.events[num] = this.events[num].SetTime(this.events[num].AbsoluteTime, this.events[num].DeltaTime + midiEvent.DeltaTime);
			}
			else
			{
				this.events.RemoveAt(num);
			}
			return true;
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x0000AF2C File Offset: 0x0000912C
		public void RemoveAll(Predicate<MIDIEvent> predicate)
		{
			if (predicate == null)
			{
				throw new ArgumentNullException("predicate");
			}
			List<MIDIEvent> list = new List<MIDIEvent>(from midiEvent in this.events
			where predicate(midiEvent)
			select midiEvent);
			foreach (MIDIEvent midiEvent2 in list)
			{
				this.Remove(midiEvent2);
			}
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x0000AFD0 File Offset: 0x000091D0
		public void RemoveAll<TMIDIEvent>(Predicate<TMIDIEvent> predicate) where TMIDIEvent : MIDIEvent
		{
			if (predicate == null)
			{
				throw new ArgumentNullException("predicate");
			}
			List<TMIDIEvent> list = new List<TMIDIEvent>(from midiEvent in this.GetAllOfType<TMIDIEvent>()
			where predicate(midiEvent)
			select midiEvent);
			foreach (TMIDIEvent tmidievent in list)
			{
				MIDIEvent midiEvent2 = tmidievent;
				this.Remove(midiEvent2);
			}
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x0000B064 File Offset: 0x00009264
		public void RemoveAll<TMIDIEvent>() where TMIDIEvent : MIDIEvent
		{
			List<TMIDIEvent> list = new List<TMIDIEvent>(this.GetAllOfType<TMIDIEvent>());
			foreach (TMIDIEvent tmidievent in list)
			{
				MIDIEvent midiEvent = tmidievent;
				this.Remove(midiEvent);
			}
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x0000B0C4 File Offset: 0x000092C4
		public void RemoveAt(ulong absoluteTime)
		{
			foreach (MIDIEvent midiEvent in this[absoluteTime])
			{
				this.Remove(midiEvent);
			}
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x0000B148 File Offset: 0x00009348
		public void RemoveRange(ulong absoluteTime, ulong pulses)
		{
			foreach (MIDIEvent midiEvent2 in from midiEvent in this.events
			where midiEvent.AbsoluteTime >= absoluteTime && midiEvent.AbsoluteTime <= absoluteTime + pulses
			select midiEvent)
			{
				this.Remove(midiEvent2);
			}
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x0000B1BC File Offset: 0x000093BC
		public bool Swap(MIDIEvent swappee, MIDIEvent swapper)
		{
			if (swappee == null)
			{
				throw new ArgumentNullException("swappee");
			}
			if (swapper == null)
			{
				throw new ArgumentNullException("swapper");
			}
			if (swappee == swapper)
			{
				throw new InvalidOperationException("Cannot swap a MIDIEvent with itself.");
			}
			int num = this.events.IndexOf(swappee);
			int num2 = this.events.IndexOf(swapper);
			if (num < 0 || num2 < 0)
			{
				return false;
			}
			this.events[num] = swapper.SetTime(swappee.AbsoluteTime, swappee.DeltaTime);
			this.events[num2] = swappee.SetTime(swapper.AbsoluteTime, swapper.DeltaTime);
			return true;
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x0000B255 File Offset: 0x00009455
		public void Terminate()
		{
			if (!this.IsTerminated)
			{
				this.Add(new EndOfTrackEvent(), this.Duration);
			}
		}

		// Token: 0x04000193 RID: 403
		private List<MIDIEvent> events;
	}
}
