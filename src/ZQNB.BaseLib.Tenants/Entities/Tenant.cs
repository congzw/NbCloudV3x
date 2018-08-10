using System;

namespace ZQNB.BaseLib.Tenants.Entities
{
    /// <summary>
    /// 租户
    /// </summary>
    public class Tenant
    {
        public const string DefaultName = "Default";

        /// <summary>
        /// 名称
        /// </summary>
        public virtual string Name { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public virtual string Description { get; set; }
        /// <summary>
        /// 主机名
        /// </summary>
        public virtual string Host { get; set; }
        /// <summary>
        /// 前缀
        /// </summary>
        public virtual string Prefix { get; set; }
        /// <summary>
        /// 主题
        /// </summary>
        public virtual string Theme { get; set; }
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public virtual string DataConnectionString { get; set; }
        /// <summary>
        /// 是否正常运行
        /// </summary>
        public virtual bool IsRunning { get; set; }
        /// <summary>
        /// 是否已经预置基础数据
        /// </summary>
        public virtual bool IsInited { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime DateCreated { get; set; }
        /// <summary>
        /// 过期时间
        /// </summary>
        public virtual DateTime? DateExpired { get; set; }
        /// <summary>
        /// 自动续期
        /// </summary>
        public virtual bool AutoExtend { get; set; }
        /// <summary>
        /// 自动续期时间 目前单位是月
        /// </summary>
        public virtual int ExtendNum { get; set; }
    }
}
