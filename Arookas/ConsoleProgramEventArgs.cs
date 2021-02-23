using System;
using Arookas.Collections;

namespace Arookas
{
	// Token: 0x02000026 RID: 38
	public class ConsoleProgramEventArgs : EventArgs
	{
		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000117 RID: 279 RVA: 0x0000440E File Offset: 0x0000260E
		public AReadOnlyArray<string> Arguments
		{
			get
			{
				return this.arguments_readOnly;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000118 RID: 280 RVA: 0x00004416 File Offset: 0x00002616
		public CommandLine CommandLine
		{
			get
			{
				return this.commandLine;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000119 RID: 281 RVA: 0x0000441E File Offset: 0x0000261E
		public ConsoleProgram ConsoleProgram
		{
			get
			{
				return this.consoleProgram;
			}
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00004428 File Offset: 0x00002628
		public ConsoleProgramEventArgs(ConsoleProgram consoleProgram, string[] arguments)
		{
			if (consoleProgram == null)
			{
				throw new ArgumentNullException("consoleProgram");
			}
			this.consoleProgram = consoleProgram;
			this.commandLine = new CommandLine(arguments);
			this.arguments = arguments.Duplicate<string>();
			this.arguments_readOnly = new AReadOnlyArray<string>(this.arguments);
		}

		// Token: 0x04000046 RID: 70
		private readonly AReadOnlyArray<string> arguments_readOnly;

		// Token: 0x04000047 RID: 71
		private readonly string[] arguments;

		// Token: 0x04000048 RID: 72
		private readonly CommandLine commandLine;

		// Token: 0x04000049 RID: 73
		private readonly ConsoleProgram consoleProgram;
	}
}
