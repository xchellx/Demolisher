using System;
using System.Globalization;
using System.IO;
using System.Text;

namespace Arookas.IO.Text
{
	// Token: 0x02000096 RID: 150
	public class AScanner
	{
		// Token: 0x17000149 RID: 329
		// (get) Token: 0x0600053F RID: 1343 RVA: 0x00010C8B File Offset: 0x0000EE8B
		public bool CanScan
		{
			get
			{
				return this.Script != null;
			}
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x06000540 RID: 1344 RVA: 0x00010C99 File Offset: 0x0000EE99
		public bool IsAtEndOfScript
		{
			get
			{
				return this.CanScan && this.Script.Position == this.Script.Length;
			}
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x06000541 RID: 1345 RVA: 0x00010CBD File Offset: 0x0000EEBD
		public string ScriptName
		{
			get
			{
				return this.scriptName;
			}
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x06000542 RID: 1346 RVA: 0x00010CC8 File Offset: 0x0000EEC8
		public int CurrentLine
		{
			get
			{
				if (!this.CanScan)
				{
					return 0;
				}
				long position = this.Script.Position;
				this.Script.Position = 0L;
				int num = 1;
				int num2 = 0;
				while ((long)num2 < position)
				{
					if (this.ReadChar() == '\n')
					{
						num++;
					}
					num2++;
				}
				this.Script.Position = position;
				return num;
			}
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x06000543 RID: 1347 RVA: 0x00010D24 File Offset: 0x0000EF24
		public int CurrentLinePosition
		{
			get
			{
				if (!this.CanScan)
				{
					return 0;
				}
				long position = this.Script.Position;
				this.Script.Position = 0L;
				int num = 1;
				int num2 = 0;
				while ((long)num2 < position)
				{
					switch (this.ReadChar())
					{
					case '\t':
						num += 4;
						break;
					case '\n':
						num = 1;
						break;
					default:
						num++;
						break;
					}
					num2++;
				}
				this.Script.Position = position;
				return num;
			}
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x06000544 RID: 1348 RVA: 0x00010D98 File Offset: 0x0000EF98
		public int Integer
		{
			get
			{
				return this.parsedInteger;
			}
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x06000545 RID: 1349 RVA: 0x00010DA0 File Offset: 0x0000EFA0
		public float Float
		{
			get
			{
				return this.parsedFloat;
			}
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x06000546 RID: 1350 RVA: 0x00010DA8 File Offset: 0x0000EFA8
		public string String
		{
			get
			{
				return this.parsedString;
			}
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x06000547 RID: 1351 RVA: 0x00010DB0 File Offset: 0x0000EFB0
		public string Token
		{
			get
			{
				return this.parsedToken;
			}
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x06000548 RID: 1352 RVA: 0x00010DB8 File Offset: 0x0000EFB8
		public string TokenUppercase
		{
			get
			{
				if (this.Token != null)
				{
					return this.Token.ToUpperInvariant();
				}
				return null;
			}
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x06000549 RID: 1353 RVA: 0x00010DCF File Offset: 0x0000EFCF
		public string TokenLowercase
		{
			get
			{
				if (this.Token != null)
				{
					return this.Token.ToLowerInvariant();
				}
				return null;
			}
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x0600054A RID: 1354 RVA: 0x00010DE6 File Offset: 0x0000EFE6
		public bool TokenIsText
		{
			get
			{
				return this.Token != null && this.TokenType == AScannerTokenType.Text;
			}
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x0600054B RID: 1355 RVA: 0x00010DFB File Offset: 0x0000EFFB
		public bool TokenIsOperator
		{
			get
			{
				return this.Token != null && this.TokenType == AScannerTokenType.Operator;
			}
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x0600054C RID: 1356 RVA: 0x00010E10 File Offset: 0x0000F010
		public bool TokenIsSymbol
		{
			get
			{
				return this.Token != null && this.TokenType == AScannerTokenType.Symbol;
			}
		}

		// Token: 0x0600054D RID: 1357 RVA: 0x00010E25 File Offset: 0x0000F025
		public AScanner()
		{
		}

		// Token: 0x0600054E RID: 1358 RVA: 0x00010E2D File Offset: 0x0000F02D
		public AScanner(Stream script, string name = null)
		{
			this.LoadScript(script, name);
		}

		// Token: 0x0600054F RID: 1359 RVA: 0x00010E3D File Offset: 0x0000F03D
		public void LoadScript(Stream script, string name = null)
		{
			this.Script = script;
			this.scriptName = name;
			if (this.Script.Position != 0L)
			{
				this.Script.Position = 0L;
			}
		}

		// Token: 0x06000550 RID: 1360 RVA: 0x00010E69 File Offset: 0x0000F069
		public void Unload()
		{
			if (this.CanScan)
			{
				this.Script.Dispose();
				this.Script = null;
			}
		}

		// Token: 0x06000551 RID: 1361 RVA: 0x00010E88 File Offset: 0x0000F088
		public AScannerError GetInteger(bool newLines = true)
		{
			this.parsedInteger = 0;
			if (this.IsAtEndOfScript)
			{
				return AScannerError.UnexpectedEOF;
			}
			if (!this.SkipWhitespace(newLines))
			{
				return AScannerError.UnexpectedEOF;
			}
			StringBuilder stringBuilder = new StringBuilder();
			while (!this.IsAtEndOfScript)
			{
				char c = this.ReadChar();
				if (!AScanner.IsNumber(c))
				{
					break;
				}
				stringBuilder.Append(c);
			}
			if (stringBuilder.Length == 0)
			{
				return AScannerError.NothingWasThere;
			}
			this.StepBack(1);
			string text = stringBuilder.ToString();
			if (text.StartsWith("0x", StringComparison.InvariantCultureIgnoreCase))
			{
				if (int.TryParse(text.Substring(2), NumberStyles.AllowLeadingSign | NumberStyles.AllowHexSpecifier, NumberFormatInfo.InvariantInfo, out this.parsedInteger))
				{
					return AScannerError.None;
				}
				return AScannerError.IncorrectNumberFormat;
			}
			else
			{
				if (int.TryParse(text, NumberStyles.Integer, NumberFormatInfo.InvariantInfo, out this.parsedInteger))
				{
					return AScannerError.None;
				}
				return AScannerError.IncorrectNumberFormat;
			}
		}

		// Token: 0x06000552 RID: 1362 RVA: 0x00010F38 File Offset: 0x0000F138
		public AScannerError CheckForInteger(bool newLines = true)
		{
			long position = this.Script.Position;
			AScannerError integer = this.GetInteger(newLines);
			this.Script.Position = position;
			return integer;
		}

		// Token: 0x06000553 RID: 1363 RVA: 0x00010F68 File Offset: 0x0000F168
		public void MustGetInteger(bool newLines = true)
		{
			switch (this.GetInteger(newLines))
			{
			case AScannerError.NothingWasThere:
				throw new AScannerScriptException(this, "Missing integer.", new object[0]);
			case AScannerError.UnexpectedEOF:
				throw new AScannerScriptException(this, "Missing integer (unexpected end of file).", new object[0]);
			case AScannerError.IncorrectNumberFormat:
				throw new AScannerScriptException(this, "Invalid integer data.", new object[0]);
			default:
				return;
			}
		}

		// Token: 0x06000554 RID: 1364 RVA: 0x00010FC8 File Offset: 0x0000F1C8
		public AScannerError GetFloat(bool newLines = true)
		{
			this.parsedFloat = 0f;
			if (this.IsAtEndOfScript)
			{
				return AScannerError.UnexpectedEOF;
			}
			if (!this.SkipWhitespace(newLines))
			{
				return AScannerError.UnexpectedEOF;
			}
			StringBuilder stringBuilder = new StringBuilder();
			while (!this.IsAtEndOfScript)
			{
				char c = this.ReadChar();
				if (!AScanner.IsFloat(c))
				{
					break;
				}
				stringBuilder.Append(c);
			}
			if (stringBuilder.Length == 0)
			{
				return AScannerError.NothingWasThere;
			}
			this.StepBack(1);
			if (float.TryParse(stringBuilder.ToString(), NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint, NumberFormatInfo.InvariantInfo, out this.parsedFloat))
			{
				return AScannerError.None;
			}
			return AScannerError.IncorrectNumberFormat;
		}

		// Token: 0x06000555 RID: 1365 RVA: 0x0001104C File Offset: 0x0000F24C
		public AScannerError CheckForFloat(bool newLines = true)
		{
			long position = this.Script.Position;
			AScannerError @float = this.GetFloat(newLines);
			this.Script.Position = position;
			return @float;
		}

		// Token: 0x06000556 RID: 1366 RVA: 0x0001107C File Offset: 0x0000F27C
		public void MustGetFloat(bool newLines = true)
		{
			switch (this.GetFloat(newLines))
			{
			case AScannerError.NothingWasThere:
				throw new AScannerScriptException(this, "Missing floating-point number.", new object[0]);
			case AScannerError.UnexpectedEOF:
				throw new AScannerScriptException(this, "Missing floating-point number (unexpected end of file).", new object[0]);
			case AScannerError.IncorrectNumberFormat:
				throw new AScannerScriptException(this, "Invalid floating-point number data.", new object[0]);
			default:
				return;
			}
		}

		// Token: 0x06000557 RID: 1367 RVA: 0x000110DC File Offset: 0x0000F2DC
		public AScannerError GetString(bool newLines = true)
		{
			this.parsedString = null;
			if (this.IsAtEndOfScript)
			{
				return AScannerError.UnexpectedEOF;
			}
			if (!this.SkipWhitespace(newLines))
			{
				return AScannerError.UnexpectedEOF;
			}
			if (this.ReadChar() != '"')
			{
				this.StepBack(1);
				return AScannerError.MissingQuote;
			}
			StringBuilder stringBuilder = new StringBuilder();
			for (;;)
			{
				char c = this.ReadChar();
				if (c == '"' && stringBuilder.Length > 0 && stringBuilder[stringBuilder.Length - 1] != '\\')
				{
					goto IL_7F;
				}
				if (AScanner.IsNewline(c))
				{
					break;
				}
				stringBuilder.Append(c);
				if (this.IsAtEndOfScript)
				{
					return AScannerError.UnexpectedEOF;
				}
			}
			this.StepBack(1);
			return AScannerError.MissingQuote;
			IL_7F:
			this.parsedString = stringBuilder.ToString();
			return AScannerError.None;
		}

		// Token: 0x06000558 RID: 1368 RVA: 0x00011178 File Offset: 0x0000F378
		public AScannerError CheckForString(bool newLines = true)
		{
			long position = this.Script.Position;
			AScannerError @string = this.GetString(newLines);
			this.Script.Position = position;
			return @string;
		}

		// Token: 0x06000559 RID: 1369 RVA: 0x000111A8 File Offset: 0x0000F3A8
		public void MustGetString(bool newLines = true)
		{
			switch (this.GetString(newLines))
			{
			case AScannerError.UnexpectedEOF:
				throw new AScannerScriptException(this, "Missing/incomplete string (unexpected end of file).", new object[0]);
			case AScannerError.IncorrectNumberFormat:
				return;
			case AScannerError.MissingQuote:
				throw new AScannerScriptException(this, "String element is missing a quotation mark.", new object[0]);
			default:
				return;
			}
		}

		// Token: 0x0600055A RID: 1370 RVA: 0x000111F6 File Offset: 0x0000F3F6
		public AScannerError GetToken()
		{
			return this.GetToken(true, AScannerTokenScanModes.Normal);
		}

		// Token: 0x0600055B RID: 1371 RVA: 0x00011200 File Offset: 0x0000F400
		public AScannerError GetToken(bool newLines)
		{
			return this.GetToken(newLines, AScannerTokenScanModes.Normal);
		}

		// Token: 0x0600055C RID: 1372 RVA: 0x0001120A File Offset: 0x0000F40A
		public AScannerError GetToken(AScannerTokenScanModes scanmode)
		{
			return this.GetToken(true, AScannerTokenScanModes.Normal);
		}

		// Token: 0x0600055D RID: 1373 RVA: 0x00011214 File Offset: 0x0000F414
		public AScannerError GetToken(bool newLines, AScannerTokenScanModes scanmode)
		{
			this.parsedToken = null;
			this.TokenType = AScannerTokenType.Text;
			if (this.IsAtEndOfScript)
			{
				return AScannerError.UnexpectedEOF;
			}
			if (!this.SkipWhitespace(newLines))
			{
				return AScannerError.UnexpectedEOF;
			}
			StringBuilder stringBuilder = new StringBuilder();
			char c = this.ReadChar();
			stringBuilder.Append(c);
			if (!scanmode.HasFlag(AScannerTokenScanModes.SymbolsAsText) && AScanner.IsTokenSymbol(c))
			{
				this.TokenType = AScannerTokenType.Symbol;
				this.parsedToken = stringBuilder.ToString();
				return AScannerError.None;
			}
			if (!scanmode.HasFlag(AScannerTokenScanModes.OperatorsAsText) && AScanner.IsTokenOperator(c))
			{
				this.TokenType = AScannerTokenType.Operator;
				while (!this.IsAtEndOfScript)
				{
					c = this.ReadChar();
					if (!AScanner.IsTokenOperator(c))
					{
						this.StepBack(1);
						break;
					}
					stringBuilder.Append(c);
				}
			}
			else
			{
				while (!this.IsAtEndOfScript)
				{
					c = this.ReadChar();
					if ((!scanmode.HasFlag(AScannerTokenScanModes.SymbolsAsText) && AScanner.IsTokenSymbol(c)) || (!scanmode.HasFlag(AScannerTokenScanModes.OperatorsAsText) && AScanner.IsTokenOperator(c)) || c == '"' || AScanner.IsWhitespace(c) || AScanner.IsNewline(c))
					{
						this.StepBack(1);
						break;
					}
					stringBuilder.Append(c);
				}
			}
			if (stringBuilder.Length == 0)
			{
				return AScannerError.NothingWasThere;
			}
			this.parsedToken = stringBuilder.ToString();
			return AScannerError.None;
		}

		// Token: 0x0600055E RID: 1374 RVA: 0x00011361 File Offset: 0x0000F561
		public AScannerError CheckForToken()
		{
			return this.CheckForToken(true, AScannerTokenScanModes.Normal);
		}

		// Token: 0x0600055F RID: 1375 RVA: 0x0001136B File Offset: 0x0000F56B
		public AScannerError CheckForToken(bool newLines)
		{
			return this.CheckForToken(newLines, AScannerTokenScanModes.Normal);
		}

		// Token: 0x06000560 RID: 1376 RVA: 0x00011375 File Offset: 0x0000F575
		public AScannerError CheckForToken(AScannerTokenScanModes scanmode)
		{
			return this.CheckForToken(true, scanmode);
		}

		// Token: 0x06000561 RID: 1377 RVA: 0x00011380 File Offset: 0x0000F580
		public AScannerError CheckForToken(bool newLines, AScannerTokenScanModes scanmode)
		{
			long position = this.Script.Position;
			AScannerError token = this.GetToken(newLines, scanmode);
			this.Script.Position = position;
			return token;
		}

		// Token: 0x06000562 RID: 1378 RVA: 0x000113AF File Offset: 0x0000F5AF
		public void MustGetToken()
		{
			this.MustGetToken(true, null, AScannerTokenScanModes.Normal);
		}

		// Token: 0x06000563 RID: 1379 RVA: 0x000113BA File Offset: 0x0000F5BA
		public void MustGetToken(bool newLines)
		{
			this.MustGetToken(newLines, null, AScannerTokenScanModes.Normal);
		}

		// Token: 0x06000564 RID: 1380 RVA: 0x000113C5 File Offset: 0x0000F5C5
		public void MustGetToken(string token)
		{
			this.MustGetToken(true, token, AScannerTokenScanModes.Normal);
		}

		// Token: 0x06000565 RID: 1381 RVA: 0x000113D0 File Offset: 0x0000F5D0
		public void MustGetToken(AScannerTokenScanModes scanmode)
		{
			this.MustGetToken(true, null, scanmode);
		}

		// Token: 0x06000566 RID: 1382 RVA: 0x000113DB File Offset: 0x0000F5DB
		public void MustGetToken(bool newLines, AScannerTokenScanModes scanmode)
		{
			this.MustGetToken(newLines, null, scanmode);
		}

		// Token: 0x06000567 RID: 1383 RVA: 0x000113E6 File Offset: 0x0000F5E6
		public void MustGetToken(string token, AScannerTokenScanModes scanmode)
		{
			this.MustGetToken(true, token, scanmode);
		}

		// Token: 0x06000568 RID: 1384 RVA: 0x000113F1 File Offset: 0x0000F5F1
		public void MustGetToken(bool newLines, string token)
		{
			this.MustGetToken(newLines, token, AScannerTokenScanModes.Normal);
		}

		// Token: 0x06000569 RID: 1385 RVA: 0x000113FC File Offset: 0x0000F5FC
		public void MustGetToken(bool newLines, string token, AScannerTokenScanModes scanmode)
		{
			switch (this.GetToken(newLines, scanmode))
			{
			case AScannerError.None:
				if (token != null && this.parsedToken != token)
				{
					throw new AScannerScriptException(this, string.Format("Expected '{0}', received '{1}'.", token, this.parsedToken), new object[0]);
				}
				return;
			case AScannerError.NothingWasThere:
				throw new AScannerScriptException(this, "Missing token.", new object[0]);
			case AScannerError.UnexpectedEOF:
				throw new AScannerScriptException(this, "Missing token (unexpected end of file).", new object[0]);
			default:
				return;
			}
		}

		// Token: 0x0600056A RID: 1386 RVA: 0x00011478 File Offset: 0x0000F678
		protected void StepBack(int spaces = 1)
		{
			if ((long)spaces > this.Script.Position)
			{
				this.Script.Position = 0L;
				return;
			}
			this.Script.Position -= (long)spaces;
		}

		// Token: 0x0600056B RID: 1387 RVA: 0x000114AB File Offset: 0x0000F6AB
		public void Goto(long offset)
		{
			this.Script.Position = ((offset < 0L) ? 0L : offset);
		}

		// Token: 0x0600056C RID: 1388 RVA: 0x000114C2 File Offset: 0x0000F6C2
		public long GetPosition()
		{
			return this.Script.Position;
		}

		// Token: 0x0600056D RID: 1389 RVA: 0x000114CF File Offset: 0x0000F6CF
		public char ReadChar()
		{
			return (char)this.Script.ReadByte();
		}

		// Token: 0x0600056E RID: 1390 RVA: 0x000114E0 File Offset: 0x0000F6E0
		public bool SkipWhitespace(bool skipNewlines = false)
		{
			if (this.IsAtEndOfScript)
			{
				return false;
			}
			char c = this.ReadChar();
			while (AScanner.IsWhitespace(c) || (skipNewlines && AScanner.IsNewline(c)) || c == '/')
			{
				if (c == '/' && !this.IsAtEndOfScript)
				{
					c = this.ReadChar();
					if (c == '/')
					{
						while (!this.IsAtEndOfScript)
						{
							if (AScanner.IsNewline(c))
							{
								break;
							}
							c = this.ReadChar();
						}
					}
					else
					{
						if (c != '*')
						{
							this.StepBack(1);
							break;
						}
						while (this.Script.Position < this.Script.Length - 1L)
						{
							char c2 = this.ReadChar();
							char c3 = this.ReadChar();
							if (c2 == '*' && c3 == '/')
							{
								break;
							}
							this.StepBack(1);
						}
					}
				}
				if (this.IsAtEndOfScript)
				{
					return false;
				}
				c = this.ReadChar();
			}
			this.StepBack(1);
			return true;
		}

		// Token: 0x0600056F RID: 1391 RVA: 0x000115BD File Offset: 0x0000F7BD
		protected static bool IsWhitespace(char character)
		{
			return " \t\u00a0".IndexOf(character) >= 0;
		}

		// Token: 0x06000570 RID: 1392 RVA: 0x000115D0 File Offset: 0x0000F7D0
		protected static bool IsWhitespace(string str)
		{
			return str != null && str.Length != 0 && AScanner.IsWhitespace(str[0]);
		}

		// Token: 0x06000571 RID: 1393 RVA: 0x000115EB File Offset: 0x0000F7EB
		protected static bool IsNewline(char character)
		{
			return "\n\r".IndexOf(character) >= 0;
		}

		// Token: 0x06000572 RID: 1394 RVA: 0x000115FE File Offset: 0x0000F7FE
		protected static bool IsNewline(string str)
		{
			return str != null && str.Length != 0 && AScanner.IsNewline(str[0]);
		}

		// Token: 0x06000573 RID: 1395 RVA: 0x00011619 File Offset: 0x0000F819
		protected static bool IsLineBreak(char character)
		{
			return character == '\n';
		}

		// Token: 0x06000574 RID: 1396 RVA: 0x00011620 File Offset: 0x0000F820
		protected static bool IsLineBreak(string str)
		{
			return str != null && str.Length != 0 && AScanner.IsLineBreak(str[0]);
		}

		// Token: 0x06000575 RID: 1397 RVA: 0x0001163B File Offset: 0x0000F83B
		protected static bool IsNumber(char character)
		{
			return "-+0123456789ABCDEFXabcdefx".IndexOf(character) >= 0;
		}

		// Token: 0x06000576 RID: 1398 RVA: 0x0001164E File Offset: 0x0000F84E
		protected static bool IsNumber(string str)
		{
			return str != null && str.Length != 0 && AScanner.IsNumber(str[0]);
		}

		// Token: 0x06000577 RID: 1399 RVA: 0x00011669 File Offset: 0x0000F869
		protected static bool IsFloat(char character)
		{
			return "-+.0123456789".IndexOf(character) >= 0;
		}

		// Token: 0x06000578 RID: 1400 RVA: 0x0001167C File Offset: 0x0000F87C
		protected static bool IsFloat(string str)
		{
			return str != null && str.Length != 0 && AScanner.IsFloat(str[0]);
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x00011697 File Offset: 0x0000F897
		protected static bool IsTokenSymbol(char character)
		{
			return ".,:;{}[]()\\".IndexOf(character) >= 0;
		}

		// Token: 0x0600057A RID: 1402 RVA: 0x000116AA File Offset: 0x0000F8AA
		protected static bool IsTokenSymbol(string str)
		{
			return str != null && str.Length != 0 && AScanner.IsTokenSymbol(str[0]);
		}

		// Token: 0x0600057B RID: 1403 RVA: 0x000116C5 File Offset: 0x0000F8C5
		protected static bool IsTokenOperator(char character)
		{
			return "=+-*/?%&|!^<>".IndexOf(character) >= 0;
		}

		// Token: 0x0600057C RID: 1404 RVA: 0x000116D8 File Offset: 0x0000F8D8
		protected static bool IsTokenOperator(string str)
		{
			return str != null && str.Length != 0 && AScanner.IsTokenOperator(str[0]);
		}

		// Token: 0x04000209 RID: 521
		public const string IncludeCommand = "#include";

		// Token: 0x0400020A RID: 522
		public const string ConstantDeclaration = "const";

		// Token: 0x0400020B RID: 523
		public const string IntegerDeclaration = "int";

		// Token: 0x0400020C RID: 524
		public const string FloatDeclaration = "float";

		// Token: 0x0400020D RID: 525
		public const string StringDeclaration = "string";

		// Token: 0x0400020E RID: 526
		public const string OpeningBrace = "{";

		// Token: 0x0400020F RID: 527
		public const string ClosingBrace = "}";

		// Token: 0x04000210 RID: 528
		public const string OpeningBracket = "[";

		// Token: 0x04000211 RID: 529
		public const string ClosingBracket = "]";

		// Token: 0x04000212 RID: 530
		public const string OpeningParenthesis = "(";

		// Token: 0x04000213 RID: 531
		public const string ClosingParenthesis = ")";

		// Token: 0x04000214 RID: 532
		public const string Colon = ":";

		// Token: 0x04000215 RID: 533
		public const string Semicolon = ";";

		// Token: 0x04000216 RID: 534
		public const string Period = ".";

		// Token: 0x04000217 RID: 535
		public const string Comma = ",";

		// Token: 0x04000218 RID: 536
		public const string AssignmentOperator = "=";

		// Token: 0x04000219 RID: 537
		public const string AdditionOperator = "+";

		// Token: 0x0400021A RID: 538
		public const string SubtractionOperator = "-";

		// Token: 0x0400021B RID: 539
		public const string MultiplicationOperator = "*";

		// Token: 0x0400021C RID: 540
		public const string DivisionOperator = "/";

		// Token: 0x0400021D RID: 541
		public const string ModuloOperator = "%";

		// Token: 0x0400021E RID: 542
		public const string ExponentOperator = "^";

		// Token: 0x0400021F RID: 543
		public const string BinaryANDOperator = "&";

		// Token: 0x04000220 RID: 544
		public const string BinaryOROperator = "|";

		// Token: 0x04000221 RID: 545
		public const string BinaryNOTOperator = "~";

		// Token: 0x04000222 RID: 546
		public const string EqualsToOperator = "==";

		// Token: 0x04000223 RID: 547
		public const string NotEqualsToOperator = "!=";

		// Token: 0x04000224 RID: 548
		public const string LessThanOperator = "<";

		// Token: 0x04000225 RID: 549
		public const string LessThanOrEqualsToOperator = "<=";

		// Token: 0x04000226 RID: 550
		public const string GreaterThanOperator = ">";

		// Token: 0x04000227 RID: 551
		public const string GreaterThanOrEqualsToOperator = ">=";

		// Token: 0x04000228 RID: 552
		public const string LogicalAndOperator = "&&";

		// Token: 0x04000229 RID: 553
		public const string LogicalOrOperator = "||";

		// Token: 0x0400022A RID: 554
		protected Stream Script;

		// Token: 0x0400022B RID: 555
		private string scriptName;

		// Token: 0x0400022C RID: 556
		protected int parsedInteger;

		// Token: 0x0400022D RID: 557
		protected float parsedFloat;

		// Token: 0x0400022E RID: 558
		protected string parsedString;

		// Token: 0x0400022F RID: 559
		protected string parsedToken;

		// Token: 0x04000230 RID: 560
		protected AScannerTokenType TokenType;
	}
}
