using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.EntityModel.BaseRole
{
    /// <summary>
    /// 角色对应功能表
    /// </summary>
    [SugarTable("SystemRoleFunction")]
    public class SystemRoleFunction
    {
        public SystemRoleFunction()
        {
            LastUpdateTime = DateTime.Now;
        }
        /// <summary>
        /// 主键ID
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
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
        public DateTime CreateTime { get; set; }
        public int CreateBy { get; set; }
        public DateTime LastUpdateTime { get; set; }
        public int LastUpdateBy { get; set; }
        #endregion

        /// <summary>
        /// 角色对应功能
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public SystemFunction Functions { get; set; }

        /// <summary>
        /// 角色详情
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public SystemRole RoleInfo { get; set; }
    }
}
