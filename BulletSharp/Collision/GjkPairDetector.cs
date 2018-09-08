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
	public class GjkPairDetector : DiscreteCollisionDetectorInterface
	{
		internal GjkPairDetector(IntPtr native)
			: base(native)
		{
		}

		public GjkPairDetector(ConvexShape objectA, ConvexShape objectB, VoronoiSimplexSolver simplexSolver,
			ConvexPenetrationDepthSolver penetrationDepthSolver)
			: base(btGjkPairDetector_new(objectA.Native, objectB.Native, simplexSolver.Native,
				(penetrationDepthSolver != null) ? penetrationDepthSolver.Native : IntPtr.Zero))
		{
		}

		public GjkPairDetector(ConvexShape objectA, ConvexShape objectB, int shapeTypeA,
			int shapeTypeB, Scalar marginA, Scalar marginB, VoronoiSimplexSolver simplexSolver,
			ConvexPenetrationDepthSolver penetrationDepthSolver)
			: base(btGjkPairDetector_new2(objectA.Native, objectB.Native, shapeTypeA,
				shapeTypeB, marginA, marginB, simplexSolver.Native, (penetrationDepthSolver != null) ? penetrationDepthSolver.Native : IntPtr.Zero))
		{
		}

		public void GetClosestPointsNonVirtual(ClosestPointInput input, Result output,
			DebugDraw debugDraw)
		{
			btGjkPairDetector_getClosestPointsNonVirtual(Native, input.Native,
				output.Native, debugDraw != null ? debugDraw._native : IntPtr.Zero);
		}

		public void SetIgnoreMargin(bool ignoreMargin)
		{
			btGjkPairDetector_setIgnoreMargin(Native, ignoreMargin);
		}

		public void SetMinkowskiA(ConvexShape minkA)
		{
			btGjkPairDetector_setMinkowskiA(Native, minkA.Native);
		}

		public void SetMinkowskiB(ConvexShape minkB)
		{
			btGjkPairDetector_setMinkowskiB(Native, minkB.Native);
		}

		public void SetPenetrationDepthSolver(ConvexPenetrationDepthSolver penetrationDepthSolver)
		{
			btGjkPairDetector_setPenetrationDepthSolver(Native, penetrationDepthSolver.Native);
		}

		public Vector3 CachedSeparatingAxis
		{
			get
			{
				Vector3 value;
				btGjkPairDetector_getCachedSeparatingAxis(Native, out value);
				return value;
			}
			set => btGjkPairDetector_setCachedSeparatingAxis(Native, ref value);
		}

		public Scalar CachedSeparatingDistance => btGjkPairDetector_getCachedSeparatingDistance(Native);

		public int CatchDegeneracies
		{
			get => btGjkPairDetector_getCatchDegeneracies(Native);
			set => btGjkPairDetector_setCatchDegeneracies(Native, value);
		}

		public int CurIter
		{
			get => btGjkPairDetector_getCurIter(Native);
			set => btGjkPairDetector_setCurIter(Native, value);
		}

		public int DegenerateSimplex
		{
			get => btGjkPairDetector_getDegenerateSimplex(Native);
			set => btGjkPairDetector_setDegenerateSimplex(Native, value);
		}

		public int FixContactNormalDirection
		{
			get => btGjkPairDetector_getFixContactNormalDirection(Native);
			set => btGjkPairDetector_setFixContactNormalDirection(Native, value);
		}

		public int LastUsedMethod
		{
			get => btGjkPairDetector_getLastUsedMethod(Native);
			set => btGjkPairDetector_setLastUsedMethod(Native, value);
		}
	}
}
