using System;
using static BulletSharp.UnsafeNativeMethods;

#if BT_USE_DOUBLE_PRECISION
using Scalar = System.Double;
#else
using Scalar = System.Single;
#endif


namespace BulletSharp
{
	public class CompoundCompoundCollisionAlgorithm : CompoundCollisionAlgorithm
	{
		public new class CreateFunc : CollisionAlgorithmCreateFunc
		{
			internal CreateFunc(IntPtr native)
				: base(native, true)
			{
			}

			public CreateFunc()
				: base(btCompoundCompoundCollisionAlgorithm_CreateFunc_new(), false)
			{
			}

            public override CollisionAlgorithm CreateCollisionAlgorithm(CollisionAlgorithmConstructionInfo __unnamed0,
                CollisionObjectWrapper body0Wrap, CollisionObjectWrapper body1Wrap)
            {
                return new CompoundCompoundCollisionAlgorithm(btCollisionAlgorithmCreateFunc_CreateCollisionAlgorithm(
                    Native, __unnamed0.Native, body0Wrap.Native, body1Wrap.Native));
            }
		}

		public new class SwappedCreateFunc : CollisionAlgorithmCreateFunc
		{
			internal SwappedCreateFunc(IntPtr native)
				: base(native, true)
			{
			}

			public SwappedCreateFunc()
				: base(btCompoundCompoundCollisionAlgorithm_SwappedCreateFunc_new(), false)
			{
			}

            public override CollisionAlgorithm CreateCollisionAlgorithm(CollisionAlgorithmConstructionInfo __unnamed0, CollisionObjectWrapper body0Wrap, CollisionObjectWrapper body1Wrap)
            {
                return new CompoundCompoundCollisionAlgorithm(btCollisionAlgorithmCreateFunc_CreateCollisionAlgorithm(
                    Native, __unnamed0.Native, body0Wrap.Native, body1Wrap.Native));
            }
		}

		internal CompoundCompoundCollisionAlgorithm(IntPtr native)
			: base(native)
		{
		}

		public CompoundCompoundCollisionAlgorithm(CollisionAlgorithmConstructionInfo ci,
			CollisionObjectWrapper body0Wrap, CollisionObjectWrapper body1Wrap, bool isSwapped)
			: base(btCompoundCompoundCollisionAlgorithm_new(ci.Native, body0Wrap.Native,
				body1Wrap.Native, isSwapped))
		{
		}
	}
}
