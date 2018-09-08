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
	public class AlignedNoteArrayDebugView
	{
		private AlignedNoteArray _array;

		public AlignedNoteArrayDebugView(AlignedNoteArray array)
		{
			_array = array;
		}

		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public Note[] Items
		{
			get
			{
				int count = _array.Count;
				var array = new Note[count];
				for (int i = 0; i < count; i++)
				{
					array[i] = _array[i];
				}
				return array;
			}
		}
	}

	public class AlignedNoteArrayEnumerator : IEnumerator<Note>
	{
		private int _i;
		private int _count;
		private AlignedNoteArray _array;

		public AlignedNoteArrayEnumerator(AlignedNoteArray array)
		{
			_array = array;
			_count = array.Count;
			_i = -1;
		}

		public Note Current => _array[_i];

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

	[Serializable, DebuggerTypeProxy(typeof(AlignedNoteArrayDebugView)), DebuggerDisplay("Count = {Count}")]
	public class AlignedNoteArray : IList<Note>
	{
		private IntPtr _native;

		internal AlignedNoteArray(IntPtr native)
		{
			_native = native;
		}

		public int IndexOf(Note item)
		{
			return btAlignedObjectArray_btSoftBody_Note_index_of(_native, item.Native);
		}

		public void Insert(int index, Note item)
		{
			throw new NotImplementedException();
		}

		public void RemoveAt(int index)
		{
			throw new NotImplementedException();
		}

		public Note this[int index]
		{
			get
			{
				if ((uint)index >= (uint)Count)
				{
					throw new ArgumentOutOfRangeException(nameof(index));
				}
				return new Note(btAlignedObjectArray_btSoftBody_Note_at(_native, index));
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		public void Add(Note item)
		{
			btAlignedObjectArray_btSoftBody_Note_push_back(_native, item.Native);
		}

		public void Clear()
		{
			btAlignedObjectArray_btSoftBody_Note_resizeNoInitialize(_native, 0);
		}

		public bool Contains(Note item)
		{
			throw new NotImplementedException();
		}

		public void CopyTo(Note[] array, int arrayIndex)
		{
			throw new NotImplementedException();
		}

		public int Count => btAlignedObjectArray_btSoftBody_Note_size(_native);

		public bool IsReadOnly => false;

		public bool Remove(Note item)
		{
			throw new NotImplementedException();
		}

		public IEnumerator<Note> GetEnumerator()
		{
			return new AlignedNoteArrayEnumerator(this);
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return new AlignedNoteArrayEnumerator(this);
		}
	}
}
