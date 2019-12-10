using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.EntityModel.BaseRole
{
    /// <summary>
    /// 系统角色表
    /// </summary>
    [SugarTable("SystemRole")]
    public class SystemRole
    {
        public SystemRole()
        {
            IsUse = 1;
            ValidFlag = 1;
            LastUpdateTime = DateTime.Now;
        }
        /// <summary>
        /// 角色ID
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int RoleID { get; set; }
        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleName { get; set; }
        /// <summary>
        /// 角色描述
        /// </summary>
        public string RoleDesc { get; set; }
        /// <summary>
        /// 是否为管理员
        /// </summary>
        public int AdminFlag { get; set; }
        /// <summary>
        /// 是否禁用
        /// </summary>
        public int IsUse { get; set; }

        /// <summary>
        /// 是否可用
        /// </summary>
        public int ValidFlag { get; set; }

        #region 公共属性
        public DateTime CreateTime { get; set; }
        public int CreateBy { get; set; }
        public DateTime LastUpdateTime { get; set; }
        public int LastUpdateBy { get; set; }
        #endregion
    }
}
