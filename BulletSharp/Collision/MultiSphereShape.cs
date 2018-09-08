using System;
using System.IO;
using BulletSharp.Math;
using static BulletSharp.UnsafeNativeMethods;
using System.Runtime.InteropServices;

#if BT_USE_DOUBLE_PRECISION
using Scalar = System.Double;
#else
using Scalar = System.Single;
#endif


namespace BulletSharp
{
	public class MultiSphereShape : ConvexInternalAabbCachingShape
	{
		public MultiSphereShape(Vector3[] positions, Scalar[] radi)
			: base(btMultiSphereShape_new(positions, radi, (radi.Length < positions.Length) ? radi.Length : positions.Length))
		{
		}

		public MultiSphereShape(Vector3Array positions, Scalar[] radi)
			: base(btMultiSphereShape_new2(positions._native, radi, (radi.Length < positions.Count) ? radi.Length : positions.Count))
		{
		}

		public Vector3 GetSpherePosition(int index)
		{
			Vector3 value;
			btMultiSphereShape_getSpherePosition(Native, index, out value);
			return value;
		}

		public Scalar GetSphereRadius(int index)
		{
			return btMultiSphereShape_getSphereRadius(Native, index);
		}
		/*
		public unsafe override string Serialize(IntPtr dataBuffer, Serializer serializer)
		{
			base.Serialize(dataBuffer, serializer);

			int numElem = SphereCount;
			if (numElem != 0)
			{
				Chunk chunk = serializer.Allocate(16 + sizeof(int), numElem);
				Marshal.WriteInt64(dataBuffer, 0, serializer.GetUniquePointer(_native + 4));
				using (var stream = new UnmanagedMemoryStream((byte*)chunk.OldPtr.ToPointer(), chunk.Length, chunk.Length, FileAccess.Write))
				{
					using (var writer = new BulletWriter(stream))
					{
						for (int i = 0; i < SphereCount; i++)
						{
							writer.Write(GetSpherePosition(i));
							writer.Write(GetSphereRadius(i));
						}
					}
				}
				serializer.FinalizeChunk(chunk, "btPositionAndRadius", DnaID.Array, _native + 4);
			}
			else
			{
				Marshal.WriteInt64(dataBuffer, 0, 0);
			}
			Marshal.WriteInt32(dataBuffer, 4, numElem);

			return "btMultiSphereShapeData";
		}
		*/
		public int SphereCount => btMultiSphereShape_getSphereCount(Native);
	}

	[StructLayout(LayoutKind.Sequential)]
	internal struct MultiSphereShapeData
	{
		public ConvexInternalShapeData ConvexInternalShapeData;
		public PositionAndRadius LocalPositionArrayPtr;
		public int LocalPositionArraySize;
		public int Padding;

		public static int Offset(string fieldName) { return Marshal.OffsetOf(typeof(MultiSphereShapeData), fieldName).ToInt32(); }
	}

	[StructLayout(LayoutKind.Sequential)]
	internal struct PositionAndRadius
	{
		public Vector3FloatData Position;
		public Scalar Radius;

		public static int Offset(string fieldName) { return Marshal.OffsetOf(typeof(PositionAndRadius), fieldName).ToInt32(); }
	}
}
