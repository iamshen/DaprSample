namespace DaprTool.BuildingBlocks.Utils;

#region Task相关的助手类

/// <summary>
///     Task相关的助手类
/// </summary>
public static class TaskHelper
{
    #region 重复执行方法

    /// <summary>
    ///     重复执行方法
    /// </summary>
    /// <param name="action">要执行的方法</param>
    /// <param name="exceptionContinue">如果发生异常后是否继续</param>
    /// <returns></returns>
    public static Task Duplication(Func<Task<bool>> action, bool exceptionContinue = true)
    {
        return Duplication(action, ex => Task.FromResult(true));
    }

    /// <summary>
    ///     重复执行方法
    /// </summary>
    /// <param name="action">要执行的方法</param>
    /// <param name="exceptionAction">执行发生异常后调用的方法</param>
    /// <returns></returns>
    public static async Task Duplication(Func<Task<bool>> action,
        Func<Exception, Task<bool>>? exceptionAction = default)
    {
        var policyResult = true;
        do
        {
            try
            {
                policyResult = await action.Invoke();
            }
            catch (Exception ex)
            {
                if (exceptionAction is not null)
                    policyResult = await exceptionAction.Invoke(ex);
            }
        } while (policyResult);
    }

    #endregion
}

#endregion