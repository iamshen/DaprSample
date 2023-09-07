#nullable disable

using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;

namespace DaprTool.BuildingBlocks.CommonUtility;

#region 常用加密相关助手类

/// <summary>
///     常用加密相关助手类
/// </summary>
public class EncryptHelper
{
    #region 密码加密(不可逆)

    /// <summary>
    ///     密码加密(不可逆)
    /// </summary>
    /// <param name="source">要加密的字符串</param>
    /// <param name="salt">随机盐</param>
    /// <param name="loopCount">要Hash运算的次数的次数</param>
    /// <returns>string</returns>
    public static string PasswordEncrypt(string source, string salt, int loopCount)
    {
        /*
         * 1.算法是首先对密码和随机盐连接成一个字符串，然后对字符串进行升序排序，然后对排序后的字符串SHA1加密,
         * 2.然后对密码+步骤一种的加密结果+随机盐，然后对连接的字符串降序排序，然后对排序的字符串SHA1
         * 3.重复步骤2，直到设定的运算次数
         */
        string[] sourceArray = { source, salt };
        var newStr = string.Join("", sourceArray);
        newStr = Md5Encrypt(newStr);

        for (var i = 1; i < loopCount; i++)
        {
            sourceArray = new[] { source, newStr, salt };
            newStr = string.Join("", sourceArray);
            newStr = string.Join("", newStr.ToCharArray().OrderByDescending(m => m));
            newStr = Md5Encrypt(newStr);
        }

        return newStr;
    }

    #endregion

    #region SHA1加密

    /// <summary>
    ///     SHA1加密
    /// </summary>
    /// <param name="value">要加密的字符串</param>
    /// <returns></returns>
    public static string SHA1Encrypt(string value)
    {
        if (string.IsNullOrEmpty(value))
            return "";

        return SHA1Encrypt(Encoding.UTF8.GetBytes(value));
    }

    /// <summary>
    ///     SHA1加密
    /// </summary>
    /// <param name="value">要加密的值</param>
    /// <returns></returns>
    public static string SHA1Encrypt(byte[] value)
    {
        var encrypt = SHA1.Create();
        var hash = encrypt.ComputeHash(value);
        encrypt.Dispose();

        if (hash == null)
            return "";

        var shaStr = BitConverter.ToString(hash);
        return shaStr.Replace("-", "");
    }

    #endregion

    #region SHA256加密

    /// <summary>
    ///     SHA256加密
    /// </summary>
    /// <param name="value">要加密的字符换</param>
    /// <returns></returns>
    public static string SHA256Encrypt(string value)
    {
        if (string.IsNullOrEmpty(value))
            return "";

        return SHA256Encrypt(Encoding.UTF8.GetBytes(value));
    }

    /// <summary>
    ///     SHA256加密
    /// </summary>
    /// <param name="value">要加密的值</param>
    /// <returns></returns>
    public static string SHA256Encrypt(byte[] value)
    {
        var encrypt = SHA256.Create();
        var hash = encrypt.ComputeHash(value);
        encrypt.Dispose();

        var shaStr = BitConverter.ToString(hash);
        return shaStr.Replace("-", "");
    }

    #endregion

    #region SHA384加密

    /// <summary>
    ///     SHA384加密
    /// </summary>
    /// <param name="value">要加密的字符串</param>
    /// <returns></returns>
    public static string SHA384Encrypt(string value)
    {
        if (string.IsNullOrEmpty(value))
            return "";

        return SHA384Encrypt(Encoding.UTF8.GetBytes(value));
    }

    /// <summary>
    ///     SHA384加密
    /// </summary>
    /// <param name="value">要加密的值</param>
    /// <returns></returns>
    public static string SHA384Encrypt(byte[] value)
    {
        var encrypt = SHA384.Create();
        var hash = encrypt.ComputeHash(value);
        encrypt.Dispose();

        var shaStr = BitConverter.ToString(hash);
        return shaStr.Replace("-", "");
    }

    #endregion

    #region SHA512加密

    /// <summary>
    ///     SHA512加密
    /// </summary>
    /// <param name="value">要加密的字符串</param>
    /// <returns></returns>
    public static string SHA512Encrypt(string value)
    {
        if (string.IsNullOrEmpty(value))
            return "";

        return SHA512Encrypt(Encoding.UTF8.GetBytes(value));
    }

    /// <summary>
    ///     SHA512加密
    /// </summary>
    /// <param name="value">要加密的值</param>
    /// <returns></returns>
    public static string SHA512Encrypt(byte[] value)
    {
        var encrypt = SHA512.Create();
        var hash = encrypt.ComputeHash(value);
        encrypt.Dispose();

        var shaStr = BitConverter.ToString(hash);
        return shaStr.Replace("-", "");
    }

    #endregion

    #region Base64加密

    /// <summary>
    ///     Base64加密
    /// </summary>
    /// <param name="value">要加密的字符串</param>
    /// <returns></returns>
    public static string Base64Encrypt(string value)
    {
        if (string.IsNullOrEmpty(value))
            return "";

        return Base64Encrypt(Encoding.UTF8.GetBytes(value));
    }

    /// <summary>
    ///     Base64加密
    /// </summary>
    /// <param name="value">要加密的值</param>
    /// <returns></returns>
    public static string Base64Encrypt(byte[] value)
    {
        if (value == null || value.Length == 0)
            return "";

        return Convert.ToBase64String(value);
    }

    #endregion

    #region Base64解密

    /// <summary>
    ///     Base64解密
    /// </summary>
    /// <param name="value"></param>
    /// <param name="offset"></param>
    /// <param name="length"></param>
    /// <returns></returns>
    public static byte[] Base64Decrypt(char[] value, int offset, int length)
    {
        if (value == null || value.Length == 0)
            return null;

        return Convert.FromBase64CharArray(value, offset, length);
    }

    /// <summary>
    ///     Base64解密
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static byte[] Base64Decrypt(string value)
    {
        if (string.IsNullOrEmpty(value))
            return null;

        return Convert.FromBase64String(value);
    }

    #endregion

    #region MD5加密

    /// <summary>
    ///     MD加密
    /// </summary>
    /// <param name="value">要加密的字符串</param>
    /// <param name="isShort">是否为16位长度</param>
    /// <returns></returns>
    public static string Md5Encrypt(string value, bool isShort = false)
    {
        if (string.IsNullOrEmpty(value))
            return "";

        return Md5Encrypt(Encoding.UTF8.GetBytes(value), isShort);
    }

    /// <summary>
    ///     MD加密
    /// </summary>
    /// <param name="value">要加密的值</param>
    /// <param name="isShort">是否为16位长度</param>
    /// <returns></returns>
    public static string Md5Encrypt(byte[] value, bool isShort = false)
    {
        var encrypt = MD5.Create();
        var hash = encrypt.ComputeHash(value);
        encrypt.Dispose();

        if (hash == null)
            return "";

        var shaStr = !isShort ? BitConverter.ToString(hash) : BitConverter.ToString(hash, 4, 8);
        return shaStr.Replace("-", "");
    }

    #endregion

    #region AES加解密

    /// <summary>
    ///     AES加密
    /// </summary>
    /// <param name="content">要加密的字符串</param>
    /// <param name="key">加密的密钥</param>
    /// <param name="iv">向量值</param>
    /// <param name="cipherMode">模式</param>
    /// <param name="paddingMode">填充模式</param>
    /// <returns></returns>
    public static string AESEncrypt(string content, string key, byte[] iv = null,
        CipherMode cipherMode = CipherMode.CBC, PaddingMode paddingMode = PaddingMode.PKCS7)
    {
        var keyBytes = Encoding.UTF8.GetBytes(key.PadRight(32));
        var bytes = AESEncrypt(Encoding.UTF8.GetBytes(content), keyBytes, iv, cipherMode, paddingMode);
        return Convert.ToHexString(bytes);
    }

    /// <summary>
    ///     AES加密
    /// </summary>
    /// <param name="content">要加密的内容</param>
    /// <param name="key">加密密钥</param>
    /// <param name="iv">向量</param>
    /// <param name="cipherMode">模式</param>
    /// <param name="paddingMode">填充模式</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static byte[] AESEncrypt(byte[] content, byte[] key, byte[] iv = null,
        CipherMode cipherMode = CipherMode.CBC, PaddingMode paddingMode = PaddingMode.PKCS7)
    {
        if (key == null || key.Length != 32)
            throw new ArgumentException("Invalid parameter", nameof(key));

        using var aes = Aes.Create();
        if (iv != null)
            aes.IV = iv;
        else
            aes.IV = new byte[16];

        aes.Mode = cipherMode;
        aes.Padding = paddingMode;
        using var encryptor = aes.CreateEncryptor(key, aes.IV);
        return encryptor.TransformFinalBlock(content, 0, content.Length);
    }

    /// <summary>
    ///     AES解密
    /// </summary>
    /// <param name="content">要解密的内容</param>
    /// <param name="key">密钥</param>
    /// <param name="iv">向量</param>
    /// <param name="cipherMode">模式</param>
    /// <param name="paddingMode">填充模式</param>
    /// <returns></returns>
    public static string AESDecryption(string content, string key, byte[] iv = null,
        CipherMode cipherMode = CipherMode.CBC, PaddingMode paddingMode = PaddingMode.PKCS7)
    {
        var keyBytes = Encoding.UTF8.GetBytes(key.PadRight(32));
        var contentBytes = Convert.FromHexString(content);
        var result = AESDecryption(contentBytes, keyBytes, iv, cipherMode, paddingMode);

        return Encoding.UTF8.GetString(result);
    }

    /// <summary>
    ///     AES解密
    /// </summary>
    /// <param name="content">要解密的内容</param>
    /// <param name="key">密钥</param>
    /// <param name="iv">向量</param>
    /// <param name="cipherMode">模式</param>
    /// <param name="paddingMode">填充模式</param>
    /// <returns></returns>
    public static byte[] AESDecryption(byte[] content, byte[] key, byte[] iv = null,
        CipherMode cipherMode = CipherMode.CBC, PaddingMode paddingMode = PaddingMode.PKCS7)
    {
        if (key == null || key.Length != 32)
            throw new ArgumentException("Invalid parameter", nameof(key));

        using var aes = Aes.Create();
        if (iv != null)
            aes.IV = iv;
        else
            aes.IV = new byte[16];

        using var decryptor = aes.CreateDecryptor(key, aes.IV);
        return decryptor.TransformFinalBlock(content, 0, content.Length);
    }

    #endregion

    #region HMACSHA1加密

    /// <summary>
    ///     HMACSHA1加密
    /// </summary>
    /// <param name="source"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public static string HMACSHA1Encrypt(string source, string key)
    {
        var byteData = Encoding.UTF8.GetBytes(source);
        var byteKey = Encoding.UTF8.GetBytes(key);
        HMACSHA1 hmac = new(byteKey);
        var bytes = hmac.ComputeHash(byteData);
        var str = Convert.ToHexString(bytes);

        hmac.Dispose();
        return str;
    }

    /// <summary>
    ///     HMACSHA1加密
    /// </summary>
    /// <param name="source"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public static byte[] HMACSHA1Encrypt(byte[] source, byte[] key)
    {
        HMACSHA1 hmac = new(key);
        var bytes = hmac.ComputeHash(source);

        hmac.Dispose();
        return bytes;
    }

    #endregion
}

#endregion