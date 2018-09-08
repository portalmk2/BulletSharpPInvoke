using System;

#if BT_USE_DOUBLE_PRECISION
using Scalar = System.Double;
#else
using Scalar = System.Single;
#endif


namespace BulletSharp
{
	public abstract class ActivatingCollisionAlgorithm : CollisionAlgorithm
	{
		internal ActivatingCollisionAlgorithm(IntPtr native, bool preventDelete = false)
			: base(native, preventDelete)
		{
		}
	}
}
