using static BulletSharp.UnsafeNativeMethods;

#if BT_USE_DOUBLE_PRECISION
using Scalar = System.Double;
#else
using Scalar = System.Single;
#endif


namespace BulletSharp
{
	public class GjkConvexCast : ConvexCast
	{
		public GjkConvexCast(ConvexShape convexA, ConvexShape convexB, VoronoiSimplexSolver simplexSolver)
			: base(btGjkConvexCast_new(convexA.Native, convexB.Native, simplexSolver.Native))
		{
		}
	}
}
