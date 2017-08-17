using System;
using System.Collections.Generic;

namespace ZQNB.Common.Ioc
{
    /// <summary>
    /// 单例形式的注册表服务接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface INbServiceRegistry<T> : ISingletonDependency
    {
        Func<T> GetDefaultServiceFunc();
        void ResetDefaultServiceFunc(Func<T> func);
        IList<T> GetServices();
        T GetService();
    }
}