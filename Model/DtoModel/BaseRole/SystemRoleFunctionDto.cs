using System;
using System.Collections.Generic;
using System.Text;

namespace Model.DtoModel.BaseRole
{
    /// <summary>
    /// 角色对应功能表
    /// </summary>
    public class SystemRoleFunctionDto
    {
        public SystemRoleFunctionDto()
        {
        }
        /// <summary>
        /// 主键ID
        /// </summary>
        public int RoleFunctionID { get; set; }
        /// <summary>
        /// 角色ID
        /// </summary>
        public int RoleID { get; set; }
        /// <summary>
        /// 功能ID
        /// </summary>
        public int FunctionID { get; set; }

        #region 公共属性
        public string CreateTime { get; set; }
        public string LastUpdateTime { get; set; }
        #endregion

        /// <summary>
        /// 角色对应功能
        /// </summary>
        //public SystemFunction Functions { get; set; }

        /// <summary>
        /// 角色详情
        /// </summary>
        //public SystemRole RoleInfo { get; set; }
    }
}
