using System;
using System.Collections.Generic;
using System.Text;
using Model.EntityModel.BaseRole;
using IServices;
using System.Threading.Tasks;
using Model.DtoModel.BaseRole;

namespace IServices.BaseRole
{
    public interface IAdminModuleService : IBaseServices<AdminModule>
    {
        /// <summary>
        /// 获取菜单列表，并排序
        /// </summary>
        /// <returns></returns>
        Task<List<AdminModule>> GetAdminModules();

        /// <summary>
        /// 获取排序后的菜单列表
        /// </summary>
        /// <returns></returns>
        Task<List<AdminModule>> AdminModuleOrder(List<AdminModule> list);
    }
}
