using System;
using System.Collections.Generic;
using System.Text;
using Model.EntityModel.BaseRole;
using IRepository.BaseRole;
using System.Threading.Tasks;
using Model;
using System.Data;

namespace Repository.BaseRole
{
    public class SystemUserRepository : BaseRepository<SystemUser>, ISystemUserRepository
    {
        /// <summary>
        /// 获取所有用户列表（分页）
        /// </summary>
        /// <param name="pm">查询参数</param>
        /// <returns></returns>
        public async Task<List<SystemUser>> GetSystemUsers(PageModel pm)
        {
            pm.Condition.Add(new SearchCondition()
            {
                ConditionField = "su.[ValidFlag]",
                SearchType = SearchType.Equal,
                ConditionValue1 = "1",
            });
            //执行存储过程
            DataTable dt = await ExecStoredProcedure("sp_GetSystemUsers", pm);
            List<SystemUser> list = new List<SystemUser>();
            foreach (DataRow item in dt.Rows)
            {
                SystemUserInfo userInfo = new SystemUserInfo()
                {
                    InfoID = item["InfoID"].ToString(),
                    UserShowName = item["UserShowName"].ToString(),
                    HeadPortrait = item["HeadPortrait"].ToString(),
                    Phone = item["Phone"].ToString(),
                    EMail = item["EMail"].ToString(),
                    BirthDate = Convert.IsDBNull(item["BirthDate"]) ? DateTime.MinValue : Convert.ToDateTime(item["BirthDate"]),
                    IDCard = item["IDCard"].ToString(),
                    QQ = item["QQ"].ToString(),
                    WeChat = item["WeChat"].ToString(),
                };

                SystemUser user = new SystemUser
                {
                    UserID = item["UserID"].ToString(),
                    UserLoginName = item["UserLoginName"].ToString(),
                    UserPassword = item["UserPassword"].ToString(),
                    SystemUserLevel = Convert.IsDBNull(item["SystemUserLevel"]) ? 0 : Convert.ToInt32(item["SystemUserLevel"]),
                    Descripts = item["Descripts"].ToString(),
                    LoginTimes = Convert.IsDBNull(item["LoginTimes"]) ? 0 : Convert.ToInt32(item["LoginTimes"]),
                    LastLoginTime = Convert.IsDBNull(item["LastLoginTime"]) ? DateTime.MinValue : Convert.ToDateTime(item["LastLoginTime"]),
                    LastLoginIP = item["LastLoginIP"].ToString(),
                    IsUse = Convert.IsDBNull(item["IsUse"]) ? 0 : Convert.ToInt32(item["IsUse"]),
                    CreateTime = Convert.IsDBNull(item["CreateTime"]) ? DateTime.MinValue : Convert.ToDateTime(item["CreateTime"]),
                    CreateBy = Convert.IsDBNull(item["CreateBy"]) ? 0 : Convert.ToInt32(item["CreateBy"]),
                    UserInfo = userInfo,
                };
                list.Add(user);
            }
            return list;
        }

        /// <summary>
        /// 获取单个用户登录信息、基础信息、角色信息
        /// </summary>
        /// <param name="pm">查询参数</param>
        /// <returns></returns>
        public async Task<SystemUser> GetSystemUsersRole(PageModel pm)
        {
            pm.Condition.Add(new SearchCondition()
            {
                ConditionField = "su.[ValidFlag]",
                SearchType = SearchType.Equal,
                ConditionValue1 = "1",
            });
            pm.Condition.Add(new SearchCondition()//角色可用性判断
            {
                ConditionField = "sr.[IsUse]",
                SearchType = SearchType.Equal,
                ConditionValue1 = "1",
            });
            pm.Condition.Add(new SearchCondition()
            {
                ConditionField = "sr.[ValidFlag]",
                SearchType = SearchType.Equal,
                ConditionValue1 = "1",
            });
            //执行存储过程
            DataTable dt = await ExecStoredProcedure("sp_GetSystemUserRoles", pm);
            SystemUser user = new SystemUser();
            if (dt.Rows.Count > 0)
            {
                #region 基础信息，这样写的好处为省得多次循环，但是写法麻烦。
                SystemUserInfo userinfo = new SystemUserInfo
                {
                    InfoID = dt.Rows[0]["InfoID"].ToString(),
                    UserShowName = dt.Rows[0]["UserShowName"].ToString(),
                    HeadPortrait = dt.Rows[0]["HeadPortrait"].ToString(),
                    Phone = dt.Rows[0]["Phone"].ToString(),
                    EMail = dt.Rows[0]["EMail"].ToString(),
                    BirthDate = Convert.IsDBNull(dt.Rows[0]["BirthDate"]) ? DateTime.MinValue : Convert.ToDateTime(dt.Rows[0]["BirthDate"]),
                    IDCard = dt.Rows[0]["IDCard"].ToString(),
                    QQ = dt.Rows[0]["QQ"].ToString(),
                    WeChat = dt.Rows[0]["WeChat"].ToString(),
                };
                #endregion

                #region 循环获取角色信息
                List<SystemRole> roles = new List<SystemRole>();
                foreach (DataRow item in dt.Rows)
                {
                    SystemRole systemRole = new SystemRole()
                    {
                        RoleID = Convert.IsDBNull(item["RoleID"]) ? 0 : Convert.ToInt32(item["RoleID"]),
                        RoleName = item["RoleName"].ToString(),
                        RoleDesc = item["RoleDesc"].ToString(),
                        AdminFlag = Convert.IsDBNull(item["AdminFlag"]) ? 0 : Convert.ToInt32(item["AdminFlag"]),
                        IsUse = Convert.IsDBNull(item["IsUse"]) ? 0 : Convert.ToInt32(item["IsUse"]),
                    };
                    roles.Add(systemRole);
                }
                #endregion

                #region 用户登录信息
                user = new SystemUser
                {
                    UserID = dt.Rows[0]["UserID"].ToString(),
                    UserLoginName = dt.Rows[0]["UserLoginName"].ToString(),
                    UserPassword = dt.Rows[0]["UserPassword"].ToString(),
                    SystemUserLevel = Convert.IsDBNull(dt.Rows[0]["SystemUserLevel"]) ? 0 : Convert.ToInt32(dt.Rows[0]["SystemUserLevel"]),
                    Descripts = dt.Rows[0]["Descripts"].ToString(),
                    LoginTimes = Convert.IsDBNull(dt.Rows[0]["LoginTimes"]) ? 0 : Convert.ToInt32(dt.Rows[0]["LoginTimes"]),
                    LastLoginTime = Convert.IsDBNull(dt.Rows[0]["LastLoginTime"]) ? DateTime.MinValue : Convert.ToDateTime(dt.Rows[0]["LastLoginTime"]),
                    LastLoginIP = dt.Rows[0]["LastLoginIP"].ToString(),
                    IsUse = Convert.IsDBNull(dt.Rows[0]["IsUse"]) ? 0 : Convert.ToInt32(dt.Rows[0]["IsUse"]),
                    CreateTime = Convert.IsDBNull(dt.Rows[0]["CreateTime"]) ? DateTime.MinValue : Convert.ToDateTime(dt.Rows[0]["CreateTime"]),
                    CreateBy = Convert.IsDBNull(dt.Rows[0]["CreateBy"]) ? 0 : Convert.ToInt32(dt.Rows[0]["CreateBy"]),
                    LastUpdateTime = Convert.IsDBNull(dt.Rows[0]["LastUpdateTime"]) ? DateTime.MinValue : Convert.ToDateTime(dt.Rows[0]["LastUpdateTime"]),
                    LastUpdateBy = Convert.IsDBNull(dt.Rows[0]["LastUpdateBy"]) ? 0 : Convert.ToInt32(dt.Rows[0]["LastUpdateBy"]),
                    Roles = roles,
                    UserInfo = userinfo,
                };
                #endregion
            }


            return user;
        }
    }
}
