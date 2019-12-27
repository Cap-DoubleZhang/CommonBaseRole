using System;
using System.Collections.Generic;
using System.Text;
using Model.EntityModel.BaseRole;
using IServices;
using System.Threading.Tasks;
using Model.DtoModel.BaseRole;
using Model;

namespace IServices.BaseRole
{
    public interface IAdminModuleService : IBaseServices<AdminModule>
    {
        /// <summary>
        /// 获取排序后的菜单列表
        /// </summary>
        /// <returns></returns>
        Task<List<AdminModule>> AdminModuleOrder(List<AdminModule> list);

        /// <summary>
        /// 获取角色权限
        /// </summary>
        /// <param name="pm">查询参数</param>
        /// <returns></returns>
        Task<List<AdminModule>> GetRoleModules(PageModel pm);
    }
}
