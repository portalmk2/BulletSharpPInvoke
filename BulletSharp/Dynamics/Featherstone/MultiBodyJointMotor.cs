using static BulletSharp.UnsafeNativeMethods;

#if BT_USE_DOUBLE_PRECISION
using Scalar = System.Double;
#else
using Scalar = System.Single;
#endif


namespace BulletSharp
{
	public class MultiBodyJointMotor : MultiBodyConstraint
	{
		public MultiBodyJointMotor(MultiBody body, int link, Scalar desiredVelocity,
			Scalar maxMotorImpulse)
			: base(btMultiBodyJointMotor_new(body.Native, link, desiredVelocity,
				maxMotorImpulse), body, body)
		{
		}

		public MultiBodyJointMotor(MultiBody body, int link, int linkDoF, Scalar desiredVelocity,
			Scalar maxMotorImpulse)
			: base(btMultiBodyJointMotor_new2(body.Native, link, linkDoF, desiredVelocity,
				maxMotorImpulse), body, body)
		{
		}

		public void SetVelocityTarget(Scalar velTarget)
		{
			btMultiBodyJointMotor_setVelocityTarget(Native, velTarget);
		}
	}
}
