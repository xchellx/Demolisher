using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Arookas.Audio.SoundFonts.Generators
{
	// Token: 0x0200002D RID: 45
	public sealed class GeneratorCollection : IList<Generator>, ICollection<Generator>, IEnumerable<Generator>, IEnumerable
	{
		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000180 RID: 384 RVA: 0x00005E74 File Offset: 0x00004074
		public int Count
		{
			get
			{
				return this.generators.Count;
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000181 RID: 385 RVA: 0x00005E81 File Offset: 0x00004081
		bool ICollection<Generator>.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000074 RID: 116
		public Generator this[int index]
		{
			get
			{
				if (index < 0 || index >= this.generators.Count)
				{
					throw new IndexOutOfRangeException();
				}
				return this.generators[index];
			}
			set
			{
				if (index < 0 || index >= this.generators.Count)
				{
					throw new IndexOutOfRangeException();
				}
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.generators[index] = value;
			}
		}

		// Token: 0x17000075 RID: 117
		public Generator this[GeneratorType generatorType]
		{
			get
			{
				if (!generatorType.IsDefined<GeneratorType>())
				{
					throw new ArgumentOutOfRangeException("value");
				}
				return this.generators.First((Generator generator) => generator.Type == generatorType);
			}
		}

		// Token: 0x06000185 RID: 389 RVA: 0x00005F41 File Offset: 0x00004141
		public GeneratorCollection()
		{
			this.generators = new List<Generator>(5);
		}

		// Token: 0x06000186 RID: 390 RVA: 0x00005F74 File Offset: 0x00004174
		public void Add(Generator generator)
		{
			if (generator == null)
			{
				throw new ArgumentNullException("generator");
			}
			if (this.Contains(generator.Type))
			{
				this.generators[this.generators.FindIndex((Generator g) => g.Type == generator.Type)] = generator;
				return;
			}
			this.generators.Add(generator);
		}

		// Token: 0x06000187 RID: 391 RVA: 0x00005FF8 File Offset: 0x000041F8
		public void AddRange(IEnumerable<Generator> generators)
		{
			if (generators == null)
			{
				throw new ArgumentNullException("generators");
			}
			foreach (Generator generator in generators)
			{
				if (generator == null)
				{
					throw new ArgumentException("The specified collection has a null element.", "generators");
				}
				this.Add(generator);
			}
		}

		// Token: 0x06000188 RID: 392 RVA: 0x00006064 File Offset: 0x00004264
		public void Clear()
		{
			this.generators.Clear();
		}

		// Token: 0x06000189 RID: 393 RVA: 0x00006071 File Offset: 0x00004271
		public bool Contains(Generator generator)
		{
			return this.generators.Contains(generator);
		}

		// Token: 0x0600018A RID: 394 RVA: 0x00006098 File Offset: 0x00004298
		public bool Contains(GeneratorType generatorType)
		{
			if (!generatorType.IsDefined<GeneratorType>())
			{
				throw new ArgumentOutOfRangeException("value");
			}
			return this.generators.Any((Generator generator) => generator.Type == generatorType);
		}

		// Token: 0x0600018B RID: 395 RVA: 0x000060E1 File Offset: 0x000042E1
		void ICollection<Generator>.CopyTo(Generator[] array, int arrayIndex)
		{
			this.generators.CopyTo(array, arrayIndex);
		}

		// Token: 0x0600018C RID: 396 RVA: 0x000060F0 File Offset: 0x000042F0
		public IEnumerator<Generator> GetEnumerator()
		{
			return this.generators.GetEnumerator();
		}

		// Token: 0x0600018D RID: 397 RVA: 0x00006102 File Offset: 0x00004302
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0600018E RID: 398 RVA: 0x0000610A File Offset: 0x0000430A
		public int IndexOf(Generator generator)
		{
			return this.generators.IndexOf(generator);
		}

		// Token: 0x0600018F RID: 399 RVA: 0x00006118 File Offset: 0x00004318
		public void Insert(int index, Generator generator)
		{
			if (index < 0 || index >= this.generators.Count)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (generator == null)
			{
				throw new ArgumentNullException("generator");
			}
			this.generators.Insert(index, generator);
		}

		// Token: 0x06000190 RID: 400 RVA: 0x00006152 File Offset: 0x00004352
		public bool Remove(Generator generator)
		{
			return this.generators.Remove(generator);
		}

		// Token: 0x06000191 RID: 401 RVA: 0x00006160 File Offset: 0x00004360
		public void RemoveAt(int index)
		{
			if (index < 0 || index >= this.generators.Count)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			this.generators.RemoveAt(index);
		}

		// Token: 0x0400007A RID: 122
		private List<Generator> generators;
	}
}
