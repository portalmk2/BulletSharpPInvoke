using System;
using System.Runtime.InteropServices;
using static BulletSharp.UnsafeNativeMethods;

#if BT_USE_DOUBLE_PRECISION
    using Scalar = System.Double;
#else
    using Scalar = System.Single;
#endif


namespace BulletSharp
{
	public class CapsuleShape : ConvexInternalShape
	{
		internal CapsuleShape(IntPtr native)
			: base(native)
		{
		}

		public CapsuleShape(Scalar radius, Scalar height)
			: base(btCapsuleShape_new(radius, height))
		{
		}

		public Scalar HalfHeight => btCapsuleShape_getHalfHeight(Native);

		public Scalar Radius => btCapsuleShape_getRadius(Native);

		public int UpAxis => btCapsuleShape_getUpAxis(Native);
	}

	public class CapsuleShapeX : CapsuleShape
	{
		public CapsuleShapeX(Scalar radius, Scalar height)
			: base(btCapsuleShapeX_new(radius, height))
		{
		}
	}

	public class CapsuleShapeZ : CapsuleShape
	{
		public CapsuleShapeZ(Scalar radius, Scalar height)
			: base(btCapsuleShapeZ_new(radius, height))
		{
		}
	}

	[StructLayout(LayoutKind.Sequential)]
	internal struct CapsuleShapeData
	{
		public ConvexInternalShapeData ConvexInternalShapeData;
		public int UpAxis;

		public static int Offset(string fieldName) { return Marshal.OffsetOf(typeof(CapsuleShapeData), fieldName).ToInt32(); }
	}
}
