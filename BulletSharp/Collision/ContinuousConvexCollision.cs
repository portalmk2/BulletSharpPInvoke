using static BulletSharp.UnsafeNativeMethods;

#if BT_USE_DOUBLE_PRECISION
using Scalar = System.Double;
#else
using Scalar = System.Single;
#endif


namespace BulletSharp
{
	public class ContinuousConvexCollision : ConvexCast
	{
		public ContinuousConvexCollision(ConvexShape shapeA, ConvexShape shapeB,
			VoronoiSimplexSolver simplexSolver, ConvexPenetrationDepthSolver penetrationDepthSolver)
			: base(btContinuousConvexCollision_new(shapeA.Native, shapeB.Native,
				simplexSolver.Native, penetrationDepthSolver.Native))
		{
		}

		public ContinuousConvexCollision(ConvexShape shapeA, StaticPlaneShape plane)
			: base(btContinuousConvexCollision_new2(shapeA.Native, plane.Native))
		{
		}
	}
}
