namespace DaprTool.BuildingBlocks.CommonUtility;

#region 字节相关的助手类

/// <summary>
///     字节相关的助手类
/// </summary>
public class ByteHelper
{
    #region 字节数组转16进制字符串

    /// <summary>
    ///     字节数组转16进制字符串
    /// </summary>
    /// <param name="bytes"></param>
    /// <param name="separator"></param>
    /// <returns></returns>
    public static string BytesToHexString(byte[] bytes, string separator = "")
    {
        if (bytes == null || bytes.Length == 0)
            return "";

        var hexStringList = new List<string>();
        foreach (var item in bytes)
            hexStringList.Add(item.ToString("X2"));

        return string.Join(separator, hexStringList);
    }

    #endregion
}

#endregion