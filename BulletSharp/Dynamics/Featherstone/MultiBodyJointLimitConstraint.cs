using static BulletSharp.UnsafeNativeMethods;

#if BT_USE_DOUBLE_PRECISION
using Scalar = System.Double;
#else
using Scalar = System.Single;
#endif


namespace BulletSharp
{
	public class MultiBodyJointLimitConstraint : MultiBodyConstraint
	{
		public MultiBodyJointLimitConstraint(MultiBody body, int link, Scalar lower,
			Scalar upper)
			: base(btMultiBodyJointLimitConstraint_new(body.Native, link, lower,
				upper), body, body)
		{
		}
	}
}
