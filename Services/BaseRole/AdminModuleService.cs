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


        public async Task<List<AdminModule>> GetAdminModules()
        {
            List<AdminModule> list = await _repository.GetEntity();
            return list;
        }

        public async Task<List<AdminModule>> AdminModuleOrder(List<AdminModule> list)
        {
            List<AdminModule> listDto = list.Where(a => a.ParentModuleID == 0).ToList();
            foreach (AdminModule item in listDto)
            {
                List<AdminModule> childrenList = await GetChildrenAdminModuleAsync(item.ModuleID);
                item.ChildrenButtons = childrenList.Where(a => a.IsButton == 1).ToList();
                item.ChildrenModules = childrenList.Where(a => a.IsButton == 0).ToList();
            }
            return listDto;
        }

        public async Task<List<AdminModule>> GetChildrenAdminModuleAsync(int pid)
        {
            List<AdminModule> listOrder = new List<AdminModule>();
            List<AdminModule> list = await GetAdminModules();//获取全部菜单列表，回头使用Redis中的缓存
            List<AdminModule> listDto = list.Where(a => a.ParentModuleID == pid).ToList();
            foreach (AdminModule item in listDto)
            {
                List<AdminModule> childrenList = await GetChildrenAdminModuleAsync(item.ModuleID);
                if (childrenList.Count() > 0)
                {
                    item.ChildrenButtons = childrenList.Where(a => a.IsButton == 1).ToList();
                    item.ChildrenModules = childrenList.Where(a => a.IsButton == 0).ToList();
                }
                listOrder.Add(item);
            }
            return listOrder;
        }
    }
}
