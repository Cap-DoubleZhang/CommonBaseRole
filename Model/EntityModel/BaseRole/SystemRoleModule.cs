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

        #region 公共属性
        public DateTime CreateTime { get; set; }
        public int CreateBy { get; set; }
        public DateTime LastUpdateTime { get; set; }
        public int LastUpdateBy { get; set; }
        #endregion

        /// <summary>
        /// 对应角色信息
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public SystemRole Role { get; set; }

        /// <summary>
        /// 对应菜单信息
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public AdminModule AdminModule { get; set; }
    }
}
