using System;
using static BulletSharp.UnsafeNativeMethods;

#if BT_USE_DOUBLE_PRECISION
using Scalar = System.Double;
#else
using Scalar = System.Single;
#endif


namespace BulletSharp
{
	public class ManifoldResult : DiscreteCollisionDetectorInterface.Result
	{
		internal ManifoldResult(IntPtr native)
			: base(native)
		{
		}

		public ManifoldResult()
			: base(btManifoldResult_new())
		{
		}

		public ManifoldResult(CollisionObjectWrapper body0Wrap, CollisionObjectWrapper body1Wrap)
			: base(btManifoldResult_new2(body0Wrap.Native, body1Wrap.Native))
		{
		}

		public static Scalar CalculateCombinedContactDamping(CollisionObject body0,
			CollisionObject body1)
		{
			return btManifoldResult_calculateCombinedContactDamping(body0.Native,
				body1.Native);
		}

		public static Scalar CalculateCombinedContactStiffness(CollisionObject body0,
			CollisionObject body1)
		{
			return btManifoldResult_calculateCombinedContactStiffness(body0.Native,
				body1.Native);
		}

		public static Scalar CalculateCombinedFriction(CollisionObject body0, CollisionObject body1)
		{
			return btManifoldResult_calculateCombinedFriction(body0.Native, body1.Native);
		}

		public static Scalar CalculateCombinedRestitution(CollisionObject body0, CollisionObject body1)
		{
			return btManifoldResult_calculateCombinedRestitution(body0.Native, body1.Native);
		}

		public static Scalar CalculateCombinedRollingFriction(CollisionObject body0,
			CollisionObject body1)
		{
			return btManifoldResult_calculateCombinedRollingFriction(body0.Native,
				body1.Native);
		}

		public void RefreshContactPoints()
		{
			btManifoldResult_refreshContactPoints(Native);
		}

		public CollisionObject Body0Internal => CollisionObject.GetManaged(btManifoldResult_getBody0Internal(Native));

		public CollisionObjectWrapper Body0Wrap
		{
			get => new CollisionObjectWrapper(btManifoldResult_getBody0Wrap(Native));
			set => btManifoldResult_setBody0Wrap(Native, value.Native);
		}

		public CollisionObject Body1Internal => CollisionObject.GetManaged(btManifoldResult_getBody1Internal(Native));

		public CollisionObjectWrapper Body1Wrap
		{
			get => new CollisionObjectWrapper(btManifoldResult_getBody1Wrap(Native));
			set => btManifoldResult_setBody1Wrap(Native, value.Native);
		}

		public Scalar ClosestPointDistanceThreshold
		{
			get => btManifoldResult_getClosestPointDistanceThreshold(Native);
			set => btManifoldResult_setClosestPointDistanceThreshold(Native, value);
		}

		public PersistentManifold PersistentManifold
		{
			get => new PersistentManifold(btManifoldResult_getPersistentManifold(Native), true);
			set => btManifoldResult_setPersistentManifold(Native, value.Native);
		}
	}
}
