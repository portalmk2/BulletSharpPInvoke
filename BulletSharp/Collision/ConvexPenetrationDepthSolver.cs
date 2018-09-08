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
	public abstract class ConvexPenetrationDepthSolver : IDisposable
	{
		internal IntPtr Native;

		internal ConvexPenetrationDepthSolver(IntPtr native)
		{
			Native = native;
		}

		public bool CalcPenDepth(VoronoiSimplexSolver simplexSolver, ConvexShape convexA,
			ConvexShape convexB, Matrix transA, Matrix transB, out Vector3 v, out Vector3 pa,
			out Vector3 pb, DebugDraw debugDraw)
		{
			return btConvexPenetrationDepthSolver_calcPenDepth(Native, simplexSolver.Native,
				convexA.Native, convexB.Native, ref transA, ref transB, out v, out pa,
				out pb, debugDraw != null ? debugDraw._native : IntPtr.Zero);
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (Native != IntPtr.Zero)
			{
				btConvexPenetrationDepthSolver_delete(Native);
				Native = IntPtr.Zero;
			}
		}

		~ConvexPenetrationDepthSolver()
		{
			Dispose(false);
		}
	}
}
