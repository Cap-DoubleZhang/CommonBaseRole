using System;
using System.Collections.Generic;
using System.Text;
using Services;
using Model;
using Model.EntityModel.BaseRole;
using IServices;
using IServices.BaseRole;
using System.Threading.Tasks;
using IRepository;
using IRepository.BaseRole;
using Model.DtoModel.BaseRole;
using System.Linq;
using AutoMapper;

namespace Services.BaseRole
{
    public class AdminModuleService : BaseServices<AdminModule>, IAdminModuleService
    {
        IAdminModuleRepository _repository;
        public AdminModuleService(IAdminModuleRepository adminModuleRepository)
        {
            this._repository = adminModuleRepository;
            base.baseRepository = adminModuleRepository;
        }

        #region 菜单--树形菜单
        /// <summary>
        /// 菜单权限进行排序
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public async Task<List<AdminModule>> AdminModuleOrder(List<AdminModule> list)
        {
            List<AdminModule> listDto = list.Where(a => a.ParentModuleID == 0).ToList();
            foreach (AdminModule item in listDto)
            {
                List<AdminModule> childrenList = await GetChildrenAdminModuleAsync(item.ModuleID, list);
                item.ChildrenButtons = childrenList.Where(a => a.IsButton == 1).ToList();
                item.ChildrenModules = childrenList.Where(a => a.IsButton == 0).ToList();
            }
            return listDto;
        }

        /// <summary>
        /// 递归将子菜单添加到父级菜单
        /// </summary>
        /// <param name="pid"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public async Task<List<AdminModule>> GetChildrenAdminModuleAsync(int pid, List<AdminModule> list)
        {
            List<AdminModule> listOrder = new List<AdminModule>();
            List<AdminModule> listDto = list.Where(a => a.ParentModuleID == pid).ToList();
            foreach (AdminModule item in listDto)
            {
                List<AdminModule> childrenList = await GetChildrenAdminModuleAsync(item.ModuleID, list);
                if (childrenList.Count() > 0)
                {
                    item.ChildrenButtons = childrenList.Where(a => a.IsButton == 1).ToList();
                    item.ChildrenModules = childrenList.Where(a => a.IsButton == 0).ToList();
                }
                listOrder.Add(item);
            }
            return listOrder;
        }
        #endregion

        /// <summary>
        /// 获取角色权限菜单
        /// </summary>
        /// <param name="pm"></param>
        /// <returns></returns>
        public async Task<List<AdminModule>> GetRoleModules(PageModel pm)
        {
            pm.lstOrder.Add(new OrderModel()
            {
                FieldName = "am.[SortIndex]",
                Order = PMSortOrder.asc,
            });
            List<AdminModule> list = await _repository.GetRoleModules(pm);
            return list;
        }
    }
}
