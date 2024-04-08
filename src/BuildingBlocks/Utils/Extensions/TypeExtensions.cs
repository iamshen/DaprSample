using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
#nullable disable

namespace System.Reflection
{
    #region Type 相关扩展

    /// <summary>
    /// Type 相关扩展
    /// </summary>
    public static class TypeExtensions
    {
        #region 判断类型是否为匿名类型

        /// <summary>
        /// 判断类型是否为匿名类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsAnonymousType(this Type type)
            => Attribute.IsDefined(type, typeof(CompilerGeneratedAttribute), false)
                             && type.IsGenericType && type.Name.Contains("AnonymousType")
                             && (type.Name.StartsWith("<>"))
                             && (type.Attributes & TypeAttributes.NotPublic) == TypeAttributes.NotPublic;

        #endregion

        #region 判断类型是否为Nullable类型

        /// <summary>
        /// 判断类型是否为Nullable类型
        /// </summary>
        /// <param name="type"> </param>
        /// <returns> </returns>
        public static bool IsNullableType(this Type type)
            => type != null && type.IsGenericType && (type.GetGenericTypeDefinition() == typeof(Nullable<>));

        #endregion

        #region 通过类型转换器获取Nullable类型的基础类型

        /// <summary>
        /// 通过类型转换器获取Nullable类型的基础类型
        /// </summary>
        /// <param name="type"> </param>
        /// <returns> </returns>
        public static Type GetUnNullableType(this Type type)
        {
            if (IsNullableType(type))
                return new NullableConverter(type).UnderlyingType;
            return type;
        }

        #endregion

        #region 获取成员元数据的Description特性描述信息

        /// <summary>
        /// 获取成员元数据的Description特性描述信息
        /// </summary>
        /// <param name="member">  成员元数据对象 </param>
        /// <param name="inherit"> 是否搜索成员的继承链以查找描述特性 </param>
        /// <returns> 返回Description特性描述信息，如不存在则返回成员的名称 </returns>
        public static string GetDescription(this MemberInfo member, bool inherit = true)
        {
            DescriptionAttribute desc = member.GetAttribute<DescriptionAttribute>(inherit);
            if (desc != null)
                return desc.Description;

            DisplayNameAttribute displayName = member.GetAttribute<DisplayNameAttribute>(inherit);

            if (displayName != null)
                return displayName.DisplayName;
            DisplayAttribute display = member.GetAttribute<DisplayAttribute>(inherit);

            return display != null ? display.Name : member.Name;
        }

        #endregion

        #region 从类型成员获取指定Attribute特性

        /// <summary>
        /// 从类型成员获取指定Attribute特性
        /// </summary>
        /// <typeparam name="T"> Attribute特性类型 </typeparam>
        /// <param name="memberInfo"> 类型类型成员 </param>
        /// <param name="inherit">    是否从继承中查找 </param>
        /// <returns> 存在返回第一个，不存在返回null </returns>
        public static T GetAttribute<T>(this MemberInfo memberInfo, bool inherit = true) where T : Attribute
        {
            var attributes = memberInfo.GetCustomAttributes(typeof(T), inherit);
            return attributes.FirstOrDefault() as T;
        }

        #endregion

        #region 从类型成员获取指定Attribute特性

        /// <summary>
        /// 从类型成员获取指定Attribute特性
        /// </summary>
        /// <typeparam name="T"> Attribute特性类型 </typeparam>
        /// <param name="memberInfo"> 类型类型成员 </param>
        /// <param name="inherit">    是否从继承中查找 </param>
        /// <returns> 返回所有指定Attribute特性的数组 </returns>
        public static T[] GetAttributes<T>(this MemberInfo memberInfo, bool inherit = true) where T : Attribute
            => memberInfo.GetCustomAttributes(typeof(T), inherit).Cast<T>().ToArray();

        #endregion
        
       
    }

    #endregion
}