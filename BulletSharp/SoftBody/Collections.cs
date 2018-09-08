﻿using System;
using System.Collections.Generic;
using static BulletSharp.UnsafeNativeMethods;

#if BT_USE_DOUBLE_PRECISION
using Scalar = System.Double;
#else
using Scalar = System.Single;
#endif


namespace BulletSharp.SoftBody
{
	public class NodePtrArrayEnumerator : IEnumerator<Node>
	{
		private int _i;
		private int _count;
		private IList<Node> _array;

		public NodePtrArrayEnumerator(IList<Node> array)
		{
			_array = array;
			_count = array.Count;
			_i = -1;
		}

		public void Dispose()
		{
		}

		public bool MoveNext()
		{
			_i++;
			return _i != _count;
		}

		public void Reset()
		{
			_i = 0;
		}

		public Node Current => _array[_i];

		object System.Collections.IEnumerator.Current => _array[_i];
	}

	public class NodePtrArray : FixedSizeArray, IList<Node>
	{
		internal NodePtrArray(IntPtr native, int count)
			: base(native, count)
		{
		}

		public void Add(Node item)
		{
			throw new InvalidOperationException();
		}

		public int IndexOf(Node item)
		{
			throw new NotImplementedException();
		}

		public Node this[int index]
		{
			get
			{
				if ((uint)index >= (uint)Count)
				{
					throw new ArgumentOutOfRangeException(nameof(index));
				}
				return new Node(btSoftBodyNodePtrArray_at(_native, index));
			}
			set
			{
				btSoftBodyNodePtrArray_set(_native, value.Native, index);
			}
		}

		public bool Contains(Node item)
		{
			throw new NotImplementedException();
		}

		public void CopyTo(Node[] array, int arrayIndex)
		{
			throw new NotImplementedException();
		}

		public IEnumerator<Node> GetEnumerator()
		{
			return new NodePtrArrayEnumerator(this);
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return new NodePtrArrayEnumerator(this);
		}

		public void Insert(int index, Node item)
		{
			throw new InvalidOperationException();
		}

		public bool Remove(Node item)
		{
			throw new NotImplementedException();
		}
	}
}
