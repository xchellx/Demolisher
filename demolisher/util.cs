using arookas.IO.Binary;
using OpenTK;

namespace arookas {
	static class demoUtil {
		public static Vector3 readVec3(this aBinaryReader reader) {
			var x = reader.ReadF32();
			var y = reader.ReadF32();
			var z = reader.ReadF32();
			return new Vector3(x, y, z);
		}
		public static int read24(this aBinaryReader reader) {
			var bytes = reader.Read8s(3);
			switch (reader.Endianness) {
				case Endianness.Big: {
					return (bytes[0] << 16) | (bytes[1] << 8) | bytes[2];
				}
				case Endianness.Little: {
					return (bytes[2] << 16) | (bytes[1] << 8) | bytes[0];
				}
			}
			return 0;
		}
		public static int readS24(this aBinaryReader reader) {
			var value = read24(reader);
			if ((value & 0x800000) != 0) {
				value |= unchecked((int)0xFF000000);
			}
			return value;
		}
	}
}
