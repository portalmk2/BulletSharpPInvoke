﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using static BulletSharp.UnsafeNativeMethods;

#if BT_USE_DOUBLE_PRECISION
using Scalar = System.Double;
#else
using Scalar = System.Single;
#endif


namespace BulletSharp.SoftBody
{
	public class AlignedAnchorArrayDebugView
	{
		private AlignedAnchorArray _array;

		public AlignedAnchorArrayDebugView(AlignedAnchorArray array)
		{
			_array = array;
		}

		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public Anchor[] Items
		{
			get
			{
				int count = _array.Count;
				var array = new Anchor[count];
				for (int i = 0; i < count; i++)
				{
					array[i] = _array[i];
				}
				return array;
			}
		}
	}

	public class AlignedAnchorArrayEnumerator : IEnumerator<Anchor>
	{
		private int _i;
		private int _count;
		private AlignedAnchorArray _array;

		public AlignedAnchorArrayEnumerator(AlignedAnchorArray array)
		{
			_array = array;
			_count = array.Count;
			_i = -1;
		}

		public Anchor Current => _array[_i];

		public void Dispose()
		{
		}

		object System.Collections.IEnumerator.Current => _array[_i];

		public bool MoveNext()
		{
			_i++;
			return _i != _count;
		}

		public void Reset()
		{
			_i = 0;
		}
	}

	[Serializable, DebuggerTypeProxy(typeof(AlignedAnchorArrayDebugView)), DebuggerDisplay("Count = {Count}")]
	public class AlignedAnchorArray : IList<Anchor>
	{
		private IntPtr _native;

		internal AlignedAnchorArray(IntPtr native)
		{
			_native = native;
		}

		public int IndexOf(Anchor item)
		{
			throw new NotImplementedException();
		}

		public void Insert(int index, Anchor item)
		{
			throw new NotImplementedException();
		}

		public void RemoveAt(int index)
		{
			throw new NotImplementedException();
		}

		public Anchor this[int index]
		{
			get
			{
				if ((uint)index >= (uint)Count)
				{
					throw new ArgumentOutOfRangeException(nameof(index));
				}
				return new Anchor(btAlignedObjectArray_btSoftBody_Anchor_at(_native, index));
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		public void Add(Anchor item)
		{
			btAlignedObjectArray_btSoftBody_Anchor_push_back(_native, item.Native);
		}

		public void Clear()
		{
			btAlignedObjectArray_btSoftBody_Anchor_resizeNoInitialize(_native, 0);
		}

		public bool Contains(Anchor item)
		{
			throw new NotImplementedException();
		}

		public void CopyTo(Anchor[] array, int arrayIndex)
		{
			throw new NotImplementedException();
		}

		public int Count => btAlignedObjectArray_btSoftBody_Anchor_size(_native);

		public bool IsReadOnly => false;

		public bool Remove(Anchor item)
		{
			throw new NotImplementedException();
		}

		public IEnumerator<Anchor> GetEnumerator()
		{
			return new AlignedAnchorArrayEnumerator(this);
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return new AlignedAnchorArrayEnumerator(this);
		}
	}
}
