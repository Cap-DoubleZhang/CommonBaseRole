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
    }
}
