using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace QinSoft.Ioc
{
    /// <summary>
    /// 类型扩展
    /// </summary>
    public static class TypeExtends
    {
        public static FieldInfo[] GetFields(this Type type, BindingFlags bindingFlags, bool isFetchBase = false)
        {
            if (type == null) throw new ArgumentNullException("type");
            List<FieldInfo> fieldInfos = new List<FieldInfo>();
            FieldInfo[] fields = type.GetFields(bindingFlags);
            fieldInfos.AddRange(fields);
            if (isFetchBase && type.BaseType != null)
            {
                fieldInfos.AddRange(GetFields(type.BaseType, bindingFlags, isFetchBase));
            }
            return fieldInfos.Distinct().ToArray();
        }

        public static FieldInfo[] GetFields(this Type type, bool isFetchBase = false)
        {
            if (type == null) throw new ArgumentNullException("type");
            List<FieldInfo> fieldInfos = new List<FieldInfo>();
            FieldInfo[] fields = type.GetFields();
            fieldInfos.AddRange(fields);
            if (isFetchBase && type.BaseType != null)
            {
                fieldInfos.AddRange(GetFields(type.BaseType, isFetchBase));
            }
            return fieldInfos.Distinct().ToArray();
        }

        public static FieldInfo GetField(this Type type, string name, BindingFlags bindingFlags, bool isFetchBase = false)
        {
            if (type == null) throw new ArgumentNullException("type");
            FieldInfo fieldInfo = type.GetField(name, bindingFlags);
            return fieldInfo ?? (isFetchBase ? GetField(type, name, bindingFlags, isFetchBase) : null);
        }

        public static FieldInfo GetField(this Type type, string name, bool isFetchBase = false)
        {
            if (type == null) throw new ArgumentNullException("type");
            FieldInfo fieldInfo = type.GetField(name);
            return fieldInfo ?? (isFetchBase ? GetField(type, name, isFetchBase) : null);
        }

        public static PropertyInfo[] GetProperties(this Type type, BindingFlags bindingFlags, bool isFetchBase = false)
        {
            if (type == null) throw new ArgumentNullException("type");
            List<PropertyInfo> propertyInfos = new List<PropertyInfo>();
            PropertyInfo[] properties = type.GetProperties(bindingFlags);
            propertyInfos.AddRange(properties);
            if (isFetchBase && type.BaseType != null)
            {
                propertyInfos.AddRange(GetProperties(type.BaseType, bindingFlags, isFetchBase));
            }
            return propertyInfos.Distinct().ToArray();
        }

        public static PropertyInfo[] GetProperties(this Type type, bool isFetchBase = false)
        {
            if (type == null) throw new ArgumentNullException("type");
            List<PropertyInfo> propertyInfos = new List<PropertyInfo>();
            PropertyInfo[] properties = type.GetProperties();
            propertyInfos.AddRange(properties);
            if (isFetchBase && type.BaseType != null)
            {
                propertyInfos.AddRange(GetProperties(type.BaseType, isFetchBase));
            }
            return propertyInfos.Distinct().ToArray();
        }

        public static MethodInfo[] GetMethods(this Type type, BindingFlags bindingFlags, bool isFetchBase = false)
        {
            if (type == null) throw new ArgumentNullException("type");
            List<MethodInfo> methodInfos = new List<MethodInfo>();
            MethodInfo[] methods = type.GetMethods(bindingFlags);
            methodInfos.AddRange(methods);
            if (isFetchBase && type.BaseType != null)
            {
                methodInfos.AddRange(GetMethods(type, bindingFlags, isFetchBase));
            }
            return methodInfos.Distinct().ToArray();
        }

        public static MethodInfo[] GetMethods(this Type type, bool isFetchBase = false)
        {
            if (type == null) throw new ArgumentNullException("type");
            List<MethodInfo> methodInfos = new List<MethodInfo>();
            MethodInfo[] methods = type.GetMethods();
            methodInfos.AddRange(methods);
            if (isFetchBase && type.BaseType != null)
            {
                methodInfos.AddRange(GetMethods(type, isFetchBase));
            }
            return methodInfos.Distinct().ToArray();
        }
    }
}
