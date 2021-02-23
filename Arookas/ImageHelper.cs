using System;
using System.Drawing.Imaging;

namespace Arookas
{
	// Token: 0x0200006D RID: 109
	public static class ImageHelper
	{
		// Token: 0x06000336 RID: 822 RVA: 0x0000BF84 File Offset: 0x0000A184
		public static string GetExtension(this ImageFormat imageFormat)
		{
			if (imageFormat == ImageFormat.Bmp)
			{
				return "bmp";
			}
			if (imageFormat == ImageFormat.Emf)
			{
				return "emf";
			}
			if (imageFormat == ImageFormat.Exif)
			{
				return "exif";
			}
			if (imageFormat == ImageFormat.Gif)
			{
				return "gif";
			}
			if (imageFormat == ImageFormat.Icon)
			{
				return "ico";
			}
			if (imageFormat == ImageFormat.Jpeg)
			{
				return "jpg";
			}
			if (imageFormat == ImageFormat.Png)
			{
				return "png";
			}
			if (imageFormat == ImageFormat.Tiff)
			{
				return "tiff";
			}
			return null;
		}
	}
}
