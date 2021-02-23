using System;

namespace Arookas.Audio.SoundFonts
{
	// Token: 0x0200004F RID: 79
	internal enum SFGenerator : ushort
	{
		// Token: 0x040000F5 RID: 245
		startAddrsOffset,
		// Token: 0x040000F6 RID: 246
		endAddrsOffset,
		// Token: 0x040000F7 RID: 247
		startloopAddrsOffset,
		// Token: 0x040000F8 RID: 248
		endloopAddrsOffset,
		// Token: 0x040000F9 RID: 249
		startAddrsCoarseOffset,
		// Token: 0x040000FA RID: 250
		modLfoToPitch,
		// Token: 0x040000FB RID: 251
		vibLfoToPitch,
		// Token: 0x040000FC RID: 252
		modEnvToPitch,
		// Token: 0x040000FD RID: 253
		initialFilterFc,
		// Token: 0x040000FE RID: 254
		initialFilterQ,
		// Token: 0x040000FF RID: 255
		modLfoToFilterFc,
		// Token: 0x04000100 RID: 256
		modEnvToFilterFc,
		// Token: 0x04000101 RID: 257
		endAddrsCoarseOffset,
		// Token: 0x04000102 RID: 258
		modLfoToVolume,
		// Token: 0x04000103 RID: 259
		chorusEffectsSend = 15,
		// Token: 0x04000104 RID: 260
		reverbeffectsSend,
		// Token: 0x04000105 RID: 261
		pan,
		// Token: 0x04000106 RID: 262
		delayModLFO = 21,
		// Token: 0x04000107 RID: 263
		freqModLFO,
		// Token: 0x04000108 RID: 264
		delayVibLFO,
		// Token: 0x04000109 RID: 265
		freqVibLFO,
		// Token: 0x0400010A RID: 266
		delayModEnv,
		// Token: 0x0400010B RID: 267
		attackModEnv,
		// Token: 0x0400010C RID: 268
		holdModEnv,
		// Token: 0x0400010D RID: 269
		decayModEnv,
		// Token: 0x0400010E RID: 270
		sustainModEnv,
		// Token: 0x0400010F RID: 271
		releaseModEnv,
		// Token: 0x04000110 RID: 272
		keynumToModEnvHold,
		// Token: 0x04000111 RID: 273
		keynumToModEnvDecay,
		// Token: 0x04000112 RID: 274
		delayVolEnv,
		// Token: 0x04000113 RID: 275
		attackVolEnv,
		// Token: 0x04000114 RID: 276
		holdVolEnv,
		// Token: 0x04000115 RID: 277
		decayVolEnv,
		// Token: 0x04000116 RID: 278
		sustainVolEnv,
		// Token: 0x04000117 RID: 279
		releaseVolEnv,
		// Token: 0x04000118 RID: 280
		keynumToVolEnvHold,
		// Token: 0x04000119 RID: 281
		keynumToVolEnvDecay,
		// Token: 0x0400011A RID: 282
		instrument,
		// Token: 0x0400011B RID: 283
		keyRange = 43,
		// Token: 0x0400011C RID: 284
		velRange,
		// Token: 0x0400011D RID: 285
		startloopAddrsCoarseOffset,
		// Token: 0x0400011E RID: 286
		keynum,
		// Token: 0x0400011F RID: 287
		velocity,
		// Token: 0x04000120 RID: 288
		initialAttenuation,
		// Token: 0x04000121 RID: 289
		endloopAddrsCoarseOffset = 50,
		// Token: 0x04000122 RID: 290
		coarseTune,
		// Token: 0x04000123 RID: 291
		fineTune,
		// Token: 0x04000124 RID: 292
		sampleID,
		// Token: 0x04000125 RID: 293
		sampleModes,
		// Token: 0x04000126 RID: 294
		scaleTuning = 56,
		// Token: 0x04000127 RID: 295
		exclusiveClass,
		// Token: 0x04000128 RID: 296
		overridingRootKey,
		// Token: 0x04000129 RID: 297
		endOper = 60
	}
}
