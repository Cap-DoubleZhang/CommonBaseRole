using System;
using System.Collections.Generic;
using System.Text;

namespace Model.DtoModel.BaseRole
{
    public class SystemRoleDto
    {
        public SystemRoleDto()
        {
            IsUse = 1;
        }
        /// <summary>
        /// 角色ID
        /// </summary>
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

        #region 公共属性
        public string CreateTime { get; set; }
        public string LastUpdateTime { get; set; }
        #endregion

        /// <summary>
        /// 用户ID
        /// </summary>
        public int SystemUserID { get; set; }

        /// <summary>
        /// 用户是否包含该角色
        /// </summary>
        public int RoleChecked { get; set; }

        /// <summary>
        /// 用户对应角色ID
        /// </summary>
        public int UserRoleID { get; set; }
    }
}
