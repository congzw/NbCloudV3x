using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;

namespace ZQNB.Common.Web.Mvc
{
    public abstract class NbAreaRegistration : AreaRegistration
    {
        #region AreaRegistration Customization

        /// <summary>
        /// 存放AreaContext,将参考排序后的AreaRegistration的Order，注册进入路由表
        /// </summary>
        protected static List<AreaRegistrationContext> AreaRegistrationContexts = new List<AreaRegistrationContext>();
        /// <summary>
        /// 存放AreaRegistration，属性Order用来排序
        /// </summary>
        public static List<NbAreaRegistration> AreaRegistrations = new List<NbAreaRegistration>();
        /// <summary>
        /// 按如下方式替换使用：AreaRegistration.RegisterAllAreas => NbAreaRegistration.RegisterAllAreasInvoke();
        /// </summary>
        public static void RegisterAllAreasInvoke()
        {
            //代替子类调用RegisterAllAreas
            RegisterAllAreas();

            //排序后注册
            RegisterByOrder();
        }

        /// <summary>
        /// 推迟到此时，Area的注册内容才被调用
        /// </summary>
        private static void RegisterByOrder()
        {
            List<AreaOrderIndex> areaOrders = new List<AreaOrderIndex>();
            for (int i = 0; i < AreaRegistrations.Count; i++)
            {
                //order index
                areaOrders.Add(new AreaOrderIndex() { Index = i, Order = AreaRegistrations[i].Order, NbAreaRegistration = AreaRegistrations[i] });
            }

            List<AreaOrderIndex> sortedOrders = areaOrders.OrderBy(x => x.Order).ToList();
            allAreaNames.Clear();

            for (int i = 0; i < sortedOrders.Count; i++)
            {
                // => 劫持的RegisterArea()此时被调用
                //推迟到此时，Area才真正的进行了注册
                int index = sortedOrders[i].Index;
                int order = sortedOrders[i].Order;

                AreaRegistrations[index].RegisterWebApis();
                AreaRegistrations[index].RegisterFilters();
                AreaRegistrations[index].RegisterRoutes(AreaRegistrationContexts[index]);

                AreaRegistrations[index].RegisterBundles();
                AreaRegistrations[index].RegisterAuths();
                AreaRegistrations[index].RegisterOthers();

                allAreaNames.Add(AreaRegistrationContexts[index].AreaName);
                TraceMessage(AreaRegistrations[index].AreaNameFriendly, order, index);
            }
        }

        #endregion

        #region ui hot points

        private string _areaNameFriendly;
        //用于显示模块的友好名称
        //不支持子类重写，而统一由MetaCustomization统一设置
        /// <summary>
        /// 用于显示模块的友好名称
        /// </summary>
        public string AreaNameFriendly
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_areaNameFriendly))
                {
                    _areaNameFriendly = AreaName;
                }
                return _areaNameFriendly;
            }
            set { _areaNameFriendly = value; }
        }

        #endregion

        //模块的加载顺序
        /// <summary>
        /// 模块的加载顺序
        /// </summary>
        public virtual int Order { get { return 10; } }

        /// <summary>
        /// 父类自定义实现将其劫持,子类不应再进行调用；而只需要根据需要实现相应的RegisterXxxs方法即可
        /// </summary>
        /// <param name="context"></param>
        public override void RegisterArea(AreaRegistrationContext context)
        {
            //此时的Area和上下文都被缓存起来，在RegisterAllAreasInvoke调用时才真正的进行注册
            AreaRegistrationContexts.Add(context);
            AreaRegistrations.Add(this);
        }

        //------------helpers-------------

        protected virtual void RegisterWebApis()
        {
            var defaultProjectPrefix = NbRegistry.Instance.CurrentProjectPrefix;
            var areaNamespace = string.Format("{0}.Web.Areas.{1}", defaultProjectPrefix, AreaName);

            GlobalConfiguration.Configuration.Routes.MapHttpRoute(
                AreaName + "_api_default",
                "api/" + AreaName + "/{controller}/{action}/{id}",
                new
                {
                    id = RouteParameter.Optional,
                    area = AreaName,
                    Namespace = new[] { string.Format("{0}.Api", areaNamespace) } //Used By NamespaceHttpControllerSelector
                });
        }
        protected virtual void RegisterFilters()
        {
        }
        protected virtual void RegisterRoutes(AreaRegistrationContext context)
        {
            var defaultProjectPrefix = NbRegistry.Instance.CurrentProjectPrefix;

            context.MapRoute(
                name: AreaName + "_default",
                url: AreaName + "/{controller}/{action}",
                defaults: new { area = AreaName },
                namespaces: new[] { string.Format("{0}.Web.Areas.{1}.Controllers", defaultProjectPrefix, AreaName) }
                );

            context.MapRoute(
                name: AreaName + "_multi_site_default",
                url: "{site}/" + AreaName + "/{controller}/{action}",
                defaults: new { area = AreaName },
                namespaces: new[] { string.Format("{0}.Web.Areas.{1}.Controllers", defaultProjectPrefix, AreaName) }
                );

        }
        protected virtual void RegisterBundles()
        {
        }
        protected virtual void RegisterAuths()
        {
        }
        protected virtual void RegisterOthers()
        {
        }

        #region trace messages

        private static readonly IList<string> allAreaNames = new List<string>();
        private static void TraceMessage(string area, int order, int index)
        {
            UtilsLogger.LogMessage(string.Format("registering area:{0}, order : {1}, original index : {2}", area, order, index));
        }

        #endregion

        class AreaOrderIndex
        {
            public int Order { get; set; }
            public int Index { get; set; }
            public NbAreaRegistration NbAreaRegistration { get; set; }
        }
    }
}