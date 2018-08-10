
using System.ComponentModel;

#pragma warning disable 1591

namespace ZQNB.BaeLib.Dics.Core.Entities
{
    /// <summary>
    /// 字典类型常量
    /// </summary>
    public class DicTypeCodes
    {
        [Description("组织类型")]
        public string OrgType = "OrgType";
        [Description("学段")]
        public string Phase = "Phase";
        [Description("学科")]
        public string Subject = "Subject";
        [Description("年级")]
        public string Grade = "Grade";
        [Description("职称")]
        public string Position = "Position";
        [Description("用户类型")]
        public string UserType = "UserType";
        [Description("角色")]
        public string Role = "Role";
        [Description("主机类型")]
        public string HostType = "HostType";
        [Description("服务类型")]
        public string ServiceType = "ServiceType";
        [Description("资源类型")]
        public string ResourceType = "ResourceType";
        [Description("用户身份")]
        public string UserTitle = "UserTitle";
    }

    /// <summary>
    /// 预置的字典关系
    /// </summary>
    public class DicRelationCodes
    {
        [Description("组织类型-学段")]
        public string OrgTypePhase = "OrgTypePhase";
        [Description("学段-学科")]
        public string PhaseSubject = "PhaseSubject";
        [Description("学段-年级")]
        public string PhaseGrade = "PhaseGrade";
    }
}
