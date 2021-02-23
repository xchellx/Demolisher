using System;
using System.IO;
using Arookas.IO.Binary;

namespace Arookas.Audio.MusicalInstrumentDigitalInterface
{
	// Token: 0x0200000D RID: 13
	public class TextEvent : MetaEvent
	{
		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000058 RID: 88 RVA: 0x00002D3F File Offset: 0x00000F3F
		public string Text
		{
			get
			{
				return this.text;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000059 RID: 89 RVA: 0x00002D47 File Offset: 0x00000F47
		public override MetaEventType Type
		{
			get
			{
				return MetaEventType.Text;
			}
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002D4A File Offset: 0x00000F4A
		public TextEvent(string text) : this(0UL, 0UL, text)
		{
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00002D57 File Offset: 0x00000F57
		public TextEvent(ulong deltaTime, string text) : this(0UL, deltaTime, text)
		{
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00002D63 File Offset: 0x00000F63
		public TextEvent(ulong absoluteTime, ulong deltaTime, string text) : base(absoluteTime, deltaTime)
		{
			if (text == null)
			{
				throw new ArgumentNullException("text");
			}
			this.text = text;
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00002D82 File Offset: 0x00000F82
		public override MIDIEvent SetTime(ulong absoluteTime, ulong deltaTime)
		{
			return new TextEvent(absoluteTime, deltaTime, this.text);
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00002D94 File Offset: 0x00000F94
		public override void ToStream(Stream stream)
		{
			ABinaryWriter abinaryWriter = new ABinaryWriter(stream, Endianness.Big);
			abinaryWriter.WriteUIntVar(base.DeltaTime);
			abinaryWriter.Write8(byte.MaxValue);
			abinaryWriter.Write8((byte)this.Type);
			abinaryWriter.WriteUIntVar((ulong)((long)this.text.Length));
			abinaryWriter.WriteString(this.text);
		}

		// Token: 0x04000010 RID: 16
		private string text;
	}
}
