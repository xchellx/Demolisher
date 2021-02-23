using System;
using System.IO;
using System.Linq;
using Arookas.IO.Binary;

namespace Arookas.Audio.MusicalInstrumentDigitalInterface
{
	// Token: 0x02000002 RID: 2
	public abstract class MIDIEvent
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public ulong AbsoluteTime
		{
			get
			{
				return this.absoluteTime;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000002 RID: 2 RVA: 0x00002058 File Offset: 0x00000258
		public ulong DeltaTime
		{
			get
			{
				return this.deltaTime;
			}
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002060 File Offset: 0x00000260
		protected MIDIEvent() : this(0UL, 0UL)
		{
		}

		// Token: 0x06000004 RID: 4 RVA: 0x0000206C File Offset: 0x0000026C
		protected MIDIEvent(ulong deltaTime) : this(0UL, deltaTime)
		{
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002077 File Offset: 0x00000277
		protected MIDIEvent(ulong absoluteTime, ulong deltaTime)
		{
			this.absoluteTime = absoluteTime;
			this.deltaTime = deltaTime;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002090 File Offset: 0x00000290
		internal static MIDIEvent FromStream(Stream stream)
		{
			ABinaryReader abinaryReader = new ABinaryReader(stream, Endianness.Big);
			ulong num = abinaryReader.ReadUIntVar();
			byte statusByte = abinaryReader.Read8();
			return MIDIEvent.FromStream(abinaryReader, num, statusByte);
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000020BC File Offset: 0x000002BC
		internal static MIDIEvent FromStream(Stream stream, ChannelEventType eventType, int channelNumber)
		{
			if (!eventType.IsDefined<ChannelEventType>())
			{
				throw new ArgumentOutOfRangeException("eventType", eventType, "The specified ChannelEventType was not a defined value.");
			}
			if (channelNumber < 0 || channelNumber > 15)
			{
				throw new ArgumentOutOfRangeException("channelNumber", channelNumber, "The specified channel number was negative or greater than 15.");
			}
			ABinaryReader abinaryReader = new ABinaryReader(stream, Endianness.Big);
			ulong num = abinaryReader.ReadUIntVar();
			byte statusByte = (byte)((int)eventType | channelNumber);
			return MIDIEvent.FromStream(abinaryReader, num, statusByte);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002124 File Offset: 0x00000324
		private static MIDIEvent FromStream(ABinaryReader binaryReader, ulong deltaTime, byte statusByte)
		{
			if (statusByte == 255)
			{
				MetaEventType metaEventType = (MetaEventType)binaryReader.Read8();
				int num = (int)binaryReader.ReadUIntVar();
				if (!metaEventType.IsDefined<MetaEventType>())
				{
					throw new InvalidDataException(string.Format("Encountered unsupported meta event type {0}.", metaEventType));
				}
				MetaEventType metaEventType2 = metaEventType;
				if (metaEventType2 <= MetaEventType.EndOfTrack)
				{
					switch (metaEventType2)
					{
					case MetaEventType.SequenceNumber:
						if (num != 2)
						{
							throw new InvalidDataException("Invalid size in sequence-number event.");
						}
						return new SequenceNumberEvent(deltaTime, (int)binaryReader.Read16());
					case MetaEventType.Text:
						return new TextEvent(deltaTime, binaryReader.ReadRawString(num));
					case MetaEventType.CopyrightNotice:
						return new CopyrightNoticeEvent(deltaTime, binaryReader.ReadRawString(num));
					case MetaEventType.Name:
						return new TrackNameEvent(deltaTime, binaryReader.ReadRawString(num));
					case MetaEventType.InstrumentName:
						return new InstrumentNameEvent(deltaTime, binaryReader.ReadRawString(num));
					case MetaEventType.Lyrics:
						return new LyricsEvent(deltaTime, binaryReader.ReadRawString(num));
					case MetaEventType.Marker:
						return new MarkerEvent(deltaTime, binaryReader.ReadRawString(num));
					case MetaEventType.CuePoint:
						return new CuePointEvent(deltaTime, binaryReader.ReadRawString(num));
					default:
						if (metaEventType2 != MetaEventType.ChannelPrefix)
						{
							if (metaEventType2 == MetaEventType.EndOfTrack)
							{
								if (num != 0)
								{
									throw new InvalidDataException("Invalid size in end-of-track event.");
								}
								return new EndOfTrackEvent(deltaTime);
							}
						}
						else
						{
							if (num != 1)
							{
								throw new InvalidDataException("Invalid size in channel-prefix event.");
							}
							return new ChannelPrefixEvent(deltaTime, binaryReader.Read8());
						}
						break;
					}
				}
				else if (metaEventType2 != MetaEventType.TempoChange)
				{
					switch (metaEventType2)
					{
					case MetaEventType.TimeSignature:
						if (num != 4)
						{
							throw new InvalidDataException("Invalid size in time-signature event.");
						}
						return new TimeSignatureEvent(deltaTime, (int)binaryReader.Read8(), (int)binaryReader.Read8(), (int)binaryReader.Read8(), (int)binaryReader.Read8());
					case MetaEventType.KeySignature:
						if (num != 2)
						{
							throw new InvalidDataException("Invalid size in key-signature event.");
						}
						return new KeySignatureEvent(deltaTime, (int)binaryReader.ReadS8(), (Scale)binaryReader.Read8());
					default:
						if (metaEventType2 == MetaEventType.SequencerSpecific)
						{
							byte[] array = binaryReader.Read8s(num);
							if (num == 0 || num < ((array[0] == 0) ? 3 : 1))
							{
								throw new InvalidDataException("Invalid size in sequencer-specific event.");
							}
							bool flag = array[0] == 0;
							int manufacturerID = flag ? ((int)array[1] << 8 | (int)array[2]) : ((int)array[0]);
							return new SequencerSpecificEvent(deltaTime, manufacturerID, flag, array.Duplicate(flag ? 3 : 1, array.Length - (flag ? 3 : 1)));
						}
						break;
					}
				}
				else
				{
					if (num != 3)
					{
						throw new InvalidDataException("Invalid size in tempo-change event.");
					}
					return new TempoChangeEvent(deltaTime, binaryReader.Read24());
				}
				throw new NotImplementedException(string.Format("Encountered unimplemented meta event type {0}.", metaEventType));
			}
			else if (statusByte == 240 || statusByte == 247)
			{
				byte[] array2 = binaryReader.Read8s((int)binaryReader.ReadUIntVar());
				if (array2.Length == 0 || array2.Length < ((array2[0] == 0) ? 3 : 1))
				{
					throw new InvalidDataException("Encountered a SysEx event with an invalid size.");
				}
				SystemExclusiveEventType type = SystemExclusiveEventType.Normal;
				bool flag2 = statusByte == 247;
				bool flag3 = array2.Last<byte>() == 247;
				bool flag4 = array2[0] == 0;
				int manufacturerID2 = flag4 ? ((int)array2[1] << 8 | (int)array2[2]) : ((int)array2[0]);
				if (flag2)
				{
					type = (flag3 ? SystemExclusiveEventType.Terminating : SystemExclusiveEventType.Continuation);
				}
				if (flag3)
				{
					array2 = array2.Duplicate(array2.Length - 1);
				}
				return new SystemExclusiveEvent(deltaTime, type, manufacturerID2, flag4, array2);
			}
			else
			{
				ChannelEventType channelEventType = (ChannelEventType)(statusByte & 240);
				byte channelNumber = (byte)(statusByte & 15);
				if (!channelEventType.IsDefined<ChannelEventType>())
				{
					throw new InvalidDataException(string.Format("Encountered undefined channel-event type {0}.", channelEventType));
				}
				ChannelEventType channelEventType2 = channelEventType;
				if (channelEventType2 <= ChannelEventType.NoteAftertouch)
				{
					if (channelEventType2 == ChannelEventType.NoteOff)
					{
						return new NoteOffEvent(deltaTime, (int)channelNumber, binaryReader.Read8(), (int)binaryReader.Read8());
					}
					if (channelEventType2 == ChannelEventType.NoteOn)
					{
						return new NoteOnEvent(deltaTime, (int)channelNumber, (int)binaryReader.Read8(), (int)binaryReader.Read8());
					}
					if (channelEventType2 == ChannelEventType.NoteAftertouch)
					{
						return new NoteAftertouchEvent(deltaTime, (int)channelNumber, (int)binaryReader.Read8(), (int)binaryReader.Read8());
					}
				}
				else if (channelEventType2 <= ChannelEventType.ProgramChange)
				{
					if (channelEventType2 == ChannelEventType.Controller)
					{
						return new ControllerEvent(deltaTime, (int)channelNumber, (int)binaryReader.Read8(), (int)binaryReader.Read8());
					}
					if (channelEventType2 == ChannelEventType.ProgramChange)
					{
						return new ProgramChangeEvent(deltaTime, (int)channelNumber, (int)binaryReader.Read8());
					}
				}
				else
				{
					if (channelEventType2 == ChannelEventType.ChannelAftertouch)
					{
						return new ChannelAftertouchEvent(deltaTime, (int)channelNumber, (int)binaryReader.Read8());
					}
					if (channelEventType2 == ChannelEventType.PitchBend)
					{
						return new PitchBendEvent(deltaTime, (int)channelNumber, (int)(binaryReader.Read8() & 127) | (int)(binaryReader.Read8() & 127) << 7);
					}
				}
				throw new NotImplementedException(string.Format("Encountered unimplemented channel event type {0}.", channelEventType));
			}
		}

		// Token: 0x06000009 RID: 9
		public abstract MIDIEvent SetTime(ulong absoluteTime, ulong deltaTime);

		// Token: 0x0600000A RID: 10
		public abstract void ToStream(Stream stream);

		// Token: 0x04000001 RID: 1
		private ulong absoluteTime;

		// Token: 0x04000002 RID: 2
		private ulong deltaTime;
	}
}
