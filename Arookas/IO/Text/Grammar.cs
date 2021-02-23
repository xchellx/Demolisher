using System;
using System.Collections.Generic;
using System.Linq;

namespace Arookas.IO.Text
{
	// Token: 0x0200007A RID: 122
	internal class Grammar
	{
		// Token: 0x06000361 RID: 865 RVA: 0x0000C2AA File Offset: 0x0000A4AA
		public void AddToken(string name, string rule)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (rule == null)
			{
				throw new ArgumentNullException("rule");
			}
		}

		// Token: 0x06000362 RID: 866 RVA: 0x0000C2D0 File Offset: 0x0000A4D0
		public void AddFragment(string name, params string[] rules)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (rules == null)
			{
				throw new ArgumentNullException("rules");
			}
			if (rules.Any((string rule) => rule == null))
			{
				throw new ArgumentException("The specified collection of token rules contains at least one null element.", "rules");
			}
		}

		// Token: 0x040001C8 RID: 456
		private List<Grammar.Token> tokens;

		// Token: 0x0200007B RID: 123
		private class Token
		{
			// Token: 0x170000FC RID: 252
			// (get) Token: 0x06000365 RID: 869 RVA: 0x0000C336 File Offset: 0x0000A536
			public string Name
			{
				get
				{
					return this.name;
				}
			}

			// Token: 0x170000FD RID: 253
			// (get) Token: 0x06000366 RID: 870 RVA: 0x0000C33E File Offset: 0x0000A53E
			public string[] Rules
			{
				get
				{
					return this.rules;
				}
			}

			// Token: 0x06000367 RID: 871 RVA: 0x0000C346 File Offset: 0x0000A546
			public Token(string name, string[] rules)
			{
				this.name = name;
				this.rules = rules;
			}

			// Token: 0x040001CA RID: 458
			private string name;

			// Token: 0x040001CB RID: 459
			private string[] rules;
		}
	}
}
