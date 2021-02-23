using System;
using System.IO;

namespace Arookas.IO
{
	// Token: 0x0200008C RID: 140
	public static class StreamHelper
	{
		// Token: 0x0600045E RID: 1118 RVA: 0x0000E2CD File Offset: 0x0000C4CD
		public static MemoryStream ToMemoryStream(this byte[] array, bool writable, bool publiclyVisible)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			return new MemoryStream(array, 0, array.Length, writable, publiclyVisible);
		}

		// Token: 0x0600045F RID: 1119 RVA: 0x0000E2EC File Offset: 0x0000C4EC
		public static bool TryReadByte(this Stream stream, out byte data)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			int num = stream.ReadByte();
			if (num < -1)
			{
				data = 0;
				return false;
			}
			data = (byte)num;
			return true;
		}
	}
}
