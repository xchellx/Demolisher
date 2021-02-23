using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Arookas.IO.Binary;
using Arookas.IO.Binary.ResourceInterchangeFileFormat;

namespace Arookas.Audio.MusicalInstrumentDigitalInterface
{
	// Token: 0x02000029 RID: 41
	public sealed class MIDI : IEnumerable<Track>, IEnumerable
	{
		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000140 RID: 320 RVA: 0x00004BDC File Offset: 0x00002DDC
		public int Count
		{
			get
			{
				return this.tracks.Count;
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000141 RID: 321 RVA: 0x00004BE9 File Offset: 0x00002DE9
		// (set) Token: 0x06000142 RID: 322 RVA: 0x00004BF1 File Offset: 0x00002DF1
		public TimeDivision Division
		{
			get
			{
				return this.division;
			}
			set
			{
				if (this.division == null)
				{
					throw new ArgumentNullException("value");
				}
				this.division = value;
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000143 RID: 323 RVA: 0x00004C15 File Offset: 0x00002E15
		public ulong Duration
		{
			get
			{
				return this.tracks.Max((Track track) => track.Duration);
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000144 RID: 324 RVA: 0x00004C3F File Offset: 0x00002E3F
		public MIDIFormat Format
		{
			get
			{
				return this.format;
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000145 RID: 325 RVA: 0x00004C47 File Offset: 0x00002E47
		public static int MaximumTrackCount
		{
			get
			{
				return 65535;
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000146 RID: 326 RVA: 0x00004C4E File Offset: 0x00002E4E
		// (set) Token: 0x06000147 RID: 327 RVA: 0x00004C70 File Offset: 0x00002E70
		public string Name
		{
			get
			{
				if (this.tracks.Count == 0)
				{
					return null;
				}
				return this.tracks[0].Name;
			}
			set
			{
				if (this.tracks.Count == 0)
				{
					throw new InvalidOperationException("There are no tracks in the MIDI, so the name cannot be set.");
				}
				this.tracks[0].Name = value;
			}
		}

		// Token: 0x17000068 RID: 104
		public Track this[int index]
		{
			get
			{
				if (index < 0 || index > this.tracks.Count)
				{
					throw new IndexOutOfRangeException();
				}
				return this.tracks[index];
			}
		}

		// Token: 0x17000069 RID: 105
		public Track this[string trackName]
		{
			get
			{
				if (trackName == null)
				{
					throw new ArgumentNullException("trackName");
				}
				return this.tracks.FirstOrDefault((Track track) => track.Name == trackName);
			}
		}

		// Token: 0x1700006A RID: 106
		public Track this[string trackName, StringComparison comparisonType]
		{
			get
			{
				if (trackName == null)
				{
					throw new ArgumentNullException("trackName");
				}
				return this.tracks.FirstOrDefault((Track track) => track.Name.Equals(trackName, comparisonType));
			}
		}

		// Token: 0x0600014B RID: 331 RVA: 0x00004D93 File Offset: 0x00002F93
		public MIDI() : this(MIDIFormat.MultiTrack, new TicksPerBeatDivision(960))
		{
		}

		// Token: 0x0600014C RID: 332 RVA: 0x00004DA6 File Offset: 0x00002FA6
		public MIDI(TicksPerBeatDivision division) : this(MIDIFormat.MultiTrack, division)
		{
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00004DB0 File Offset: 0x00002FB0
		public MIDI(MIDIFormat format) : this(format, new TicksPerBeatDivision(960))
		{
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00004DC4 File Offset: 0x00002FC4
		public MIDI(MIDIFormat format, TicksPerBeatDivision division)
		{
			if (!format.IsDefined<MIDIFormat>())
			{
				throw new ArgumentOutOfRangeException("format");
			}
			if (division == null)
			{
				throw new ArgumentNullException("division");
			}
			this.format = format;
			this.division = division;
			this.tracks = new List<Track>(16);
		}

		// Token: 0x0600014F RID: 335 RVA: 0x00004E14 File Offset: 0x00003014
		public Track Add()
		{
			if (this.tracks.Count >= MIDI.MaximumTrackCount)
			{
				throw new InvalidOperationException("The maximum number of Tracks in the MIDI has been reached and another Track may not be added.");
			}
			Track track = new Track();
			this.tracks.Add(track);
			return track;
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00004E51 File Offset: 0x00003051
		public void Add(Track track)
		{
			if (track == null)
			{
				throw new ArgumentNullException("track");
			}
			this.tracks.Add(track);
		}

		// Token: 0x06000151 RID: 337 RVA: 0x00004E70 File Offset: 0x00003070
		public void AddRange(IEnumerable<Track> tracks)
		{
			if (tracks == null)
			{
				throw new ArgumentNullException("tracks");
			}
			foreach (Track track in tracks)
			{
				if (track == null)
				{
					throw new ArgumentException("The specified collection of tracks contains at least one null element.", "tracks");
				}
				this.Add(track);
			}
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00004EDC File Offset: 0x000030DC
		public void Clear()
		{
			this.tracks.Clear();
		}

		// Token: 0x06000153 RID: 339 RVA: 0x00004EE9 File Offset: 0x000030E9
		public bool Contains(Track track)
		{
			if (track == null)
			{
				throw new ArgumentNullException("track");
			}
			return this.tracks.Contains(track);
		}

		// Token: 0x06000154 RID: 340 RVA: 0x00004F08 File Offset: 0x00003108
		public static MIDI FromFile(string fileName)
		{
			if (fileName == null)
			{
				throw new ArgumentNullException("fileName");
			}
			MIDI result;
			using (FileStream fileStream = File.OpenRead(fileName))
			{
				result = MIDI.FromStream(fileStream);
			}
			return result;
		}

		// Token: 0x06000155 RID: 341 RVA: 0x00004F50 File Offset: 0x00003150
		public static MIDI FromStream(Stream stream)
		{
			ABinaryReader abinaryReader = new ABinaryReader(stream, Endianness.Big);
			MIDI midi = new MIDI();
			if (abinaryReader.ReadRawString(4) != "MThd")
			{
				throw new InvalidDataException("Missing header chunk in MIDI file.");
			}
			if (abinaryReader.Read32() != 6U)
			{
				throw new InvalidDataException("Invalid header size in MIDI file.");
			}
			MIDIFormat enumValue = (MIDIFormat)abinaryReader.Read16();
			if (!enumValue.IsDefined<MIDIFormat>())
			{
				throw new InvalidDataException("Invalid format in MIDI file.");
			}
			abinaryReader.Read16();
			int num = (int)abinaryReader.Read16();
			if ((num & 32768) != 0)
			{
				midi.division = new SMPTEDivision((SMPTEFrameRate)(-(SMPTEFrameRate)(num >> 8)), num & 255);
			}
			else
			{
				midi.division = new TicksPerBeatDivision(num);
			}
			int num2 = 0;
			while (!abinaryReader.IsAtEndOfStream)
			{
				if (abinaryReader.ReadRawString(4) != "MTrk")
				{
					throw new InvalidDataException("Missing track chunk in MIDI file.");
				}
				ulong num3 = 0UL;
				uint num4 = abinaryReader.Read32();
				abinaryReader.SetAnchor();
				Track track = new Track();
				byte b = 0;
				while (abinaryReader.Position < (long)((ulong)num4))
				{
					ulong num5 = abinaryReader.ReadUIntVar();
					byte b2 = abinaryReader.Read8();
					if (b2 < 128)
					{
						if (b == 0)
						{
							throw new InvalidDataException("Encountered running status event with no previous status available.");
						}
						b2 = b;
						abinaryReader.Position -= 1L;
					}
					if (b2 >= 240 && b2 <= 247)
					{
						b = 0;
					}
					else if (b2 >= 128 && b2 <= 239)
					{
						b = b2;
					}
					num3 += num5;
					if (b2 == 255)
					{
						MetaEventType metaEventType = (MetaEventType)abinaryReader.Read8();
						int num6 = (int)abinaryReader.ReadUIntVar();
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
								if (num6 != 2)
								{
									throw new InvalidDataException("Invalid size in sequence-number event.");
								}
								track.Add(new SequenceNumberEvent(num5, (int)abinaryReader.Read16()), num3);
								continue;
							case MetaEventType.Text:
								track.Add(new TextEvent(num5, abinaryReader.ReadRawString(num6)), num3);
								continue;
							case MetaEventType.CopyrightNotice:
								track.Add(new CopyrightNoticeEvent(num5, abinaryReader.ReadRawString(num6)), num3);
								continue;
							case MetaEventType.Name:
								track.Add(new TrackNameEvent(num5, abinaryReader.ReadRawString(num6)), num3);
								continue;
							case MetaEventType.InstrumentName:
								track.Add(new InstrumentNameEvent(num5, abinaryReader.ReadRawString(num6)), num3);
								continue;
							case MetaEventType.Lyrics:
								track.Add(new LyricsEvent(num5, abinaryReader.ReadRawString(num6)), num3);
								continue;
							case MetaEventType.Marker:
								track.Add(new MarkerEvent(num5, abinaryReader.ReadRawString(num6)), num3);
								continue;
							case MetaEventType.CuePoint:
								track.Add(new CuePointEvent(num5, abinaryReader.ReadRawString(num6)), num3);
								continue;
							default:
								if (metaEventType2 != MetaEventType.ChannelPrefix)
								{
									if (metaEventType2 == MetaEventType.EndOfTrack)
									{
										if (num6 != 0)
										{
											throw new InvalidDataException("Invalid size in end-of-track event.");
										}
										track.Add(new EndOfTrackEvent(num5), num3);
										continue;
									}
								}
								else
								{
									if (num6 != 1)
									{
										throw new InvalidDataException("Invalid size in channel-prefix event.");
									}
									track.Add(new ChannelPrefixEvent(num5, abinaryReader.Read8()), num3);
									continue;
								}
								break;
							}
						}
						else if (metaEventType2 != MetaEventType.TempoChange)
						{
							switch (metaEventType2)
							{
							case MetaEventType.TimeSignature:
								if (num6 != 4)
								{
									throw new InvalidDataException("Invalid size in time-signature event.");
								}
								track.Add(new TimeSignatureEvent(num5, (int)abinaryReader.Read8(), (int)abinaryReader.Read8(), (int)abinaryReader.Read8(), (int)abinaryReader.Read8()), num3);
								continue;
							case MetaEventType.KeySignature:
								if (num6 != 2)
								{
									throw new InvalidDataException("Invalid size in key-signature event.");
								}
								track.Add(new KeySignatureEvent(num5, (int)abinaryReader.ReadS8(), (Scale)abinaryReader.Read8()), num3);
								continue;
							default:
								if (metaEventType2 == MetaEventType.SequencerSpecific)
								{
									byte[] array = abinaryReader.Read8s(num6);
									if (num6 == 0 || num6 < ((array[0] == 0) ? 3 : 1))
									{
										throw new InvalidDataException("Invalid size in sequencer-specific event.");
									}
									bool flag = array[0] == 0;
									int manufacturerID = flag ? ((int)array[1] << 8 | (int)array[2]) : ((int)array[0]);
									track.Add(new SequencerSpecificEvent(num5, manufacturerID, flag, array.Duplicate(flag ? 3 : 1, array.Length - (flag ? 3 : 1))), num3);
									continue;
								}
								break;
							}
						}
						else
						{
							if (num6 != 3)
							{
								throw new InvalidDataException("Invalid size in tempo-change event.");
							}
							track.Add(new TempoChangeEvent(num5, abinaryReader.Read24()), num3);
							continue;
						}
						throw new NotImplementedException(string.Format("Encountered unimplemented meta event type {0}.", metaEventType));
					}
					else if (b2 == 240 || b2 == 247)
					{
						byte[] array2 = abinaryReader.Read8s((int)abinaryReader.ReadUIntVar());
						if (array2.Length == 0 || array2.Length < ((array2[0] == 0) ? 3 : 1))
						{
							throw new InvalidDataException("Encountered a SysEx event with an invalid size.");
						}
						SystemExclusiveEventType type = SystemExclusiveEventType.Normal;
						bool flag2 = b2 == 247;
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
						track.Add(new SystemExclusiveEvent(num5, type, manufacturerID2, flag4, array2), num3);
					}
					else
					{
						ChannelEventType channelEventType = (ChannelEventType)(b2 & 240);
						byte channelNumber = b2 & 15;
						if (!channelEventType.IsDefined<ChannelEventType>())
						{
							throw new InvalidDataException(string.Format("Encountered undefined channel-event type {0}.", channelEventType));
						}
						ChannelEventType channelEventType2 = channelEventType;
						if (channelEventType2 <= ChannelEventType.NoteAftertouch)
						{
							if (channelEventType2 == ChannelEventType.NoteOff)
							{
								track.Add(new NoteOffEvent(num5, (int)channelNumber, abinaryReader.Read8(), (int)abinaryReader.Read8()), num3);
								continue;
							}
							if (channelEventType2 == ChannelEventType.NoteOn)
							{
								track.Add(new NoteOnEvent(num5, (int)channelNumber, (int)abinaryReader.Read8(), (int)abinaryReader.Read8()), num3);
								continue;
							}
							if (channelEventType2 == ChannelEventType.NoteAftertouch)
							{
								track.Add(new NoteAftertouchEvent(num5, (int)channelNumber, (int)abinaryReader.Read8(), (int)abinaryReader.Read8()), num3);
								continue;
							}
						}
						else if (channelEventType2 <= ChannelEventType.ProgramChange)
						{
							if (channelEventType2 == ChannelEventType.Controller)
							{
								track.Add(new ControllerEvent(num5, (int)channelNumber, (int)abinaryReader.Read8(), (int)abinaryReader.Read8()), num3);
								continue;
							}
							if (channelEventType2 == ChannelEventType.ProgramChange)
							{
								track.Add(new ProgramChangeEvent(num5, (int)channelNumber, (int)abinaryReader.Read8()), num3);
								continue;
							}
						}
						else
						{
							if (channelEventType2 == ChannelEventType.ChannelAftertouch)
							{
								track.Add(new ChannelAftertouchEvent(num5, (int)channelNumber, (int)abinaryReader.Read8()), num3);
								continue;
							}
							if (channelEventType2 == ChannelEventType.PitchBend)
							{
								track.Add(new PitchBendEvent(num5, (int)channelNumber, (int)(abinaryReader.Read8() & 127) | (int)(abinaryReader.Read8() & 127) << 7), num3);
								continue;
							}
						}
						throw new NotImplementedException(string.Format("Encountered unimplemented channel event type {0}.", channelEventType));
					}
				}
				midi.tracks.Add(track);
				abinaryReader.ResetAnchor();
				num2++;
			}
			return midi;
		}

		// Token: 0x06000156 RID: 342 RVA: 0x00005661 File Offset: 0x00003861
		public void ForEach(Action<Track> action)
		{
			this.tracks.ForEach(action);
		}

		// Token: 0x06000157 RID: 343 RVA: 0x0000566F File Offset: 0x0000386F
		public IEnumerator<Track> GetEnumerator()
		{
			return this.tracks.GetEnumerator();
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00005681 File Offset: 0x00003881
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00005689 File Offset: 0x00003889
		public int IndexOf(Track track)
		{
			if (track == null)
			{
				throw new ArgumentNullException("track");
			}
			return this.tracks.IndexOf(track);
		}

		// Token: 0x0600015A RID: 346 RVA: 0x000056A5 File Offset: 0x000038A5
		public bool Remove(Track track)
		{
			if (track == null)
			{
				throw new ArgumentNullException("track");
			}
			return this.tracks.Remove(track);
		}

		// Token: 0x0600015B RID: 347 RVA: 0x000056C1 File Offset: 0x000038C1
		public int RemoveAll(Predicate<Track> predicate)
		{
			return this.tracks.RemoveAll(predicate);
		}

		// Token: 0x0600015C RID: 348 RVA: 0x000056CF File Offset: 0x000038CF
		public void RemoveAt(int index)
		{
			this.tracks.RemoveAt(index);
		}

		// Token: 0x0600015D RID: 349 RVA: 0x000056DD File Offset: 0x000038DD
		public void RemoveRange(int startingIndex, int count)
		{
			this.tracks.RemoveRange(startingIndex, count);
		}

		// Token: 0x0600015E RID: 350 RVA: 0x000056EC File Offset: 0x000038EC
		public void Save(string fileName)
		{
			if (fileName == null)
			{
				throw new ArgumentNullException("fileName");
			}
			using (FileStream fileStream = File.Create(fileName))
			{
				this.Save(fileStream);
			}
		}

		// Token: 0x0600015F RID: 351 RVA: 0x00005830 File Offset: 0x00003A30
		public void Save(Stream stream)
		{
			RIFFWriter riffwriter = new RIFFWriter(stream, "MThd", Endianness.Big, RIFFWriterOptions.SkipHeader | RIFFWriterOptions.SkipAligment);
			riffwriter.WriteChunk("MThd", delegate(ABinaryWriter mThdWriter)
			{
				mThdWriter.Write16((ushort)this.format);
				mThdWriter.Write16((ushort)this.tracks.Count);
				if (this.division is TicksPerBeatDivision)
				{
					mThdWriter.Write16((ushort)(this.division as TicksPerBeatDivision).TicksPerBeat);
					return;
				}
				if (this.division is SMPTEDivision)
				{
					SMPTEDivision smptedivision = this.division as SMPTEDivision;
					mThdWriter.WriteS8((sbyte)(-smptedivision.FrameRate << 8));
					mThdWriter.Write8((byte)smptedivision.TicksPerFrame);
				}
			});
			using (List<Track>.Enumerator enumerator = this.tracks.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Track track = enumerator.Current;
					riffwriter.WriteChunk("MTrk", delegate(ABinaryWriter mTrkWriter)
					{
						if (!track.IsTerminated)
						{
							throw new InvalidOperationException("All of the Tracks in the MIDI must be terminated with an end-of-track event before saving.");
						}
						foreach (MIDIEvent midievent in track)
						{
							midievent.ToStream(mTrkWriter.Stream);
						}
					});
				}
			}
			riffwriter.Close();
		}

		// Token: 0x04000052 RID: 82
		private TimeDivision division;

		// Token: 0x04000053 RID: 83
		private MIDIFormat format;

		// Token: 0x04000054 RID: 84
		private List<Track> tracks;
	}
}
