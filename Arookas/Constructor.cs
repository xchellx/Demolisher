using System;
using System.Reflection;

namespace Arookas
{
	// Token: 0x0200006B RID: 107
	public static class Constructor
	{
		// Token: 0x06000330 RID: 816 RVA: 0x0000BC08 File Offset: 0x00009E08
		public static TObject Construct<TObject>() where TObject : new()
		{
			if (default(TObject) != null)
			{
				return default(TObject);
			}
			return Activator.CreateInstance<TObject>();
		}

		// Token: 0x06000331 RID: 817 RVA: 0x0000BC34 File Offset: 0x00009E34
		public static TObject Construct<TObject, TParameter1>(TParameter1 parameter1)
		{
			object[] parameters = new object[]
			{
				parameter1
			};
			foreach (ConstructorInfo constructorInfo in typeof(TObject).GetConstructors())
			{
				try
				{
					return (TObject)((object)constructorInfo.Invoke(parameters));
				}
				catch (ArgumentException)
				{
				}
			}
			throw new MissingMethodException(string.Format("Failed to find a constructor on type {0} with parameters ({1}).", typeof(TObject).Name, typeof(TParameter1).Name));
		}

		// Token: 0x06000332 RID: 818 RVA: 0x0000BCD0 File Offset: 0x00009ED0
		public static TObject Construct<TObject, TParameter1, TParameter2>(TParameter1 parameter1, TParameter2 parameter2)
		{
			object[] parameters = new object[]
			{
				parameter1,
				parameter2
			};
			foreach (ConstructorInfo constructorInfo in typeof(TObject).GetConstructors())
			{
				try
				{
					return (TObject)((object)constructorInfo.Invoke(parameters));
				}
				catch (ArgumentException)
				{
				}
			}
			throw new MissingMethodException(string.Format("Failed to find a constructor on type {0} with parameters ({1}, {2}).", typeof(TObject).Name, typeof(TParameter1).Name, typeof(TParameter2).Name));
		}

		// Token: 0x06000333 RID: 819 RVA: 0x0000BD84 File Offset: 0x00009F84
		public static TObject Construct<TObject, TParameter1, TParameter2, TParameter3>(TParameter1 parameter1, TParameter2 parameter2, TParameter3 parameter3)
		{
			object[] parameters = new object[]
			{
				parameter1,
				parameter2,
				parameter3
			};
			foreach (ConstructorInfo constructorInfo in typeof(TObject).GetConstructors())
			{
				try
				{
					return (TObject)((object)constructorInfo.Invoke(parameters));
				}
				catch (ArgumentException)
				{
				}
			}
			throw new MissingMethodException(string.Format("Failed to find a constructor on type {0} with parameters ({1}, {2}, {3}).", new object[]
			{
				typeof(TObject).Name,
				typeof(TParameter1).Name,
				typeof(TParameter2).Name,
				typeof(TParameter3).Name
			}));
		}

		// Token: 0x06000334 RID: 820 RVA: 0x0000BE68 File Offset: 0x0000A068
		public static TObject Construct<TObject, TParameter1, TParameter2, TParameter3, TParameter4>(TParameter1 parameter1, TParameter2 parameter2, TParameter3 parameter3, TParameter4 parameter4)
		{
			object[] parameters = new object[]
			{
				parameter1,
				parameter2,
				parameter3,
				parameter4
			};
			foreach (ConstructorInfo constructorInfo in typeof(TObject).GetConstructors())
			{
				try
				{
					return (TObject)((object)constructorInfo.Invoke(parameters));
				}
				catch (ArgumentException)
				{
				}
			}
			throw new MissingMethodException(string.Format("Failed to find a constructor on type {0} with parameters ({1}, {2}, {3}, {4}).", new object[]
			{
				typeof(TObject).Name,
				typeof(TParameter1).Name,
				typeof(TParameter2).Name,
				typeof(TParameter3).Name,
				typeof(TParameter4).Name
			}));
		}
	}
}
