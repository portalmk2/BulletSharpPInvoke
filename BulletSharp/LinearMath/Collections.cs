using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using BulletSharp.Math;
using System.Diagnostics;
using static BulletSharp.UnsafeNativeMethods;

#if BT_USE_DOUBLE_PRECISION
using Scalar = System.Double;
#else
using Scalar = System.Single;
#endif


namespace BulletSharp
{
	internal class ListDebugView
	{
		private System.Collections.IEnumerable _list;

		public ListDebugView(System.Collections.IEnumerable list)
		{
			_list = list;
		}

		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public System.Collections.ArrayList Items
		{
			get
			{
				var list = new System.Collections.ArrayList();
				foreach (var o in _list)
					list.Add(o);
				return list;
			}
		}
	};

	public class CompoundShapeChildArrayEnumerator : IEnumerator<CompoundShapeChild>
	{
		private int _i;
		private int _count;
		private CompoundShapeChild[] _array;

		public CompoundShapeChildArrayEnumerator(CompoundShapeChildArray array)
		{
			_array = array._backingArray;
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

		public CompoundShapeChild Current => _array[_i];

		object System.Collections.IEnumerator.Current => _array[_i];
	}

	public class UIntArrayEnumerator : IEnumerator<uint>
	{
		private int _i;
		private int _count;
		private  IList<uint> _array;

		public UIntArrayEnumerator(IList<uint> array)
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

		public uint Current => _array[_i];

		object System.Collections.IEnumerator.Current => _array[_i];
	}

	public class FixedSizeArray
	{
		internal IntPtr _native;

		protected int _count;

		public FixedSizeArray(IntPtr native, int count)
		{
			_native = native;
			_count = count;
		}

		public void Clear()
		{
			throw new InvalidOperationException();
		}

		public void RemoveAt(int index)
		{
			throw new NotSupportedException();
		}

		public int Count => _count;

		public bool IsReadOnly => false;
	}

	public class CompoundShapeChildArray : FixedSizeArray, IList<CompoundShapeChild>
	{
		internal CompoundShapeChild[] _backingArray = new CompoundShapeChild[0];

		internal CompoundShapeChildArray(IntPtr compoundShape)
			: base(compoundShape, 0)
		{
		}
		
		public void Add(CompoundShapeChild item)
		{
			throw new NotSupportedException();
		}

		public void AddChildShape(ref Matrix localTransform, CollisionShape shape)
		{
			IntPtr childListOld = (_count != 0) ? btCompoundShape_getChildList(_native) : IntPtr.Zero;
			btCompoundShape_addChildShape(_native, ref localTransform, shape.Native);
			IntPtr childList = btCompoundShape_getChildList(_native);

			// Adjust the native pointer of existing children if the array was reallocated.
			if (childListOld != childList)
			{
				for (int i = 0; i < _count; i++)
				{
					_backingArray[i].Native = btCompoundShapeChild_array_at(childList, i);
				}
			}

			// Add the child to the backing store.
			int childIndex = _count;
			_count++;
			Array.Resize(ref _backingArray, _count);
			_backingArray[childIndex] = new CompoundShapeChild(btCompoundShapeChild_array_at(childList, childIndex), shape);
		}

		public int IndexOf(CompoundShapeChild item)
		{
			throw new NotImplementedException();
		}

		public CompoundShapeChild this[int index]
		{
			get => _backingArray[index];
			set => throw new NotImplementedException();
		}

		public bool Contains(CompoundShapeChild item)
		{
			throw new NotImplementedException();
		}

		public void CopyTo(CompoundShapeChild[] array, int arrayIndex)
		{
			throw new NotImplementedException();
		}

		public IEnumerator<CompoundShapeChild> GetEnumerator()
		{
			return new CompoundShapeChildArrayEnumerator(this);
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return new CompoundShapeChildArrayEnumerator(this);
		}

		public void Insert(int index, CompoundShapeChild item)
		{
			throw new InvalidOperationException();
		}

		public bool Remove(CompoundShapeChild item)
		{
			throw new NotSupportedException();
		}

		public void RemoveChildShape(CollisionShape shape)
		{
			IntPtr shapePtr = shape.Native;
			for (int i = 0; i < _count; i++)
			{
				if (_backingArray[i].ChildShape.Native == shapePtr)
				{
					RemoveChildShapeByIndex(i);
				}
			}
		}

		internal void RemoveChildShapeByIndex(int childShapeIndex)
		{
			btCompoundShape_removeChildShapeByIndex(_native, childShapeIndex);
			_count--;

			// Swap the last item with the item to be removed like Bullet does.
			if (childShapeIndex != _count)
			{
				CompoundShapeChild lastItem = _backingArray[_count];
				lastItem.Native = _backingArray[childShapeIndex].Native;
				_backingArray[childShapeIndex] = lastItem;
			}
			_backingArray[_count] = null;
		}
	}

	[DebuggerTypeProxy(typeof(ListDebugView))]
	public class UIntArray : FixedSizeArray, IList<uint>
	{
		internal UIntArray(IntPtr native, int count)
			: base(native, count)
		{
		}

		public void Add(uint item)
		{
			throw new NotSupportedException();
		}

		public int IndexOf(uint item)
		{
			throw new NotImplementedException();
		}

		public uint this[int index]
		{
			get
			{
				if ((uint)index >= (uint)Count)
				{
					throw new ArgumentOutOfRangeException(nameof(index));
				}
				return (uint)Marshal.ReadInt32(_native, index * sizeof(uint));
			}
			set
			{
				if ((uint)index >= (uint)Count)
				{
					throw new ArgumentOutOfRangeException(nameof(index));
				}
				Marshal.WriteInt32(_native, index * sizeof(uint), (int)value);
			}
		}

		public bool Contains(uint item)
		{
			throw new NotImplementedException();
		}

		public void CopyTo(uint[] array, int arrayIndex)
		{
			throw new NotImplementedException();
		}

		public IEnumerator<uint> GetEnumerator()
		{
			return new UIntArrayEnumerator(this);
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return new UIntArrayEnumerator(this);
		}

		public void Insert(int index, uint item)
		{
			throw new InvalidOperationException();
		}

		public bool Remove(uint item)
		{
			throw new NotSupportedException();
		}
	}
}
