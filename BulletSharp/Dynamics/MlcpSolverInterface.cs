using System;
using static BulletSharp.UnsafeNativeMethods;

#if BT_USE_DOUBLE_PRECISION
using Scalar = System.Double;
#else
using Scalar = System.Single;
#endif


namespace BulletSharp
{
	public abstract class MlcpSolverInterface : IDisposable
	{
		internal IntPtr Native;

		internal MlcpSolverInterface(IntPtr native)
		{
			Native = native;
		}
		/*
		public bool SolveMLCP(btMatrixX<Scalar> a, btVectorX<Scalar> b, btVectorX<Scalar> x,
			btVectorX<Scalar> lo, btVectorX<Scalar> hi, AlignedObjectArray<int> limitDependency,
			int numIterations, bool useSparsity = true)
		{
			return btMLCPSolverInterface_solveMLCP(Native, a.Native, b.Native,
				x.Native, lo.Native, hi.Native, limitDependency.Native, numIterations,
				useSparsity);
		}
		*/
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (Native != IntPtr.Zero)
			{
				btMLCPSolverInterface_delete(Native);
				Native = IntPtr.Zero;
			}
		}

		~MlcpSolverInterface()
		{
			Dispose(false);
		}
	}
}
