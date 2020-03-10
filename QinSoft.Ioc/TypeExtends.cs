using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace QinSoft.Ioc
{
    /// <summary>
    /// 类型扩展
    /// 支持递归反射父类信息
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
            if (fieldInfo != null) return fieldInfo;
            else if (isFetchBase && type.BaseType != null) return GetField(type.BaseType, name, bindingFlags, isFetchBase);
            else return null;
        }

        public static FieldInfo GetField(this Type type, string name, bool isFetchBase = false)
        {
            if (type == null) throw new ArgumentNullException("type");
            FieldInfo fieldInfo = type.GetField(name);
            if (fieldInfo != null) return fieldInfo;
            else if (isFetchBase && type.BaseType != null) return GetField(type.BaseType, name, isFetchBase);
            else return null;
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

        public static PropertyInfo GetProperty(this Type type, string name, BindingFlags bindingFlags, bool isFetchBase = false)
        {
            if (type == null) throw new ArgumentNullException("type");
            PropertyInfo propertyInfo = type.GetProperty(name, bindingFlags);
            if (propertyInfo != null) return propertyInfo;
            else if (isFetchBase && type.BaseType != null) return GetProperty(type.BaseType, name, bindingFlags, isFetchBase);
            else return null;
        }

        public static PropertyInfo GetProperty(this Type type, string name, bool isFetchBase = false)
        {
            if (type == null) throw new ArgumentNullException("type");
            PropertyInfo propertyInfo = type.GetProperty(name);
            if (propertyInfo != null) return propertyInfo;
            else if (isFetchBase && type.BaseType != null) return GetProperty(type.BaseType, name, isFetchBase);
            else return null;
        }

        public static MethodInfo[] GetMethods(this Type type, BindingFlags bindingFlags, bool isFetchBase = false)
        {
            if (type == null) throw new ArgumentNullException("type");
            List<MethodInfo> methodInfos = new List<MethodInfo>();
            MethodInfo[] methods = type.GetMethods(bindingFlags);
            methodInfos.AddRange(methods);
            if (isFetchBase && type.BaseType != null)
            {
                methodInfos.AddRange(GetMethods(type.BaseType, bindingFlags, isFetchBase));
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
                methodInfos.AddRange(GetMethods(type.BaseType, isFetchBase));
            }
            return methodInfos.Distinct().ToArray();
        }

        public static MethodInfo GetMethod(this Type type, string name, BindingFlags bindingFlags, bool isFetchBase = false)
        {
            if (type == null) throw new ArgumentNullException("type");
            MethodInfo methodInfo = type.GetMethod(name, bindingFlags);
            if (methodInfo != null) return methodInfo;
            else if (isFetchBase && type.BaseType != null) return GetMethod(type.BaseType, name, bindingFlags, isFetchBase);
            else return null;
        }

        public static MethodInfo GetMethod(this Type type, string name, bool isFetchBase = false)
        {
            if (type == null) throw new ArgumentNullException("type");
            MethodInfo methodInfo = type.GetMethod(name);
            if (methodInfo != null) return methodInfo;
            else if (isFetchBase && type.BaseType != null) return GetMethod(type.BaseType, name, isFetchBase);
            else return null;
        }

        public static MemberInfo[] GetMembers(this Type type, BindingFlags bindingFlags, bool isFetchBase = false)
        {
            if (type == null) throw new ArgumentNullException("type");
            List<MemberInfo> memberInfos = new List<MemberInfo>();
            MemberInfo[] members = type.GetMembers(bindingFlags);
            memberInfos.AddRange(members);
            if (isFetchBase && type.BaseType != null)
            {
                memberInfos.AddRange(GetMembers(type.BaseType, bindingFlags, isFetchBase));
            }
            return memberInfos.Distinct().ToArray();
        }

        public static MemberInfo[] GetMembers(this Type type, bool isFetchBase = false)
        {
            if (type == null) throw new ArgumentNullException("type");
            List<MemberInfo> memberInfos = new List<MemberInfo>();
            MemberInfo[] members = type.GetMembers();
            memberInfos.AddRange(members);
            if (isFetchBase && type.BaseType != null)
            {
                memberInfos.AddRange(GetMembers(type.BaseType, isFetchBase));
            }
            return memberInfos.Distinct().ToArray();
        }

        public static MemberInfo[] GetMember(this Type type, string name, BindingFlags bindingFlags, bool isFetchBase = false)
        {
            if (type == null) throw new ArgumentNullException("type");
            MemberInfo[] memberInfo = type.GetMember(name, bindingFlags);
            if (memberInfo != null && memberInfo.Length > 0) return memberInfo;
            else if (isFetchBase && type.BaseType != null) return GetMember(type.BaseType, name, bindingFlags, isFetchBase);
            else return null;
        }

        public static MemberInfo[] GetMember(this Type type, string name, bool isFetchBase = false)
        {
            if (type == null) throw new ArgumentNullException("type");
            MemberInfo[] memberInfo = type.GetMember(name);
            if (memberInfo != null && memberInfo.Length > 0) return memberInfo;
            else if (isFetchBase && type.BaseType != null) return GetMember(type.BaseType, name, isFetchBase);
            else return null;
        }
    }
}
