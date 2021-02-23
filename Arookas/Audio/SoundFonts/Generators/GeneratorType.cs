using System;

namespace Arookas.Audio.SoundFonts.Generators
{
	// Token: 0x0200002F RID: 47
	public enum GeneratorType : ushort
	{
		// Token: 0x0400007F RID: 127
		KeyNumber = 46,
		// Token: 0x04000080 RID: 128
		Velocity,
		// Token: 0x04000081 RID: 129
		Pan = 17,
		// Token: 0x04000082 RID: 130
		InitialAttenuation = 48,
		// Token: 0x04000083 RID: 131
		InitialFilterFc = 8,
		// Token: 0x04000084 RID: 132
		InitialFilterQ,
		// Token: 0x04000085 RID: 133
		SampleModes = 54,
		// Token: 0x04000086 RID: 134
		ExclusiveClass = 57,
		// Token: 0x04000087 RID: 135
		OverridingRootKey,
		// Token: 0x04000088 RID: 136
		CoarseTuning = 51,
		// Token: 0x04000089 RID: 137
		FineTuning,
		// Token: 0x0400008A RID: 138
		ScaleTuning = 56,
		// Token: 0x0400008B RID: 139
		SampleStartCoarseOffset = 4,
		// Token: 0x0400008C RID: 140
		SampleStartFineOffset = 0,
		// Token: 0x0400008D RID: 141
		SampleEndCoarseOffset = 12,
		// Token: 0x0400008E RID: 142
		SampleEndFineOffset = 1,
		// Token: 0x0400008F RID: 143
		SampleLoopStartCoarseOffset = 45,
		// Token: 0x04000090 RID: 144
		SampleLoopStartFineOffset = 2,
		// Token: 0x04000091 RID: 145
		SampleLoopEndCoarseOffset = 50,
		// Token: 0x04000092 RID: 146
		SampleLoopEndFineOffset = 3,
		// Token: 0x04000093 RID: 147
		ModulatorLFODelay = 21,
		// Token: 0x04000094 RID: 148
		ModulatorLFOFrequency,
		// Token: 0x04000095 RID: 149
		ModulatorLFOToPitch = 5,
		// Token: 0x04000096 RID: 150
		ModulatorLFOToVolume = 13,
		// Token: 0x04000097 RID: 151
		ModulatorLFOToFilterFc = 10,
		// Token: 0x04000098 RID: 152
		ModulatorDelayEnvelope = 25,
		// Token: 0x04000099 RID: 153
		ModulatorAttackEnvelope,
		// Token: 0x0400009A RID: 154
		ModulatorHoldEnvelope,
		// Token: 0x0400009B RID: 155
		ModulatorDecayEnvelope,
		// Token: 0x0400009C RID: 156
		ModulatorSustainEnvelope,
		// Token: 0x0400009D RID: 157
		ModulatorReleaseEnvelope,
		// Token: 0x0400009E RID: 158
		ModulatorEnvelopeToPitch = 7,
		// Token: 0x0400009F RID: 159
		ModulatorEnvelopeToFilterFc = 11,
		// Token: 0x040000A0 RID: 160
		VibratoLFODelay = 23,
		// Token: 0x040000A1 RID: 161
		VibratoLFOFrequency,
		// Token: 0x040000A2 RID: 162
		VibratoLFOToPitch = 6,
		// Token: 0x040000A3 RID: 163
		VolumeDelayEnvelope = 33,
		// Token: 0x040000A4 RID: 164
		VolumeAttackEnvelope,
		// Token: 0x040000A5 RID: 165
		VolumeHoldEnvelope,
		// Token: 0x040000A6 RID: 166
		VolumeDecayEnvelope,
		// Token: 0x040000A7 RID: 167
		VolumeSustainEnvelope,
		// Token: 0x040000A8 RID: 168
		VolumeReleaseEnvelope,
		// Token: 0x040000A9 RID: 169
		KeyNumberToModulatorHoldEnvelope = 31,
		// Token: 0x040000AA RID: 170
		KeyNumberToModulatorDecayEnvelope,
		// Token: 0x040000AB RID: 171
		KeyNumberToVolumeHoldEnvelope = 39,
		// Token: 0x040000AC RID: 172
		KeynumberToVolumeDecayEnvelope,
		// Token: 0x040000AD RID: 173
		ChorusEffectsSend = 15,
		// Token: 0x040000AE RID: 174
		ReverbEffectsSend
	}
}
