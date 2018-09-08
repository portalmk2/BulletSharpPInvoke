using System;
using static BulletSharp.UnsafeNativeMethods;

#if BT_USE_DOUBLE_PRECISION
using Scalar = System.Double;
#else
using Scalar = System.Single;
#endif


namespace BulletSharp.SoftBody
{
	public class SoftRigidDynamicsWorld : DiscreteDynamicsWorld
	{
		private AlignedSoftBodyArray _softBodyArray;
		private SoftBodySolver _softBodySolver; // private ref passed to bodies during AddSoftBody
		private bool _ownsSolver;

		public SoftRigidDynamicsWorld(Dispatcher dispatcher, BroadphaseInterface pairCache,
			ConstraintSolver constraintSolver, CollisionConfiguration collisionConfiguration,
			SoftBodySolver softBodySolver = null)
			: base(IntPtr.Zero, dispatcher, pairCache)
		{
			if (softBodySolver != null) {
				_softBodySolver = softBodySolver;
				_ownsSolver = false;
			} else {
				_softBodySolver = new DefaultSoftBodySolver();
				_ownsSolver = true;
			}

			Native = btSoftRigidDynamicsWorld_new(dispatcher.Native, pairCache.Native,
				(constraintSolver != null) ? constraintSolver.Native : IntPtr.Zero,
				collisionConfiguration.Native, _softBodySolver._native);

			CollisionObjectArray = new AlignedCollisionObjectArray(btCollisionWorld_getCollisionObjectArray(Native), this);

			_constraintSolver = constraintSolver;
			WorldInfo = new SoftBodyWorldInfo(btSoftRigidDynamicsWorld_getWorldInfo(Native), true);
			WorldInfo.Dispatcher = dispatcher;
			WorldInfo.Broadphase = pairCache;
		}

		public void AddSoftBody(SoftBody body)
		{
			body.SoftBodySolver = _softBodySolver;
			CollisionObjectArray.Add(body);
		}

		public void AddSoftBody(SoftBody body, CollisionFilterGroups collisionFilterGroup, CollisionFilterGroups collisionFilterMask)
		{
			body.SoftBodySolver = _softBodySolver;
			CollisionObjectArray.Add(body, (int)collisionFilterGroup, (int)collisionFilterMask);
		}

		public void AddSoftBody(SoftBody body, int collisionFilterGroup, int collisionFilterMask)
		{
			body.SoftBodySolver = _softBodySolver;
			CollisionObjectArray.Add(body, collisionFilterGroup, collisionFilterMask);
		}

		public void RemoveSoftBody(SoftBody body)
		{
			CollisionObjectArray.Remove(body);
		}

		public int DrawFlags
		{
			get => btSoftRigidDynamicsWorld_getDrawFlags(Native);
			set => btSoftRigidDynamicsWorld_setDrawFlags(Native, value);
		}

		public AlignedSoftBodyArray SoftBodyArray
		{
			get
			{
				if (_softBodyArray == null)
				{
					_softBodyArray = new AlignedSoftBodyArray(btSoftRigidDynamicsWorld_getSoftBodyArray(Native));
				}
				return _softBodyArray;
			}
		}

		public SoftBodyWorldInfo WorldInfo { get; }

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (_ownsSolver)
				{
					_softBodySolver.Dispose();
				}
			}
			base.Dispose(disposing);
		}
	}
}
