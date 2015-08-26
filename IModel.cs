using System;
using OpenTK;

namespace Arookas.Demolisher
{
	interface IModel : IRenderable, IDisposable
	{
		Vector3 Position { get; set; }
		Vector3 Rotation { get; set; }
		Vector3 Scale { get; set; }
		string Name { get; }

		void Render();
	}

	interface IRenderable
	{
		bool Visible { get; set; }
	}
}