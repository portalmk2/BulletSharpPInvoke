using System;
using static BulletSharp.UnsafeNativeMethods;

#if BT_USE_DOUBLE_PRECISION
using Scalar = System.Double;
#else
using Scalar = System.Single;
#endif


namespace BulletSharp
{
	public abstract class MultiBodyConstraint : IDisposable
	{
		internal IntPtr Native;

		internal MultiBodyConstraint(IntPtr native, MultiBody bodyA, MultiBody bodyB)
		{
			Native = native;
			MultiBodyA = bodyA;
			MultiBodyB = bodyB;
		}

		public void AllocateJacobiansMultiDof()
		{
			btMultiBodyConstraint_allocateJacobiansMultiDof(Native);
		}
		/*
		public void CreateConstraintRows(MultiBodyConstraintArray constraintRows,
			MultiBodyJacobianData data, ContactSolverInfo infoGlobal)
		{
			btMultiBodyConstraint_createConstraintRows(Native, constraintRows.Native,
				data.Native, infoGlobal.Native);
		}
		*/
		public void DebugDraw(DebugDraw drawer)
		{
			btMultiBodyConstraint_debugDraw(Native, drawer._native);
		}

		public void FinalizeMultiDof()
		{
			btMultiBodyConstraint_finalizeMultiDof(Native);
		}

		public Scalar GetAppliedImpulse(int dof)
		{
			return btMultiBodyConstraint_getAppliedImpulse(Native, dof);
		}

		public Scalar GetPosition(int row)
		{
			return btMultiBodyConstraint_getPosition(Native, row);
		}

		public void InternalSetAppliedImpulse(int dof, Scalar appliedImpulse)
		{
			btMultiBodyConstraint_internalSetAppliedImpulse(Native, dof, appliedImpulse);
		}
		/*
		public Scalar JacobianA(int row)
		{
			return btMultiBodyConstraint_jacobianA(Native, row);
		}

		public Scalar JacobianB(int row)
		{
			return btMultiBodyConstraint_jacobianB(Native, row);
		}
		*/
		public void SetPosition(int row, Scalar pos)
		{
			btMultiBodyConstraint_setPosition(Native, row, pos);
		}

		public void UpdateJacobianSizes()
		{
			btMultiBodyConstraint_updateJacobianSizes(Native);
		}

		public int IslandIdA => btMultiBodyConstraint_getIslandIdA(Native);

		public int IslandIdB => btMultiBodyConstraint_getIslandIdB(Native);

		public bool IsUnilateral => btMultiBodyConstraint_isUnilateral(Native);

		public Scalar MaxAppliedImpulse
		{
			get => btMultiBodyConstraint_getMaxAppliedImpulse(Native);
			set => btMultiBodyConstraint_setMaxAppliedImpulse(Native, value);
		}

		public MultiBody MultiBodyA { get; }

		public MultiBody MultiBodyB { get; }

		public int NumRows => btMultiBodyConstraint_getNumRows(Native);

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (Native != IntPtr.Zero)
			{
				btMultiBodyConstraint_delete(Native);
				Native = IntPtr.Zero;
			}
		}

		~MultiBodyConstraint()
		{
			Dispose(false);
		}
	}
}
