using static BulletSharp.UnsafeNativeMethods;

#if BT_USE_DOUBLE_PRECISION
using Scalar = System.Double;
#else
using Scalar = System.Single;
#endif


namespace BulletSharp
{
	public class NncgConstraintSolver : SequentialImpulseConstraintSolver
	{
		public NncgConstraintSolver()
			: base(btNNCGConstraintSolver_new(), false)
		{
		}

		public bool OnlyForNoneContact
		{
			get => btNNCGConstraintSolver_getOnlyForNoneContact(Native);
			set => btNNCGConstraintSolver_setOnlyForNoneContact(Native, value);
		}
	}
}
