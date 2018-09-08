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
	public class ConeShape : ConvexInternalShape
	{
		internal ConeShape(IntPtr native)
			: base(native)
		{
		}

		public ConeShape(Scalar radius, Scalar height)
			: base(btConeShape_new(radius, height))
		{
		}

		public int ConeUpIndex
		{
			get => btConeShape_getConeUpIndex(Native);
			set => btConeShape_setConeUpIndex(Native, value);
		}

		public Scalar Height
		{
			get => btConeShape_getHeight(Native);
			set => btConeShape_setHeight(Native, value);
		}

		public Scalar Radius
		{
			get => btConeShape_getRadius(Native);
			set => btConeShape_setRadius(Native, value);
		}
	}

	public class ConeShapeX : ConeShape
	{
		public ConeShapeX(Scalar radius, Scalar height)
			: base(btConeShapeX_new(radius, height))
		{
		}
	}

	public class ConeShapeZ : ConeShape
	{
		public ConeShapeZ(Scalar radius, Scalar height)
			: base(btConeShapeZ_new(radius, height))
		{
		}
	}

	[StructLayout(LayoutKind.Sequential)]
	internal struct ConeShapeData
	{
		public ConvexInternalShapeData ConvexInternalShapeData;
		public int UpAxis;
		public int Padding;

		public static int Offset(string fieldName) { return Marshal.OffsetOf(typeof(ConeShapeData), fieldName).ToInt32(); }
	}
}
