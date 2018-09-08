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
	public abstract class ConvexInternalShape : ConvexShape
	{
		internal ConvexInternalShape(IntPtr native)
			: base(native)
		{
		}

		public void SetSafeMargin(Scalar minDimension, Scalar defaultMarginMultiplier = 0.1f)
		{
			btConvexInternalShape_setSafeMargin(Native, minDimension, defaultMarginMultiplier);
		}

		public void SetSafeMarginRef(ref Vector3 halfExtents, Scalar defaultMarginMultiplier = 0.1f)
		{
			btConvexInternalShape_setSafeMargin2(Native, ref halfExtents, defaultMarginMultiplier);
		}

		public void SetSafeMargin(Vector3 halfExtents, Scalar defaultMarginMultiplier = 0.1f)
		{
			btConvexInternalShape_setSafeMargin2(Native, ref halfExtents, defaultMarginMultiplier);
		}

		public Vector3 ImplicitShapeDimensions
		{
			get
			{
				Vector3 value;
				btConvexInternalShape_getImplicitShapeDimensions(Native, out value);
				return value;
			}
			set { btConvexInternalShape_setImplicitShapeDimensions(Native, ref value); }
		}

		public Vector3 LocalScalingNV
		{
			get
			{
				Vector3 value;
				btConvexInternalShape_getLocalScalingNV(Native, out value);
				return value;
			}
		}

		public Scalar MarginNV => btConvexInternalShape_getMarginNV(Native);
	}

	public abstract class ConvexInternalAabbCachingShape : ConvexInternalShape
	{
		internal ConvexInternalAabbCachingShape(IntPtr native)
			: base(native)
		{
		}

		public void RecalcLocalAabb()
		{
			btConvexInternalAabbCachingShape_recalcLocalAabb(Native);
		}
	}

	[StructLayout(LayoutKind.Sequential)]
	internal struct ConvexInternalShapeData
	{
		public CollisionShapeData CollisionShapeData;
		public Vector3FloatData LocalScaling;
		public Vector3FloatData ImplicitShapeDimensions;
		public Scalar CollisionMargin;
		public int Padding;

		public static int Offset(string fieldName) { return Marshal.OffsetOf(typeof(ConvexInternalShapeData), fieldName).ToInt32(); }
	}
}
