using System;
using System.Web;

namespace ZQNB.Common
{
    /// <summary>
    /// 调试帮助类
    /// </summary>
    public class MyDebugHelper
    {
        private static string Config_Common_DebugMode = "Config.Common.DebugMode";
        private static bool? _isDebugModeInConfig = null;
        /// <summary>
        /// Site是否处于调试状态
        /// </summary>
        /// <returns></returns>
        public static bool IsDebugMode(bool detectHttpRequest = false)
        {
            if (_isDebugModeInConfig == null)
            {
                _isDebugModeInConfig = MyConfigHelper.GetAppSettingValueAsBool(Config_Common_DebugMode, false);
            }

            if (_isDebugModeInConfig.Value)
            {
                return true;
            }

            if (!detectHttpRequest)
            {
                return false;
            }

            return IsDebugModeFromHttpRequest();
        }

        private static bool IsDebugModeFromHttpRequest()
        {
            //如果没有启用，侦测url
            if (HttpContext.Current == null)
            {
                return false;
            }
            var value = HttpContext.Current.Request.Params.Get("debug");
            var debug = "true".Equals(value, StringComparison.OrdinalIgnoreCase);
            return debug;
        }

    }

    public interface IDebugHelper
    {
        bool IsDebugMode();
    }

    public class DebugHelper:IDebugHelper
    {
        public bool IsDebugMode()
        {
            throw new NotImplementedException();
        }

        public static IDebugHelper Instance = new DebugHelper();
    }
}