using System;
using System.Collections.Generic;
using System.Text;
using IRepository;
using Model.EntityModel.BaseRole;
using IRepository.BaseRole;

namespace Repository.BaseRole
{
    public class AdminModuleRepository : BaseRepository<AdminModule>, IAdminModuleRepository
    {
    }
}
