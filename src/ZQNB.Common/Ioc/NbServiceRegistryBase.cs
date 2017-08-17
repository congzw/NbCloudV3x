using System;
using System.Collections.Generic;
using System.Linq;

namespace ZQNB.Common.Ioc
{
    /// <summary>
    /// 单例形式的注册表服务接口的默认实现基类，支持继承扩展，每个继承子类都是一个单例
    /// </summary>
    /// <typeparam name="TServiceRegistry"></typeparam>
    /// <typeparam name="TService"></typeparam>
    public abstract class NbServiceRegistryBase<TServiceRegistry, TService> : INbServiceRegistry<TService>
        where TServiceRegistry : NbServiceRegistryBase<TServiceRegistry, TService>, new()
    {
        private Func<TService> _theFunc = null;
        private bool _iocHasSomeImpls = true;
        private bool _hasBeenReseted = false;
        private bool _logFuncMessage = false;
        private bool _logIocMessage = false;

        private Func<TService> GetServiceFunc()
        {
            if (_hasBeenReseted)
            {
                return _theFunc;
            }

            _theFunc = GetDefaultServiceFunc();
            return _theFunc;
        }

        /// <summary>
        /// GetDefaultServiceFunc
        /// </summary>
        /// <returns></returns>
        public abstract Func<TService> GetDefaultServiceFunc();
        /// <summary>
        /// ResetDefaultServiceFunc
        /// </summary>
        /// <param name="func"></param>
        public void ResetDefaultServiceFunc(Func<TService> func)
        {
            _theFunc = func;
            _hasBeenReseted = true;
        }

        /// <summary>
        /// GetServices
        /// </summary>
        /// <returns></returns>
        public IList<TService> GetServices()
        {
            var allServices = new List<TService>();
            if (_iocHasSomeImpls)
            {
                if (CoreServiceProvider.Current != null)
                {
                    List<TService> serviceImpls = CoreServiceProvider.Current.GetAllInstances<TService>().ToList();
                    //Count > 0，意味着定义了其他的Provider
                    //否则，下次没有机会在执行此代码块
                    _iocHasSomeImpls = serviceImpls.Count > 0;
                    //log for ioc providers
                    for (int i = 0; i < serviceImpls.Count; i++)
                    {
                        var serviceImpl = serviceImpls[i];
                        if (!_logIocMessage)
                        {
                            Log(string.Format("CoreServiceProvider impls for {0} => {1}/{2} => {3}", typeof(TService).Name, i + 1, serviceImpls.Count, serviceImpl.GetType().FullName));
                            if (i == serviceImpls.Count - 1)
                            {
                                _logIocMessage = true;
                            }
                        }
                        allServices.Add(serviceImpl);
                    }
                }
            }

            //默认的提供，放在最后面
            var serviceFunc = GetServiceFunc();
            var service = serviceFunc.Invoke();
            if (service != null)
            {
                if (!_logFuncMessage)
                {
                    //log for default providers
                    string defaultImplMessage = string.Format("default func impl for {0} is {1}", typeof(TService).Name, service.GetType().Name);
                    Log(defaultImplMessage);
                    _logFuncMessage = true;
                }
                allServices.Add(service);
            }
            return allServices;
        }
        public TService GetService()
        {
            var services = GetServices();
            return services.FirstOrDefault();
        }

        /// <summary>
        /// The Singleton
        /// </summary>
        public static TServiceRegistry Instance
        {
            get
            {
                if (Singleton<TServiceRegistry>.Instance == null)
                {
                    Singleton<TServiceRegistry>.Instance = new TServiceRegistry();
                }
                return Singleton<TServiceRegistry>.Instance;
            }
        }

        #region log

        //log
        private bool _logging = true;
        /// <summary>
        /// 是否启用日志
        /// </summary>
        /// <param name="logging"></param>
        public void EnableLog(bool logging)
        {
            _logging = logging;
        }
        private void Log(string message)
        {
            if (_logging)
            {
                UtilsLogger.LogMessage(message);
            }
        }

        #endregion
    }
}
