using System.Collections.Generic;
using ZQNB.Common.Data.Model;

namespace ZQNB.BaseLib.Ds.Sites.Entities
{
    /// <summary>
    /// 站群
    /// </summary>
    public class SiteGroup : NbEntity<SiteGroup>
    {
        /// <summary>
        /// 站群
        /// </summary>
        public SiteGroup()
        {
            Sites = new List<Site>();
        }

        /// <summary>
        /// 站群Uri，默认为http://localhost
        /// </summary>
        public virtual string BaseUri { get; set; }
        /// <summary>
        /// 备注信息
        /// </summary>
        public virtual string Description { get; set; }

        /// <summary>
        /// 该站群下的站点
        /// </summary>
        public virtual IList<Site> Sites { get; set; }
    }
}
