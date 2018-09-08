using System;
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
	public class CylinderShape : ConvexInternalShape
	{
		internal CylinderShape(IntPtr native)
			: base(native)
		{
		}

		public CylinderShape(Vector3 halfExtents)
			: base(btCylinderShape_new(ref halfExtents))
		{
		}

		public CylinderShape(Scalar halfExtentX, Scalar halfExtentY, Scalar halfExtentZ)
			: base(btCylinderShape_new2(halfExtentX, halfExtentY, halfExtentZ))
		{
		}

		public Vector3 HalfExtentsWithMargin
		{
			get
			{
				Vector3 value;
				btCylinderShape_getHalfExtentsWithMargin(Native, out value);
				return value;
			}
		}

		public Vector3 HalfExtentsWithoutMargin
		{
			get
			{
				Vector3 value;
				btCylinderShape_getHalfExtentsWithoutMargin(Native, out value);
				return value;
			}
		}

		public Scalar Radius => btCylinderShape_getRadius(Native);

		public int UpAxis => btCylinderShape_getUpAxis(Native);
	}

	public class CylinderShapeX : CylinderShape
	{
		public CylinderShapeX(Vector3 halfExtents)
			: base(btCylinderShapeX_new(ref halfExtents))
		{
		}

		public CylinderShapeX(Scalar halfExtentX, Scalar halfExtentY, Scalar halfExtentZ)
			: base(btCylinderShapeX_new2(halfExtentX, halfExtentY, halfExtentZ))
		{
		}
	}

	public class CylinderShapeZ : CylinderShape
	{
		public CylinderShapeZ(Vector3 halfExtents)
			: base(btCylinderShapeZ_new(ref halfExtents))
		{
		}

		public CylinderShapeZ(Scalar halfExtentX, Scalar halfExtentY, Scalar halfExtentZ)
			: base(btCylinderShapeZ_new2(halfExtentX, halfExtentY, halfExtentZ))
		{
		}
	}

	[StructLayout(LayoutKind.Sequential)]
	internal struct CylinderShapeData
	{
		public ConvexInternalShapeData ConvexInternalShapeData;
		public int UpAxis;
		public int Padding;

		public static int Offset(string fieldName) { return Marshal.OffsetOf(typeof(CylinderShapeData), fieldName).ToInt32(); }
	}
}
