using ZQNB.Common.Data.Model;

namespace ZQNB.BaseLib.Ds.Sites.Entities
{
    /// <summary>
    /// GlobalCatalog
    /// </summary>
    public class GlobalCatalog : NbEntity<GlobalCatalog>
    {
        /// <summary>
        /// 全局目录（GC）
        /// </summary>
        public virtual string BaseUri { get; set; }
        /// <summary>
        /// 备注信息
        /// </summary>
        public virtual string Description { get; set; }
        /// <summary>
        /// 是否启用分布式Gc,自动创建的默认那条记录，永远为false。
        /// 后期由Gc端设置为true，也可以作为判断是否启用站群的唯一标志。
        /// </summary>
        public virtual bool Enabled { get; set; }
    }
}
