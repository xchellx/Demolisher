using OpenTK;

namespace Arookas.Demolisher
{
	struct BoundingBox
	{
		public Vector3 Max { get; private set; }
		public Vector3 Min { get; private set; }

		public BoundingBox(Vector3 min, Vector3 max)
			: this() // stupid fucking C#
		{
			Min = min;
			Max = max;
		}

		public static BoundingBox operator *(BoundingBox bbox, float scalar)
		{
			return new BoundingBox(bbox.Min * scalar, bbox.Max * scalar);
		}
	}
}