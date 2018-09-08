using System;
using static BulletSharp.UnsafeNativeMethods;

#if BT_USE_DOUBLE_PRECISION
using Scalar = System.Double;
#else
using Scalar = System.Single;
#endif


namespace BulletSharp
{
	public class HeightfieldTerrainShape : ConcaveShape
	{
		public HeightfieldTerrainShape(int heightStickWidth, int heightStickLength,
			IntPtr heightfieldData, Scalar heightScale, Scalar minHeight, Scalar maxHeight,
			int upAxis, PhyScalarType heightDataType, bool flipQuadEdges)
			: base(btHeightfieldTerrainShape_new(heightStickWidth, heightStickLength,
				heightfieldData, heightScale, minHeight, maxHeight, upAxis, heightDataType,
				flipQuadEdges))
		{
		}

		public void SetUseDiamondSubdivision()
		{
			btHeightfieldTerrainShape_setUseDiamondSubdivision(Native);
		}

		public void SetUseDiamondSubdivision(bool useDiamondSubdivision)
		{
			btHeightfieldTerrainShape_setUseDiamondSubdivision2(Native, useDiamondSubdivision);
		}

		public void SetUseZigzagSubdivision()
		{
			btHeightfieldTerrainShape_setUseZigzagSubdivision(Native);
		}

		public void SetUseZigzagSubdivision(bool useZigzagSubdivision)
		{
			btHeightfieldTerrainShape_setUseZigzagSubdivision2(Native, useZigzagSubdivision);
		}
	}
}
