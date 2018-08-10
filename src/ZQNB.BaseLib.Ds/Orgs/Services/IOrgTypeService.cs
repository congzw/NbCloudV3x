using System.Collections.Generic;
using ZQNB.BaseLib.Ds.Orgs.Entities;

namespace ZQNB.BaseLib.Ds.Orgs.Services
{
    /// <summary>
    /// 组织类型服务
    /// </summary>
    public interface IOrgTypeService
    {
        /// <summary>
        /// 获取组织类型
        /// </summary>
        /// <returns></returns>
        IList<OrgType> GetOrgTypes(GetOrgTypesArgs args);
    }

    public class GetOrgTypesArgs
    {
        public bool? Enabled { get; set; }
    }
}
