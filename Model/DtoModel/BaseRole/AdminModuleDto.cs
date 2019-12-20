using System;
using System.Collections.Generic;
using System.Text;

namespace Model.DtoModel.BaseRole
{
    /// <summary>
    /// 系统菜单表（当父菜单被禁用时，子菜单自动被禁用）
    /// </summary>
    public class AdminModuleDto
    {
        public AdminModuleDto()
        {
            IsUse = 1;
            LastUpdateTime = DateTime.Now;
        }
        /// <summary>
        /// 主键ID
        /// </summary>
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
        /// 子菜单
        /// </summary>
        public List<AdminModuleDto> ChildrenModules { get; set; }

        /// <summary>
        /// 子按钮
        /// </summary>
        public List<AdminModuleDto> ChildrenButtons { get; set; }

        /// <summary>
        /// 是否禁用
        /// </summary>
        public int IsUse { get; set; }

        #region 公共属性
        public DateTime CreateTime { get; set; }
        public DateTime LastUpdateTime { get; set; }
        #endregion
    }
}
