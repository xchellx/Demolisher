using System;

namespace Arookas
{
	// Token: 0x02000082 RID: 130
	public static class Repeater
	{
		// Token: 0x060003CF RID: 975 RVA: 0x0000D570 File Offset: 0x0000B770
		public static void Repeat(int times, Action action)
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			if (times < 0)
			{
				throw new ArgumentOutOfRangeException("times", times, "The specified repeat count was negative.");
			}
			for (int i = 0; i < times; i++)
			{
				action();
			}
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x0000D5B8 File Offset: 0x0000B7B8
		public static void Repeat(int times, Action<int> action)
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			if (times < 0)
			{
				throw new ArgumentOutOfRangeException("times", times, "The specified repeat count was negative.");
			}
			for (int i = 0; i < times; i++)
			{
				action(i);
			}
		}
	}
}
