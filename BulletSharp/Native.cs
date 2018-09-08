using System.Runtime.InteropServices;

#if BT_USE_DOUBLE_PRECISION
using Scalar = System.Double;
#else
using Scalar = System.Single;
#endif


namespace BulletSharp
{
    public static class Native
    {
        public const string Dll = "libbulletc";
        public const CallingConvention Conv = CallingConvention.Cdecl;
    }
}
