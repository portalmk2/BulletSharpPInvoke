using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

#if BT_USE_DOUBLE_PRECISION
using Scalar = System.Double;
#else
using Scalar = System.Single;
#endif


namespace BulletSharp.Math
{
/*
#if BT_USE_DOUBLE_PRECISION

    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct Scalar {
        public double Value;

        public Scalar(double d) { Value = d; }

        public static implicit operator double(Scalar s) {
            return s.Value;
        }
        public static implicit operator Scalar(double d) {
            return new Scalar(d);
        }

        public const int Size = sizeof(double);
    }

#else
    public struct Scalar {
        public Scalar Value;
    }
#endif
*/
}
