namespace DaprTool.BuildingBlocks.CommonUtility;

#region 字符串助手类

/// <summary>
///     字符串助手类
/// </summary>
public class StringHelper
{
    #region 私有成员

    /// <summary>
    ///     26个英文字母
    /// </summary>
    private static readonly List<string> _allChars = new()
    {
        "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v",
        "w", "x", "y", "z"
    };

    #endregion

    #region 比较两个字符串是否相等

    /// <summary>
    ///     比较两个字符串是否相等
    /// </summary>
    /// <param name="strA">字符串a</param>
    /// <param name="strB">字符串b</param>
    /// <param name="ignoreCase">是否忽略大小写</param>
    /// <returns></returns>
    public static bool Compare(string strA, string strB, bool ignoreCase = true)
    {
        return string.Compare(strA, strB, ignoreCase) == 0;
    }

    #endregion

    #region 字符串是否为null或者空字符串或者是空格字符串

    /// <summary>
    ///     字符串是否为null或者空字符串或者是空格字符串
    /// </summary>
    /// <param name="str">要判断的字符串</param>
    /// <returns></returns>
    public static bool IsNullOrEmptyOrSpace(string? str)
    {
        if (!string.IsNullOrEmpty(str))
            return string.IsNullOrWhiteSpace(str);
        return true;
    }

    #endregion

    #region 获取替换掉指定范围的字符后的字符串

    /// <summary>
    ///     获取替换掉指定范围的字符后的字符串
    /// </summary>
    /// <param name="source">源字符串</param>
    /// <param name="startIndex">从第几个开是替换</param>
    /// <param name="endIndex">到第几个字符结束</param>
    /// <param name="character">隐藏部分显示的字符</param>
    /// <returns></returns>
    public static string GetDisplayString(string source, int startIndex, int endIndex, char character = '*')
    {
        if (IsNullOrEmptyOrSpace(source))
            return source;

        var stringLength = source.Length;
        if (startIndex < 0)
            startIndex = 0;
        else
            startIndex = Math.Min(startIndex, stringLength);

        if (endIndex < 0)
            endIndex = 0;
        else
            endIndex = Math.Min(endIndex, stringLength);

        if (startIndex >= endIndex)
            return source;

        var startString = source[..startIndex];
        var endString = source[endIndex..];
        var resultString = startString.PadRight(endIndex, character);

        return resultString + endString;
    }

    #endregion

    #region 生成固定长度的随机纯数字字符串(不保证重复)

    /// <summary>
    ///     生成固定长度的随机纯数字字符串(不保证重复)
    /// </summary>
    /// <param name="length">字符串的长度</param>
    /// <returns></returns>
    public static string GetFixLengthNumber(short length)
    {
        var seeds = IntegerHelper.GetRange(0, 10);
        var randomString = "";

        while (true)
        {
            var @char = seeds.OrderBy(x => Guid.NewGuid()).FirstOrDefault(); //打乱seeds的顺序，然后取第一个
            if (randomString == "")
            {
                randomString += @char;
            }
            else
            {
                var prvChar = randomString.Substring(randomString.Length - 1, 1);
                if (prvChar == @char.ToString())
                    continue;
                randomString += @char;
            }

            if (randomString.Length == length)
                break;
        }

        return randomString;
    }

    #endregion

    #region 生成固定长度的纯字母字符串

    /// <summary>
    ///     生成固定长度的纯字母字符串
    /// </summary>
    /// <param name="length">字符串的长度</param>
    /// <returns></returns>
    public static string GetFixLengthString(short length)
    {
        var seedsString = _allChars;
        var randomString = "";

        while (true)
        {
            var @char = seedsString.OrderBy(x => Guid.NewGuid()).FirstOrDefault(); //打乱seeds的顺序，然后取第一个
            if (randomString == "")
            {
                randomString += @char;
            }
            else
            {
                var prvChar = randomString.Substring(randomString.Length - 1, 1);
                if (prvChar == @char)
                    continue;
                randomString += @char;
            }

            if (randomString.Length == length)
                break;
        }

        return randomString;
    }

    #endregion

    #region 生成随机文件名(保证不重复)

    /// <summary>
    ///     生成随机文件名(保证不重复)
    /// </summary>
    /// <returns></returns>
    public static string GetRandomFileName()
    {
        return Path.GetRandomFileName();
    }

    #endregion

    #region 根据GUID生成唯一ID

    /// <summary>
    ///     根据GUID生成唯一ID
    /// </summary>
    /// <returns></returns>
    public static string NewIdWithGuid()
    {
        return NewIdWithGuid(Guid.NewGuid());
    }

    /// <summary>
    ///     根据GUID生成唯一ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public static string NewIdWithGuid(Guid id)
    {
        var bytes = id.ToByteArray();
        long i = 1;
        foreach (var b in bytes)
            i *= b + 1;

        return (i - DateTime.Now.Ticks).ToString("X");
    }

    #endregion

    #region GUID压缩到12位的字符串

    /// <summary>
    ///     GUID压缩到12位的字符串
    /// </summary>
    /// <returns></returns>
    public static string NewIdForShortWithGuid()
    {
        return NewIdForShortWithGuid(Guid.NewGuid());
    }

    /// <summary>
    ///     GUID压缩到12位的字符串
    /// </summary>
    /// <param name="guid">要处理的GUID</param>
    /// <returns></returns>
    public static string NewIdForShortWithGuid(Guid guid)
    {
        var newLong = BitConverter.ToInt64(guid.ToByteArray(), 0);
        var newValueString = Math.Abs(newLong).ToString();

        var newValueStringBytes = new byte[newValueString.Length];
        var newValueStringIndex = 0;

        for (var i = 0; i < newValueString.Length;)
        {
            var byteValue = Convert.ToByte(newValueString[i]);
            var fixedIndex = 1;
            if (i + 1 < newValueString.Length)
            {
                newValueStringBytes[newValueStringIndex] =
                    (byte)((byteValue << 4) + Convert.ToByte(newValueString[i + 1]));
                fixedIndex = 2;
            }
            else
            {
                newValueStringBytes[newValueStringIndex] = byteValue;
            }

            if (i + 3 < newValueString.Length)
                if (Convert.ToInt16(newValueString.Substring(i, 3)) < 256)
                {
                    newValueStringBytes[newValueStringIndex] = Convert.ToByte(newValueString.Substring(i, 3));
                    fixedIndex = 3;
                }

            newValueStringIndex++;
            i += fixedIndex;
        }

        var resultBytes = new byte[newValueStringIndex];
        for (var i = 0; i < newValueStringIndex; i++)
            resultBytes[i] = newValueStringBytes[i];

        var cRtn = Convert.ToBase64String(resultBytes) ?? "";
        cRtn = cRtn.ToLowerInvariant();
        cRtn = cRtn.Replace("/", "");
        cRtn = cRtn.Replace("+", "");
        cRtn = cRtn.Replace("=", "");

        return cRtn.Length == 12 ? cRtn : NewIdForShortWithGuid(Guid.NewGuid());
    }

    #endregion
}

#endregion