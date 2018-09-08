using static BulletSharp.UnsafeNativeMethods;

#if BT_USE_DOUBLE_PRECISION
using Scalar = System.Double;
#else
using Scalar = System.Single;
#endif


namespace BulletSharp
{
	public class UniformScalingShape : ConvexShape
	{
		public UniformScalingShape(ConvexShape convexChildShape, Scalar uniformScalingFactor)
			: base(btUniformScalingShape_new(convexChildShape.Native, uniformScalingFactor))
		{
			ChildShape = convexChildShape;
		}

		public ConvexShape ChildShape { get; }

		public Scalar UniformScalingFactor
		{
			get { return btUniformScalingShape_getUniformScalingFactor(Native); }
		}
	}
}
