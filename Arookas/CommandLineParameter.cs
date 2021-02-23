using System;
using System.Collections;
using System.Collections.Generic;

namespace Arookas
{
	// Token: 0x02000024 RID: 36
	public sealed class CommandLineParameter : IEnumerable<string>, IEnumerable
	{
		// Token: 0x17000054 RID: 84
		// (get) Token: 0x0600010A RID: 266 RVA: 0x0000429C File Offset: 0x0000249C
		public int Count
		{
			get
			{
				return this.arguments.Length;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x0600010B RID: 267 RVA: 0x000042A6 File Offset: 0x000024A6
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000056 RID: 86
		public string this[int index]
		{
			get
			{
				return this.arguments[index];
			}
		}

		// Token: 0x0600010D RID: 269 RVA: 0x000042B8 File Offset: 0x000024B8
		internal CommandLineParameter(string name, string[] arguments)
		{
			this.name = name;
			this.arguments = arguments;
		}

		// Token: 0x0600010E RID: 270 RVA: 0x000042CE File Offset: 0x000024CE
		public IEnumerator<string> GetEnumerator()
		{
			return this.arguments.GetArrayEnumerator<string>();
		}

		// Token: 0x0600010F RID: 271 RVA: 0x000042DB File Offset: 0x000024DB
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x04000042 RID: 66
		private string[] arguments;

		// Token: 0x04000043 RID: 67
		private string name;
	}
}
