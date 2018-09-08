using BulletSharp.Math;

#if BT_USE_DOUBLE_PRECISION
using Scalar = System.Double;
#else
using Scalar = System.Single;
#endif


namespace BulletSharp
{
    public class VehicleRaycasterResult
    {
        public Scalar DistFraction { get; set; }
        public Vector3 HitNormalInWorld { get; set; }
        public Vector3 HitPointInWorld { get; set; }
    }
    
    public interface IVehicleRaycaster
	{
        object CastRay(ref Vector3 from, ref Vector3 to, VehicleRaycasterResult result);
	}
}
