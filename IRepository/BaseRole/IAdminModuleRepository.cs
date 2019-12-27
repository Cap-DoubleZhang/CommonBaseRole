using System;
using System.Collections.Generic;
using System.Text;
using Model.EntityModel.BaseRole;
using Model;
using System.Threading.Tasks;

namespace IRepository.BaseRole
{
    public interface IAdminModuleRepository : IBaseRepository<AdminModule>
    {
        /// <summary>
        /// 获取菜单权限
        /// </summary>
        /// <param name="pm"></param>
        /// <returns></returns>
        Task<List<AdminModule>> GetRoleModules(PageModel pm);
    }
}
