using System;

namespace Arookas
{
	// Token: 0x0200006C RID: 108
	public static class EnumHelper
	{
		// Token: 0x06000335 RID: 821 RVA: 0x0000BF6C File Offset: 0x0000A16C
		public static bool IsDefined<T>(this T enumValue) where T : struct, IComparable, IFormattable, IConvertible
		{
			return Enum.IsDefined(typeof(T), enumValue);
		}
	}
}
