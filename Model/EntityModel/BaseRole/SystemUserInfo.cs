using System;
using System.Collections.Generic;
using System.Text;
using SqlSugar;

namespace Model.EntityModel.BaseRole
{
    /// <summary>
    /// 用户基础信息
    /// </summary>
    [SugarTable("SystemUserInfo")]
    public class SystemUserInfo
    {
        public SystemUserInfo()
        {
            InfoID = Guid.NewGuid().ToString().ToUpper();
            LastUpdateTime = DateTime.Now;
        }

        /// <summary>
        /// 主键ID
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public string InfoID { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public string UserID { get; set; }

        /// <summary>
        /// 用户展示名称（昵称）
        /// </summary>
        public string UserShowName { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        public string HeadPortrait { get; set; }

        /// <summary>
        /// 手机
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string EMail { get; set; }

        /// <summary>
        /// 出生日期
        /// </summary>
        public DateTime BirthDate { get; set; }

        /// <summary>
        /// 身份证号
        /// </summary>
        public string IDCard { get; set; }
        
        /// <summary>
        /// QQ
        /// </summary>
        public string QQ { get; set; }

        /// <summary>
        /// 微信
        /// </summary>
        public string WeChat { get; set; }

        #region 公共属性
        public DateTime CreateTime { get; set; }
        public int CreateBy { get; set; }
        public DateTime LastUpdateTime { get; set; }
        public int LastUpdateBy { get; set; }
        #endregion
    }
}
