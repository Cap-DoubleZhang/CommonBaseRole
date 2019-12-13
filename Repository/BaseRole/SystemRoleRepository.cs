using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using IRepository.BaseRole;
using Model;
using Model.EntityModel.BaseRole;

namespace Repository.BaseRole
{
    public class SystemRoleRepository : BaseRepository<SystemRole>, ISystemRoleRepository
    {
        ///// <summary>
        ///// 获取单个角色所拥有的权限
        ///// </summary>
        ///// <param name="pm"></param>
        ///// <returns></returns>
        //public async Task<List<SystemRole>> GetSystemRoleFunctions(PageModel pm)
        //{
        //    pm.Condition.Add(new SearchCondition()
        //    {
        //        ConditionField = "sr.[ValidFlag]",
        //        SearchType = SearchType.Equal,
        //        ConditionValue1 = "1",
        //    });
        //    pm.Condition.Add(new SearchCondition()
        //    {
        //        ConditionField = "sf.[IsUse]",
        //        SearchType = SearchType.Equal,
        //        ConditionValue1 = "1",
        //    });
        //    pm.Condition.Add(new SearchCondition()
        //    {
        //        ConditionField = "sf.[ValidFlag]",
        //        SearchType = SearchType.Equal,
        //        ConditionValue1 = "1",
        //    });
        //    DataTable dt = await ExecStoredProcedure("sp_GetSystemRoleFunctions", pm);
        //    List<SystemRole> list = new List<SystemRole>();

        //    foreach (DataRow item in dt.Rows)
        //    {

        //    }
        //    throw new NotImplementedException();
        //}
    }
}
