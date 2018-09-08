using System.Runtime.InteropServices;
using BulletSharp.Math;
using static BulletSharp.UnsafeNativeMethods;

#if BT_USE_DOUBLE_PRECISION
using Scalar = System.Double;
#else
using Scalar = System.Single;
#endif


namespace BulletSharp
{
	public class StaticPlaneShape : ConcaveShape
	{
		public StaticPlaneShape(Vector3 planeNormal, Scalar planeConstant)
			: base(btStaticPlaneShape_new(ref planeNormal, planeConstant))
		{
		}

		public Scalar PlaneConstant => btStaticPlaneShape_getPlaneConstant(Native);

		public Vector3 PlaneNormal
		{
			get
			{
				Vector3 value;
				btStaticPlaneShape_getPlaneNormal(Native, out value);
				return value;
			}
		}
	}

	[StructLayout(LayoutKind.Sequential)]
	internal struct StaticPlaneShapeData
	{
		public CollisionShapeData CollisionShapeData;
		public Vector3FloatData LocalScaling;
		public Vector3FloatData PlaneNormal;
		public Scalar PlaneConstant;
		public int Padding;

		public static int Offset(string fieldName) { return Marshal.OffsetOf(typeof(StaticPlaneShapeData), fieldName).ToInt32(); }
	}
}
