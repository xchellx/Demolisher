using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Arookas.Audio.SoundFonts.Generators;
using Arookas.Audio.SoundFonts.Modulators;
using Arookas.IO.Binary;
using Arookas.IO.Binary.ResourceInterchangeFileFormat;

namespace Arookas.Audio.SoundFonts
{
	// Token: 0x0200005B RID: 91
	public sealed class SoundFont
	{
		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x06000260 RID: 608 RVA: 0x000074CA File Offset: 0x000056CA
		// (set) Token: 0x06000261 RID: 609 RVA: 0x000074D2 File Offset: 0x000056D2
		public string Comment
		{
			get
			{
				return this.comment;
			}
			set
			{
				this.comment = value;
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x06000262 RID: 610 RVA: 0x000074DB File Offset: 0x000056DB
		// (set) Token: 0x06000263 RID: 611 RVA: 0x000074E3 File Offset: 0x000056E3
		public string Copyright
		{
			get
			{
				return this.copyright;
			}
			set
			{
				this.copyright = value;
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x06000264 RID: 612 RVA: 0x000074EC File Offset: 0x000056EC
		// (set) Token: 0x06000265 RID: 613 RVA: 0x000074F4 File Offset: 0x000056F4
		public string CreatingSoftware
		{
			get
			{
				return this.creatingSoftware;
			}
			set
			{
				this.creatingSoftware = value;
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000266 RID: 614 RVA: 0x000074FD File Offset: 0x000056FD
		public DateTime CreationDate
		{
			get
			{
				return this.creationDate;
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06000267 RID: 615 RVA: 0x00007505 File Offset: 0x00005705
		// (set) Token: 0x06000268 RID: 616 RVA: 0x0000750D File Offset: 0x0000570D
		public string Engineers
		{
			get
			{
				return this.engineers;
			}
			set
			{
				this.engineers = value;
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000269 RID: 617 RVA: 0x00007516 File Offset: 0x00005716
		public List<Instrument> Instruments
		{
			get
			{
				return this.instruments;
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x0600026A RID: 618 RVA: 0x0000751E File Offset: 0x0000571E
		// (set) Token: 0x0600026B RID: 619 RVA: 0x00007526 File Offset: 0x00005726
		public bool Is24Bit
		{
			get
			{
				return this.is24Bit;
			}
			set
			{
				this.is24Bit = value;
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x0600026C RID: 620 RVA: 0x0000752F File Offset: 0x0000572F
		// (set) Token: 0x0600026D RID: 621 RVA: 0x00007537 File Offset: 0x00005737
		public string ModifyingSoftware
		{
			get
			{
				return this.modifyingSoftware;
			}
			set
			{
				this.modifyingSoftware = value;
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x0600026E RID: 622 RVA: 0x00007540 File Offset: 0x00005740
		// (set) Token: 0x0600026F RID: 623 RVA: 0x00007548 File Offset: 0x00005748
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
				this.name = value;
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000270 RID: 624 RVA: 0x0000755F File Offset: 0x0000575F
		public List<Preset> Presets
		{
			get
			{
				return this.presets;
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000271 RID: 625 RVA: 0x00007567 File Offset: 0x00005767
		// (set) Token: 0x06000272 RID: 626 RVA: 0x0000756F File Offset: 0x0000576F
		public string Product
		{
			get
			{
				return this.product;
			}
			set
			{
				this.product = value;
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000273 RID: 627 RVA: 0x00007578 File Offset: 0x00005778
		// (set) Token: 0x06000274 RID: 628 RVA: 0x00007580 File Offset: 0x00005780
		public string ROMName
		{
			get
			{
				return this.romName;
			}
			set
			{
				this.romName = value;
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000275 RID: 629 RVA: 0x00007589 File Offset: 0x00005789
		// (set) Token: 0x06000276 RID: 630 RVA: 0x00007591 File Offset: 0x00005791
		public Version ROMVersion
		{
			get
			{
				return this.romVersion;
			}
			set
			{
				this.romVersion = value;
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x06000277 RID: 631 RVA: 0x0000759A File Offset: 0x0000579A
		public List<Sample> Samples
		{
			get
			{
				return this.samples;
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x06000278 RID: 632 RVA: 0x000075A2 File Offset: 0x000057A2
		// (set) Token: 0x06000279 RID: 633 RVA: 0x000075AA File Offset: 0x000057AA
		public string SoundEngine
		{
			get
			{
				return this.soundEngine;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.soundEngine = value;
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x0600027A RID: 634 RVA: 0x000075C1 File Offset: 0x000057C1
		public Version Version
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x0600027B RID: 635 RVA: 0x000075C9 File Offset: 0x000057C9
		public SoundFont() : this("New SoundFont", "")
		{
		}

		// Token: 0x0600027C RID: 636 RVA: 0x000075DC File Offset: 0x000057DC
		public SoundFont(string name, string soundEngine)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (soundEngine == null)
			{
				throw new ArgumentNullException("soundEngine");
			}
			this.name = name;
			this.soundEngine = soundEngine;
			this.creationDate = DateTime.Today;
			this.version = new Version(2, 4);
			this.presets = new List<Preset>(10);
			this.instruments = new List<Instrument>(10);
			this.samples = new List<Sample>(10);
		}

		// Token: 0x0600027D RID: 637 RVA: 0x000076A0 File Offset: 0x000058A0
		private SoundFont(RIFF riffFile)
		{
			if (riffFile == null)
			{
				throw new ArgumentNullException("riffFile");
			}
			if (riffFile.Identification != "sfbk")
			{
				throw new InvalidDataException("Not a SoundFont file.");
			}
			this.GetInformation(riffFile);
			if (this.version == null)
			{
				throw new InvalidDataException("Missing SoundFont version.");
			}
			if (this.soundEngine == null)
			{
				throw new InvalidDataException("Missing SoundFont sound engine.");
			}
			if (this.name == null)
			{
				throw new InvalidDataException("Missing SoundFont name.");
			}
			RIFFList rifflist = riffFile.Items.Lists.Single((RIFFList list) => list.Identification == "sdta");
			RIFFChunk smplChunk = rifflist.Items.Chunks.SingleOrDefault((RIFFChunk chunk) => chunk.Identification == "smpl");
			RIFFChunk sm24Chunk = rifflist.Items.Chunks.SingleOrDefault((RIFFChunk chunk) => chunk.Identification == "sm24");
			RIFFList pdtaList = riffFile.Items.Lists.Single((RIFFList list) => list.Identification == "pdta");
			this.LoadSamples(smplChunk, sm24Chunk, this.GetSampleHeaders(pdtaList));
			this.LoadInstruments(this.GetInstrumentHeaders(pdtaList), this.GetInstrumentBags(pdtaList), this.GetInstrumentGenerators(pdtaList), this.GetInstrumentModulators(pdtaList));
			this.LoadPresets(this.GetPresetHeaders(pdtaList), this.GetPresetBags(pdtaList), this.GetPresetGenerators(pdtaList), this.GetPresetModulators(pdtaList));
		}

		// Token: 0x0600027E RID: 638 RVA: 0x00007830 File Offset: 0x00005A30
		public static SoundFont FromFile(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (!File.Exists(path))
			{
				throw new FileNotFoundException("The specified SoundFont file was not found.", path);
			}
			SoundFont result;
			using (FileStream fileStream = File.OpenRead(path))
			{
				result = SoundFont.FromStream(fileStream);
			}
			return result;
		}

		// Token: 0x0600027F RID: 639 RVA: 0x0000788C File Offset: 0x00005A8C
		public static SoundFont FromStream(Stream stream)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			if (!stream.CanRead)
			{
				throw new ArgumentException("The specified Stream cannot read.", "stream");
			}
			SoundFont result;
			using (RIFF riff = new RIFF(stream))
			{
				try
				{
					result = new SoundFont(riff);
				}
				catch (Exception innerException)
				{
					throw new Exception("Failed to load SoundFont.", innerException);
				}
			}
			return result;
		}

		// Token: 0x06000280 RID: 640 RVA: 0x00007918 File Offset: 0x00005B18
		private void GetInformation(RIFF riffFile)
		{
			RIFFList rifflist = riffFile.Items.Lists.Single((RIFFList list) => list.Identification == "INFO");
			foreach (RIFFChunk riffchunk in rifflist.Items.Chunks)
			{
				riffchunk.Goto();
				string identification;
				switch (identification = rifflist.Identification)
				{
				case "ifil":
					this.version = new Version((int)riffchunk.BinaryReader.Read16(), (int)riffchunk.BinaryReader.Read16());
					break;
				case "isng":
					this.soundEngine = riffchunk.BinaryReader.ReadNullTerminatedString();
					break;
				case "irom":
					this.romName = riffchunk.BinaryReader.ReadNullTerminatedString();
					break;
				case "iver":
					this.romVersion = new Version((int)riffchunk.BinaryReader.Read16(), (int)riffchunk.BinaryReader.Read16());
					break;
				case "ICRD":
					this.creationDate = DateTime.Parse(riffchunk.BinaryReader.ReadNullTerminatedString());
					break;
				case "IENG":
					this.engineers = riffchunk.BinaryReader.ReadNullTerminatedString();
					break;
				case "IPRD":
					this.product = riffchunk.BinaryReader.ReadNullTerminatedString();
					break;
				case "ICOP":
					this.copyright = riffchunk.BinaryReader.ReadNullTerminatedString();
					break;
				case "ICMT":
					this.comment = riffchunk.BinaryReader.ReadNullTerminatedString();
					break;
				case "ISFT":
				{
					Regex regex = new Regex("(?'creatingSoftware'.*):(?'modifyingSoftware'.*)");
					Match match = regex.Match(riffchunk.BinaryReader.ReadNullTerminatedString());
					if (match.Success)
					{
						this.creatingSoftware = match.Groups["creatingSoftware"].Value;
						this.modifyingSoftware = match.Groups["modifyingSoftware"].Value;
					}
					break;
				}
				}
			}
		}

		// Token: 0x06000281 RID: 641 RVA: 0x00007BEC File Offset: 0x00005DEC
		private sfInstBag[] GetInstrumentBags(RIFFList pdtaList)
		{
			RIFFChunk riffchunk = pdtaList.Items.Chunks.Single((RIFFChunk chunk) => chunk.Identification == "ibag");
			int num = (int)(riffchunk.Length / 4U);
			if (riffchunk.Length % 4U != 0U)
			{
				throw new InvalidDataException("Instrument bag size is invalid.");
			}
			riffchunk.Goto();
			sfInstBag[] array = new sfInstBag[num];
			for (int i = 0; i < num; i++)
			{
				array[i] = new sfInstBag(riffchunk.BinaryReader);
			}
			return array;
		}

		// Token: 0x06000282 RID: 642 RVA: 0x00007C88 File Offset: 0x00005E88
		private sfInst[] GetInstrumentHeaders(RIFFList pdtaList)
		{
			RIFFChunk riffchunk = pdtaList.Items.Chunks.Single((RIFFChunk chunk) => chunk.Identification == "inst");
			int num = (int)(riffchunk.Length / 22U);
			if (riffchunk.Length % 22U != 0U || num < 2)
			{
				throw new InvalidDataException("Instrument header size is invalid.");
			}
			riffchunk.Goto();
			sfInst[] array = new sfInst[num];
			for (int i = 0; i < num; i++)
			{
				array[i] = new sfInst(riffchunk.BinaryReader);
			}
			return array;
		}

		// Token: 0x06000283 RID: 643 RVA: 0x00007D2C File Offset: 0x00005F2C
		private sfGenList[] GetInstrumentGenerators(RIFFList pdtaList)
		{
			RIFFChunk riffchunk = pdtaList.Items.Chunks.Single((RIFFChunk chunk) => chunk.Identification == "igen");
			int num = (int)(riffchunk.Length / 4U);
			if (riffchunk.Length % 4U != 0U)
			{
				throw new InvalidDataException("Instrument generator size is invalid.");
			}
			riffchunk.Goto();
			sfGenList[] array = new sfGenList[num];
			for (int i = 0; i < num; i++)
			{
				array[i] = new sfGenList(riffchunk.BinaryReader);
			}
			return array;
		}

		// Token: 0x06000284 RID: 644 RVA: 0x00007DC8 File Offset: 0x00005FC8
		private sfModList[] GetInstrumentModulators(RIFFList pdtaList)
		{
			RIFFChunk riffchunk = pdtaList.Items.Chunks.Single((RIFFChunk chunk) => chunk.Identification == "imod");
			int num = (int)(riffchunk.Length / 10U);
			if (riffchunk.Length % 10U != 0U)
			{
				throw new InvalidDataException("Instrument modulator size is invalid.");
			}
			riffchunk.Goto();
			sfModList[] array = new sfModList[num];
			for (int i = 0; i < num; i++)
			{
				array[i] = new sfModList(riffchunk.BinaryReader);
			}
			return array;
		}

		// Token: 0x06000285 RID: 645 RVA: 0x00007E68 File Offset: 0x00006068
		private sfPresetBag[] GetPresetBags(RIFFList pdtaList)
		{
			RIFFChunk riffchunk = pdtaList.Items.Chunks.Single((RIFFChunk chunk) => chunk.Identification == "pbag");
			int num = (int)(riffchunk.Length / 4U);
			if (riffchunk.Length % 4U != 0U)
			{
				throw new InvalidDataException("Preset bag size is invalid.");
			}
			riffchunk.Goto();
			sfPresetBag[] array = new sfPresetBag[num];
			for (int i = 0; i < num; i++)
			{
				array[i] = new sfPresetBag(riffchunk.BinaryReader);
			}
			return array;
		}

		// Token: 0x06000286 RID: 646 RVA: 0x00007F04 File Offset: 0x00006104
		private sfPresetHeader[] GetPresetHeaders(RIFFList pdtaList)
		{
			RIFFChunk riffchunk = pdtaList.Items.Chunks.Single((RIFFChunk chunk) => chunk.Identification == "phdr");
			int num = (int)(riffchunk.Length / 38U);
			if (riffchunk.Length % 38U != 0U || num < 2)
			{
				throw new InvalidDataException("Preset header size is invalid.");
			}
			riffchunk.Goto();
			sfPresetHeader[] array = new sfPresetHeader[num];
			for (int i = 0; i < num; i++)
			{
				array[i] = new sfPresetHeader(riffchunk.BinaryReader);
			}
			return array;
		}

		// Token: 0x06000287 RID: 647 RVA: 0x00007FA8 File Offset: 0x000061A8
		private sfGenList[] GetPresetGenerators(RIFFList pdtaList)
		{
			RIFFChunk riffchunk = pdtaList.Items.Chunks.Single((RIFFChunk chunk) => chunk.Identification == "pgen");
			int num = (int)(riffchunk.Length / 4U);
			if (riffchunk.Length % 4U != 0U)
			{
				throw new InvalidDataException("Preset generator size is invalid.");
			}
			riffchunk.Goto();
			sfGenList[] array = new sfGenList[num];
			for (int i = 0; i < num; i++)
			{
				array[i] = new sfGenList(riffchunk.BinaryReader);
			}
			return array;
		}

		// Token: 0x06000288 RID: 648 RVA: 0x00008044 File Offset: 0x00006244
		private sfModList[] GetPresetModulators(RIFFList pdtaList)
		{
			RIFFChunk riffchunk = pdtaList.Items.Chunks.Single((RIFFChunk chunk) => chunk.Identification == "pmod");
			int num = (int)(riffchunk.Length / 10U);
			if (riffchunk.Length % 10U != 0U)
			{
				throw new InvalidDataException("Preset modulator size is invalid.");
			}
			riffchunk.Goto();
			sfModList[] array = new sfModList[num];
			for (int i = 0; i < num; i++)
			{
				array[i] = new sfModList(riffchunk.BinaryReader);
			}
			return array;
		}

		// Token: 0x06000289 RID: 649 RVA: 0x000080E4 File Offset: 0x000062E4
		private sfSample[] GetSampleHeaders(RIFFList pdtaList)
		{
			RIFFChunk riffchunk = pdtaList.Items.Chunks.Single((RIFFChunk chunk) => chunk.Identification == "shdr");
			int num = (int)(riffchunk.Length / 46U);
			if (riffchunk.Length % 46U != 0U)
			{
				throw new InvalidDataException("Sample header size is invalid.");
			}
			riffchunk.Goto();
			sfSample[] array = new sfSample[num];
			for (int i = 0; i < num; i++)
			{
				array[i] = new sfSample(riffchunk.BinaryReader);
			}
			return array;
		}

		// Token: 0x0600028A RID: 650 RVA: 0x00008170 File Offset: 0x00006370
		private void LoadInstruments(sfInst[] instrumentHeaders, sfInstBag[] instrumentBags, sfGenList[] instrumentGenerators, sfModList[] instrumentModulators)
		{
			this.instruments = new List<Instrument>(instrumentHeaders.Length - 1);
			List<Tuple<ModulatorModulatorDestination, ushort>> list = new List<Tuple<ModulatorModulatorDestination, ushort>>(10);
			for (int i = 0; i < instrumentHeaders.Length - 1; i++)
			{
				Instrument instrument = new Instrument(instrumentHeaders[i].achInstName);
				for (int j = (int)instrumentHeaders[i].wInstBagNdx; j < (int)instrumentHeaders[i + 1].wInstBagNdx; j++)
				{
					InstrumentZone instrumentZone = new InstrumentZone();
					bool flag = true;
					bool flag2 = true;
					int k = (int)instrumentBags[j].wInstGenNdx;
					while (k < (int)instrumentBags[j + 1].wInstGenNdx)
					{
						switch (instrumentGenerators[k].sfGenOper)
						{
						case SFGenerator.instrument:
							if (k < (int)(instrumentBags[j + 1].wInstGenNdx - 1))
							{
								throw new InvalidDataException("Instrument generator at invalid index.");
							}
							instrumentZone.Sample = this.samples[(int)instrumentGenerators[k].genAmount.wAmount];
							break;
						case (SFGenerator)42:
							goto IL_161;
						case SFGenerator.keyRange:
							if (!flag)
							{
								throw new InvalidDataException("Key-range generator at invalid index.");
							}
							instrumentZone.KeyRange = new KeyRange((int)instrumentGenerators[k].genAmount.byLo, (int)instrumentGenerators[k].genAmount.byHi);
							flag = false;
							break;
						case SFGenerator.velRange:
							if (!flag2)
							{
								throw new InvalidDataException("Velocity-range generator at invalid index.");
							}
							instrumentZone.VelocityRange = new VelocityRange((int)instrumentGenerators[k].genAmount.byLo, (int)instrumentGenerators[k].genAmount.byHi);
							flag2 = false;
							break;
						default:
							goto IL_161;
						}
						IL_18D:
						k++;
						continue;
						IL_161:
						Generator generator = Generator.FromInternal(instrumentGenerators[k]);
						if (generator != null)
						{
							instrumentZone.Generators.Add(generator);
						}
						flag = false;
						flag2 = false;
						goto IL_18D;
					}
					for (int l = (int)instrumentBags[j].wInstModNdx; l < (int)instrumentBags[j + 1].wInstModNdx; l++)
					{
						ModulatorSource source = ModulatorSource.FromInternal(instrumentModulators[l].sfModSrcOper);
						ModulatorDestination modulatorDestination = ModulatorDestination.FromInternal(instrumentModulators[l].sfModDestOper);
						short modAmount = instrumentModulators[l].modAmount;
						ModulatorSource amountSource = ModulatorSource.FromInternal(instrumentModulators[l].sfModAmtSrcOper);
						ModulatorTransform sfModTransOper = (ModulatorTransform)instrumentModulators[l].sfModTransOper;
						instrumentZone.Modulators.Add(new Modulator
						{
							Source = source,
							Destination = modulatorDestination,
							Amount = (int)modAmount,
							AmountSource = amountSource,
							Transform = sfModTransOper
						});
						if (modulatorDestination is ModulatorModulatorDestination)
						{
							list.Add(new Tuple<ModulatorModulatorDestination, ushort>(modulatorDestination as ModulatorModulatorDestination, instrumentModulators[l].sfModDestOper & 32767));
						}
					}
					foreach (Tuple<ModulatorModulatorDestination, ushort> tuple in list)
					{
						if ((int)tuple.Item2 >= instrumentZone.Modulators.Count)
						{
							throw new InvalidDataException("Instrument zone modulator link index is invalid.");
						}
						tuple.Item1.DestinationModulator = instrumentZone.Modulators[(int)tuple.Item2];
					}
					list.Clear();
					instrument.Zones.Add(instrumentZone);
				}
				this.instruments.Add(instrument);
			}
		}

		// Token: 0x0600028B RID: 651 RVA: 0x000084E8 File Offset: 0x000066E8
		private void LoadPresets(sfPresetHeader[] presetHeaders, sfPresetBag[] presetBags, sfGenList[] presetGenerators, sfModList[] presetModulators)
		{
			this.presets = new List<Preset>(presetHeaders.Length - 1);
			List<Tuple<ModulatorModulatorDestination, ushort>> list = new List<Tuple<ModulatorModulatorDestination, ushort>>(10);
			for (int i = 0; i < presetHeaders.Length - 1; i++)
			{
				Preset preset = new Preset(presetHeaders[i].achPresetName, (int)presetHeaders[i].wBank, (int)presetHeaders[i].wPreset)
				{
					Genre = presetHeaders[i].dwGenre,
					Library = presetHeaders[i].dwGenre,
					Morphology = presetHeaders[i].dwMorphology
				};
				for (int j = (int)presetHeaders[i].wPresetBagNdx; j < (int)presetHeaders[i + 1].wPresetBagNdx; j++)
				{
					PresetZone presetZone = new PresetZone();
					bool flag = true;
					bool flag2 = true;
					int k = (int)presetBags[j].wGenNdx;
					while (k < (int)presetBags[j + 1].wGenNdx)
					{
						switch (presetGenerators[k].sfGenOper)
						{
						case SFGenerator.instrument:
							if (k < (int)(presetBags[j + 1].wGenNdx - 1))
							{
								throw new InvalidDataException("Instrument generator at invalid index.");
							}
							presetZone.Instrument = this.instruments[(int)presetGenerators[k].genAmount.wAmount];
							break;
						case (SFGenerator)42:
							goto IL_1B6;
						case SFGenerator.keyRange:
							if (!flag)
							{
								throw new InvalidDataException("Key-range generator at invalid index.");
							}
							presetZone.KeyRange = new KeyRange((int)presetGenerators[k].genAmount.byLo, (int)presetGenerators[k].genAmount.byHi);
							flag = false;
							break;
						case SFGenerator.velRange:
							if (!flag2)
							{
								throw new InvalidDataException("Velocity-range generator at invalid index.");
							}
							presetZone.VelocityRange = new VelocityRange((int)presetGenerators[k].genAmount.byLo, (int)presetGenerators[k].genAmount.byHi);
							flag2 = false;
							break;
						default:
							goto IL_1B6;
						}
						IL_1E2:
						k++;
						continue;
						IL_1B6:
						Generator generator = Generator.FromInternal(presetGenerators[k]);
						if (generator != null)
						{
							presetZone.Generators.Add(generator);
						}
						flag = false;
						flag2 = false;
						goto IL_1E2;
					}
					for (int l = (int)presetBags[j].wModNdx; l < (int)presetBags[j + 1].wModNdx; l++)
					{
						ModulatorSource source = ModulatorSource.FromInternal(presetModulators[l].sfModSrcOper);
						ModulatorDestination modulatorDestination = ModulatorDestination.FromInternal(presetModulators[l].sfModDestOper);
						short modAmount = presetModulators[l].modAmount;
						ModulatorSource amountSource = ModulatorSource.FromInternal(presetModulators[l].sfModAmtSrcOper);
						ModulatorTransform sfModTransOper = (ModulatorTransform)presetModulators[l].sfModTransOper;
						presetZone.Modulators.Add(new Modulator
						{
							Source = source,
							Destination = modulatorDestination,
							Amount = (int)modAmount,
							AmountSource = amountSource,
							Transform = sfModTransOper
						});
						if (modulatorDestination is ModulatorModulatorDestination)
						{
							list.Add(new Tuple<ModulatorModulatorDestination, ushort>(modulatorDestination as ModulatorModulatorDestination, presetModulators[l].sfModDestOper & 32767));
						}
					}
					foreach (Tuple<ModulatorModulatorDestination, ushort> tuple in list)
					{
						if ((int)tuple.Item2 >= presetZone.Modulators.Count)
						{
							throw new InvalidDataException("Preset zone modulator link index is invalid.");
						}
						tuple.Item1.DestinationModulator = presetZone.Modulators[(int)tuple.Item2];
					}
					list.Clear();
					preset.Zones.Add(presetZone);
				}
				this.presets.Add(preset);
			}
		}

		// Token: 0x0600028C RID: 652 RVA: 0x000088BC File Offset: 0x00006ABC
		private void LoadSamples(RIFFChunk smplChunk, RIFFChunk sm24Chunk, sfSample[] sampleHeaders)
		{
			this.samples = new List<Sample>(sampleHeaders.Length - 1);
			for (int i = 0; i < sampleHeaders.Length - 1; i++)
			{
				Sample sample = new Sample(sampleHeaders[i].achSampleName)
				{
					LoopStart = (int)sampleHeaders[i].dwStartLoop,
					LoopEnd = (int)sampleHeaders[i].dwEndLoop,
					SampleRate = (int)sampleHeaders[i].dwSampleRate,
					RootNote = (int)sampleHeaders[i].byOriginalKey,
					PitchCorrection = (int)sampleHeaders[i].chCorrect,
					IsROM = (sampleHeaders[i].sfSampleType > SFSampleLink.Linked)
				};
				smplChunk.Goto();
				smplChunk.BinaryReader.Step((long)((ulong)sampleHeaders[i].dwStart));
				sample.Samples = Array.ConvertAll<short, Int24>(smplChunk.BinaryReader.ReadS16s((int)((sampleHeaders[i].dwEnd - sampleHeaders[i].dwStart) / 2U)), (short smpl) => smpl);
				if (sm24Chunk != null)
				{
					sm24Chunk.Goto();
					sm24Chunk.BinaryReader.Step((long)((ulong)(sampleHeaders[i].dwStart / 2U)));
					for (int j = 0; j < sample.Samples.Length; j++)
					{
						sample.Samples[j] = (Int24)(sample.Samples[j] << 8 | (int)sm24Chunk.BinaryReader.Read8());
					}
				}
				this.samples.Add(sample);
			}
			int k = 0;
			while (k < sampleHeaders.Length - 1)
			{
				SFSampleLink sfSampleType = sampleHeaders[k].sfSampleType;
				if (sfSampleType <= SFSampleLink.Linked)
				{
					switch (sfSampleType)
					{
					case SFSampleLink.Right:
						goto IL_238;
					case (SFSampleLink)3:
						break;
					case SFSampleLink.Left:
						goto IL_1F9;
					default:
						if (sfSampleType == SFSampleLink.Linked)
						{
							goto IL_277;
						}
						break;
					}
				}
				else
				{
					switch (sfSampleType)
					{
					case SFSampleLink.RightROM:
						goto IL_238;
					case (SFSampleLink)32771:
						break;
					case SFSampleLink.LeftROM:
						goto IL_1F9;
					default:
						if (sfSampleType == SFSampleLink.LinkedROM)
						{
							goto IL_277;
						}
						break;
					}
				}
				IL_2B4:
				k++;
				continue;
				IL_1F9:
				this.samples[k].Link = this.samples[(int)sampleHeaders[k].wSampleLink];
				this.samples[k].LinkType = SampleLinkType.Right;
				goto IL_2B4;
				IL_238:
				this.samples[k].Link = this.samples[(int)sampleHeaders[k].wSampleLink];
				this.samples[k].LinkType = SampleLinkType.Left;
				goto IL_2B4;
				IL_277:
				this.samples[k].Link = this.samples[(int)sampleHeaders[k].wSampleLink];
				this.samples[k].LinkType = SampleLinkType.Linked;
				goto IL_2B4;
			}
		}

		// Token: 0x0600028D RID: 653 RVA: 0x00008B90 File Offset: 0x00006D90
		public void Save(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			using (FileStream fileStream = File.Create(path))
			{
				this.Save(fileStream);
			}
		}

		// Token: 0x0600028E RID: 654 RVA: 0x00009D84 File Offset: 0x00007F84
		public void Save(Stream stream)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			if (!stream.CanWrite)
			{
				throw new ArgumentException("The specified Stream cannot write.", "stream");
			}
			using (RIFFWriter riffWriter = new RIFFWriter(stream, "sfbk"))
			{
				riffWriter.WriteLIST("INFO", delegate
				{
					riffWriter.WriteChunk("ifil", delegate(ABinaryWriter ifilWriter)
					{
						ifilWriter.Write16((ushort)this.version.Major);
						ifilWriter.Write16((ushort)this.version.Minor);
					});
					riffWriter.WriteChunk("isng", delegate(ABinaryWriter isngWriter)
					{
						isngWriter.WriteString(this.soundEngine, ABinaryStringFormat.NullTerminated);
					});
					riffWriter.WriteChunk("INAM", delegate(ABinaryWriter inamWriter)
					{
						inamWriter.WriteString(this.name, ABinaryStringFormat.NullTerminated);
					});
					if (this.romName != null)
					{
						riffWriter.WriteChunk("irom", delegate(ABinaryWriter iromWriter)
						{
							iromWriter.WriteString(this.romName, ABinaryStringFormat.NullTerminated);
						});
					}
					if (this.romVersion != null)
					{
						riffWriter.WriteChunk("iver", delegate(ABinaryWriter iverWriter)
						{
							iverWriter.Write16((ushort)this.romVersion.Major);
							iverWriter.Write16((ushort)this.romVersion.Minor);
						});
					}
					riffWriter.WriteChunk("ICRD", delegate(ABinaryWriter icrdWriter)
					{
						icrdWriter.WriteString(this.creationDate.ToString("MMMM dd, YYYY"));
					});
					if (this.engineers != null)
					{
						riffWriter.WriteChunk("IENG", delegate(ABinaryWriter iengWriter)
						{
							iengWriter.WriteString(this.engineers, ABinaryStringFormat.NullTerminated);
						});
					}
					if (this.product != null)
					{
						riffWriter.WriteChunk("IPRD", delegate(ABinaryWriter iprdWriter)
						{
							iprdWriter.WriteString(this.product, ABinaryStringFormat.NullTerminated);
						});
					}
					if (this.copyright != null)
					{
						riffWriter.WriteChunk("ICOP", delegate(ABinaryWriter icopWriter)
						{
							icopWriter.WriteString(this.copyright, ABinaryStringFormat.NullTerminated);
						});
					}
					if (this.comment != null)
					{
						riffWriter.WriteChunk("ICMT", delegate(ABinaryWriter icmtWriter)
						{
							icmtWriter.WriteString(this.comment, ABinaryStringFormat.NullTerminated);
						});
					}
					if (this.creatingSoftware != null)
					{
						riffWriter.WriteChunk("ISFT", delegate(ABinaryWriter isftWriter)
						{
							isftWriter.WriteString(string.Format("{0}:{1}", this.creatingSoftware, this.modifyingSoftware), ABinaryStringFormat.NullTerminated);
						});
					}
				});
				riffWriter.WriteLIST("sdta", delegate
				{
					riffWriter.WriteChunk("smpl", delegate(ABinaryWriter smplWriter)
					{
						foreach (Sample sample in this.samples)
						{
							if (sample.Samples != null)
							{
								foreach (Int24 other in sample.Samples)
								{
									smplWriter.Write16(this.is24Bit ? ((ushort)(other >> 8)) : ((ushort)other));
								}
							}
						}
					});
					if (this.is24Bit)
					{
						riffWriter.WriteChunk("sm24", delegate(ABinaryWriter sm24Writer)
						{
							foreach (Sample sample in this.samples)
							{
								if (sample.Samples != null)
								{
									foreach (Int24 other in sample.Samples)
									{
										sm24Writer.Write16((ushort)(other & 255));
									}
								}
							}
						});
					}
				});
				riffWriter.WriteLIST("pdta", delegate
				{
					riffWriter.WriteChunk("phdr", delegate(ABinaryWriter phdrWriter)
					{
						ushort num = 0;
						foreach (Preset preset in this.presets)
						{
							phdrWriter.WriteString(preset.Name, ABinaryStringFormat.Clamped(20));
							phdrWriter.Write16((ushort)preset.BankNumber);
							phdrWriter.Write16((ushort)preset.ProgramNumber);
							phdrWriter.Write16(num);
							phdrWriter.Write32(preset.Library);
							phdrWriter.Write32(preset.Genre);
							phdrWriter.Write32(preset.Morphology);
							num += (ushort)preset.Zones.Count;
						}
					});
					riffWriter.WriteChunk("pbag", delegate(ABinaryWriter pbagWriter)
					{
						ushort num = 0;
						ushort num2 = 0;
						foreach (Preset preset in this.presets)
						{
							foreach (PresetZone presetZone in preset.Zones)
							{
								pbagWriter.Write16(num);
								pbagWriter.Write16(num2);
								num += (ushort)presetZone.Generators.Count;
								num2 += (ushort)presetZone.Modulators.Count;
								if (presetZone.KeyRange != KeyRange.FullRange)
								{
									num += 1;
								}
								if (presetZone.VelocityRange != VelocityRange.FullRange)
								{
									num += 1;
								}
								if (presetZone.Instrument != null)
								{
									num += 1;
								}
							}
						}
					});
					riffWriter.WriteChunk("pmod", delegate(ABinaryWriter pmodWriter)
					{
						foreach (Preset preset in this.presets)
						{
							foreach (PresetZone presetZone in preset.Zones)
							{
								foreach (Modulator modulator in presetZone.Modulators)
								{
									pmodWriter.Write16(modulator.Source.ToInternal());
									if (modulator.Destination is ModulatorGeneratorDestination)
									{
										pmodWriter.Write16((ushort)(modulator.Destination as ModulatorGeneratorDestination).DestinationGenerator);
									}
									else
									{
										if (!(modulator.Destination is ModulatorModulatorDestination))
										{
											throw new InvalidOperationException("The modulator source is not a general controller or MIDI controller.");
										}
										int num = presetZone.Modulators.IndexOf((modulator.Destination as ModulatorModulatorDestination).DestinationModulator);
										if (num < 0)
										{
											throw new InvalidOperationException("A preset zone refers to a modulator not belonging to the same preset zone.");
										}
										pmodWriter.Write16((ushort)(32768 + num));
									}
									pmodWriter.WriteS16((short)modulator.Amount);
									pmodWriter.Write16(modulator.AmountSource.ToInternal());
									pmodWriter.Write16((ushort)modulator.Transform);
								}
							}
						}
					});
					riffWriter.WriteChunk("pgen", delegate(ABinaryWriter pgenWriter)
					{
						foreach (Preset preset in this.presets)
						{
							foreach (PresetZone presetZone in preset.Zones)
							{
								if (presetZone.KeyRange != KeyRange.FullRange)
								{
									pgenWriter.Write16(43);
									pgenWriter.Write8((byte)presetZone.KeyRange.LowKey);
									pgenWriter.Write8((byte)presetZone.KeyRange.HighKey);
								}
								if (presetZone.VelocityRange != VelocityRange.FullRange)
								{
									pgenWriter.Write16(44);
									pgenWriter.Write8((byte)presetZone.VelocityRange.LowVelocity);
									pgenWriter.Write8((byte)presetZone.VelocityRange.HighVelocity);
								}
								foreach (Generator generator in presetZone.Generators)
								{
									sfGenList sfGenList = generator.ToInternal();
									pgenWriter.Write16((ushort)sfGenList.sfGenOper);
									pgenWriter.Write16(sfGenList.genAmount.wAmount);
								}
								if (presetZone.Instrument != null)
								{
									if (!this.instruments.Contains(presetZone.Instrument))
									{
										throw new InvalidOperationException("A preset zone refers to an instrument not belonging to the same SoundFont.");
									}
									pgenWriter.Write16(41);
									pgenWriter.Write16((ushort)this.instruments.IndexOf(presetZone.Instrument));
								}
							}
						}
					});
					riffWriter.WriteChunk("inst", delegate(ABinaryWriter instWriter)
					{
						ushort num = 0;
						foreach (Instrument instrument in this.instruments)
						{
							instWriter.WriteString(instrument.Name, ABinaryStringFormat.Clamped(20));
							instWriter.Write16(num);
							num += (ushort)instrument.Zones.Count;
						}
					});
					riffWriter.WriteChunk("ibag", delegate(ABinaryWriter ibagWriter)
					{
						ushort num = 0;
						ushort num2 = 0;
						foreach (Instrument instrument in this.instruments)
						{
							foreach (InstrumentZone instrumentZone in instrument.Zones)
							{
								ibagWriter.Write16(num);
								ibagWriter.Write16(num2);
								num += (ushort)instrumentZone.Generators.Count;
								num2 += (ushort)instrumentZone.Modulators.Count;
								if (instrumentZone.KeyRange != KeyRange.FullRange)
								{
									num += 1;
								}
								if (instrumentZone.VelocityRange != VelocityRange.FullRange)
								{
									num += 1;
								}
								if (instrumentZone.Sample != null)
								{
									num += 1;
								}
							}
						}
					});
					riffWriter.WriteChunk("imod", delegate(ABinaryWriter imodWriter)
					{
						foreach (Instrument instrument in this.instruments)
						{
							foreach (InstrumentZone instrumentZone in instrument.Zones)
							{
								foreach (Modulator modulator in instrumentZone.Modulators)
								{
									imodWriter.Write16(modulator.Source.ToInternal());
									if (modulator.Destination is ModulatorGeneratorDestination)
									{
										imodWriter.Write16((ushort)(modulator.Destination as ModulatorGeneratorDestination).DestinationGenerator);
									}
									else
									{
										if (!(modulator.Destination is ModulatorModulatorDestination))
										{
											throw new InvalidOperationException("The modulator source is not a general controller or MIDI controller.");
										}
										int num = instrumentZone.Modulators.IndexOf((modulator.Destination as ModulatorModulatorDestination).DestinationModulator);
										if (num < 0)
										{
											throw new InvalidOperationException("An instrument zone refers to a modulator not belonging to the same instrument zone.");
										}
										imodWriter.Write16((ushort)(32768 + num));
									}
									imodWriter.WriteS16((short)modulator.Amount);
									imodWriter.Write16(modulator.AmountSource.ToInternal());
									imodWriter.Write16((ushort)modulator.Transform);
								}
							}
						}
					});
					riffWriter.WriteChunk("igen", delegate(ABinaryWriter igenWriter)
					{
						foreach (Instrument instrument in this.instruments)
						{
							foreach (InstrumentZone instrumentZone in instrument.Zones)
							{
								if (instrumentZone.KeyRange != KeyRange.FullRange)
								{
									igenWriter.Write16(43);
									igenWriter.Write8((byte)instrumentZone.KeyRange.LowKey);
									igenWriter.Write8((byte)instrumentZone.KeyRange.HighKey);
								}
								if (instrumentZone.VelocityRange != VelocityRange.FullRange)
								{
									igenWriter.Write16(44);
									igenWriter.Write8((byte)instrumentZone.VelocityRange.LowVelocity);
									igenWriter.Write8((byte)instrumentZone.VelocityRange.HighVelocity);
								}
								foreach (Generator generator in instrumentZone.Generators)
								{
									sfGenList sfGenList = generator.ToInternal();
									igenWriter.Write16((ushort)sfGenList.sfGenOper);
									igenWriter.Write16(sfGenList.genAmount.wAmount);
								}
								if (instrumentZone.Sample != null)
								{
									if (!this.samples.Contains(instrumentZone.Sample))
									{
										throw new InvalidOperationException("An instrument zone refers to an sample not belonging to the same SoundFont.");
									}
									igenWriter.Write16(41);
									igenWriter.Write16((ushort)this.samples.IndexOf(instrumentZone.Sample));
								}
							}
						}
					});
					riffWriter.WriteChunk("shdr", delegate(ABinaryWriter shdrWriter)
					{
						uint num = 0U;
						foreach (Sample sample in this.samples)
						{
							shdrWriter.WriteString(sample.Name, ABinaryStringFormat.Clamped(20));
							shdrWriter.Write32(num);
							shdrWriter.Write32((uint)((ulong)num + (ulong)((long)sample.Samples.Length)));
							shdrWriter.Write32((uint)sample.LoopStart);
							shdrWriter.Write32((uint)sample.LoopEnd);
							shdrWriter.Write32((uint)sample.SampleRate);
							shdrWriter.Write8((byte)sample.RootNote);
							shdrWriter.WriteS8((sbyte)sample.PitchCorrection);
							shdrWriter.Write16((ushort)(sample.IsMono ? 0 : this.samples.IndexOf(sample.Link)));
							shdrWriter.Write16((ushort)((sample.IsROM ? 32768 : 0) + (sample.IsMono ? ((SampleLinkType)1) : sample.LinkType)));
							if (sample.Samples != null)
							{
								num += (uint)(sample.Samples.Length + 46);
							}
						}
					});
				});
			}
		}

		// Token: 0x04000160 RID: 352
		private string comment;

		// Token: 0x04000161 RID: 353
		private string copyright;

		// Token: 0x04000162 RID: 354
		private string creatingSoftware;

		// Token: 0x04000163 RID: 355
		private DateTime creationDate;

		// Token: 0x04000164 RID: 356
		private string engineers;

		// Token: 0x04000165 RID: 357
		private List<Instrument> instruments;

		// Token: 0x04000166 RID: 358
		private bool is24Bit;

		// Token: 0x04000167 RID: 359
		private string modifyingSoftware;

		// Token: 0x04000168 RID: 360
		private string name;

		// Token: 0x04000169 RID: 361
		private List<Preset> presets;

		// Token: 0x0400016A RID: 362
		private string product;

		// Token: 0x0400016B RID: 363
		private string romName;

		// Token: 0x0400016C RID: 364
		private Version romVersion;

		// Token: 0x0400016D RID: 365
		private List<Sample> samples;

		// Token: 0x0400016E RID: 366
		private string soundEngine;

		// Token: 0x0400016F RID: 367
		private Version version;
	}
}
