using System;
using System.Collections.Generic;
using ZQNB.Common.Data.Model;

namespace ZQNB.BaseLib.Ds.Sites.Entities
{
    /// <summary>
    /// 站点
    /// </summary>
    public class Site : NbEntity<Site>
    {
        /// <summary>
        /// 站点
        /// </summary>
        public Site()
        {
            SiteOrgs = new List<SiteOrg>();
        }

        /// <summary>
        /// 站点名称(唯一名)
        /// 用于展示
        /// </summary>
        public virtual string Name { get; set; }
        /// <summary>
        /// 自定义名称
        /// 用于url的生成 比如mh、whhc
        /// </summary>
        public virtual string UrlName { get; set; }
        /// <summary>
        /// 排序码
        /// </summary>
        public virtual double SortNum { get; set; }
        /// <summary>
        /// 站点的描述
        /// </summary>
        public virtual string Description { get; set; }
        
        /// <summary>
        /// 关联的组织关系
        /// </summary>
        public virtual IList<SiteOrg> SiteOrgs { get; set; }
    }

    public class SiteOrg
    {
        public virtual Guid SiteId { get; set; }
        public virtual Guid OrgId { get; set; }
    }
}
