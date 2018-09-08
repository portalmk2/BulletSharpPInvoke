using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using BulletSharp.Math;
using static BulletSharp.UnsafeNativeMethods;

#if BT_USE_DOUBLE_PRECISION
using Scalar = System.Double;
#else
using Scalar = System.Single;
#endif


namespace BulletSharp
{
	public class ConvexHullShape : PolyhedralConvexAabbCachingShape
	{
		private Vector3Array _points;
		private Vector3Array _unscaledPoints;

		public ConvexHullShape()
			: base(btConvexHullShape_new())
		{
		}

		public ConvexHullShape(Scalar[] points)
			: this(points, points.Length / 3, 3 * sizeof(Scalar))
		{
		}

		public ConvexHullShape(Scalar[] points, int numPoints, int stride = 3 * sizeof(Scalar))
			: base(btConvexHullShape_new4(points, numPoints, stride))
		{
		}

		public ConvexHullShape(IEnumerable<Vector3> points, int numPoints)
			: base(btConvexHullShape_new())
		{
			int i = 0;
			foreach (Vector3 v in points)
			{
				Vector3 viter = v;
				AddPointRef(ref viter, false);
				i++;
				if (i == numPoints)
				{
					break;
				}
			}
			RecalcLocalAabb();
		}

		public ConvexHullShape(IEnumerable<Vector3> points)
			: base(btConvexHullShape_new())
		{
			foreach (Vector3 v in points)
			{
				Vector3 viter = v;
				AddPointRef(ref viter, false);
			}
			RecalcLocalAabb();
		}

		public void AddPointRef(ref Vector3 point, bool recalculateLocalAabb = true)
		{
			btConvexHullShape_addPoint(Native, ref point, recalculateLocalAabb);
		}

		public void AddPoint(Vector3 point, bool recalculateLocalAabb = true)
		{
			btConvexHullShape_addPoint(Native, ref point, recalculateLocalAabb);
		}

		public void GetScaledPoint(int i, out Vector3 value)
		{
			btConvexHullShape_getScaledPoint(Native, i, out value);
		}

		public Vector3 GetScaledPoint(int i)
		{
			Vector3 value;
			btConvexHullShape_getScaledPoint(Native, i, out value);
			return value;
		}

		public void OptimizeConvexHull()
		{
			btConvexHullShape_optimizeConvexHull(Native);
		}

		public int NumPoints => btConvexHullShape_getNumPoints(Native);

		public Vector3Array Points
		{
			get
			{
				if (_points == null || _points.Count != NumPoints)
				{
					_points = new Vector3Array(btConvexHullShape_getPoints(Native), NumPoints);
				}
				return _points;
			}
		}

		public Vector3Array UnscaledPoints
		{
			get
			{
				if (_unscaledPoints == null || _unscaledPoints.Count != NumPoints)
				{
					_unscaledPoints = new Vector3Array(btConvexHullShape_getUnscaledPoints(Native), NumPoints);
				}
				return _unscaledPoints;
			}
		}
	}

	[StructLayout(LayoutKind.Sequential)]
	internal struct ConvexHullShapeData
	{
		public ConvexInternalShapeData ConvexInternalShapeData;
		public IntPtr UnscaledPointsFloatPtr;
		public IntPtr UnscaledPointsDoublePtr;
		public int NumUnscaledPoints;
		public int Padding;

		public static int Offset(string fieldName) { return Marshal.OffsetOf(typeof(ConvexHullShapeData), fieldName).ToInt32(); }
	}
}
