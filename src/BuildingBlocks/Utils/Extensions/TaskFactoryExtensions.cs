using DaprTool.BuildingBlocks.Utils;

namespace System.Threading.Tasks;

#region TaskFactory相关的扩展方法

/// <summary>
///     TaskFactory相关的扩展方法
/// </summary>
public static class TaskFactoryExtensions
{
    #region 重复执行方法

    /// <summary>
    ///     重复执行方法
    /// </summary>
    /// <param name="factory"></param>
    /// <param name="action">要执行的方法</param>
    /// <param name="exceptionContinue">如果发生异常后是否继续</param>
    /// <returns></returns>
    public static Task Duplication(this TaskFactory factory, Func<Task<bool>> action, bool exceptionContinue = true)
    {
        return TaskHelper.Duplication(action, exceptionContinue);
    }

    /// <summary>
    ///     重复执行方法
    /// </summary>
    /// <param name="factory"></param>
    /// <param name="action">要执行的方法</param>
    /// <param name="exceptionAction">执行发生异常后调用的方法</param>
    /// <returns></returns>
    public static Task Duplication(this TaskFactory factory, Func<Task<bool>> action,
        Func<Exception, Task<bool>>? exceptionAction = default)
    {
        return TaskHelper.Duplication(action, exceptionAction);
    }

    #endregion
}

#endregion