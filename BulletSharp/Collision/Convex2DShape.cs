using static BulletSharp.UnsafeNativeMethods;

#if BT_USE_DOUBLE_PRECISION
using Scalar = System.Double;
#else
using Scalar = System.Single;
#endif


namespace BulletSharp
{
	public class Convex2DShape : ConvexShape
	{
		public Convex2DShape(ConvexShape convexChildShape)
			: base(btConvex2dShape_new(convexChildShape.Native))
		{
			ChildShape = convexChildShape;
		}

		public ConvexShape ChildShape { get; }
	}
}
