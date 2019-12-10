using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Model;
using Model.EntityModel.BaseRole;

namespace IRepository.BaseRole
{
    public interface ISystemUserRepository : IBaseRepository<SystemUser>
    {
        /// <summary>
        /// 获取单个用户登录信息、基础信息、角色信息
        /// </summary>
        /// <param name="pm">查询参数</param>
        /// <returns></returns>
        Task<SystemUser> GetSystemUsersRole(PageModel pm);

        /// <summary>
        /// 获取所有用户列表（分页）
        /// </summary>
        /// <param name="pm">查询参数</param>
        /// <returns></returns>
        Task<List<SystemUser>> GetSystemUsers(PageModel pm);
    }
}
