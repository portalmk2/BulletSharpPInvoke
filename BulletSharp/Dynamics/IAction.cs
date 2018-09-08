using System;
using System.Runtime.InteropServices;
using System.Security;
using static BulletSharp.UnsafeNativeMethods;

#if BT_USE_DOUBLE_PRECISION
using Scalar = System.Double;
#else
using Scalar = System.Single;
#endif


namespace BulletSharp
{
	public interface IAction
	{
		void DebugDraw(DebugDraw debugDrawer);
		void UpdateAction(CollisionWorld collisionWorld, Scalar deltaTimeStep);
	}

	internal class ActionInterfaceWrapper : IDisposable
	{
		internal IntPtr _native;
		private IAction _actionInterface;
		private DynamicsWorld _world;

		[UnmanagedFunctionPointer(Native.Conv), SuppressUnmanagedCodeSecurity]
		private delegate void DebugDrawUnmanagedDelegate(IntPtr debugDrawer);
		[UnmanagedFunctionPointer(Native.Conv), SuppressUnmanagedCodeSecurity]
		private delegate void UpdateActionUnmanagedDelegate(IntPtr collisionWorld, Scalar deltaTimeStep);

		private DebugDrawUnmanagedDelegate _debugDraw;
		private UpdateActionUnmanagedDelegate _updateAction;

		public ActionInterfaceWrapper(IAction actionInterface, DynamicsWorld world)
		{
			_actionInterface = actionInterface;
			_world = world;

			_debugDraw = new DebugDrawUnmanagedDelegate(DebugDrawUnmanaged);
			_updateAction = new UpdateActionUnmanagedDelegate(UpdateActionUnmanaged);

			_native = btActionInterfaceWrapper_new(
				Marshal.GetFunctionPointerForDelegate(_debugDraw),
				Marshal.GetFunctionPointerForDelegate(_updateAction));
		}

		private void DebugDrawUnmanaged(IntPtr debugDrawer)
		{
			_actionInterface.DebugDraw(DebugDraw.GetManaged(debugDrawer));
		}

		private void UpdateActionUnmanaged(IntPtr collisionWorld, Scalar deltaTimeStep)
		{
			_actionInterface.UpdateAction(_world, deltaTimeStep);
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (_native != IntPtr.Zero)
			{
				btActionInterface_delete(_native);
				_native = IntPtr.Zero;
			}
		}

		~ActionInterfaceWrapper()
		{
			Dispose(false);
		}
	}
}
