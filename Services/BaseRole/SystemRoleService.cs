using System;
using System.Collections.Generic;
using System.Text;
using IServices.BaseRole;
using Model.EntityModel.BaseRole;
using IRepository.BaseRole;

namespace Services.BaseRole
{
    public class SystemRoleService : BaseServices<SystemRole>, ISystemRoleService
    {
        ISystemRoleRepository _repository;
        public SystemRoleService(ISystemRoleRepository repository)
        {
            this._repository = repository;
            base.baseRepository = repository;
        }
    }
}
