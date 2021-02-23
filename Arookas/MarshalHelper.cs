using System;
using System.Runtime.InteropServices;

namespace Arookas
{
	// Token: 0x0200008D RID: 141
	public static class MarshalHelper
	{
		// Token: 0x06000460 RID: 1120 RVA: 0x0000E31C File Offset: 0x0000C51C
		public static IntPtr Allocate<T>()
		{
			return Marshal.AllocHGlobal(Marshal.SizeOf(typeof(T)));
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x0000E332 File Offset: 0x0000C532
		public static int SizeOf<T>()
		{
			return Marshal.SizeOf(typeof(T));
		}

		// Token: 0x06000462 RID: 1122 RVA: 0x0000E343 File Offset: 0x0000C543
		public static T ToStructure<T>(this IntPtr pointer)
		{
			return (T)((object)Marshal.PtrToStructure(pointer, typeof(T)));
		}
	}
}
