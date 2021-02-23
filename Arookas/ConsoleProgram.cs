using System;

namespace Arookas
{
	// Token: 0x02000027 RID: 39
	public class ConsoleProgram
	{
		// Token: 0x1700005C RID: 92
		// (get) Token: 0x0600011B RID: 283 RVA: 0x00004479 File Offset: 0x00002679
		// (set) Token: 0x0600011C RID: 284 RVA: 0x00004481 File Offset: 0x00002681
		public Func<int, bool> ArgumentCountValidation
		{
			get
			{
				return this.argumentCountValidation;
			}
			set
			{
				this.argumentCountValidation = value;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x0600011D RID: 285 RVA: 0x0000448A File Offset: 0x0000268A
		public string Author
		{
			get
			{
				return this.author;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x0600011E RID: 286 RVA: 0x00004492 File Offset: 0x00002692
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x0600011F RID: 287 RVA: 0x0000449A File Offset: 0x0000269A
		public string Separator
		{
			get
			{
				return new string('=', this.separatorWidth);
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000120 RID: 288 RVA: 0x000044A9 File Offset: 0x000026A9
		// (set) Token: 0x06000121 RID: 289 RVA: 0x000044B1 File Offset: 0x000026B1
		public int SeparatorWidth
		{
			get
			{
				return this.separatorWidth;
			}
			set
			{
				if (this.separatorWidth < 0)
				{
					throw new ArgumentOutOfRangeException("value", value, "The specified separator width was negative.");
				}
				this.separatorWidth = value;
			}
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000122 RID: 290 RVA: 0x000044DC File Offset: 0x000026DC
		// (remove) Token: 0x06000123 RID: 291 RVA: 0x00004514 File Offset: 0x00002714
		public event EventHandler<ConsoleProgramEventArgs> ShowUsage;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000124 RID: 292 RVA: 0x0000454C File Offset: 0x0000274C
		// (remove) Token: 0x06000125 RID: 293 RVA: 0x00004584 File Offset: 0x00002784
		public event EventHandler<ConsoleProgramEventArgs> Started;

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000126 RID: 294 RVA: 0x000045B9 File Offset: 0x000027B9
		public Version Version
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x06000127 RID: 295 RVA: 0x000045C1 File Offset: 0x000027C1
		public ConsoleProgram(string name, Version version) : this(name, version, "arookas")
		{
		}

		// Token: 0x06000128 RID: 296 RVA: 0x000045D0 File Offset: 0x000027D0
		private ConsoleProgram(string name, Version version, string author)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (author == null)
			{
				throw new ArgumentNullException("author");
			}
			this.name = name;
			this.version = version;
			this.author = author;
		}

		// Token: 0x06000129 RID: 297 RVA: 0x0000461C File Offset: 0x0000281C
		public void Error(string formatString, params object[] arguments)
		{
			if (formatString == null)
			{
				throw new ArgumentNullException("formatString");
			}
			if (arguments == null)
			{
				throw new ArgumentNullException("arguments");
			}
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine("ERROR: " + formatString, arguments);
			Console.ResetColor();
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00004657 File Offset: 0x00002857
		public void Finish()
		{
			Console.WriteLine(this.Separator);
			Console.WriteLine("Done!");
			Console.ReadKey();
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00004674 File Offset: 0x00002874
		private void OnShowUsage(string[] arguments)
		{
			EventHandler<ConsoleProgramEventArgs> showUsage = this.ShowUsage;
			if (showUsage != null)
			{
				showUsage(this, new ConsoleProgramEventArgs(this, arguments));
			}
		}

		// Token: 0x0600012C RID: 300 RVA: 0x0000469C File Offset: 0x0000289C
		private void OnStarted(string[] arguments)
		{
			EventHandler<ConsoleProgramEventArgs> started = this.Started;
			if (started != null)
			{
				started(this, new ConsoleProgramEventArgs(this, arguments));
			}
		}

		// Token: 0x0600012D RID: 301 RVA: 0x000046C1 File Offset: 0x000028C1
		public void WriteSeparator()
		{
			Console.WriteLine(this.Separator);
		}

		// Token: 0x0600012E RID: 302 RVA: 0x000046D0 File Offset: 0x000028D0
		public void Start(string[] arguments)
		{
			if (this.isStarted)
			{
				throw new InvalidOperationException("The ConsoleProgram has already been started.");
			}
			Console.WriteLine("{0} v{1} {2}", this.name, this.version, this.author);
			Console.WriteLine(this.Separator);
			this.isStarted = true;
			if (this.argumentCountValidation != null && !this.argumentCountValidation(arguments.Length))
			{
				Console.Write("Usage: {0}.exe ", this.name);
				this.OnShowUsage(arguments);
				Console.ReadKey();
				return;
			}
			try
			{
				this.OnStarted(arguments);
				this.Finish();
			}
			catch (Exception ex)
			{
				Console.WriteLine(this.Separator);
				this.Error("{0}\nAn Exception of type {1} occured\n{2}", new object[]
				{
					ex.Message,
					ex.GetType().Name,
					ex.StackTrace
				});
				Console.ReadKey();
			}
			finally
			{
				this.isStarted = false;
			}
		}

		// Token: 0x0600012F RID: 303 RVA: 0x000047D0 File Offset: 0x000029D0
		public void Warning(string formatString, params object[] arguments)
		{
			if (formatString == null)
			{
				throw new ArgumentNullException("formatString");
			}
			if (arguments == null)
			{
				throw new ArgumentNullException("arguments");
			}
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine("WARNING: " + formatString, arguments);
			Console.ResetColor();
		}

		// Token: 0x0400004A RID: 74
		private Func<int, bool> argumentCountValidation;

		// Token: 0x0400004B RID: 75
		private string author;

		// Token: 0x0400004C RID: 76
		private bool isStarted;

		// Token: 0x0400004D RID: 77
		private string name;

		// Token: 0x0400004E RID: 78
		private int separatorWidth = 79;

		// Token: 0x04000051 RID: 81
		private Version version;
	}
}
