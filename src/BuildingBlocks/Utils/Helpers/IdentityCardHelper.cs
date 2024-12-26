using System.Globalization;

namespace DaprTool.BuildingBlocks.Utils.Helpers;

public static class IdentityCardHelper
{
    /// <summary>
    /// 根据身份证号码计算出生日期和性别信息。
    /// </summary>
    /// <param name="idCardNumber">身份证号码</param>
    /// <returns>包含生日和性别的对象</returns>
    public static (long? BirthTimestamp, string Gender, bool IsValid) ParseIdCard(string idCardNumber)
    {
        // 校验身份证号码格式
        if (string.IsNullOrWhiteSpace(idCardNumber) || (idCardNumber.Length != 18 && idCardNumber.Length != 15))
        {
            return (null, "未知", false);
        }

        try
        {
            string birthDateString;
            if (idCardNumber.Length == 18)
            {
                // 18位身份证号码，出生日期是第7到14位
                birthDateString = idCardNumber.Substring(6, 8);
            }
            else
            {
                // 15位身份证号码，出生日期是第7到12位
                birthDateString = "19" + idCardNumber.Substring(6, 6);
            }

            // 将字符串转为日期
            DateTime birthDate = DateTime.ParseExact(birthDateString, "yyyyMMdd", CultureInfo.InvariantCulture);
            // 转换为 Unix 时间戳
            long birthTimestamp = ((DateTimeOffset)birthDate).ToUnixTimeSeconds();

            // 计算性别（18位第17位或15位第15位，奇数为男，偶数为女）
            int genderCode = idCardNumber.Length == 18
                ? int.Parse(idCardNumber.Substring(16, 1))
                : int.Parse(idCardNumber.Substring(14, 1));

            string gender = (genderCode % 2 == 0) ? "女性" : "男性";

            return (birthTimestamp, gender, true);
        }
        catch
        {
            // 捕获任何解析失败的异常
            return (0, "未知", false);
        }
    }
}