using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Model.EntityModel.BaseRole;
using Model;

namespace IRepository.BaseRole
{
    public interface ISystemRoleRepository : IBaseRepository<SystemRole>
    {
        ///// <summary>
        ///// 获取角色所拥有的权限
        ///// </summary>
        ///// <param name="pm"></param>
        ///// <returns></returns>
        //Task<List<SystemRole>> GetSystemRoleFunctions(PageModel pm);
    }
}
