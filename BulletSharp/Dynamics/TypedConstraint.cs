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
	public enum ConstraintParam
	{
		Erp = 1,
		StopErp,
		Cfm,
		StopCfm
	}

	public enum TypedConstraintType
	{
		Point2Point = 3,
		Hinge,
		ConeTwist,
		D6,
		Slider,
		Contact,
		D6Spring,
		Gear,
		Fixed,
		D6Spring2,
		Max
	}

	public class JointFeedback : IDisposable
	{
		internal IntPtr Native;

		public JointFeedback()
		{
			Native = btJointFeedback_new();
		}

		public Vector3 AppliedForceBodyA
		{
			get
			{
				Vector3 value;
				btJointFeedback_getAppliedForceBodyA(Native, out value);
				return value;
			}
			set => btJointFeedback_setAppliedForceBodyA(Native, ref value);
		}

		public Vector3 AppliedForceBodyB
		{
			get
			{
				Vector3 value;
				btJointFeedback_getAppliedForceBodyB(Native, out value);
				return value;
			}
			set => btJointFeedback_setAppliedForceBodyB(Native, ref value);
		}

		public Vector3 AppliedTorqueBodyA
		{
			get
			{
				Vector3 value;
				btJointFeedback_getAppliedTorqueBodyA(Native, out value);
				return value;
			}
			set => btJointFeedback_setAppliedTorqueBodyA(Native, ref value);
		}

		public Vector3 AppliedTorqueBodyB
		{
			get
			{
				Vector3 value;
				btJointFeedback_getAppliedTorqueBodyB(Native, out value);
				return value;
			}
			set => btJointFeedback_setAppliedTorqueBodyB(Native, ref value);
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (Native != IntPtr.Zero)
			{
				btJointFeedback_delete(Native);
				Native = IntPtr.Zero;
			}
		}

		~JointFeedback()
		{
			Dispose(false);
		}
	}

	public abstract class TypedConstraint : IDisposable
	{
		public class ConstraintInfo1 : IDisposable
		{
			internal IntPtr _native;

			public ConstraintInfo1()
			{
				_native = btTypedConstraint_btConstraintInfo1_new();
			}

			public int Nub
			{
				get => btTypedConstraint_btConstraintInfo1_getNub(_native);
				set => btTypedConstraint_btConstraintInfo1_setNub(_native, value);
			}

			public int NumConstraintRows
			{
				get => btTypedConstraint_btConstraintInfo1_getNumConstraintRows(_native);
				set => btTypedConstraint_btConstraintInfo1_setNumConstraintRows(_native, value);
			}

			public void Dispose()
			{
				Dispose(true);
				GC.SuppressFinalize(this);
			}

			protected virtual void Dispose(bool disposing)
			{
				if (_native != IntPtr.Zero)
				{
					btTypedConstraint_btConstraintInfo1_delete(_native);
					_native = IntPtr.Zero;
				}
			}

			~ConstraintInfo1()
			{
				Dispose(false);
			}
		}

		public class ConstraintInfo2 : IDisposable
		{
			internal IntPtr _native;

			public ConstraintInfo2()
			{
				_native = btTypedConstraint_btConstraintInfo2_new();
			}
			/*
			public Scalar Cfm
			{
				get { return btTypedConstraint_btConstraintInfo2_getCfm(_native); }
				set { btTypedConstraint_btConstraintInfo2_setCfm(_native, value._native); }
			}

			public Scalar ConstraintError
			{
				get { return btTypedConstraint_btConstraintInfo2_getConstraintError(_native); }
				set { btTypedConstraint_btConstraintInfo2_setConstraintError(_native, value._native); }
			}
			*/
			public Scalar Damping
			{
				get => btTypedConstraint_btConstraintInfo2_getDamping(_native);
				set => btTypedConstraint_btConstraintInfo2_setDamping(_native, value);
			}

			public Scalar Erp
			{
				get => btTypedConstraint_btConstraintInfo2_getErp(_native);
				set => btTypedConstraint_btConstraintInfo2_setErp(_native, value);
			}
			/*
			public int Findex
			{
				get { return btTypedConstraint_btConstraintInfo2_getFindex(_native); }
				set { btTypedConstraint_btConstraintInfo2_setFindex(_native, value._native); }
			}
			*/
			public Scalar Fps
			{
				get => btTypedConstraint_btConstraintInfo2_getFps(_native);
				set => btTypedConstraint_btConstraintInfo2_setFps(_native, value);
			}
			/*
			public Scalar J1angularAxis
			{
				get { return btTypedConstraint_btConstraintInfo2_getJ1angularAxis(_native); }
				set { btTypedConstraint_btConstraintInfo2_setJ1angularAxis(_native, value._native); }
			}

			public Scalar J1linearAxis
			{
				get { return btTypedConstraint_btConstraintInfo2_getJ1linearAxis(_native); }
				set { btTypedConstraint_btConstraintInfo2_setJ1linearAxis(_native, value._native); }
			}

			public Scalar J2angularAxis
			{
				get { return btTypedConstraint_btConstraintInfo2_getJ2angularAxis(_native); }
				set { btTypedConstraint_btConstraintInfo2_setJ2angularAxis(_native, value._native); }
			}

			public Scalar J2linearAxis
			{
				get { return btTypedConstraint_btConstraintInfo2_getJ2linearAxis(_native); }
				set { btTypedConstraint_btConstraintInfo2_setJ2linearAxis(_native, value._native); }
			}

			public Scalar LowerLimit
			{
				get { return btTypedConstraint_btConstraintInfo2_getLowerLimit(_native); }
				set { btTypedConstraint_btConstraintInfo2_setLowerLimit(_native, value._native); }
			}
			*/
			public int NumIterations
			{
				get => btTypedConstraint_btConstraintInfo2_getNumIterations(_native);
				set => btTypedConstraint_btConstraintInfo2_setNumIterations(_native, value);
			}

			public int Rowskip
			{
				get => btTypedConstraint_btConstraintInfo2_getRowskip(_native);
				set => btTypedConstraint_btConstraintInfo2_setRowskip(_native, value);
			}
			/*
			public Scalar UpperLimit
			{
				get { return btTypedConstraint_btConstraintInfo2_getUpperLimit(_native); }
				set { btTypedConstraint_btConstraintInfo2_setUpperLimit(_native, value._native); }
			}
			*/
			public void Dispose()
			{
				Dispose(true);
				GC.SuppressFinalize(this);
			}

			protected virtual void Dispose(bool disposing)
			{
				if (_native != IntPtr.Zero)
				{
					btTypedConstraint_btConstraintInfo2_delete(_native);
					_native = IntPtr.Zero;
				}
			}

			~ConstraintInfo2()
			{
				Dispose(false);
			}
		}

		internal IntPtr Native;

		private JointFeedback _jointFeedback;
		protected RigidBody _rigidBodyA;
		protected RigidBody _rigidBodyB;

		private static RigidBody _fixedBody;

		internal TypedConstraint(IntPtr native)
		{
			Native = native;
		}

		public void BuildJacobian()
		{
			btTypedConstraint_buildJacobian(Native);
		}

		public int CalculateSerializeBufferSize()
		{
			return btTypedConstraint_calculateSerializeBufferSize(Native);
		}

		public void EnableFeedback(bool needsFeedback)
		{
			btTypedConstraint_enableFeedback(Native, needsFeedback);
		}

		public static RigidBody GetFixedBody()
		{
			if (_fixedBody == null)
			{
				using (var cinfo = new RigidBodyConstructionInfo(0, null, null))
				{
					_fixedBody = new RigidBody(cinfo);
					_fixedBody.SetMassProps(0, Vector3.Zero);
				}
			}
			return _fixedBody;
		}

		public void GetInfo1(ConstraintInfo1 info)
		{
			btTypedConstraint_getInfo1(Native, info._native);
		}

		public void GetInfo2(ConstraintInfo2 info)
		{
			btTypedConstraint_getInfo2(Native, info._native);
		}

		public Scalar GetParam(ConstraintParam num)
		{
			return btTypedConstraint_getParam(Native, num);
		}

		public Scalar GetParam(ConstraintParam num, int axis)
		{
			return btTypedConstraint_getParam2(Native, num, axis);
		}

		public Scalar InternalGetAppliedImpulse()
		{
			return btTypedConstraint_internalGetAppliedImpulse(Native);
		}

		public void InternalSetAppliedImpulse(Scalar appliedImpulse)
		{
			btTypedConstraint_internalSetAppliedImpulse(Native, appliedImpulse);
		}

		public string Serialize(IntPtr dataBuffer, Serializer serializer)
		{
			return Marshal.PtrToStringAnsi(btTypedConstraint_serialize(Native, dataBuffer, serializer._native));
		}

		public void SetParam(ConstraintParam num, Scalar value)
		{
			btTypedConstraint_setParam(Native, num, value);
		}

		public void SetParam(ConstraintParam num, Scalar value, int axis)
		{
			btTypedConstraint_setParam2(Native, num, value, axis);
		}
		/*
		public void SetupSolverConstraint(btAlignedObjectArray<btSolverConstraint> ca,
			int solverBodyA, int solverBodyB, Scalar timeStep)
		{
			btTypedConstraint_setupSolverConstraint(_native, ca._native, solverBodyA,
				solverBodyB, timeStep);
		}

		public void SolveConstraintObsolete(SolverBody __unnamed0, SolverBody __unnamed1,
			Scalar __unnamed2)
		{
			btTypedConstraint_solveConstraintObsolete(_native, __unnamed0._native,
				__unnamed1._native, __unnamed2);
		}
		*/
		public Scalar AppliedImpulse => btTypedConstraint_getAppliedImpulse(Native);

		public Scalar BreakingImpulseThreshold
		{
			get => btTypedConstraint_getBreakingImpulseThreshold(Native);
			set => btTypedConstraint_setBreakingImpulseThreshold(Native, value);
		}

		public TypedConstraintType ConstraintType => btTypedConstraint_getConstraintType(Native);

		public Scalar DebugDrawSize
		{
			get => btTypedConstraint_getDbgDrawSize(Native);
			set => btTypedConstraint_setDbgDrawSize(Native, value);
		}

		public bool IsEnabled
		{
			get => btTypedConstraint_isEnabled(Native);
			set => btTypedConstraint_setEnabled(Native, value);
		}

		public JointFeedback JointFeedback
		{
			get => _jointFeedback;
			set
			{
				btTypedConstraint_setJointFeedback(Native, (value == null) ? value.Native : IntPtr.Zero);
				_jointFeedback = value;
			}
		}

		public bool NeedsFeedback => btTypedConstraint_needsFeedback(Native);

		public int OverrideNumSolverIterations
		{
			get => btTypedConstraint_getOverrideNumSolverIterations(Native);
			set => btTypedConstraint_setOverrideNumSolverIterations(Native, value);
		}

		public RigidBody RigidBodyA => _rigidBodyA;

		public RigidBody RigidBodyB => _rigidBodyB;

		public int Uid => btTypedConstraint_getUid(Native);

		public int UserConstraintId
		{
			get => btTypedConstraint_getUserConstraintId(Native);
			set => btTypedConstraint_setUserConstraintId(Native, value);
		}

		public Object Userobject { get; set; }

		public int UserConstraintType
		{
			get => btTypedConstraint_getUserConstraintType(Native);
			set => btTypedConstraint_setUserConstraintType(Native, value);
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}


		protected virtual void Dispose(bool disposing)
		{
			if (Native != IntPtr.Zero)
			{
				btTypedConstraint_delete(Native);
				Native = IntPtr.Zero;
			}
		}

		~TypedConstraint()
		{
			Dispose(false);
		}
	}

	public class AngularLimit : IDisposable
	{
		internal IntPtr Native;
		private bool _preventDelete;

		internal AngularLimit(IntPtr native, bool preventDelete)
		{
			Native = native;
			_preventDelete = preventDelete;
		}

		public AngularLimit()
		{
			Native = btAngularLimit_new();
		}

		public void Fit(ref Scalar angle)
		{
			btAngularLimit_fit(Native, ref angle);
		}

		public void Set(Scalar low, Scalar high, Scalar softness = 0.9f, Scalar biasFactor = 0.3f,
			Scalar relaxationFactor = 1.0f)
		{
			btAngularLimit_set(Native, low, high, softness, biasFactor, relaxationFactor);
		}

		public void Test(Scalar angle)
		{
			btAngularLimit_test(Native, angle);
		}

		public Scalar BiasFactor => btAngularLimit_getBiasFactor(Native);

		public Scalar Correction => btAngularLimit_getCorrection(Native);

		public Scalar Error => btAngularLimit_getError(Native);

		public Scalar HalfRange => btAngularLimit_getHalfRange(Native);

		public Scalar High => btAngularLimit_getHigh(Native);

		public bool IsLimit => btAngularLimit_isLimit(Native);

		public Scalar Low => btAngularLimit_getLow(Native);

		public Scalar RelaxationFactor => btAngularLimit_getRelaxationFactor(Native);

		public Scalar Sign => btAngularLimit_getSign(Native);

		public Scalar Softness => btAngularLimit_getSoftness(Native);

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (Native != IntPtr.Zero)
			{
				if (!_preventDelete)
				{
					btAngularLimit_delete(Native);
				}
				Native = IntPtr.Zero;
			}
		}

		~AngularLimit()
		{
			Dispose(false);
		}
	}

	[StructLayout(LayoutKind.Sequential)]
	internal struct TypedConstraintFloatData
	{
		public IntPtr RigidBodyA;
		public IntPtr RigidBodyB;
		public IntPtr Name;
		public int ObjectType;
		public int UserConstraintType;
		public int UserConstraintId;
		public int NeedsFeedback;
		public Scalar AppliedImpulse;
		public Scalar DebugDrawSize;
		public int DisableCollisionsBetweenLinkedBodies;
		public int OverrideNumSolverIterations;
		public Scalar BreakingImpulseThreshold;
		public int IsEnabled;

		public static int Offset(string fieldName) { return Marshal.OffsetOf(typeof(TypedConstraintFloatData), fieldName).ToInt32(); }
	}

	[StructLayout(LayoutKind.Sequential)]
	internal struct TypedConstraintDoubleData
	{
		public IntPtr RigidBodyA;
		public IntPtr RigidBodyB;
		public IntPtr Name;
		public int ObjectType;
		public int UserConstraintType;
		public int UserConstraintId;
		public int NeedsFeedback;
		public double AppliedImpulse;
		public double DebugDrawSize;
		public int DisableCollisionsBetweenLinkedBodies;
		public int OverrideNumSolverIterations;
		public double BreakingImpulseThreshold;
		public int IsEnabled;

		public static int Offset(string fieldName) { return Marshal.OffsetOf(typeof(TypedConstraintDoubleData), fieldName).ToInt32(); }
	}
}
