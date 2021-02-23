using System;
using System.Collections.Generic;

namespace Arookas
{
	// Token: 0x02000028 RID: 40
	public static class CollectionHelper
	{
		// Token: 0x06000130 RID: 304 RVA: 0x0000480B File Offset: 0x00002A0B
		public static IEnumerable<T> DistinctBy<T>(this IEnumerable<T> collection)
		{
			return new HashSet<T>(collection);
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00004814 File Offset: 0x00002A14
		public static IEnumerable<TKey> DistinctBy<TSource, TKey>(this IEnumerable<TSource> collection, Func<TSource, TKey> selector)
		{
			HashSet<TKey> hashSet = new HashSet<TKey>();
			foreach (TSource arg in collection)
			{
				hashSet.Add(selector(arg));
			}
			return hashSet;
		}

		// Token: 0x06000132 RID: 306 RVA: 0x0000486C File Offset: 0x00002A6C
		public static T[] Duplicate<T>(this T[] array)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			return array.Duplicate(0, array.Length);
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00004886 File Offset: 0x00002A86
		public static T[] Duplicate<T>(this T[] array, int count)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			return array.Duplicate(0, count);
		}

		// Token: 0x06000134 RID: 308 RVA: 0x000048A0 File Offset: 0x00002AA0
		public static T[] Duplicate<T>(this T[] array, int startingIndex, int count)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (count == 0)
			{
				return new T[0];
			}
			if (startingIndex < 0 || startingIndex >= array.Length)
			{
				throw new ArgumentOutOfRangeException("startingIndex", startingIndex, "The specified starting index was negative or greater than or equal to the size of the array.");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", count, "The specified count was negative.");
			}
			if (startingIndex + count > array.Length)
			{
				throw new ArgumentException("The sum of the specified starting index and count was greater than the size of the array.");
			}
			T[] array2 = new T[count];
			Array.Copy(array, startingIndex, array2, 0, count);
			return array2;
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00004924 File Offset: 0x00002B24
		public static IEnumerator<T> GetArrayEnumerator<T>(this T[] array)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			return ((IEnumerable<T>)array).GetEnumerator();
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00004940 File Offset: 0x00002B40
		public static int IndexOfFirst<T>(this IEnumerable<T> collection, Predicate<T> predicate)
		{
			int num = 0;
			foreach (T obj in collection)
			{
				if (predicate(obj))
				{
					return num;
				}
				num++;
			}
			return -1;
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00004998 File Offset: 0x00002B98
		public static int IndexOfLast<T>(this IEnumerable<T> collection, Predicate<T> predicate)
		{
			int num = 0;
			int result = -1;
			foreach (T obj in collection)
			{
				if (predicate(obj))
				{
					result = num;
				}
				num++;
			}
			return result;
		}

		// Token: 0x06000138 RID: 312 RVA: 0x000049F0 File Offset: 0x00002BF0
		public static int IndexOfSingle<T>(this IEnumerable<T> collection, Predicate<T> predicate)
		{
			int num = 0;
			int num2 = -1;
			foreach (T obj in collection)
			{
				if (predicate(obj))
				{
					if (num2 >= 0)
					{
						throw new InvalidOperationException("There was not a single element in the collection which matched the predicate.");
					}
					num2 = num;
				}
				num++;
			}
			return num2;
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00004A54 File Offset: 0x00002C54
		public static T[] Initialize<T>(int count, Func<T> predicate)
		{
			T[] array = new T[count];
			for (int i = 0; i < count; i++)
			{
				array[i] = predicate();
			}
			return array;
		}

		// Token: 0x0600013A RID: 314 RVA: 0x00004A84 File Offset: 0x00002C84
		public static T[] Initialize<T>(int count, Func<int, T> predicate)
		{
			T[] array = new T[count];
			for (int i = 0; i < count; i++)
			{
				array[i] = predicate(i);
			}
			return array;
		}

		// Token: 0x0600013B RID: 315 RVA: 0x00004AB4 File Offset: 0x00002CB4
		public static void Transform<T>(this T[] collection, Func<T, T> action)
		{
			for (int i = 0; i < collection.Length; i++)
			{
				collection[i] = action(collection[i]);
			}
		}

		// Token: 0x0600013C RID: 316 RVA: 0x00004AE4 File Offset: 0x00002CE4
		public static void Transform<T>(this T[] collection, Func<int, T, T> action)
		{
			for (int i = 0; i < collection.Length; i++)
			{
				collection[i] = action(i, collection[i]);
			}
		}

		// Token: 0x0600013D RID: 317 RVA: 0x00004B14 File Offset: 0x00002D14
		public static void Transform<T>(this IList<T> collection, Func<T, T> action)
		{
			for (int i = 0; i < collection.Count; i++)
			{
				collection[i] = action(collection[i]);
			}
		}

		// Token: 0x0600013E RID: 318 RVA: 0x00004B48 File Offset: 0x00002D48
		public static void Transform<T>(this IList<T> collection, Func<int, T, T> action)
		{
			for (int i = 0; i < collection.Count; i++)
			{
				collection[i] = action(i, collection[i]);
			}
		}

		// Token: 0x0600013F RID: 319 RVA: 0x00004B7C File Offset: 0x00002D7C
		public static bool Unique<TSource, TKey>(this IEnumerable<TSource> collection, Func<TSource, TKey> comparer)
		{
			HashSet<TKey> hashSet = new HashSet<TKey>();
			foreach (TSource arg in collection)
			{
				if (!hashSet.Add(comparer(arg)))
				{
					return false;
				}
			}
			return true;
		}
	}
}
