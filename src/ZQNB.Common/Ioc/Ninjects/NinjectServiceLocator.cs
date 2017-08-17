using System;
using System.Collections.Generic;
using Ninject;

namespace ZQNB.Common.Ioc.Ninjects
{
    /// <summary>
    /// NinjectServiceLocator
    /// </summary>
    public class NinjectServiceLocator : ServiceLocatorImplBase
    {
        public Func<IKernel> KernelFunc { get; private set; }

        /// <summary>
        /// CTOR
        /// </summary>
        /// <param name="kernelFunc"></param>
        public NinjectServiceLocator(Func<IKernel> kernelFunc)
        {
            KernelFunc = kernelFunc;
        }

        protected override object DoGetInstance(Type serviceType, string key)
        {
            var kernel = KernelFunc.Invoke();
            if (key == null)
            {
                return kernel.TryGet(serviceType);
            }
            return kernel.TryGet(serviceType, key);
        }

        protected override IEnumerable<object> DoGetAllInstances(Type serviceType)
        {
            var kernel = KernelFunc.Invoke();
            return kernel.GetAll(serviceType);
        }
    }
}
