using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.EntityModel.BaseRole
{
    /// <summary>
    /// 系统菜单表（当父菜单被禁用时，子菜单自动被禁用）
    /// </summary>
    [SugarTable("AdminModule")]
    public class AdminModule
    {
        public AdminModule()
        {
            ValidFlag = 1;
            IsUse = 1;
            LastUpdateTime = DateTime.Now;
        }
        /// <summary>
        /// 主键ID
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int ModuleID { get; set; }

        /// <summary>
        /// 菜单名称
        /// </summary>
        public string ModuleName { get; set; }

        /// <summary>
        /// 菜单Code
        /// </summary>
        public string ModuleCode { get; set; }

        /// <summary>
        /// 菜单图标
        /// </summary>
        public string MenuIcon { get; set; }

        /// <summary>
        /// 菜单标题
        /// </summary>
        public string MenuTitle { get; set; }

        /// <summary>
        /// 父级菜单
        /// </summary>
        public int ParentModuleID { get; set; }

        /// <summary>
        /// 菜单路径
        /// </summary>
        public string MenuPath { get; set; }

        /// <summary>
        /// 排序值
        /// </summary>
        public int SortIndex { get; set; }

        /// <summary>
        /// 是否禁用
        /// </summary>
        public int IsUse { get; set; }

        /// <summary>
        /// 是否是按钮（0：否，1：是）
        /// </summary>
        public int IsButton { get; set; }

        /// <summary>
        /// 子菜单
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public List<AdminModule> ChildrenModules { get; set; }

        /// <summary>
        /// 子按钮
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public List<AdminModule> ChildrenButtons { get; set; }

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
