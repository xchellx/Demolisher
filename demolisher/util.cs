using arookas.IO.Binary;
using OpenTK;

namespace arookas {
	static class demoUtil {
		public static Vector3 readVec3(this aBinaryReader reader) {
			return new Vector3(reader.ReadF32(), reader.ReadF32(), reader.ReadF32());
		}
		public static int read24(this aBinaryReader reader) {
			if (reader.Endianness == Endianness.Big) {
				return (reader.Read8() << 16) | (reader.Read8() << 8) | reader.Read8();
			}
			return reader.Read8() | (reader.Read8() << 8) | (reader.Read8() << 24);
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
