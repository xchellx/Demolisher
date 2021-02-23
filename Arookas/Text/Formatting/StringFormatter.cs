using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Arookas.Text.Formatting
{
	// Token: 0x020000A2 RID: 162
	public sealed class StringFormatter
	{
		// Token: 0x060005A0 RID: 1440 RVA: 0x00011915 File Offset: 0x0000FB15
		private StringFormatter()
		{
		}

		// Token: 0x060005A1 RID: 1441 RVA: 0x00011934 File Offset: 0x0000FB34
		public string Format(params object[] arguments)
		{
			return this.Format(null, arguments);
		}

		// Token: 0x060005A2 RID: 1442 RVA: 0x00011940 File Offset: 0x0000FB40
		public string Format(IFormatProvider formatProvider, params object[] arguments)
		{
			if (arguments.Length <= this.argumentCount)
			{
				throw new ArgumentOutOfRangeException("Collection of object arguments is not of a compatible length for this token tree.");
			}
			this.stringBuilder = new StringBuilder(this.minimalLength + arguments.Length * 8);
			this.Format(this.tokens, formatProvider, arguments);
			return this.stringBuilder.ToString();
		}

		// Token: 0x060005A3 RID: 1443 RVA: 0x00011994 File Offset: 0x0000FB94
		private void Format(FormatTokenCollection tokens, IFormatProvider formatProvider, object[] arguments)
		{
			for (int i = 0; i < tokens.Count; i++)
			{
				FormatToken formatToken = tokens[i];
				LiteralFormatToken literalFormatToken = formatToken as LiteralFormatToken;
				if (literalFormatToken != null)
				{
					this.stringBuilder.Append(literalFormatToken.Literal);
				}
				else
				{
					FormattedFormatToken formattedFormatToken = formatToken as FormattedFormatToken;
					if (formattedFormatToken != null)
					{
						IFormattable formattable = arguments[formattedFormatToken.ArgumentNumber] as IFormattable;
						string text;
						if (formattable != null)
						{
							text = formattable.ToString(formattedFormatToken.FormatString, formatProvider);
						}
						else if (arguments[formattedFormatToken.ArgumentNumber] != null)
						{
							text = arguments[formattedFormatToken.ArgumentNumber].ToString();
						}
						else
						{
							text = "";
						}
						if (formattedFormatToken.Spacing > 0 && text.Length < formattedFormatToken.Spacing)
						{
							if (formattedFormatToken.Alignment == FormattedFormatToken.StringAlignment.LeftAligned)
							{
								this.stringBuilder.Append(text);
								this.stringBuilder.Append(' ', formattedFormatToken.Spacing - text.Length);
							}
							else
							{
								this.stringBuilder.Append(' ', formattedFormatToken.Spacing - text.Length);
								this.stringBuilder.Append(text);
							}
						}
						else
						{
							this.stringBuilder.Append(text);
						}
					}
					else
					{
						ConditionalFormatToken conditionalFormatToken = formatToken as ConditionalFormatToken;
						if (conditionalFormatToken != null)
						{
							bool flag = true;
							if (arguments[conditionalFormatToken.ArgumentNumber] == null)
							{
								flag = false;
							}
							else
							{
								bool? flag2 = arguments[conditionalFormatToken.ArgumentNumber] as bool?;
								if (flag2 != null)
								{
									flag = flag2.Value;
								}
							}
							if (conditionalFormatToken.Inverted)
							{
								flag = !flag;
							}
							this.Format(flag ? conditionalFormatToken.TrueTokens : conditionalFormatToken.FalseTokens, formatProvider, arguments);
						}
					}
				}
			}
		}

		// Token: 0x060005A4 RID: 1444 RVA: 0x00011B3C File Offset: 0x0000FD3C
		public static StringFormatter Create(string formatString)
		{
			if (formatString == null)
			{
				throw new ArgumentNullException("formatString");
			}
			StringFormatter stringFormatter = new StringFormatter();
			StringBuilder stringBuilder = new StringBuilder();
			FormatTokenCollection formatTokenCollection = stringFormatter.tokens;
			Stack<ConditionalFormatToken> stack = new Stack<ConditionalFormatToken>();
			for (int i = 0; i < formatString.Length; i++)
			{
				if (formatString[i] == '}')
				{
					throw new FormatException("Format string is invalid (contains a hanging '}' character).");
				}
				if (formatString[i] == '{')
				{
					if (stringBuilder.Length > 0)
					{
						formatTokenCollection.Add(new LiteralFormatToken(stringBuilder.ToString()));
					}
					stringBuilder.Clear();
					if (formatString.ElementAtOrDefault(++i) == '\'')
					{
						while (formatString.ElementAtOrDefault(++i) != '\'')
						{
							stringBuilder.Append(formatString[i]);
							stringFormatter.minimalLength++;
						}
						if (formatString.ElementAtOrDefault(++i) != '}')
						{
							throw new FormatException("Format string is invalid (literal token is missing closing brace '}').");
						}
						if (formatTokenCollection.Count > 1 && formatTokenCollection[formatTokenCollection.Count - 1] is LiteralFormatToken)
						{
							LiteralFormatToken literalFormatToken = formatTokenCollection[formatTokenCollection.Count - 1] as LiteralFormatToken;
							LiteralFormatToken value = new LiteralFormatToken(literalFormatToken.Literal + stringBuilder.ToString());
							formatTokenCollection[formatTokenCollection.Count - 1] = value;
						}
						else
						{
							formatTokenCollection.Add(new LiteralFormatToken(stringBuilder.ToString()));
						}
						stringBuilder.Clear();
					}
					else
					{
						int num = 0;
						int value2 = 0;
						FormattedFormatToken.StringAlignment alignment = FormattedFormatToken.StringAlignment.RightAligned;
						string formatString2 = "";
						while (char.IsDigit(formatString.ElementAtOrDefault(i)))
						{
							stringBuilder.Append(formatString[i++]);
						}
						if (!int.TryParse(stringBuilder.ToString(), NumberStyles.None, CultureInfo.CurrentCulture, out num))
						{
							throw new FormatException("Format string is invalid (failed to parse argument index).");
						}
						if (num > stringFormatter.argumentCount)
						{
							stringFormatter.argumentCount = num;
						}
						if (formatString.ElementAtOrDefault(i) == '<' || formatString.ElementAtOrDefault(i) == '>')
						{
							stringBuilder.Clear();
							alignment = ((formatString.ElementAtOrDefault(i) == '<') ? FormattedFormatToken.StringAlignment.LeftAligned : FormattedFormatToken.StringAlignment.RightAligned);
							while (char.IsDigit(formatString.ElementAtOrDefault(i)))
							{
								stringBuilder.Append(formatString[i++]);
							}
							if (!int.TryParse(stringBuilder.ToString(), NumberStyles.None, CultureInfo.CurrentCulture, out value2))
							{
								throw new FormatException("Format string is invalid (failed to parse spacing argument).");
							}
						}
						if (formatString.ElementAtOrDefault(i) == ':')
						{
							stringBuilder.Clear();
							while (formatString.ElementAtOrDefault(i) != '}')
							{
								stringBuilder.Append(formatString[i++]);
							}
							if (stringBuilder.Length <= 0)
							{
								throw new FormatException("Format string is invalid (missing argument format string).");
							}
							formatString2 = stringBuilder.ToString();
						}
						if (formatString.ElementAtOrDefault(i) != '}')
						{
							throw new FormatException("Format string is invalid (formatted token is missing closing brace '}').");
						}
						formatTokenCollection.Add(new FormattedFormatToken(num, System.Math.Abs(value2), alignment, formatString2));
						stringBuilder.Clear();
					}
				}
				else if (formatString[i] == ']')
				{
					if (formatTokenCollection.IsGlobalScope)
					{
						throw new FormatException("Format string is invalid (contains a hanging ']' character).");
					}
					if (stringBuilder.Length > 0)
					{
						formatTokenCollection.Add(new LiteralFormatToken(stringBuilder.ToString()));
					}
					stringBuilder.Clear();
					formatTokenCollection = formatTokenCollection.Parent;
					stack.Pop();
				}
				else if (formatString[i] == ':' && !formatTokenCollection.IsGlobalScope)
				{
					ConditionalFormatToken conditionalFormatToken = stack.Peek();
					if (formatTokenCollection == conditionalFormatToken.FalseTokens)
					{
						throw new FormatException("Format string is invalid (conditional token contains more than one false scope).");
					}
					if (stringBuilder.Length > 0)
					{
						formatTokenCollection.Add(new LiteralFormatToken(stringBuilder.ToString()));
					}
					stringBuilder.Clear();
					formatTokenCollection = conditionalFormatToken.FalseTokens;
					i++;
				}
				else if (formatString[i] == '[')
				{
					if (stringBuilder.Length > 0)
					{
						formatTokenCollection.Add(new LiteralFormatToken(stringBuilder.ToString()));
					}
					stringBuilder.Clear();
					bool inverted = false;
					int num2 = 0;
					if (formatString[++i] == '!')
					{
						inverted = true;
						i++;
					}
					while (char.IsDigit(formatString.ElementAtOrDefault(i)))
					{
						stringBuilder.Append(formatString[i++]);
					}
					if (!int.TryParse(stringBuilder.ToString(), NumberStyles.None, CultureInfo.CurrentCulture, out num2))
					{
						throw new FormatException("Format string is invalid (failed to parse argument index).");
					}
					if (num2 > stringFormatter.argumentCount)
					{
						stringFormatter.argumentCount = num2;
					}
					if (formatString.ElementAtOrDefault(i) != '?')
					{
						throw new FormatException("Format string is invalid (missing true-scope identifier '?').");
					}
					ConditionalFormatToken conditionalFormatToken2 = new ConditionalFormatToken(num2, inverted, formatTokenCollection);
					formatTokenCollection.Add(conditionalFormatToken2);
					formatTokenCollection = conditionalFormatToken2.TrueTokens;
					stack.Push(conditionalFormatToken2);
					stringBuilder.Clear();
				}
				else
				{
					stringBuilder.Append(formatString[i]);
					stringFormatter.minimalLength++;
				}
			}
			return stringFormatter;
		}

		// Token: 0x0400025C RID: 604
		private FormatTokenCollection tokens = new FormatTokenCollection(null);

		// Token: 0x0400025D RID: 605
		private StringBuilder stringBuilder = new StringBuilder();

		// Token: 0x0400025E RID: 606
		private int minimalLength;

		// Token: 0x0400025F RID: 607
		private int argumentCount;
	}
}
