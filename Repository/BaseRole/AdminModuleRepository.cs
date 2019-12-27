using System;
using System.Collections.Generic;
using System.Text;
using IRepository;
using Model.EntityModel.BaseRole;
using IRepository.BaseRole;
using System.Threading.Tasks;
using Model;
using System.Data;
using System.Linq;

namespace Repository.BaseRole
{
    public class AdminModuleRepository : BaseRepository<AdminModule>, IAdminModuleRepository
    {
        /// <summary>
        /// 获取角色权限
        /// </summary>
        /// <param name="pm">查询参数</param>
        /// <returns></returns>
        public async Task<List<AdminModule>> GetRoleModules(PageModel pm)
        {
            pm.Condition.Add(new SearchCondition()
            {
                ConditionField = "sr.[IsUse]",//角色可用
                SearchType = SearchType.Equal,
                ConditionValue1 = "1",
            });
            pm.Condition.Add(new SearchCondition()
            {
                ConditionField = "sr.[ValidFlag]",//角色可用
                SearchType = SearchType.Equal,
                ConditionValue1 = "1",
            });
            pm.Condition.Add(new SearchCondition()
            {
                ConditionField = "am.[IsUse]",//权限可用
                SearchType = SearchType.Equal,
                ConditionValue1 = "1",
            });
            pm.Condition.Add(new SearchCondition()
            {
                ConditionField = "am.[ValidFlag]",//权限可用
                SearchType = SearchType.Equal,
                ConditionValue1 = "1",
            });
            List<AdminModule> list = new List<AdminModule>();
            DataTable dt = await ExecStoredProcedure("sp_GetRoleModules", pm);
            foreach (DataRow item in dt.Rows)
            {
                AdminModule adminModule = new AdminModule()
                {
                    ModuleID = Convert.IsDBNull(item["ModuleID"]) ? 0 : Convert.ToInt32(item["ModuleID"]),
                    ModuleCode = item["ModuleCode"].ToString(),
                    ModuleName = item["ModuleName"].ToString(),
                    ParentModuleID = Convert.IsDBNull(item["ParentModuleID"]) ? 0 : Convert.ToInt32(item["ParentModuleID"]),
                    MenuIcon = item["MenuIcon"].ToString(),
                    MenuTitle = item["MenuTitle"].ToString(),
                    MenuPath = item["MenuPath"].ToString(),
                    SortIndex = Convert.IsDBNull(item["SortIndex"]) ? 0 : Convert.ToInt32(item["SortIndex"]),
                    IsButton = Convert.IsDBNull(item["IsButton"]) ? 0 : Convert.ToInt32(item["IsButton"]),
                };
                list.Add(adminModule);
            }
            //数据去重
            list = list.Where((x, i) => list.FindIndex(z => z.ModuleID == x.ModuleID) == i).ToList();
            return list;
        }
    }
}
