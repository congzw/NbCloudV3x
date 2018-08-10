using ZQNB.Common.Data.Model;

namespace ZQNB.BaseLib.Ds.Orgs.Entities
{
    public class Org : NbEntity<Org>
    {
        public virtual string Name { get; set; }
        public virtual string OrgTypeCode { get; set; }
    }

    public class OrgType : NbEntity<OrgType>
    {
        public virtual string Code { get; set; }
        public virtual string Name { get; set; }
        public virtual bool Enabled { get; set; }
    }
}
