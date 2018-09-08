using System;
using BulletSharp.Math;
using static BulletSharp.UnsafeNativeMethods;

#if BT_USE_DOUBLE_PRECISION
using Scalar = System.Double;
#else
using Scalar = System.Single;
#endif


namespace BulletSharp
{
	public enum PhyScalarType
	{
		Single,
		Double,
		Int32,
		Int16,
		FixedPoint88,
		Byte
	}

	public abstract class ConcaveShape : CollisionShape
	{
		internal ConcaveShape(IntPtr native)
			: base(native)
		{
		}

		public void ProcessAllTriangles(TriangleCallback callback, Vector3 aabbMin,
			Vector3 aabbMax)
		{
			btConcaveShape_processAllTriangles(Native, callback.Native, ref aabbMin,
				ref aabbMax);
		}
	}
}
