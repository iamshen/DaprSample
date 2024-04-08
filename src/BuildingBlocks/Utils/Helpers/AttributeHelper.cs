﻿#nullable disable

using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace DaprTool.BuildingBlocks.Utils;

#region Attribute相关的助手类

/// <summary>
///     Attribute相关的助手类
/// </summary>
public class AttributeHelper
{
    #region 获取枚举的DisplayAttribute的Name

    /// <summary>
    ///     获取枚举的DisplayAttribute的Name
    /// </summary>
    /// <param name="enum"></param>
    /// <param name="isCulture">是否使用资源文件</param>
    /// <param name="inherit">是否同时查找从父类继承的特性</param>
    /// <returns></returns>
    public static string GetDisplayName(Enum @enum, bool isCulture = true, bool inherit = false)
    {
        return GetDisplayNameWithField(@enum, @enum.ToString(), isCulture, inherit);
    }

    #endregion

    #region 获取枚举的DisplayAttribute的ShortName

    /// <summary>
    ///     获取枚举的DisplayAttribute的ShortName
    /// </summary>
    /// <param name="enum"></param>
    /// <param name="isCulture">是否使用资源文件</param>
    /// <param name="inherit">是否同时查找从父类继承的特性</param>
    /// <returns></returns>
    public static string GetDisplayShortName(Enum @enum, bool isCulture = true, bool inherit = false)
    {
        return GetDisplayShortNameWithField(@enum, @enum.ToString(), isCulture, inherit);
    }

    #endregion

    #region 获取枚举的DisplayAttribute的Description

    /// <summary>
    ///     获取枚举的DisplayAttribute的Description
    /// </summary>
    /// <param name="enum"></param>
    /// <param name="isCulture">是否使用资源文件</param>
    /// <param name="inherit">是否同时查找从父类继承的特性</param>
    /// <returns></returns>
    public static string GetDisplayDescription(Enum @enum, bool isCulture = true, bool inherit = false)
    {
        return GetDisplayDescriptionWithField(@enum, @enum.ToString(), isCulture, inherit);
    }

    #endregion

    #region 字段上的特性操作

    /// <summary>
    ///     获取对象字段上的特性
    /// </summary>
    /// <typeparam name="TReturn">特性类型</typeparam>
    /// <typeparam name="TInput">对象类型</typeparam>
    /// <param name="object">要操作的对象</param>
    /// <param name="fieldName">字段名称</param>
    /// <param name="inherit">是否同时查找从父类继承的特性</param>
    /// <returns></returns>
    public static TReturn GetCustomAttributeByField<TReturn, TInput>(TInput @object, string fieldName,
        bool inherit = false) where TReturn : Attribute
    {
        if (@object == null)
            return default;

        var field = @object.GetType().GetField(fieldName);
        if (field == null)
            return null;

        return field.GetCustomAttribute<TReturn>(inherit);
    }

    /// <summary>
    ///     获取对象字段上的特性集合
    /// </summary>
    /// <typeparam name="TReturn">特性类型</typeparam>
    /// <typeparam name="TInput">对象类型</typeparam>
    /// <param name="object">要操作的对象</param>
    /// <param name="fieldName">字段名称</param>
    /// <param name="inherit">是否同时查找从父类继承的特性</param>
    /// <returns></returns>
    public static IEnumerable<TReturn> GetCustomAttributesByField<TReturn, TInput>(TInput @object, string fieldName,
        bool inherit = false) where TReturn : Attribute
    {
        if (@object == null)
            return default;

        var field = @object.GetType().GetField(fieldName);
        if (field == null)
            return null;

        return field.GetCustomAttributes<TReturn>(inherit);
    }

    /// <summary>
    ///     获取对象字段上的特性
    /// </summary>
    /// <typeparam name="T">特性类型</typeparam>
    /// <param name="object">要操作的对象</param>
    /// <param name="fieldName">字段名称</param>
    /// <param name="inherit">是否同时查找从父类继承的特性</param>
    /// <returns></returns>
    public static T GetCustomAttributeByField<T>(object @object, string fieldName, bool inherit = false)
        where T : Attribute
    {
        if (@object == null)
            return default;

        var field = @object.GetType().GetField(fieldName);
        if (field == null)
            return null;

        return field.GetCustomAttribute<T>(inherit);
    }

    /// <summary>
    ///     获取对象字段上的特性集合
    /// </summary>
    /// <typeparam name="T">特性类型</typeparam>
    /// <param name="object">要操作的对象</param>
    /// <param name="fieldName">字段名称</param>
    /// <param name="inherit">是否同时查找从父类继承的特性</param>
    /// <returns></returns>
    public static IEnumerable<T> GetCustomAttributesByField<T>(object @object, string fieldName, bool inherit = false)
        where T : Attribute
    {
        if (@object == null)
            return default;

        var field = @object.GetType().GetField(fieldName);
        if (field == null)
            return null;

        return field.GetCustomAttributes<T>(inherit);
    }

    /// <summary>
    ///     获取对象字段上的特性集合
    /// </summary>
    /// <param name="object">要操作的对象</param>
    /// <param name="fieldName">字段名称</param>
    /// <param name="inherit">是否同时查找从父类继承的特性</param>
    /// <returns></returns>
    public static IEnumerable<object> GetCustomAttributesByField(object @object, string fieldName, bool inherit = false)
    {
        if (@object == null)
            return default;

        var field = @object.GetType().GetField(fieldName);
        if (field == null)
            return null;

        return field.GetCustomAttributes(inherit);
    }

    #endregion

    #region 属性上的特性操作

    /// <summary>
    ///     获取对象属性上的特性
    /// </summary>
    /// <typeparam name="TReturn">特性类型</typeparam>
    /// <typeparam name="TInput">对象类型</typeparam>
    /// <param name="object">要操作的对象</param>
    /// <param name="propertyName">属性名称</param>
    /// <param name="inherit">是否同时查找从父类继承的特性</param>
    /// <returns></returns>
    public static TReturn GetCustomAttributeByProperty<TReturn, TInput>(TInput @object, string propertyName,
        bool inherit = false) where TReturn : Attribute
    {
        if (@object == null)
            return default;

        var property = @object.GetType().GetProperty(propertyName);
        if (property == null)
            return null;

        return property.GetCustomAttribute<TReturn>(inherit);
    }

    /// <summary>
    ///     获取对象属性上的特性
    /// </summary>
    /// <typeparam name="TReturn">特性类型</typeparam>
    /// <typeparam name="TInput">对象类型</typeparam>
    /// <param name="object">要操作的对象</param>
    /// <param name="propertyName">属性名称</param>
    /// <param name="inherit">是否同时查找从父类继承的特性</param>
    /// <returns></returns>
    public static IEnumerable<TReturn> GetCustomAttributesByProperty<TReturn, TInput>(TInput @object,
        string propertyName, bool inherit = false) where TReturn : Attribute
    {
        if (@object == null)
            return default;

        var property = @object.GetType().GetProperty(propertyName);
        if (property == null)
            return null;

        return property.GetCustomAttributes<TReturn>(inherit);
    }

    /// <summary>
    ///     获取对象属性上的特性
    /// </summary>
    /// <typeparam name="T">特性类型</typeparam>
    /// <param name="object">对象类型</param>
    /// <param name="propertyName">属性名称</param>
    /// <param name="inherit">是否同时查找从父类继承的特性</param>
    /// <returns></returns>
    public static T GetCustomAttributeByProperty<T>(object @object, string propertyName, bool inherit = false)
        where T : Attribute
    {
        if (@object == null)
            return default;

        var property = @object.GetType().GetProperty(propertyName);
        if (property == null)
            return null;

        return property.GetCustomAttribute<T>(inherit);
    }

    /// <summary>
    ///     获取对象属性上的特性
    /// </summary>
    /// <typeparam name="T">特性类型</typeparam>
    /// <param name="object">对象类型</param>
    /// <param name="propertyName">属性名称</param>
    /// <param name="inherit">是否同时查找从父类继承的特性</param>
    /// <returns></returns>
    public static IEnumerable<T> GetCustomAttributesByProperty<T>(object @object, string propertyName,
        bool inherit = false) where T : Attribute
    {
        if (@object == null)
            return default;

        var property = @object.GetType().GetProperty(propertyName);
        if (property == null)
            return null;

        return property.GetCustomAttributes<T>(inherit);
    }

    /// <summary>
    ///     获取对象属性上的特性
    /// </summary>
    /// <param name="object">对象类型</param>
    /// <param name="propertyName">属性名称</param>
    /// <param name="inherit">是否同时查找从父类继承的特性</param>
    /// <returns></returns>
    public static IEnumerable<object> GetCustomAttributesByProperty(object @object, string propertyName,
        bool inherit = false)
    {
        if (@object == null)
            return default;

        var property = @object.GetType().GetProperty(propertyName);
        if (property == null)
            return null;

        return property.GetCustomAttributes(inherit);
    }

    #endregion

    #region 获取DisplayAttribute特性的ShortName值

    /// <summary>
    ///     获取字段上的DisplayAttribute特性的ShortName值
    /// </summary>
    /// <typeparam name="T">对象类型</typeparam>
    /// <param name="object">要操作的对象</param>
    /// <param name="fieldName">字段名称</param>
    /// <param name="isCulture">是否使用资源文件</param>
    /// <param name="inherit">是否同时查找从父类继承的特性</param>
    /// <returns></returns>
    public static string GetDisplayShortNameWithField<T>(T @object, string fieldName, bool isCulture = true,
        bool inherit = false)
    {
        var attribute = GetCustomAttributeByField<DisplayAttribute, T>(@object, fieldName, inherit);
        if (attribute == null)
            return "";

        try
        {
            return isCulture ? attribute.GetShortName() : attribute.ShortName;
        }
        catch
        {
            return attribute.ShortName;
        }
    }

    /// <summary>
    ///     获取字段上的DisplayAttribute特性的Name值
    /// </summary>
    /// <param name="object">要操作的对象</param>
    /// <param name="fieldName">字段名称</param>
    /// <param name="isCulture">是否使用资源文件</param>
    /// <param name="inherit">是否同时查找从父类继承的特性</param>
    /// <returns></returns>
    public static string GetDisplayShortNameWithField(object @object, string fieldName, bool isCulture = true,
        bool inherit = false)
    {
        var attribute = GetCustomAttributeByField<DisplayAttribute>(@object, fieldName, inherit);
        if (attribute == null)
            return "";

        try
        {
            return isCulture ? attribute.GetShortName() : attribute.ShortName;
        }
        catch
        {
            return attribute.ShortName;
        }
    }

    /// <summary>
    ///     获取属性上的DisplayAttribute特性的Name值
    /// </summary>
    /// <typeparam name="T">要操作的对象类型</typeparam>
    /// <param name="object">要操作的对象</param>
    /// <param name="propertyName">属性名称</param>
    /// <param name="isCulture">是否使用资源文件</param>
    /// <param name="inherit">是否同时查找从父类继承的特性</param>
    /// <returns></returns>
    public static string GetDisplayShortNameWithProperty<T>(T @object, string propertyName, bool isCulture = true,
        bool inherit = false)
    {
        var attribute = GetCustomAttributeByProperty<DisplayAttribute, T>(@object, propertyName, inherit);
        if (attribute == null)
            return "";

        try
        {
            return isCulture ? attribute.GetShortName() : attribute.ShortName;
        }
        catch
        {
            return attribute.ShortName;
        }
    }

    /// <summary>
    ///     获取属性上的DisplayAttribute特性的Name值
    /// </summary>
    /// <param name="object">要操作的对象</param>
    /// <param name="propertyName">属性名称</param>
    /// <param name="isCulture">是否使用资源文件</param>
    /// <param name="inherit">是否同时查找从父类继承的特性</param>
    /// <returns></returns>
    public static string GetDisplayShortNameWithProperty(object @object, string propertyName, bool isCulture = true,
        bool inherit = false)
    {
        var attribute = GetCustomAttributeByProperty<DisplayAttribute>(@object, propertyName, inherit);
        if (attribute == null)
            return "";

        try
        {
            return isCulture ? attribute.GetShortName() : attribute.ShortName;
        }
        catch
        {
            return attribute.ShortName;
        }
    }

    #endregion

    #region 获取DisplayAttribute特性的Name值

    /// <summary>
    ///     获取字段上的DisplayAttribute特性的Name值
    /// </summary>
    /// <typeparam name="T">对象类型</typeparam>
    /// <param name="object">要操作的对象</param>
    /// <param name="fieldName">字段名称</param>
    /// <param name="isCulture">是否使用资源文件</param>
    /// <param name="inherit">是否同时查找从父类继承的特性</param>
    /// <returns></returns>
    public static string GetDisplayNameWithField<T>(T @object, string fieldName, bool isCulture = true,
        bool inherit = false)
    {
        var attribute = GetCustomAttributeByField<DisplayAttribute, T>(@object, fieldName, inherit);
        if (attribute == null)
            return "";

        try
        {
            return isCulture ? attribute.GetName() : attribute.Name;
        }
        catch
        {
            return attribute.Name;
        }
    }

    /// <summary>
    ///     获取字段上的DisplayAttribute特性的Name值
    /// </summary>
    /// <param name="object">要操作的对象</param>
    /// <param name="fieldName">字段名称</param>
    /// <param name="isCulture">是否使用资源文件</param>
    /// <param name="inherit">是否同时查找从父类继承的特性</param>
    /// <returns></returns>
    public static string GetDisplayNameWithField(object @object, string fieldName, bool isCulture = true,
        bool inherit = false)
    {
        var attribute = GetCustomAttributeByField<DisplayAttribute>(@object, fieldName, inherit);
        if (attribute == null)
            return "";

        try
        {
            return isCulture ? attribute.GetName() : attribute.Name;
        }
        catch
        {
            return attribute.Name;
        }
    }

    /// <summary>
    ///     获取属性上的DisplayAttribute特性的Name值
    /// </summary>
    /// <typeparam name="T">要操作的对象类型</typeparam>
    /// <param name="object">要操作的对象</param>
    /// <param name="propertyName">属性名称</param>
    /// <param name="isCulture">是否使用资源文件</param>
    /// <param name="inherit">是否同时查找从父类继承的特性</param>
    /// <returns></returns>
    public static string GetDisplayNameWithProperty<T>(T @object, string propertyName, bool isCulture = true,
        bool inherit = false)
    {
        var attribute = GetCustomAttributeByProperty<DisplayAttribute, T>(@object, propertyName, inherit);
        if (attribute == null)
            return "";

        try
        {
            return isCulture ? attribute.GetName() : attribute.Name;
        }
        catch
        {
            return attribute.Name;
        }
    }

    /// <summary>
    ///     获取属性上的DisplayAttribute特性的Name值
    /// </summary>
    /// <param name="object">要操作的对象</param>
    /// <param name="propertyName">属性名称</param>
    /// <param name="isCulture">是否使用资源文件</param>
    /// <param name="inherit">是否同时查找从父类继承的特性</param>
    /// <returns></returns>
    public static string GetDisplayNameWithProperty(object @object, string propertyName, bool isCulture = true,
        bool inherit = false)
    {
        var attribute = GetCustomAttributeByProperty<DisplayAttribute>(@object, propertyName, inherit);
        if (attribute == null)
            return "";

        try
        {
            return isCulture ? attribute.GetName() : attribute.Name;
        }
        catch
        {
            return attribute.Name;
        }
    }

    #endregion

    #region 获取DisplayAttribute特性的Description值

    /// <summary>
    ///     获取字段上的DisplayAttribute特性的Description值
    /// </summary>
    /// <typeparam name="T">对象类型</typeparam>
    /// <param name="object">要操作的对象</param>
    /// <param name="fieldName">字段名称</param>
    /// <param name="isCulture">是否使用资源文件</param>
    /// <param name="inherit">是否同时查找从父类继承的特性</param>
    /// <returns></returns>
    public static string GetDisplayDescriptionWithField<T>(T @object, string fieldName, bool isCulture = true,
        bool inherit = false)
    {
        var attribute = GetCustomAttributeByField<DisplayAttribute, T>(@object, fieldName, inherit);
        if (attribute == null)
            return "";

        try
        {
            return isCulture ? attribute.GetDescription() : attribute.Description;
        }
        catch
        {
            return attribute.Description;
        }
    }

    /// <summary>
    ///     获取字段上的DisplayAttribute特性的Description值
    /// </summary>
    /// <param name="object">要操作的对象</param>
    /// <param name="fieldName">字段名称</param>
    /// <param name="isCulture">是否使用资源文件</param>
    /// <param name="inherit">是否同时查找从父类继承的特性</param>
    /// <returns></returns>
    public static string GetDisplayDescriptionWithField(object @object, string fieldName, bool isCulture = true,
        bool inherit = false)
    {
        var attribute = GetCustomAttributeByField<DisplayAttribute>(@object, fieldName, inherit);
        if (attribute == null)
            return "";

        try
        {
            return isCulture ? attribute.GetDescription() : attribute.Description;
        }
        catch
        {
            return attribute.Description;
        }
    }

    /// <summary>
    ///     获取属性上的DisplayAttribute特性的Description值
    /// </summary>
    /// <typeparam name="T">要操作的对象类型</typeparam>
    /// <param name="object">要操作的对象</param>
    /// <param name="propertyName">属性名称</param>
    /// <param name="isCulture">是否使用资源文件</param>
    /// <param name="inherit">是否同时查找从父类继承的特性</param>
    /// <returns></returns>
    public static string GetDisplayDescriptionWithProperty<T>(T @object, string propertyName, bool isCulture = true,
        bool inherit = false)
    {
        var attribute = GetCustomAttributeByProperty<DisplayAttribute, T>(@object, propertyName, inherit);
        if (attribute == null)
            return "";

        try
        {
            return isCulture ? attribute.GetDescription() : attribute.Description;
        }
        catch
        {
            return attribute.Description;
        }
    }

    /// <summary>
    ///     获取属性上的DisplayAttribute特性的Description值
    /// </summary>
    /// <param name="object">要操作的对象</param>
    /// <param name="propertyName">属性名称</param>
    /// <param name="isCulture">是否使用资源文件</param>
    /// <param name="inherit">是否同时查找从父类继承的特性</param>
    /// <returns></returns>
    public static string GetDisplayDescriptionWithProperty(object @object, string propertyName, bool isCulture = true,
        bool inherit = false)
    {
        var attribute = GetCustomAttributeByProperty<DisplayAttribute>(@object, propertyName, inherit);
        if (attribute == null)
            return "";

        try
        {
            return isCulture ? attribute.GetDescription() : attribute.Description;
        }
        catch
        {
            return attribute.Description;
        }
    }

    #endregion
}

#endregion