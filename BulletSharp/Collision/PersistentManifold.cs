using System;
using System.Runtime.InteropServices;
using System.Security;
using BulletSharp.Math;
using static BulletSharp.UnsafeNativeMethods;

#if BT_USE_DOUBLE_PRECISION
using Scalar = System.Double;
#else
using Scalar = System.Single;
#endif


namespace BulletSharp
{
    public delegate void ContactDestroyedEventHandler(Object userPersistantData);
    public delegate void ContactProcessedEventHandler(ManifoldPoint cp, CollisionObject body0, CollisionObject body1);

    public class PersistentManifold : IDisposable //: TypedObject
    {
        internal IntPtr Native;
        private bool _preventDelete;

        private static ContactDestroyedEventHandler _contactDestroyed;
        private static ContactProcessedEventHandler _contactProcessed;
        private static ContactDestroyedUnmanagedDelegate _contactDestroyedUnmanaged;
        private static ContactProcessedUnmanagedDelegate _contactProcessedUnmanaged;
        private static IntPtr _contactDestroyedUnmanagedPtr;
        private static IntPtr _contactProcessedUnmanagedPtr;

        [UnmanagedFunctionPointer(BulletSharp.Native.Conv), SuppressUnmanagedCodeSecurity]
        private delegate bool ContactDestroyedUnmanagedDelegate(IntPtr userPersistantData);
        [UnmanagedFunctionPointer(BulletSharp.Native.Conv), SuppressUnmanagedCodeSecurity]
        private delegate bool ContactProcessedUnmanagedDelegate(IntPtr cp, IntPtr body0, IntPtr body1);

        private static bool ContactDestroyedUnmanaged(IntPtr userPersistentData)
        {
            _contactDestroyed.Invoke(GCHandle.FromIntPtr(userPersistentData).Target);
            return false;
        }

        private static bool ContactProcessedUnmanaged(IntPtr cp, IntPtr body0, IntPtr body1)
        {
            _contactProcessed.Invoke(new ManifoldPoint(cp, true), CollisionObject.GetManaged(body0), CollisionObject.GetManaged(body1));
            return false;
        }

        public static event ContactDestroyedEventHandler ContactDestroyed
        {
            add
            {
                if (_contactDestroyedUnmanaged == null)
                {
                    _contactDestroyedUnmanaged = new ContactDestroyedUnmanagedDelegate(ContactDestroyedUnmanaged);
                    _contactDestroyedUnmanagedPtr = Marshal.GetFunctionPointerForDelegate(_contactDestroyedUnmanaged);
                }
                setGContactDestroyedCallback(_contactDestroyedUnmanagedPtr);
                _contactDestroyed += value;
            }
            remove
            {
                _contactDestroyed -= value;
                if (_contactDestroyed == null)
                {
                    setGContactDestroyedCallback(IntPtr.Zero);
                }
            }
        }

        public static event ContactProcessedEventHandler ContactProcessed
        {
            add
            {
                if (_contactProcessedUnmanaged == null)
                {
                    _contactProcessedUnmanaged = new ContactProcessedUnmanagedDelegate(ContactProcessedUnmanaged);
                    _contactProcessedUnmanagedPtr = Marshal.GetFunctionPointerForDelegate(_contactProcessedUnmanaged);
                }
                setGContactProcessedCallback(_contactProcessedUnmanagedPtr);
                _contactProcessed += value;
            }
            remove
            {
                _contactProcessed -= value;
                if (_contactProcessed == null)
                {
                    setGContactProcessedCallback(IntPtr.Zero);
                }
            }
        }

        internal PersistentManifold(IntPtr native, bool preventDelete)
        {
            Native = native;
            _preventDelete = preventDelete;
        }

        public PersistentManifold()
        {
            Native = btPersistentManifold_new();
        }

        public PersistentManifold(CollisionObject body0, CollisionObject body1, int __unnamed2,
            Scalar contactBreakingThreshold, Scalar contactProcessingThreshold)
        {
            Native = btPersistentManifold_new2(body0.Native, body1.Native, __unnamed2,
                contactBreakingThreshold, contactProcessingThreshold);
        }

        public int AddManifoldPoint(ManifoldPoint newPoint, bool isPredictive = false)
        {
            return btPersistentManifold_addManifoldPoint(Native, newPoint.Native,
                isPredictive);
        }

        public void ClearManifold()
        {
            btPersistentManifold_clearManifold(Native);
        }

        public void ClearUserCache(ManifoldPoint pt)
        {
            btPersistentManifold_clearUserCache(Native, pt.Native);
        }

        public int GetCacheEntry(ManifoldPoint newPoint)
        {
            return btPersistentManifold_getCacheEntry(Native, newPoint.Native);
        }

        public ManifoldPoint GetContactPoint(int index)
        {
            return new ManifoldPoint(btPersistentManifold_getContactPoint(Native, index), true);
        }

        public void RefreshContactPointsRef(ref Matrix trA, ref Matrix trB)
        {
            btPersistentManifold_refreshContactPoints(Native, ref trA, ref trB);
        }

        public void RefreshContactPoints(Matrix trA, Matrix trB)
        {
            btPersistentManifold_refreshContactPoints(Native, ref trA, ref trB);
        }

        public void RemoveContactPoint(int index)
        {
            btPersistentManifold_removeContactPoint(Native, index);
        }

        public void ReplaceContactPoint(ManifoldPoint newPoint, int insertIndex)
        {
            btPersistentManifold_replaceContactPoint(Native, newPoint.Native, insertIndex);
        }

        public void SetBodies(CollisionObject body0, CollisionObject body1)
        {
            btPersistentManifold_setBodies(Native, body0.Native, body1.Native);
        }

        public bool ValidContactDistance(ManifoldPoint pt)
        {
            return btPersistentManifold_validContactDistance(Native, pt.Native);
        }

        public CollisionObject Body0 => CollisionObject.GetManaged(btPersistentManifold_getBody0(Native));

        public CollisionObject Body1 => CollisionObject.GetManaged(btPersistentManifold_getBody1(Native));

        public int CompanionIdA
        {
            get => btPersistentManifold_getCompanionIdA(Native);
            set => btPersistentManifold_setCompanionIdA(Native, value);
        }

        public int CompanionIdB
        {
            get => btPersistentManifold_getCompanionIdB(Native);
            set => btPersistentManifold_setCompanionIdB(Native, value);
        }

        public Scalar ContactBreakingThreshold
        {
            get => btPersistentManifold_getContactBreakingThreshold(Native);
            set => btPersistentManifold_setContactBreakingThreshold(Native, value);
        }

        public Scalar ContactProcessingThreshold
        {
            get => btPersistentManifold_getContactProcessingThreshold(Native);
            set => btPersistentManifold_setContactProcessingThreshold(Native, value);
        }

        public int Index1A
        {
            get => btPersistentManifold_getIndex1a(Native);
            set => btPersistentManifold_setIndex1a(Native, value);
        }

        public int NumContacts
        {
            get => btPersistentManifold_getNumContacts(Native);
            set => btPersistentManifold_setNumContacts(Native, value);
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
                if (!_preventDelete)
                {
                    btPersistentManifold_delete(Native);
                }
                Native = IntPtr.Zero;
            }
        }

        ~PersistentManifold()
        {
            Dispose(false);
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct PersistentManifoldDoubleData
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        Vector3DoubleData[] PointCacheLocalPointA;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        Vector3DoubleData[] PointCacheLocalPointB;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        Vector3DoubleData[] PointCachePositionWorldOnA;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        Vector3DoubleData[] PointCachePositionWorldOnB;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        Vector3DoubleData[] PointCacheNormalWorldOnB;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        Vector3DoubleData[] PointCacheLateralFrictionDir1;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        Vector3DoubleData[] PointCacheLateralFrictionDir2;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        double[] PointCacheDistance;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        double[] PointCacheAppliedImpulse;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        double[] PointCacheCombinedFriction;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        double[] PointCacheCombinedRollingFriction;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        double[] PointCacheCombinedSpinningFriction;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        double[] PointCacheCombinedRestitution;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        int[] PointCachePartId0;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        int[] PointCachePartId1;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        int[] PointCacheIndex0;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        int[] PointCacheIndex1;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        int[] PointCacheContactPointFlags;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        double[] PointCacheAppliedImpulseLateral1;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        double[] PointCacheAppliedImpulseLateral2;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        double[] PointCacheContactMotion1;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        double[] PointCacheContactMotion2;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        double[] PointCacheContactCfm;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        double[] PointCacheCombinedContactStiffness1;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        double[] PointCacheContactErp;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        double[] PointCacheCombinedContactDamping1;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        double[] PointCacheFrictionCfm;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        int[] PointCacheLifeTime;
        int NumCachedPoints;
        int CompanionIdA;
        int CompanionIdB;
        int Index1a;
        int ObjectType;
        double ContactBreakingThreshold;
        double ContactProcessingThreshold;
        int Padding;
        IntPtr Body0;
        IntPtr Body1;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct PersistentManifoldFloatData
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        Vector3FloatData[] PointCacheLocalPointA;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        Vector3FloatData[] PointCacheLocalPointB;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        Vector3FloatData[] PointCachePositionWorldOnA;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        Vector3FloatData[] PointCachePositionWorldOnB;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        Vector3FloatData[] PointCacheNormalWorldOnB;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        Vector3FloatData[] PointCacheLateralFrictionDir1;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        Vector3FloatData[] PointCacheLateralFrictionDir2;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        Scalar[] PointCacheDistance;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        Scalar[] PointCacheAppliedImpulse;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        Scalar[] PointCacheCombinedFriction;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        Scalar[] PointCacheCombinedRollingFriction;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        Scalar[] PointCacheCombinedSpinningFriction;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        Scalar[] PointCacheCombinedRestitution;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        int[] PointCachePartId0;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        int[] PointCachePartId1;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        int[] PointCacheIndex0;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        int[] PointCacheIndex1;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        int[] PointCacheContactPointFlags;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        Scalar[] PointCacheAppliedImpulseLateral1;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        Scalar[] PointCacheAppliedImpulseLateral2;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        Scalar[] PointCacheContactMotion1;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        Scalar[] PointCacheContactMotion2;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        Scalar[] PointCacheContactCfm;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        Scalar[] PointCacheCombinedContactStiffness1;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        Scalar[] PointCacheContactErp;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        Scalar[] PointCacheCombinedContactDamping1;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        Scalar[] PointCacheFrictionCfm;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        int[] PointCacheLifeTime;
        int NumCachedPoints;
        int CompanionIdA;
        int CompanionIdB;
        int Index1a;
        int ObjectType;
        Scalar ContactBreakingThreshold;
        Scalar ContactProcessingThreshold;
        int Padding;
        IntPtr Body0;
        IntPtr Body1;
    }
}
