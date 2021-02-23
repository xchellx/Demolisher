using System;

namespace Arookas.IO.Text
{
	// Token: 0x02000099 RID: 153
	public class AScannerScriptException : Exception
	{
		// Token: 0x0600057D RID: 1405 RVA: 0x000116F3 File Offset: 0x0000F8F3
		public AScannerScriptException()
		{
		}

		// Token: 0x0600057E RID: 1406 RVA: 0x000116FB File Offset: 0x0000F8FB
		public AScannerScriptException(string message) : base(message)
		{
		}

		// Token: 0x0600057F RID: 1407 RVA: 0x00011704 File Offset: 0x0000F904
		public AScannerScriptException(AScanner scanner, string format, params object[] args) : base(string.Format("Script \"{0}\" on line {1}, pos {2}:\n{3}", new object[]
		{
			scanner.ScriptName,
			scanner.CurrentLine,
			scanner.CurrentLinePosition,
			string.Format(format, args)
		}))
		{
		}
	}
}
