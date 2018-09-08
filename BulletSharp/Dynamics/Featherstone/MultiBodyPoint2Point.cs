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
	public class MultiBodyPoint2Point : MultiBodyConstraint
	{
		public MultiBodyPoint2Point(MultiBody body, int link, RigidBody bodyB, Vector3 pivotInA,
			Vector3 pivotInB)
			: base(btMultiBodyPoint2Point_new(body.Native, link, bodyB != null ? bodyB.Native : IntPtr.Zero,
				ref pivotInA, ref pivotInB), body, null)
		{
		}

		public MultiBodyPoint2Point(MultiBody bodyA, int linkA, MultiBody bodyB,
			int linkB, Vector3 pivotInA, Vector3 pivotInB)
			: base(btMultiBodyPoint2Point_new2(bodyA.Native, linkA, bodyB.Native,
				linkB, ref pivotInA, ref pivotInB), bodyA, bodyB)
		{
		}

		public Vector3 PivotInB
		{
			get
			{
				Vector3 value;
				btMultiBodyPoint2Point_getPivotInB(Native, out value);
				return value;
			}
			set => btMultiBodyPoint2Point_setPivotInB(Native, ref value);
		}
	}
}
