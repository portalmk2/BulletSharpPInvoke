using System;
using static BulletSharp.UnsafeNativeMethods;

#if BT_USE_DOUBLE_PRECISION
using Scalar = System.Double;
#else
using Scalar = System.Single;
#endif


namespace BulletSharp
{
	public class DantzigSolver : MlcpSolverInterface
	{
		internal DantzigSolver(IntPtr native)
			: base(native)
		{
		}

		public DantzigSolver()
			: base(btDantzigSolver_new())
		{
		}
	}
}
