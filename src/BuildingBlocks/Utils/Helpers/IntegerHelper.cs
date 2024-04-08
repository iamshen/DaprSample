using System.Runtime.CompilerServices;

namespace DaprTool.BuildingBlocks.Utils;

#region 整形相关操作助手

/// <summary>
///     整形相关操作助手
/// </summary>
public class IntegerHelper
{
    #region 生成随机数

    /// <summary>
    ///     生成随机数
    /// </summary>
    /// <param name="min">随机数的最小数</param>
    /// <param name="max">随机数的最大数</param>
    /// <returns></returns>
    public static int GetRandomNumber(int min, int max)
    {
        Random rdm = new((int)GetSeed());
        return rdm.Next(min, max + 1);
    }

    #endregion

    #region GUID转成一个数字

    /// <summary>
    ///     GUID转成一个数字
    /// </summary>
    /// <param name="guid">要处理的GUID</param>
    /// <returns></returns>
    public static long GuidToNumber(Guid guid)
    {
        return BitConverter.ToInt64(guid.ToByteArray(), 0);
    }

    #endregion

    #region 生成一组连续的整数

    /// <summary>
    ///     生成一组连续的整数
    /// </summary>
    /// <param name="start">开始的整数</param>
    /// <param name="end">结束的整数(小于end)</param>
    /// <returns></returns>
    public static List<int> GetRange(int start, int end)
    {
        return Enumerable.Range(start, end).ToList();
    }

    #endregion

    #region 获取一个随机数种子

    /// <summary>
    ///     获取一个随机数种子
    /// </summary>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static long GetSeed()
    {
        var buffer = Guid.NewGuid().ToByteArray();
        return BitConverter.ToInt64(buffer, 0);
    }

    #endregion
}

#endregion