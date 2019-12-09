using System;
using System.Collections.Generic;
using System.Text;
using Model.EntityModel.BaseRole;
using IServices.BaseRole;
using System.Threading.Tasks;
using Model;
using IRepository.BaseRole;

namespace Services.BaseRole
{
    public class SystemUserService : BaseServices<SystemUser>, ISystemUserService
    {
        ISystemUserRepository ISystemUserService;
        public SystemUserService(ISystemUserRepository systemUserRepository)
        {
            this.ISystemUserService = systemUserRepository;
            //base.baseRepository = systemUserRepository;
        }

        /// <summary>
        /// 获取单个用户登录信息、基础信息、角色信息
        /// </summary>
        /// <param name="pm">查询参数</param>
        /// <returns></returns>
        public async Task<SystemUser> GetSystemUsersRole(PageModel pm)
        {
            //按照角色创建时间正序
            pm.lstOrder.Add(new OrderModel()
            {
                FieldName = "sr.CreateTime",
                Order = PMSortOrder.asc,
            });
            SystemUser user = await ISystemUserService.GetSystemUsersRole(pm) ?? new SystemUser();
            return user;
        }
    }
}
