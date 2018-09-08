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
	public static class TransformUtil
	{
		public static void CalculateDiffAxisAngle(ref Matrix transform0, ref Matrix transform1,
			out Vector3 axis, out Scalar angle)
		{
			btTransformUtil_calculateDiffAxisAngle(ref transform0, ref transform1,
				out axis, out angle);
		}

		public static void CalculateDiffAxisAngleQuaternion(ref Quaternion orn0, ref Quaternion orn1a,
			out Vector3 axis, out Scalar angle)
		{
			btTransformUtil_calculateDiffAxisAngleQuaternion(ref orn0, ref orn1a,
				out axis, out angle);
		}

		public static void CalculateVelocity(ref Matrix transform0, ref Matrix transform1,
			Scalar timeStep, out Vector3 linVel, out Vector3 angVel)
		{
			btTransformUtil_calculateVelocity(ref transform0, ref transform1, timeStep,
				out linVel, out angVel);
		}

		public static void CalculateVelocityQuaternion(ref Vector3 pos0, ref Vector3 pos1,
			ref Quaternion orn0, ref Quaternion orn1, Scalar timeStep, out Vector3 linVel, out Vector3 angVel)
		{
			btTransformUtil_calculateVelocityQuaternion(ref pos0, ref pos1, ref orn0,
				ref orn1, timeStep, out linVel, out angVel);
		}

		public static void IntegrateTransform(ref Matrix curTrans, ref Vector3 linvel, ref Vector3 angvel,
			Scalar timeStep, out Matrix predictedTransform)
		{
			btTransformUtil_integrateTransform(ref curTrans, ref linvel, ref angvel,
				timeStep, out predictedTransform);
		}
	}

	public class ConvexSeparatingDistanceUtil : IDisposable
	{
		internal IntPtr _native;

		public ConvexSeparatingDistanceUtil(Scalar boundingRadiusA, Scalar boundingRadiusB)
		{
			_native = btConvexSeparatingDistanceUtil_new(boundingRadiusA, boundingRadiusB);
		}

		public void InitSeparatingDistance(ref Vector3 separatingVector, Scalar separatingDistance,
			ref Matrix transA, ref Matrix transB)
		{
			btConvexSeparatingDistanceUtil_initSeparatingDistance(_native, ref separatingVector,
				separatingDistance, ref transA, ref transB);
		}

		public void UpdateSeparatingDistance(ref Matrix transA, ref Matrix transB)
		{
			btConvexSeparatingDistanceUtil_updateSeparatingDistance(_native, ref transA,
				ref transB);
		}

		public Scalar ConservativeSeparatingDistance => btConvexSeparatingDistanceUtil_getConservativeSeparatingDistance(_native);

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (_native != IntPtr.Zero)
			{
				btConvexSeparatingDistanceUtil_delete(_native);
				_native = IntPtr.Zero;
			}
		}

		~ConvexSeparatingDistanceUtil()
		{
			Dispose(false);
		}
	}
}
