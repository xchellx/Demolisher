using System;
using System.Collections.Generic;

namespace Arookas.Audio.SoundFonts
{
	// Token: 0x0200004D RID: 77
	public sealed class Instrument
	{
		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000236 RID: 566 RVA: 0x00007060 File Offset: 0x00005260
		// (set) Token: 0x06000237 RID: 567 RVA: 0x00007068 File Offset: 0x00005268
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (value.Length > 20)
				{
					throw new ArgumentException("The specified name is too long.", "value");
				}
				this.name = value;
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000238 RID: 568 RVA: 0x00007099 File Offset: 0x00005299
		public List<InstrumentZone> Zones
		{
			get
			{
				return this.zones;
			}
		}

		// Token: 0x06000239 RID: 569 RVA: 0x000070A1 File Offset: 0x000052A1
		public Instrument() : this(string.Empty)
		{
		}

		// Token: 0x0600023A RID: 570 RVA: 0x000070B0 File Offset: 0x000052B0
		public Instrument(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name.Length > 20)
			{
				throw new ArgumentException("The specified name is too long.", "name");
			}
			this.name = name;
			this.zones = new List<InstrumentZone>(5);
		}

		// Token: 0x040000EE RID: 238
		private string name;

		// Token: 0x040000EF RID: 239
		private List<InstrumentZone> zones;
	}
}
