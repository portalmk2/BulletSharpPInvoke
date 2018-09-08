using System;
using System.Collections.Generic;
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
	public enum RigidBodyFlags
	{
		None = 0,
		DisableWorldGravity = 1,
		EnableGyroscopicForceExplicit = 2,
		EnableGyroscopicForceImplicitWorld = 4,
		EnableGyroscopicForceImplicitBody = 8,
		EnableGyroscopicForce = EnableGyroscopicForceImplicitBody
	}

	public class RigidBody : CollisionObject
	{
		private MotionState _motionState;
		internal List<TypedConstraint> _constraintRefs;

		public RigidBody(RigidBodyConstructionInfo constructionInfo)
			: base(btRigidBody_new(constructionInfo.Native))
		{
			_collisionShape = constructionInfo.CollisionShape;
			_motionState = constructionInfo.MotionState;
		}

		public void AddConstraintRef(TypedConstraint constraint)
		{
			if (_constraintRefs == null)
			{
				_constraintRefs = new List<TypedConstraint>();
			}
			_constraintRefs.Add(constraint);
			btRigidBody_addConstraintRef(Native, constraint.Native);
		}

		public void ApplyCentralForceRef(ref Vector3 force)
		{
			btRigidBody_applyCentralForce(Native, ref force);
		}

		public void ApplyCentralForce(Vector3 force)
		{
			btRigidBody_applyCentralForce(Native, ref force);
		}

		public void ApplyCentralImpulseRef(ref Vector3 impulse)
		{
			btRigidBody_applyCentralImpulse(Native, ref impulse);
		}

		public void ApplyCentralImpulse(Vector3 impulse)
		{
			btRigidBody_applyCentralImpulse(Native, ref impulse);
		}

		public void ApplyDamping(Scalar timeStep)
		{
			btRigidBody_applyDamping(Native, timeStep);
		}

		public void ApplyForceRef(ref Vector3 force, ref Vector3 relPos)
		{
			btRigidBody_applyForce(Native, ref force, ref relPos);
		}

		public void ApplyForce(Vector3 force, Vector3 relPos)
		{
			btRigidBody_applyForce(Native, ref force, ref relPos);
		}

		public void ApplyGravity()
		{
			btRigidBody_applyGravity(Native);
		}

		public void ApplyImpulseRef(ref Vector3 impulse, ref Vector3 relPos)
		{
			btRigidBody_applyImpulse(Native, ref impulse, ref relPos);
		}

		public void ApplyImpulse(Vector3 impulse, Vector3 relPos)
		{
			btRigidBody_applyImpulse(Native, ref impulse, ref relPos);
		}

		public void ApplyTorqueRef(ref Vector3 torque)
		{
			btRigidBody_applyTorque(Native, ref torque);
		}

		public void ApplyTorque(Vector3 torque)
		{
			btRigidBody_applyTorque(Native, ref torque);
		}

		public void ApplyTorqueImpulseRef(ref Vector3 torque)
		{
			btRigidBody_applyTorqueImpulse(Native, ref torque);
		}

		public void ApplyTorqueImpulse(Vector3 torque)
		{
			btRigidBody_applyTorqueImpulse(Native, ref torque);
		}

		public void ClearForces()
		{
			btRigidBody_clearForces(Native);
		}

		public void ComputeAngularImpulseDenominator(ref Vector3 axis, out Scalar result)
		{
			result = btRigidBody_computeAngularImpulseDenominator(Native, ref axis);
		}

		public Scalar ComputeAngularImpulseDenominator(Vector3 axis)
		{
			return btRigidBody_computeAngularImpulseDenominator(Native, ref axis);
		}

		public Vector3 ComputeGyroscopicForceExplicit(Scalar maxGyroscopicForce)
		{
			Vector3 value;
			btRigidBody_computeGyroscopicForceExplicit(Native, maxGyroscopicForce,
				out value);
			return value;
		}

		public Vector3 ComputeGyroscopicImpulseImplicitBody(Scalar step)
		{
			Vector3 value;
			btRigidBody_computeGyroscopicImpulseImplicit_Body(Native, step, out value);
			return value;
		}

		public Vector3 ComputeGyroscopicImpulseImplicitWorld(Scalar deltaTime)
		{
			Vector3 value;
			btRigidBody_computeGyroscopicImpulseImplicit_World(Native, deltaTime,
				out value);
			return value;
		}

		public Scalar ComputeImpulseDenominator(Vector3 pos, Vector3 normal)
		{
			return btRigidBody_computeImpulseDenominator(Native, ref pos, ref normal);
		}

		public void GetAabb(out Vector3 aabbMin, out Vector3 aabbMax)
		{
			btRigidBody_getAabb(Native, out aabbMin, out aabbMax);
		}

		public TypedConstraint GetConstraintRef(int index)
		{
			System.Diagnostics.Debug.Assert(_constraintRefs != null);
			return _constraintRefs[index];
		}

		public void GetVelocityInLocalPoint(ref Vector3 relPos, out Vector3 value)
		{
			btRigidBody_getVelocityInLocalPoint(Native, ref relPos, out value);
		}

		public Vector3 GetVelocityInLocalPoint(Vector3 relPos)
		{
			Vector3 value;
			btRigidBody_getVelocityInLocalPoint(Native, ref relPos, out value);
			return value;
		}

		public void IntegrateVelocities(Scalar step)
		{
			btRigidBody_integrateVelocities(Native, step);
		}

		public void PredictIntegratedTransform(Scalar step, out Matrix predictedTransform)
		{
			btRigidBody_predictIntegratedTransform(Native, step, out predictedTransform);
		}

		public void ProceedToTransformRef(ref Matrix newTrans)
		{
			btRigidBody_proceedToTransform(Native, ref newTrans);
		}

		public void ProceedToTransform(Matrix newTrans)
		{
			btRigidBody_proceedToTransform(Native, ref newTrans);
		}

		public void RemoveConstraintRef(TypedConstraint constraint)
		{
			if (_constraintRefs != null)
			{
				_constraintRefs.Remove(constraint);
				btRigidBody_removeConstraintRef(Native, constraint.Native);
			}
		}

		public void SaveKinematicState(Scalar step)
		{
			btRigidBody_saveKinematicState(Native, step);
		}

		public void SetDamping(Scalar linDamping, Scalar angDamping)
		{
			btRigidBody_setDamping(Native, linDamping, angDamping);
		}

		public void SetMassPropsRef(Scalar mass, ref Vector3 inertia)
		{
			btRigidBody_setMassProps(Native, mass, ref inertia);
		}

		public void SetMassProps(Scalar mass, Vector3 inertia)
		{
			btRigidBody_setMassProps(Native, mass, ref inertia);
		}

		public void SetNewBroadphaseProxy(BroadphaseProxy broadphaseProxy)
		{
			btRigidBody_setNewBroadphaseProxy(Native, broadphaseProxy.Native);
		}

		public void SetSleepingThresholds(Scalar linear, Scalar angular)
		{
			btRigidBody_setSleepingThresholds(Native, linear, angular);
		}

		public void TranslateRef(ref Vector3 v)
		{
			btRigidBody_translate(Native, ref v);
		}

		public void Translate(Vector3 v)
		{
			btRigidBody_translate(Native, ref v);
		}

		public static RigidBody Upcast(CollisionObject colObj)
		{
			return GetManaged(btRigidBody_upcast(colObj.Native)) as RigidBody;
		}

		public void UpdateDeactivation(Scalar timeStep)
		{
			btRigidBody_updateDeactivation(Native, timeStep);
		}

		public void UpdateInertiaTensor()
		{
			btRigidBody_updateInertiaTensor(Native);
		}

		public bool WantsSleeping()
		{
			return btRigidBody_wantsSleeping(Native);
		}

		public Scalar AngularDamping => btRigidBody_getAngularDamping(Native);

		public Vector3 AngularFactor
		{
			get
			{
				Vector3 value;
				btRigidBody_getAngularFactor(Native, out value);
				return value;
			}
			set => btRigidBody_setAngularFactor(Native, ref value);
		}

		public Scalar AngularSleepingThreshold => btRigidBody_getAngularSleepingThreshold(Native);

		public Vector3 AngularVelocity
		{
			get
			{
				Vector3 value;
				btRigidBody_getAngularVelocity(Native, out value);
				return value;
			}
			set => btRigidBody_setAngularVelocity(Native, ref value);
		}

		public BroadphaseProxy BroadphaseProxy => BroadphaseProxy.GetManaged(btRigidBody_getBroadphaseProxy(Native));

		public Vector3 CenterOfMassPosition
		{
			get
			{
				Vector3 value;
				btRigidBody_getCenterOfMassPosition(Native, out value);
				return value;
			}
		}

		public Matrix CenterOfMassTransform
		{
			get
			{
				Matrix value;
				btRigidBody_getCenterOfMassTransform(Native, out value);
				return value;
			}
			set => btRigidBody_setCenterOfMassTransform(Native, ref value);
		}

		public int ContactSolverType
		{
			get => btRigidBody_getContactSolverType(Native);
			set => btRigidBody_setContactSolverType(Native, value);
		}

		public RigidBodyFlags Flags
		{
			get => btRigidBody_getFlags(Native);
			set => btRigidBody_setFlags(Native, value);
		}

		public int FrictionSolverType
		{
			get => btRigidBody_getFrictionSolverType(Native);
			set => btRigidBody_setFrictionSolverType(Native, value);
		}

		public Vector3 Gravity
		{
			get
			{
				Vector3 value;
				btRigidBody_getGravity(Native, out value);
				return value;
			}
			set => btRigidBody_setGravity(Native, ref value);
		}

		public Vector3 InvInertiaDiagLocal
		{
			get
			{
				Vector3 value;
				btRigidBody_getInvInertiaDiagLocal(Native, out value);
				return value;
			}
			set => btRigidBody_setInvInertiaDiagLocal(Native, ref value);
		}

		public Matrix InvInertiaTensorWorld
		{
			get
			{
				Matrix value;
				btRigidBody_getInvInertiaTensorWorld(Native, out value);
				return value;
			}
		}

		public Scalar InvMass => btRigidBody_getInvMass(Native);

		public bool IsInWorld => btRigidBody_isInWorld(Native);

		public Scalar LinearDamping => btRigidBody_getLinearDamping(Native);

		public Vector3 LinearFactor
		{
			get
			{
				Vector3 value;
				btRigidBody_getLinearFactor(Native, out value);
				return value;
			}
			set => btRigidBody_setLinearFactor(Native, ref value);
		}

		public Scalar LinearSleepingThreshold => btRigidBody_getLinearSleepingThreshold(Native);

		public Vector3 LinearVelocity
		{
			get
			{
				Vector3 value;
				btRigidBody_getLinearVelocity(Native, out value);
				return value;
			}
			set => btRigidBody_setLinearVelocity(Native, ref value);
		}

		public Vector3 LocalInertia
		{
			get
			{
				Vector3 value;
				btRigidBody_getLocalInertia(Native, out value);
				return value;
			}
		}

		public MotionState MotionState
		{
			get => _motionState;
			set
			{
				btRigidBody_setMotionState(Native, (value != null) ? value._native : IntPtr.Zero);
				_motionState = value;
			}
		}

		public int NumConstraintRefs => (_constraintRefs != null) ? _constraintRefs.Count : 0;

		public Quaternion Orientation
		{
			get
			{
				Quaternion value;
				btRigidBody_getOrientation(Native, out value);
				return value;
			}
		}

		public Vector3 TotalForce
		{
			get
			{
				Vector3 value;
				btRigidBody_getTotalForce(Native, out value);
				return value;
			}
		}

		public Vector3 TotalTorque
		{
			get
			{
				Vector3 value;
				btRigidBody_getTotalTorque(Native, out value);
				return value;
			}
		}
	}

	[StructLayout(LayoutKind.Sequential)]
	internal struct RigidBodyFloatData
	{
		public CollisionObjectFloatData CollisionObjectData;
		public Matrix3x3FloatData InvInertiaTensorWorld;
		public Vector3FloatData LinearVelocity;
		public Vector3FloatData AngularVelocity;
		public Vector3FloatData AngularFactor;
		public Vector3FloatData LinearFactor;
		public Vector3FloatData Gravity;
		public Vector3FloatData GravityAcceleration;
		public Vector3FloatData InvInertiaLocal;
		public Vector3FloatData TotalForce;
		public Vector3FloatData TotalTorque;
		public Scalar InverseMass;
		public Scalar LinearDamping;
		public Scalar AngularDamping;
		public Scalar AdditionalDampingFactor;
		public Scalar AdditionalLinearDampingThresholdSqr;
		public Scalar AdditionalAngularDampingThresholdSqr;
		public Scalar AdditionalAngularDampingFactor;
		public Scalar LinearSleepingThreshold;
		public Scalar AngularSleepingThreshold;
		public int AdditionalDamping;
		//public int Padding;

		public static int Offset(string fieldName) { return Marshal.OffsetOf(typeof(RigidBodyFloatData), fieldName).ToInt32(); }
	}

	[StructLayout(LayoutKind.Sequential)]
	internal struct RigidBodyDoubleData
	{
		public CollisionObjectDoubleData CollisionObjectData;
		public Matrix3x3DoubleData InvInertiaTensorWorld;
		public Vector3DoubleData LinearVelocity;
		public Vector3DoubleData AngularVelocity;
		public Vector3DoubleData AngularFactor;
		public Vector3DoubleData LinearFactor;
		public Vector3DoubleData Gravity;
		public Vector3DoubleData GravityAcceleration;
		public Vector3DoubleData InvInertiaLocal;
		public Vector3DoubleData TotalForce;
		public Vector3DoubleData TotalTorque;
		public double InverseMass;
		public double LinearDamping;
		public double AngularDamping;
		public double AdditionalDampingFactor;
		public double AdditionalLinearDampingThresholdSqr;
		public double AdditionalAngularDampingThresholdSqr;
		public double AdditionalAngularDampingFactor;
		public double LinearSleepingThreshold;
		public double AngularSleepingThreshold;
		public int AdditionalDamping;
		//public int Padding;

		public static int Offset(string fieldName) { return Marshal.OffsetOf(typeof(RigidBodyDoubleData), fieldName).ToInt32(); }
	}
}
