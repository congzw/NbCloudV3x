using System;
using System.Web;

namespace ZQNB.Common
{
    /// <summary>
    /// ���԰�����
    /// </summary>
    public class MyDebugHelper
    {
        private static string Config_Common_DebugMode = "Config.Common.DebugMode";
        private static bool? _isDebugModeInConfig = null;
        /// <summary>
        /// Site�Ƿ��ڵ���״̬
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
            //���û�����ã����url
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