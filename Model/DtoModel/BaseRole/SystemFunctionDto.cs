using System;
using System.Collections.Generic;
using System.Text;

namespace Model.DtoModel.BaseRole
{
    /// <summary>
    /// 系统功能表
    /// </summary>
    public class SystemFunctionDto
    {
        public SystemFunctionDto()
        {
            IsUse = 1;
        }
        /// <summary>
        /// 主键
        /// </summary>
        public int FunctionID { get; set; }
        /// <summary>
        /// 功能Code
        /// </summary>
        public string FunctionCode { get; set; }
        /// <summary>
        /// 功能名称
        /// </summary>
        public string FunctionName { get; set; }
        /// <summary>
        /// 功能路径
        /// </summary>
        public string FunctionPath { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Describe { get; set; }
        /// <summary>
        /// 所属菜单
        /// </summary>
        public int ModuleID { get; set; }

        /// <summary>
        /// 是否可用
        /// </summary>
        public int IsUse { get; set; }

        #region 公共属性
        public string CreateTime { get; set; }
        public string LastUpdateTime { get; set; }
        #endregion

        ///// <summary>
        ///// 角色功能项
        ///// </summary>
        //[SugarColumn(IsIgnore = true)]
        //public SystemRoleFunction roleFunction { get; set; }

        /// <summary>
        /// 角色对应功能项，对应则打钩
        /// </summary>
        public int NoChecked { get; set; }
    }
}
