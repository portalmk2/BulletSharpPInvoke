using static BulletSharp.UnsafeNativeMethods;
#if BT_USE_DOUBLE_PRECISION
using Scalar = System.Double;
#else
using Scalar = System.Single;
#endif


namespace BulletSharp
{
	public class EmptyShape : ConcaveShape
	{
		public EmptyShape()
			: base(btEmptyShape_new())
		{
		}
	}
}
