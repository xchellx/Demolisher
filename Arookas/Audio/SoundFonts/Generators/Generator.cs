using System;

namespace Arookas.Audio.SoundFonts.Generators
{
	// Token: 0x0200002E RID: 46
	public abstract class Generator
	{
		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000192 RID: 402 RVA: 0x0000618B File Offset: 0x0000438B
		// (set) Token: 0x06000193 RID: 403 RVA: 0x00006193 File Offset: 0x00004393
		protected byte InternalRangeHigh
		{
			get
			{
				return this.internalRangeHigh;
			}
			set
			{
				this.internalRangeHigh = value;
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000194 RID: 404 RVA: 0x0000619C File Offset: 0x0000439C
		// (set) Token: 0x06000195 RID: 405 RVA: 0x000061A4 File Offset: 0x000043A4
		protected byte InternalRangeLow
		{
			get
			{
				return this.internalRangeLow;
			}
			set
			{
				this.internalRangeLow = value;
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000196 RID: 406
		public abstract GeneratorType Type { get; }

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000197 RID: 407 RVA: 0x000061AD File Offset: 0x000043AD
		// (set) Token: 0x06000198 RID: 408 RVA: 0x000061B5 File Offset: 0x000043B5
		protected int InternalValue
		{
			get
			{
				return this.internalValue;
			}
			set
			{
				this.internalValue = value;
			}
		}

		// Token: 0x06000199 RID: 409 RVA: 0x000061C0 File Offset: 0x000043C0
		internal static Generator FromInternal(sfGenList generator)
		{
			switch (generator.sfGenOper)
			{
			case SFGenerator.startAddrsOffset:
				return new SampleStartFineOffsetGenerator(generator.genAmount.wAmount);
			case SFGenerator.endAddrsOffset:
				return new SampleEndFineOffsetGenerator(generator.genAmount.wAmount);
			case SFGenerator.startloopAddrsOffset:
				return new SampleLoopStartFineOffsetGenerator(generator.genAmount.wAmount);
			case SFGenerator.endloopAddrsOffset:
				return new SampleLoopEndFineOffsetGenerator(generator.genAmount.wAmount);
			case SFGenerator.startAddrsCoarseOffset:
				return new SampleStartCoarseOffsetGenerator(generator.genAmount.wAmount);
			case SFGenerator.modLfoToPitch:
				return null;
			case SFGenerator.vibLfoToPitch:
				return null;
			case SFGenerator.modEnvToPitch:
				return null;
			case SFGenerator.initialFilterFc:
				return null;
			case SFGenerator.initialFilterQ:
				return null;
			case SFGenerator.modLfoToFilterFc:
				return null;
			case SFGenerator.modEnvToFilterFc:
				return null;
			case SFGenerator.endAddrsCoarseOffset:
				return new SampleEndCoarseOffsetGenerator(generator.genAmount.wAmount);
			case SFGenerator.modLfoToVolume:
				return null;
			case SFGenerator.chorusEffectsSend:
				return null;
			case SFGenerator.reverbeffectsSend:
				return null;
			case SFGenerator.pan:
				return new PanGenerator(generator.genAmount.shAmount);
			case SFGenerator.delayModLFO:
				return new ModulatorLFODelayGenerator(generator.genAmount.shAmount);
			case SFGenerator.freqModLFO:
				return new ModulatorLFOFrequencyGenerator(generator.genAmount.shAmount);
			case SFGenerator.delayVibLFO:
				return null;
			case SFGenerator.freqVibLFO:
				return null;
			case SFGenerator.delayModEnv:
				return null;
			case SFGenerator.attackModEnv:
				return null;
			case SFGenerator.holdModEnv:
				return null;
			case SFGenerator.decayModEnv:
				return null;
			case SFGenerator.sustainModEnv:
				return null;
			case SFGenerator.releaseModEnv:
				return null;
			case SFGenerator.keynumToModEnvHold:
				return null;
			case SFGenerator.keynumToModEnvDecay:
				return null;
			case SFGenerator.delayVolEnv:
				return null;
			case SFGenerator.attackVolEnv:
				return null;
			case SFGenerator.holdVolEnv:
				return null;
			case SFGenerator.decayVolEnv:
				return null;
			case SFGenerator.sustainVolEnv:
				return null;
			case SFGenerator.releaseVolEnv:
				return null;
			case SFGenerator.keynumToVolEnvHold:
				return null;
			case SFGenerator.keynumToVolEnvDecay:
				return null;
			case SFGenerator.instrument:
				throw new NotSupportedException("Cannot create an instrument generator.");
			case SFGenerator.keyRange:
				throw new NotSupportedException("Cannot create a keyRange generator.");
			case SFGenerator.velRange:
				throw new NotSupportedException("Cannot create a velRange generator.");
			case SFGenerator.startloopAddrsCoarseOffset:
				return new SampleLoopStartCoarseOffsetGenerator(generator.genAmount.wAmount);
			case SFGenerator.keynum:
				return null;
			case SFGenerator.velocity:
				return null;
			case SFGenerator.initialAttenuation:
				return null;
			case SFGenerator.endloopAddrsCoarseOffset:
				return new SampleLoopEndCoarseOffsetGenerator(generator.genAmount.wAmount);
			case SFGenerator.coarseTune:
				return null;
			case SFGenerator.fineTune:
				return null;
			case SFGenerator.sampleID:
				throw new NotSupportedException("Cannot create a sampleID generator.");
			case SFGenerator.sampleModes:
				return null;
			case SFGenerator.scaleTuning:
				return null;
			case SFGenerator.exclusiveClass:
				return null;
			case SFGenerator.overridingRootKey:
				return null;
			case SFGenerator.endOper:
				return null;
			}
			return null;
		}

		// Token: 0x0600019A RID: 410
		internal abstract sfGenList ToInternal();

		// Token: 0x0400007B RID: 123
		private byte internalRangeHigh;

		// Token: 0x0400007C RID: 124
		private byte internalRangeLow;

		// Token: 0x0400007D RID: 125
		private int internalValue;
	}
}
