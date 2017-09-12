using System;

namespace ZQNB.Common
{
    /// <summary>
    /// Nb注册表
    /// </summary>
    public interface INbRegistry
    {
        /// <summary>
        /// 是否初始化过
        /// </summary>
        bool Inited { get; set; }
        /// <summary>
        /// 初始化逻辑
        /// </summary>
        Action<INbRegistry> InitAction { get; set; }  

        /// <summary>
        /// 当前项目前缀
        /// </summary>
        string CurrentProjectPrefix { get; set; }

        /// <summary>
        /// 当前平台的产品版本
        /// </summary>
        string CurrentPlatformVersion { get; set; }
    }

    public class NbRegistry : INbRegistry
    {
        public Action<INbRegistry> InitAction { get; set; }
        public string CurrentProjectPrefix { get; set; }
        public string CurrentPlatformVersion { get; set; }
        public bool Inited { get; set; }

        internal void Init()
        {
            lock (_lock)
            {
                if (Inited)
                {
                    return;
                }
                //todo read from config or...
                CurrentProjectPrefix = "ZQNB";
                CurrentPlatformVersion = "3.2";

                if (InitAction != null)
                {
                    InitAction(this);
                }
                Inited = true;
            }
        }
        
        #region static helpers

        private static object _lock = new object();

        private static INbRegistry _instance = new NbRegistry();
        /// <summary>
        /// Singleton
        /// </summary>
        public static INbRegistry Instance
        {
            get
            {
                lock (_lock)
                {
                    if (!_instance.Inited)
                    {
                        NbRegistry instance = _instance as NbRegistry;
                        if (instance != null)
                        {
                            instance.Init();
                        }
                    }
                }
                return _instance;
            }
            set { _instance = value; }
        }

        #endregion
    }

    #region common extensions

    public static class NbRegistryExtensions
    {
        //todo
    }

    #endregion
}