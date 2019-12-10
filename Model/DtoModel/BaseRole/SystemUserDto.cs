using System;
using System.Collections.Generic;
using System.Text;

namespace Model.DtoModel.BaseRole
{
    /// <summary>
    /// 系统角色表
    /// </summary>
    public class SystemUserDto
    {
        public SystemUserDto()
        {
        }

        /// <summary>
        /// 用户ID
        /// </summary>
        public int SystemUserID { get; set; }
        /// <summary>
        /// 登录名
        /// </summary>
        public string LoginName { get; set; }
        /// <summary>
        /// 展示名
        /// </summary>
        public string ShowName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 用户等级
        /// </summary>
        public string SystemUserLevel { get; set; }
        /// <summary>
        /// 用户描述
        /// </summary>
        public string UserDesc { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        public string LogoURL { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string EMail { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        public string Mobile { get; set; }
        /// <summary>
        /// 登录次数
        /// </summary>
        public int LoginTimes { get; set; }
        /// <summary>
        /// 上次登录时间
        /// </summary>
        public string LastLoginTime { get; set; }

        /// <summary>
        /// 上次登录IP
        /// </summary>
        public string LastLoginIP { get; set; }
        /// <summary>
        /// 是否禁用
        /// </summary>
        public int IsUse { get; set; }

        #region 公共属性
        public string CreateTime { get; set; }
        public string LastUpdateTime { get; set; }
        #endregion
    }
}
