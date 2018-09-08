/*
using System;
using System.Runtime.InteropServices;
using System.Security;

#if BT_USE_DOUBLE_PRECISION
using Scalar = System.Double;
#else
using Scalar = System.Single;
#endif


namespace BulletSharp
{
	public abstract class ContactConstraint : TypedConstraint
	{
		public PersistentManifold ContactManifold
		{
            get { return new PersistentManifold(btContactConstraint_getContactManifold(_native), true); }
			set { btContactConstraint_setContactManifold(_native, value._native); }
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btContactConstraint_getContactManifold(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btContactConstraint_setContactManifold(IntPtr obj, IntPtr contactManifold);
	}
}
*/