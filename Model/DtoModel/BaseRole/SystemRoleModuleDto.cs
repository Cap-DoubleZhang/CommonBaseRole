using System;
using System.Collections.Generic;
using System.Text;

namespace Model.DtoModel.BaseRole
{
    /// <summary>
    /// 角色菜单表
    /// </summary>
    public class SystemRoleModuleDto
    {
        /// <summary>
        /// 主键ID
        /// </summary>
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
        public string CreateTime { get; set; }
        public string LastUpdateTime { get; set; }
        #endregion

        /// <summary>
        /// 对应角色信息
        /// </summary>
        //public SystemRole Role { get; set; }

        /// <summary>
        /// 对应菜单信息
        /// </summary>
        //public AdminModule AdminModule { get; set; }
    }
}
