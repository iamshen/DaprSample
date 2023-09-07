using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;

namespace DaprTool.BuildingBlocks.CommonUtility;

#region 动态方法助手类

/// <summary>
///     动态方法助手类
/// </summary>
public class DynamicMethodHelper
{
    #region 获取处理器实例的指定名称的全部方法

    /// <summary>
    ///     获取处理器实例的指定名称的全部方法
    /// </summary>
    /// <param name="handlerType"></param>
    /// <param name="handlerName">处理器方法名称</param>
    /// <param name="bindingFlags"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static MethodInfo[] GetHandlerMethods<T>(Type handlerType, string handlerName, BindingFlags? bindingFlags)
    {
        MethodInfo[] methods;
        if (bindingFlags.HasValue)
            methods = handlerType.GetMethods(bindingFlags.Value);
        else
            methods = handlerType.GetMethods();

        return methods.Where(m =>
        {
            var parameters = m.GetParameters();
            return parameters.Length >= 1 &&
                   m.Name == handlerName &&
                   (typeof(T).IsAssignableFrom(parameters[0].ParameterType) || parameters[0].GetType() == typeof(T));
        }).ToArray();
    }

    #endregion

    #region 构建一个动态调用方法

    /// <summary>
    ///     构建一个动态调用方法
    /// </summary>
    /// <typeparam name="T">方法的参数类型</typeparam>
    /// <param name="handlerInstanceType">方法所属的class类型</param>
    /// <param name="handlerName">class中定义的方法名称</param>
    /// <param name="bindingFlags">查找方法的方式(共有、私有等)</param>
    /// <param name="notFoundAction">如果没有找到合适的方法的时候的回调方法</param>
    /// <returns></returns>
    public static Func<object, T, Task> BuildMethod<T>(Type handlerInstanceType, string handlerName,
        BindingFlags? bindingFlags, Func<T, Task> notFoundAction)
    {
        if (notFoundAction is null)
            throw new ArgumentNullException(nameof(notFoundAction));

        //定义动态方法，使用IL直接调用方法
        DynamicMethod dynamicMethod = new(string.Empty, typeof(Task), new[] { typeof(object), typeof(T) },
            handlerInstanceType, false);
        var il = dynamicMethod.GetILGenerator();
        var methodInfos = GetHandlerMethods<T>(handlerInstanceType, handlerName, bindingFlags);
        List<(Type, Label, MethodInfo, int)> switchMethods = new();
        foreach (var item in methodInfos)
        {
            var parameters = item.GetParameters();
            switchMethods.Add((parameters[0].ParameterType, il.DefineLabel(), item, parameters.Length));
        }

        foreach (var item in switchMethods)
        {
            //将动态方法的第二个参数(命令对象)加载到堆栈中
            il.Emit(OpCodes.Ldarg_1);
            //将上面加载的参数进行类型测试
            il.Emit(OpCodes.Isinst, item.Item1);
            //跳转到指定标签，相当于goto跳转到指定的标签处
            il.Emit(OpCodes.Brtrue, item.Item2);
        }

        //定义检查是否忽略事件处理器的标签，如果代码执行到这里，说明没有找到事件处理器，则需要跳转到检查是否忽略事件的执行方法
        var checkIgnoreEventLabel = il.DefineLabel();
        //无条件跳转到指定标签
        il.Emit(OpCodes.Br, checkIgnoreEventLabel);

        foreach (var item in switchMethods)
        {
            //定义一个标签，相当于定一个goto的标签
            il.MarkLabel(item.Item2);
            il.Emit(OpCodes.Ldarg_0);

            //加载动态方法中的第二个参数(命令对象)
            il.Emit(OpCodes.Ldarg_1);

            //呼叫指定的方法，并对方法传递上面加载的参数
            il.Emit(OpCodes.Call, item.Item3);
            //执行结束，返回，方法调用结束
            il.Emit(OpCodes.Ret);
        }

        //生成一个检查事件是否被忽略的开始执行的位置的标签
        il.MarkLabel(checkIgnoreEventLabel);
        il.Emit(OpCodes.Ldarg_0);
        il.Emit(OpCodes.Ldarg_1);
        il.Emit(OpCodes.Call, notFoundAction.GetMethodInfo());
        il.Emit(OpCodes.Ret);

        var parames = new[] { Expression.Parameter(typeof(object)), Expression.Parameter(typeof(T)) };
        var body = Expression.Call(dynamicMethod, parames);
        return Expression.Lambda<Func<object, T, Task>>(body, parames).Compile();
    }

    #endregion
}

#endregion