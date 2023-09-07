using System.Runtime.InteropServices;

namespace DaprTool.BuildingBlocks.CommonUtility.ValueObjects
{
    #region .Net运行时信息对象

    /// <summary>
    /// .Net运行时信息对象
    /// </summary>
    public record RuntimeInfoObject
    {
        /// <summary>
        /// .Net框架描述
        /// </summary>
        public string FrameworkDescription { get; set; } = RuntimeInformation.FrameworkDescription;

        /// <summary>
        /// 运行时目录
        /// </summary>
        public string RuntimeDirectory { get; set; } = RuntimeEnvironment.GetRuntimeDirectory();

        /// <summary>
        /// 公共语言运行时版本
        /// </summary>
        public string HostClrVersion { get; set; } = RuntimeEnvironment.GetSystemVersion();
    }

    #endregion
}
