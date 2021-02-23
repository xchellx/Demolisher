using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Arookas
{
	// Token: 0x02000025 RID: 37
	public sealed class CommandLine : IEnumerable<CommandLineParameter>, IEnumerable
	{
		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000110 RID: 272 RVA: 0x000042E3 File Offset: 0x000024E3
		public int Count
		{
			get
			{
				return this.parameters.Length;
			}
		}

		// Token: 0x17000058 RID: 88
		public CommandLineParameter this[int index]
		{
			get
			{
				return this.parameters[index];
			}
		}

		// Token: 0x06000112 RID: 274 RVA: 0x000042F7 File Offset: 0x000024F7
		public CommandLine(string[] arguments) : this(arguments, '-')
		{
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00004308 File Offset: 0x00002508
		public CommandLine(string[] arguments, char parameterDelimiter)
		{
			if (arguments == null)
			{
				throw new ArgumentNullException("arguments");
			}
			if (arguments.Any((string argument) => argument == null))
			{
				throw new ArgumentException("The specified argument collection contains at least one null element.", "arguments");
			}
			List<CommandLineParameter> list = new List<CommandLineParameter>(10);
			string text = null;
			List<string> list2 = new List<string>(10);
			foreach (string text2 in arguments)
			{
				if (text2.StartsWith(parameterDelimiter.ToString()))
				{
					if (text != null)
					{
						list.Add(new CommandLineParameter(text, list2.ToArray()));
					}
					text = text2;
					list2.Clear();
				}
				else if (text != null)
				{
					list2.Add(text2);
				}
				else
				{
					list.Add(new CommandLineParameter(text2, new string[0]));
				}
			}
			if (text != null)
			{
				list.Add(new CommandLineParameter(text, list2.ToArray()));
			}
			this.parameters = list.ToArray();
		}

		// Token: 0x06000114 RID: 276 RVA: 0x000043F9 File Offset: 0x000025F9
		public IEnumerator<CommandLineParameter> GetEnumerator()
		{
			return this.parameters.GetArrayEnumerator<CommandLineParameter>();
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00004406 File Offset: 0x00002606
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x04000044 RID: 68
		private CommandLineParameter[] parameters;
	}
}
