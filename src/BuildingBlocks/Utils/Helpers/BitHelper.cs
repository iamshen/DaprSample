using System.Collections;

namespace DaprTool.BuildingBlocks.Utils;

#region 位运算相关的助手类

/// <summary>
///     位运算相关的助手类
/// </summary>
public class BitHelper
{
    #region 私有变量

    /// <summary>
    ///     一个字节的大小
    /// </summary>
    private const byte bitLength = 8;

    #endregion

    #region 私有方法

    /// <summary>
    ///     获取bit的位置
    /// </summary>
    /// <param name="byteLength">类型的大小</param>
    /// <param name="index">要从第几位开始读取</param>
    /// <returns></returns>
    private static int GetIndex(int byteLength, int index)
    {
        if (index < 0)
            throw new ArgumentOutOfRangeException(nameof(index));

        //类型大小对应的bit的大小,一个字节8bit
        var bitSize = byteLength * bitLength;

        if (index > bitSize)
            throw new ArgumentOutOfRangeException(nameof(index));

        //如果当前系统架构是小端模式，那么按位读取的时候是从右边往左边读取，
        //因为小端模式是高位字节在前(左)，低位字节在后(右)
        //大端模式则和小端模式相反
        if (BitConverter.IsLittleEndian)
            return bitSize - index - 1;
        return index;
    }

    /// <summary>
    ///     获取数据指定位置的bit值
    /// </summary>
    /// <param name="value">要获取的原始数据</param>
    /// <param name="typeLength">原始数据的类型的长度</param>
    /// <param name="index">第几个位置的数据</param>
    /// <returns></returns>
    private static bool GetBitValue(long value, byte typeLength, int index)
    {
        var bitValue = value >> GetIndex(typeLength, index);
        return (bitValue & 1) == 1;
    }

    /// <summary>
    ///     设置数据某一个位置的bit值
    /// </summary>
    /// <param name="original">要操作的原始数据</param>
    /// <param name="value">要设置的值</param>
    /// <param name="typeLength">原始数据的类型长度</param>
    /// <param name="index">第几个位置的数据</param>
    /// <returns></returns>
    private static long SetBitValue(long original, bool value, byte typeLength, int index)
    {
        long newValue = 1 << GetIndex(typeLength, index);
        return value ? original | newValue : original & ~newValue;
    }

    #endregion

    #region 获取指定位置的bit值

    /// <summary>
    ///     获取指定位置的bit值
    /// </summary>
    /// <param name="value">要读取的整形</param>
    /// <param name="index">要读取的第几位的值</param>
    /// <returns></returns>
    public static bool GetBitValue(long value, int index)
    {
        return GetBitValue(value, sizeof(long), index);
    }

    /// <summary>
    ///     获取指定位置的bit值
    /// </summary>
    /// <param name="value">要读取的整形</param>
    /// <param name="index">要读取的第几位的值</param>
    /// <returns></returns>
    public static bool GetBitValue(int value, int index)
    {
        return GetBitValue(value, sizeof(int), index);
    }

    /// <summary>
    ///     获取指定位置的bit值
    /// </summary>
    /// <param name="value">要读取的整形</param>
    /// <param name="index">要读取的第几位的值</param>
    /// <returns></returns>
    public static bool GetBitValue(short value, int index)
    {
        return GetBitValue(value, sizeof(short), index);
    }

    /// <summary>
    ///     获取指定位置的bit值
    /// </summary>
    /// <param name="value">要读取的byte数组</param>
    /// <param name="index">要读取的第几位的值</param>
    /// <returns></returns>
    public static bool GetBitValue(byte[] value, int index)
    {
        var bitArray = new BitArray(value);
        return bitArray.Get(GetIndex(value.Length, index));
    }

    #endregion

    #region 获取数据的二进制数组

    /// <summary>
    ///     获取数据的二进制数组
    /// </summary>
    /// <param name="value">要转换的数据</param>
    /// <returns></returns>
    public static List<bool> GetBitValue(long value)
    {
        return GetBitValue(BitConverter.GetBytes(value));
    }

    /// <summary>
    ///     获取数据的二进制数组
    /// </summary>
    /// <param name="value">要转换的数据</param>
    /// <returns></returns>
    public static List<bool> GetBitValue(int value)
    {
        return GetBitValue(BitConverter.GetBytes(value));
    }

    /// <summary>
    ///     获取数据的二进制数组
    /// </summary>
    /// <param name="value">要转换的数据</param>
    /// <returns></returns>
    public static List<bool> GetBitValue(short value)
    {
        return GetBitValue(BitConverter.GetBytes(value));
    }

    /// <summary>
    ///     获取数据的二进制数组
    /// </summary>
    /// <param name="value">要转换的数据</param>
    /// <returns></returns>
    public static List<bool> GetBitValue(byte[] value)
    {
        var bitArray = new BitArray(value);
        var bitValueList = new List<bool>();

        for (var i = 0; i < bitArray.Count; i++)
            bitValueList.Add(bitArray.Get(i));

        return bitValueList;
    }

    #endregion

    #region 设置数据的某一个bit的值

    /// <summary>
    ///     设置数据的某一个bit的值
    /// </summary>
    /// <param name="original">要修改的数据</param>
    /// <param name="value">要设置的值</param>
    /// <param name="index">第几位的值</param>
    /// <returns></returns>
    public static long SetBitValue(long original, bool value, int index)
    {
        return SetBitValue(original, value, sizeof(long), index);
    }

    /// <summary>
    ///     设置数据的某一个bit的值
    /// </summary>
    /// <param name="original">要修改的数据</param>
    /// <param name="value">要设置的值</param>
    /// <param name="index">第几位的值</param>
    /// <returns></returns>
    public static int SetBitValue(int original, bool value, int index)
    {
        return (int)SetBitValue(original, value, sizeof(int), index);
    }

    /// <summary>
    ///     设置数据的某一个bit的值
    /// </summary>
    /// <param name="original">要修改的数据</param>
    /// <param name="value">要设置的值</param>
    /// <param name="index">第几位的值</param>
    /// <returns></returns>
    public static short SetBitValue(short original, bool value, int index)
    {
        return (short)SetBitValue(original, value, sizeof(short), index);
    }

    /// <summary>
    ///     设置数据的某一个bit的值
    /// </summary>
    /// <param name="original">要修改的数据</param>
    /// <param name="value">要设置的值</param>
    /// <param name="index">第几位的值</param>
    /// <returns></returns>
    public static byte[] SetBitValue(byte[] original, bool value, int index)
    {
        var bitArray = new BitArray(original);
        bitArray.Set(GetIndex(original.Length, index), value);

        var bytes = new byte[original.Length];
        bitArray.CopyTo(bytes, 0);

        return bytes;
    }

    /// <summary>
    ///     设置数据的所有bit为指定值
    /// </summary>
    /// <param name="original">要修改的数据</param>
    /// <param name="value">要设置的值</param>
    /// <returns></returns>
    public static long SetBitValue(long original, bool value)
    {
        return BitConverter.ToInt64(SetBitValue(BitConverter.GetBytes(original), value), 0);
    }

    /// <summary>
    ///     设置数据的所有bit为指定值
    /// </summary>
    /// <param name="original">要修改的数据</param>
    /// <param name="value">要设置的值</param>
    /// <returns></returns>
    public static int SetBitValue(int original, bool value)
    {
        return BitConverter.ToInt32(SetBitValue(BitConverter.GetBytes(original), value), 0);
    }

    /// <summary>
    ///     设置数据的所有bit为指定值
    /// </summary>
    /// <param name="original">要修改的数据</param>
    /// <param name="value">要设置的值</param>
    /// <returns></returns>
    public static short SetBitValue(short original, bool value)
    {
        return BitConverter.ToInt16(SetBitValue(BitConverter.GetBytes(original), value), 0);
    }

    /// <summary>
    ///     设置数据的所有bit为指定值
    /// </summary>
    /// <param name="original">要修改的数据</param>
    /// <param name="value">要设置的值</param>
    /// <returns></returns>
    public static byte[] SetBitValue(byte[] original, bool value)
    {
        var bitArray = new BitArray(original);
        bitArray.SetAll(value);

        var bytes = new byte[original.Length];
        bitArray.CopyTo(bytes, 0);

        return bytes;
    }

    #endregion

    #region 获取数据的二进制字符串

    /// <summary>
    ///     获取数据的二进制字符串
    /// </summary>
    /// <param name="value">要转二进制字符串的数据</param>
    /// <returns></returns>
    public static string GetBitString(long value)
    {
        var bitString = Convert.ToString(value, 2);
        return bitString.PadLeft(8 * bitLength, '0');
    }

    /// <summary>
    ///     获取数据的二进制字符串
    /// </summary>
    /// <param name="value">要转二进制字符串的数据</param>
    /// <returns></returns>
    public static string GetBitString(int value)
    {
        var bitString = Convert.ToString(value, 2);
        return bitString.PadLeft(4 * bitLength, '0');
    }

    /// <summary>
    ///     获取数据的二进制字符串
    /// </summary>
    /// <param name="value">要转二进制字符串的数据</param>
    /// <returns></returns>
    public static string GetBitString(short value)
    {
        var bitString = Convert.ToString(value, 2);
        return bitString.PadLeft(2 * bitLength, '0');
    }

    #endregion

    #region 二进制字符串转整形

    /// <summary>
    ///     二进制字符串转short
    /// </summary>
    /// <param name="bitString">字符串</param>
    /// <returns></returns>
    public static short ToInt16(string bitString)
    {
        return Convert.ToInt16(bitString, 2);
    }

    /// <summary>
    ///     二进制字符串转int
    /// </summary>
    /// <param name="bitString">字符串</param>
    /// <returns></returns>
    public static int ToInt32(string bitString)
    {
        return Convert.ToInt32(bitString, 2);
    }

    /// <summary>
    ///     二进制字符串转long
    /// </summary>
    /// <param name="bitString">字符串</param>
    /// <returns></returns>
    public static long ToInt64(string bitString)
    {
        return Convert.ToInt64(bitString, 2);
    }

    #endregion
}

#endregion