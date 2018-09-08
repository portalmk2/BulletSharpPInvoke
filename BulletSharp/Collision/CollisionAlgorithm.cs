using System;
using static BulletSharp.UnsafeNativeMethods;

#if BT_USE_DOUBLE_PRECISION
using Scalar = System.Double;
#else
using Scalar = System.Single;
#endif


namespace BulletSharp
{
	public class CollisionAlgorithmConstructionInfo : IDisposable
	{
		internal IntPtr Native;

		private Dispatcher _dispatcher1;
		private PersistentManifold _manifold;

		public CollisionAlgorithmConstructionInfo()
		{
			Native = btCollisionAlgorithmConstructionInfo_new();
		}

		public CollisionAlgorithmConstructionInfo(Dispatcher dispatcher, int temp)
		{
			Native = btCollisionAlgorithmConstructionInfo_new2((dispatcher != null) ? dispatcher.Native : IntPtr.Zero,
				temp);
			_dispatcher1 = dispatcher;
		}

		public Dispatcher Dispatcher
		{
			get => _dispatcher1;
			set
			{
				btCollisionAlgorithmConstructionInfo_setDispatcher1(Native, (value != null) ? value.Native : IntPtr.Zero);
				_dispatcher1 = value;
			}
		}

		public PersistentManifold Manifold
		{
			get => _manifold;
			set
			{
				btCollisionAlgorithmConstructionInfo_setManifold(Native, (value != null) ? value.Native : IntPtr.Zero);
				_manifold = value;
			}
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
				btCollisionAlgorithmConstructionInfo_delete(Native);
				Native = IntPtr.Zero;
			}
		}

		~CollisionAlgorithmConstructionInfo()
		{
			Dispose(false);
		}
	}

	public class CollisionAlgorithm : IDisposable
	{
		internal IntPtr Native;
		private readonly bool _preventDelete;

		internal CollisionAlgorithm(IntPtr native, bool preventDelete = false)
		{
			Native = native;
			_preventDelete = preventDelete;
		}

		public Scalar CalculateTimeOfImpact(CollisionObject body0, CollisionObject body1,
			DispatcherInfo dispatchInfo, ManifoldResult resultOut)
		{
			return btCollisionAlgorithm_calculateTimeOfImpact(Native, body0.Native,
				body1.Native, dispatchInfo.Native, resultOut.Native);
		}

		public void GetAllContactManifolds(AlignedManifoldArray manifoldArray)
		{
			btCollisionAlgorithm_getAllContactManifolds(Native, manifoldArray._native);
		}

		public void ProcessCollision(CollisionObjectWrapper body0Wrap, CollisionObjectWrapper body1Wrap,
			DispatcherInfo dispatchInfo, ManifoldResult resultOut)
		{
			btCollisionAlgorithm_processCollision(Native, body0Wrap.Native, body1Wrap.Native,
				dispatchInfo.Native, resultOut.Native);
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
				if (!_preventDelete)
				{
					btCollisionAlgorithm_delete(Native);
				}
				Native = IntPtr.Zero;
			}
		}

		~CollisionAlgorithm()
		{
			Dispose(false);
		}
	}
}
