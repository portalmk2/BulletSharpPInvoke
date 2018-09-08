using System;
using System.Runtime.InteropServices;
using BulletSharp.Math;
using static BulletSharp.UnsafeNativeMethods;

#if BT_USE_DOUBLE_PRECISION
using Scalar = System.Double;
#else
using Scalar = System.Single;
#endif


namespace BulletSharp
{
	[Flags]
	public enum HingeFlags
	{
		None = 0,
		CfmStop = 1,
		ErpStop = 2,
		CfmNormal = 4,
		ErpNormal = 8
	}

	public class HingeConstraint : TypedConstraint
	{
		internal HingeConstraint(IntPtr native)
			: base(native)
		{
		}

		public HingeConstraint(RigidBody rigidBodyA, RigidBody rigidBodyB, Vector3 pivotInA,
			Vector3 pivotInB, Vector3 axisInA, Vector3 axisInB, bool useReferenceFrameA = false)
			: base(btHingeConstraint_new(rigidBodyA.Native, rigidBodyB.Native,
				ref pivotInA, ref pivotInB, ref axisInA, ref axisInB, useReferenceFrameA))
		{
			_rigidBodyA = rigidBodyA;
			_rigidBodyB = rigidBodyB;
		}

		public HingeConstraint(RigidBody rigidBodyA, Vector3 pivotInA, Vector3 axisInA,
			bool useReferenceFrameA = false)
			: base(btHingeConstraint_new2(rigidBodyA.Native, ref pivotInA, ref axisInA,
				useReferenceFrameA))
		{
			_rigidBodyA = rigidBodyA;
			_rigidBodyB = GetFixedBody();
		}

		public HingeConstraint(RigidBody rigidBodyA, RigidBody rigidBodyB, Matrix rigidBodyAFrame,
			Matrix rigidBodyBFrame, bool useReferenceFrameA = false)
			: base(btHingeConstraint_new3(rigidBodyA.Native, rigidBodyB.Native,
				ref rigidBodyAFrame, ref rigidBodyBFrame, useReferenceFrameA))
		{
			_rigidBodyA = rigidBodyA;
			_rigidBodyB = rigidBodyB;
		}

		public HingeConstraint(RigidBody rigidBodyA, Matrix rigidBodyAFrame, bool useReferenceFrameA = false)
			: base(btHingeConstraint_new4(rigidBodyA.Native, ref rigidBodyAFrame,
				useReferenceFrameA))
		{
			_rigidBodyA = rigidBodyA;
			_rigidBodyB = GetFixedBody();
		}

		public void EnableAngularMotor(bool enableMotor, Scalar targetVelocity, Scalar maxMotorImpulse)
		{
			btHingeConstraint_enableAngularMotor(Native, enableMotor, targetVelocity,
				maxMotorImpulse);
		}

		public Scalar GetHingeAngleRef(ref Matrix transA, ref Matrix transB)
		{
			return btHingeConstraint_getHingeAngle(Native, ref transA, ref transB);
		}

		public Scalar GetHingeAngle(Matrix transA, Matrix transB)
		{
			return btHingeConstraint_getHingeAngle(Native, ref transA, ref transB);
		}

		public void GetInfo1NonVirtual(ConstraintInfo1 info)
		{
			btHingeConstraint_getInfo1NonVirtual(Native, info._native);
		}

		public void GetInfo2Internal(ConstraintInfo2 info, Matrix transA, Matrix transB,
			Vector3 angVelA, Vector3 angVelB)
		{
			btHingeConstraint_getInfo2Internal(Native, info._native, ref transA,
				ref transB, ref angVelA, ref angVelB);
		}

		public void GetInfo2InternalUsingFrameOffset(ConstraintInfo2 info, Matrix transA,
			Matrix transB, Vector3 angVelA, Vector3 angVelB)
		{
			btHingeConstraint_getInfo2InternalUsingFrameOffset(Native, info._native,
				ref transA, ref transB, ref angVelA, ref angVelB);
		}

		public void GetInfo2NonVirtual(ConstraintInfo2 info, Matrix transA, Matrix transB,
			Vector3 angVelA, Vector3 angVelB)
		{
			btHingeConstraint_getInfo2NonVirtual(Native, info._native, ref transA,
				ref transB, ref angVelA, ref angVelB);
		}

		public void SetAxisRef(ref Vector3 axisInA)
		{
			btHingeConstraint_setAxis(Native, ref axisInA);
		}

		public void SetAxis(Vector3 axisInA)
		{
			btHingeConstraint_setAxis(Native, ref axisInA);
		}

		public void SetFramesRef(ref Matrix frameA, ref Matrix frameB)
		{
			btHingeConstraint_setFrames(Native, ref frameA, ref frameB);
		}

		public void SetFrames(Matrix frameA, Matrix frameB)
		{
			btHingeConstraint_setFrames(Native, ref frameA, ref frameB);
		}

		public void SetLimit(Scalar low, Scalar high)
		{
			btHingeConstraint_setLimit(Native, low, high);
		}

		public void SetLimit(Scalar low, Scalar high, Scalar softness)
		{
			btHingeConstraint_setLimit2(Native, low, high, softness);
		}

		public void SetLimit(Scalar low, Scalar high, Scalar softness, Scalar biasFactor)
		{
			btHingeConstraint_setLimit3(Native, low, high, softness, biasFactor);
		}

		public void SetLimit(Scalar low, Scalar high, Scalar softness, Scalar biasFactor,
			Scalar relaxationFactor)
		{
			btHingeConstraint_setLimit4(Native, low, high, softness, biasFactor,
				relaxationFactor);
		}

		public void SetMotorTarget(Scalar targetAngle, Scalar deltaTime)
		{
			btHingeConstraint_setMotorTarget(Native, targetAngle, deltaTime);
		}

		public void SetMotorTargetRef(ref Quaternion qAinB, Scalar deltaTime)
		{
			btHingeConstraint_setMotorTarget2(Native, ref qAinB, deltaTime);
		}

		public void SetMotorTarget(Quaternion qAinB, Scalar deltaTime)
		{
			btHingeConstraint_setMotorTarget2(Native, ref qAinB, deltaTime);
		}

		public void TestLimitRef(ref Matrix transA, ref Matrix transB)
		{
			btHingeConstraint_testLimit(Native, ref transA, ref transB);
		}

		public void TestLimit(Matrix transA, Matrix transB)
		{
			btHingeConstraint_testLimit(Native, ref transA, ref transB);
		}

		public void UpdateRhs(Scalar timeStep)
		{
			btHingeConstraint_updateRHS(Native, timeStep);
		}

		public Matrix AFrame
		{
			get
			{
				Matrix value;
				btHingeConstraint_getAFrame(Native, out value);
				return value;
			}
		}

		public bool AngularOnly
		{
			get => btHingeConstraint_getAngularOnly(Native);
			set => btHingeConstraint_setAngularOnly(Native, value);
		}

		public Matrix BFrame
		{
			get
			{
				Matrix value;
				btHingeConstraint_getBFrame(Native, out value);
				return value;
			}
		}

		public bool EnableMotor
		{
			get => btHingeConstraint_getEnableAngularMotor(Native);
			set => btHingeConstraint_enableMotor(Native, value);
		}

		public HingeFlags Flags => btHingeConstraint_getFlags(Native);

		public Matrix FrameOffsetA
		{
			get
			{
				Matrix value;
				btHingeConstraint_getFrameOffsetA(Native, out value);
				return value;
			}
		}

		public Matrix FrameOffsetB
		{
			get
			{
				Matrix value;
				btHingeConstraint_getFrameOffsetB(Native, out value);
				return value;
			}
		}

		public bool HasLimit => btHingeConstraint_hasLimit(Native);

		public Scalar HingeAngle => btHingeConstraint_getHingeAngle2(Native);

		public Scalar LimitBiasFactor => btHingeConstraint_getLimitBiasFactor(Native);

		public Scalar LimitRelaxationFactor => btHingeConstraint_getLimitRelaxationFactor(Native);

		public Scalar LimitSign => btHingeConstraint_getLimitSign(Native);

		public Scalar LimitSoftness => btHingeConstraint_getLimitSoftness(Native);

		public Scalar LowerLimit => btHingeConstraint_getLowerLimit(Native);

		public Scalar MaxMotorImpulse
		{
			get => btHingeConstraint_getMaxMotorImpulse(Native);
			set => btHingeConstraint_setMaxMotorImpulse(Native, value);
		}

		public Scalar MotorTargetVelocity => btHingeConstraint_getMotorTargetVelocity(Native);

		public int SolveLimit => btHingeConstraint_getSolveLimit(Native);

		public Scalar UpperLimit => btHingeConstraint_getUpperLimit(Native);

		public bool UseFrameOffset
		{
			get => btHingeConstraint_getUseFrameOffset(Native);
			set => btHingeConstraint_setUseFrameOffset(Native, value);
		}

		public bool UseReferenceFrameA
		{
			get => btHingeConstraint_getUseReferenceFrameA(Native);
			set => btHingeConstraint_setUseReferenceFrameA(Native, value);
		}
	}

	public class HingeAccumulatedAngleConstraint : HingeConstraint
	{
		public HingeAccumulatedAngleConstraint(RigidBody rigidBodyA, RigidBody rigidBodyB,
			Vector3 pivotInA, Vector3 pivotInB, Vector3 axisInA, Vector3 axisInB, bool useReferenceFrameA = false)
			: base(btHingeAccumulatedAngleConstraint_new(rigidBodyA.Native, rigidBodyB.Native,
				ref pivotInA, ref pivotInB, ref axisInA, ref axisInB, useReferenceFrameA))
		{
			_rigidBodyA = rigidBodyA;
			_rigidBodyB = rigidBodyB;
		}

		public HingeAccumulatedAngleConstraint(RigidBody rigidBodyA, Vector3 pivotInA,
			Vector3 axisInA, bool useReferenceFrameA = false)
			: base(btHingeAccumulatedAngleConstraint_new2(rigidBodyA.Native, ref pivotInA,
				ref axisInA, useReferenceFrameA))
		{
			_rigidBodyA = rigidBodyA;
			_rigidBodyB = GetFixedBody();
		}

		public HingeAccumulatedAngleConstraint(RigidBody rigidBodyA, RigidBody rigidBodyB,
			Matrix rigidBodyAFrame, Matrix rigidBodyBFrame, bool useReferenceFrameA = false)
			: base(btHingeAccumulatedAngleConstraint_new3(rigidBodyA.Native, rigidBodyB.Native,
				ref rigidBodyAFrame, ref rigidBodyBFrame, useReferenceFrameA))
		{
			_rigidBodyA = rigidBodyA;
			_rigidBodyB = rigidBodyB;
		}

		public HingeAccumulatedAngleConstraint(RigidBody rigidBodyA, Matrix rigidBodyAFrame,
			bool useReferenceFrameA = false)
			: base(btHingeAccumulatedAngleConstraint_new4(rigidBodyA.Native, ref rigidBodyAFrame,
				useReferenceFrameA))
		{
			_rigidBodyA = rigidBodyA;
			_rigidBodyB = GetFixedBody();
		}

		public Scalar AccumulatedHingeAngle
		{
			get => btHingeAccumulatedAngleConstraint_getAccumulatedHingeAngle(Native);
			set => btHingeAccumulatedAngleConstraint_setAccumulatedHingeAngle(Native, value);
		}
	}

	[StructLayout(LayoutKind.Sequential)]
	internal struct HingeConstraintFloatData
	{
		public TypedConstraintFloatData TypedConstraintData;
		public TransformFloatData RigidBodyAFrame;
		public TransformFloatData RigidBodyBFrame;
		public int UseReferenceFrameA;
		public int AngularOnly;
		public int EnableAngularMotor;
		public Scalar MotorTargetVelocity;
		public Scalar MaxMotorImpulse;
		public Scalar LowerLimit;
		public Scalar UpperLimit;
		public Scalar LimitSoftness;
		public Scalar BiasFactor;
		public Scalar RelaxationFactor;

		public static int Offset(string fieldName) { return Marshal.OffsetOf(typeof(HingeConstraintFloatData), fieldName).ToInt32(); }
	}

	[StructLayout(LayoutKind.Sequential)]
	internal struct HingeConstraintDoubleData
	{
		public TypedConstraintDoubleData TypedConstraintData;
		public TransformDoubleData RigidBodyAFrame;
		public TransformDoubleData RigidBodyBFrame;
		public int UseReferenceFrameA;
		public int AngularOnly;
		public int EnableAngularMotor;
		public double MotorTargetVelocity;
		public double MaxMotorImpulse;
		public double LowerLimit;
		public double UpperLimit;
		public double LimitSoftness;
		public double BiasFactor;
		public double RelaxationFactor;

		public static int Offset(string fieldName) { return Marshal.OffsetOf(typeof(HingeConstraintDoubleData), fieldName).ToInt32(); }
	}
}
