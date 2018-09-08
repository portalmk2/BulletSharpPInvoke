using static BulletSharp.UnsafeNativeMethods;

#if BT_USE_DOUBLE_PRECISION
using Scalar = System.Double;
#else
using Scalar = System.Single;
#endif


namespace BulletSharp
{
	public class SphereShape : ConvexInternalShape
	{
		public SphereShape(Scalar radius)
			: base(btSphereShape_new(radius))
		{
		}

		public void SetUnscaledRadius(Scalar radius)
		{
			btSphereShape_setUnscaledRadius(Native, radius);
		}

		public Scalar Radius => btSphereShape_getRadius(Native);
	}
}
