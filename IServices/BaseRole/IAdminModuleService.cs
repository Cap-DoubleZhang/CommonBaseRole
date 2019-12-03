using System;
using System.Collections.Generic;
using System.Text;
using Model.EntityModel.BaseRole;
using IServices;
using System.Threading.Tasks;

namespace IServices.BaseRole
{
    public interface IAdminModuleService : IBaseServices<AdminModule>
    {
        //Task<List<AdminModule>> GetAdminModuleList();
    }
}
