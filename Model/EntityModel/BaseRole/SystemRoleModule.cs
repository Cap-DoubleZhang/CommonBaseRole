using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.EntityModel.BaseRole
{
    /// <summary>
    /// 角色菜单表
    /// </summary>
    [SugarTable("SystemRoleModule")]
    public class SystemRoleModule
    {
        public SystemRoleModule()
        {
            ValidFlag = 1;
            LastUpdateTime = DateTime.Now;
        }
        /// <summary>
        /// 主键ID
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int RoleMenuID { get; set; }
        /// <summary>
        /// 角色ID
        /// </summary>
        public int RoleID { get; set; }
        /// <summary>
        /// 菜单ID 
        /// </summary>
        public int ModuleID { get; set; }
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
