using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.EntityModel.BaseRole
{
    /// <summary>
    /// 系统角色表
    /// </summary>
    [SugarTable("SystemUser")]
    public class SystemUser
    {
        public SystemUser()
        {
            //随机产生GUID，并全部大写
            //UserID = Guid.NewGuid().ToString().ToUpper();
            LastLoginTime = new DateTime(1990, 1, 1);
            ValidFlag = 1;
            LastUpdateTime = DateTime.Now;
        }

        /// <summary>
        /// 用户ID
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public string UserID { get; set; }
        /// <summary>
        /// 登录名
        /// </summary>
        public string UserLoginName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string UserPassword { get; set; }
        /// <summary>
        /// 用户等级
        /// </summary>
        public int SystemUserLevel { get; set; }
        /// <summary>
        /// 用户描述
        /// </summary>
        public string Descripts { get; set; }
        /// <summary>
        /// 登录次数
        /// </summary>
        public int LoginTimes { get; set; }
        /// <summary>
        /// 上次登录时间
        /// </summary>
        public DateTime LastLoginTime { get; set; }

        /// <summary>
        /// 上次登录IP
        /// </summary>
        public string LastLoginIP { get; set; }
        /// <summary>
        /// 是否禁用
        /// </summary>
        public int IsUse { get; set; }
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

        /// <summary>
        /// 用户基础信息
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public SystemUserInfo UserInfo { get; set; }

        /// <summary>
        /// 用户对应角色列表
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public List<SystemRole> Roles { get; set; }
    }
}
