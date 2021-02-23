using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Arookas
{
	// Token: 0x0200008E RID: 142
	public static class ReflectionHelper
	{
		// Token: 0x06000463 RID: 1123 RVA: 0x0000E35C File Offset: 0x0000C55C
		public static TCast CreateInstance<TCast>(this Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			TCast result;
			try
			{
				result = (TCast)((object)Activator.CreateInstance(type));
			}
			catch (InvalidCastException)
			{
				result = default(TCast);
			}
			return result;
		}

		// Token: 0x06000464 RID: 1124 RVA: 0x0000E3AC File Offset: 0x0000C5AC
		public static TCast CreateInstance<TCast>(this Type type, params object[] constructorArguments)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			TCast result;
			try
			{
				result = (TCast)((object)Activator.CreateInstance(type, constructorArguments));
			}
			catch (InvalidCastException)
			{
				result = default(TCast);
			}
			return result;
		}

		// Token: 0x06000465 RID: 1125 RVA: 0x0000E3FC File Offset: 0x0000C5FC
		public static Dictionary<TAttribute, Type> GetTypesWithAttribute<TAttribute>(this Assembly assembly) where TAttribute : Attribute
		{
			return assembly.GetTypesWithAttribute<TAttribute>(true);
		}

		// Token: 0x06000466 RID: 1126 RVA: 0x0000E410 File Offset: 0x0000C610
		public static Dictionary<TAttribute, Type> GetTypesWithAttribute<TAttribute>(this Assembly assembly, bool publicOnly) where TAttribute : Attribute
		{
			return assembly.GetTypesWithAttribute<TAttribute>(publicOnly, (Type type) => !type.IsAbstract);
		}

		// Token: 0x06000467 RID: 1127 RVA: 0x0000E425 File Offset: 0x0000C625
		public static Dictionary<TAttribute, Type> GetTypesWithAttribute<TAttribute>(this Assembly assembly, Func<Type, bool> predicate) where TAttribute : Attribute
		{
			return assembly.GetTypesWithAttribute<TAttribute>(true, predicate);
		}

		// Token: 0x06000468 RID: 1128 RVA: 0x0000E42F File Offset: 0x0000C62F
		public static Dictionary<TAttribute, Type> GetTypesWithAttribute<TAttribute>(this Assembly assembly, bool publicOnly, Func<Type, bool> predicate) where TAttribute : Attribute
		{
			return assembly.GetTypesWithAttribute<TAttribute>(publicOnly, predicate);
		}

		// Token: 0x06000469 RID: 1129 RVA: 0x0000E439 File Offset: 0x0000C639
		public static Dictionary<TAttribute, Type> GetTypesWithAttribute<TAttribute, TBase>(this Assembly assembly) where TAttribute : Attribute
		{
			return assembly.GetTypesWithAttribute<TAttribute>(true);
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x0000E44D File Offset: 0x0000C64D
		public static Dictionary<TAttribute, Type> GetTypesWithAttribute<TAttribute, TBase>(this Assembly assembly, bool publicOnly) where TAttribute : Attribute
		{
			return assembly.GetTypesWithAttribute<TAttribute>(publicOnly, (Type type) => !type.IsAbstract);
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x0000E462 File Offset: 0x0000C662
		public static Dictionary<TAttribute, Type> GetTypesWithAttribute<TAttribute, TBase>(this Assembly assembly, Func<Type, bool> predicate) where TAttribute : Attribute
		{
			return assembly.GetTypesWithAttribute<TAttribute>(true, predicate);
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x0000E730 File Offset: 0x0000C930
		public static Dictionary<TAttribute, Type> GetTypesWithAttribute<TAttribute, TBase>(this Assembly assembly, bool publicOnly, Func<Type, bool> predicate) where TAttribute : Attribute
		{
			if (assembly == null)
			{
				throw new ArgumentNullException("assembly");
			}
			if (predicate == null)
			{
				throw new ArgumentNullException("predicate");
			}
			IEnumerable<KeyValuePair<TAttribute, Type>> source = from type in publicOnly ? assembly.GetExportedTypes() : assembly.GetTypes()
			where typeof(TBase).IsAssignableFrom(type)
			where predicate(type)
			let attributes = type.GetCustomAttributes(typeof(TAttribute), false)
			where attributes.Length > 0
			let attribute = attributes[0] as TAttribute
			select new KeyValuePair<TAttribute, Type>(attribute, type);
			return source.ToDictionary((KeyValuePair<TAttribute, Type> k) => k.Key, (KeyValuePair<TAttribute, Type> e) => e.Value);
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x0000E808 File Offset: 0x0000CA08
		public static bool ImplementsInterface<TInterface>(this Type type) where TInterface : class
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			Type typeFromHandle = typeof(TInterface);
			if (!typeFromHandle.IsInterface)
			{
				throw new ArgumentException(string.Format("The specified type {0} is not an interface.", typeFromHandle.Name), "type");
			}
			return type.GetInterface(typeFromHandle.Name) != null;
		}
	}
}
