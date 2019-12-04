﻿using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.EntityModel.BaseRole
{
    /// <summary>
    /// 用户对应角色
    /// </summary>
    [SugarTable("SystemUserRole")]
    public class SystemUserRole
    {
        public SystemUserRole()
        {
            LastUpdateTime = DateTime.Now;
        }
        /// <summary>
        /// 主键ID
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int UserRoleID { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserID { get; set; }
        /// <summary>
        /// 角色ID
        /// </summary>
        public int RoleID { get; set; }

        #region 公共属性
        public DateTime CreateTime { get; set; }
        public int CreateBy { get; set; }
        public DateTime LastUpdateTime { get; set; }
        public int LastUpdateBy { get; set; }
        #endregion
    }
}