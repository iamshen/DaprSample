namespace DaprTool.BuildingBlocks.CommonUtility;

/// <summary>
///     文件帮助类
/// </summary>
public class FileHelper
{
    #region 创建目录

    /// <summary>
    ///     创建目录
    /// </summary>
    /// <param name="dirName"> 目录名称 </param>
    /// <returns> </returns>
    public static string CreateIfNotExists(string dirName)
    {
        var path = Path.Combine(Environment.CurrentDirectory, $"authentication\\{dirName}\\");
        if (!Directory.Exists(path)) Directory.CreateDirectory(path);
        return path;
    }

    #endregion 创建目录
}