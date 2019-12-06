using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.EntityModel.BaseRole;
using Model;
using IServices.BaseRole;
using Microsoft.Extensions.Logging;
using Model.DtoModel.BaseRole;

namespace CommonBaseRole.Controllers.BaseRole
{
    [Route("api/[controller]")]
    [ApiController]
    public class SystemController : CommonController
    {
        private IAdminModuleService AdminModuleService;
        private ISystemRoleService SystemRoleService;
        private ILogger<SystemController> _logger;

        public SystemController(ILogger<SystemController> logger, IAdminModuleService adminModuleService
            , ISystemRoleService systemRoleService)
        {
            this.AdminModuleService = adminModuleService;
            this.SystemRoleService = systemRoleService;
            this._logger = logger;
        }

        /// <summary>
        /// 获取后台菜单全部数据（不分页）
        /// </summary>
        /// <returns></returns>
        [HttpGet("Menus")]
        public async Task<object> GetMenuList()
        {
            JsonpResult<object> json = GetErrorJSONP("初始化中...");
            try
            {
                List<AdminModule> list = await AdminModuleService.GetEntity();
                var getval = new
                {
                    success = true,
                    data = list,
                };
                json = new JsonpResult<object>(getval);
            }
            catch (Exception ex)
            {
                json = GetErrorJSONP(ex.Message);
            }
            return Ok(json);
        }

        /// <summary>
        /// 获取角色列表（分页）
        /// </summary>
        /// <param name="p">当前页</param>
        /// <param name="s">每页展示</param>
        /// <param name="keyword">关键词</param>
        /// <returns></returns>
        [HttpGet("Roles")]
        public async Task<object> GetRoleList(int p = 1, int s = 1, string keyword = "")
        {
            JsonpResult<object> json = GetErrorJSONP("初始化中...");
            try
            {
                PageModel pm = new PageModel { CurrentPage = p, PageSize = s };
                if (!string.IsNullOrWhiteSpace(keyword))
                {
                    pm.OrCondition.Add(new SearchCondition()
                    {
                        ConditionField = "RoleName",
                        SearchType = SearchType.Like,
                        ConditionValue1 = keyword,
                    });
                    pm.OrCondition.Add(new SearchCondition()
                    {
                        ConditionField = "RoleDesc",
                        SearchType = SearchType.Like,
                        ConditionValue1 = keyword,
                    });
                }
                pm.TableName = "SystemRole";
                pm.KeyField = "RoleID";
                List<SystemRole> list = await SystemRoleService.GetEntityPage(pm);
                
                var getval = new
                {
                    success = true,
                    data = list,
                    pageSize = s,
                    page = p,
                    maxPage = pm.MaxPage,
                    dataCount = pm.DataCount,
                };
                json = new JsonpResult<object>(getval);
            }
            catch (Exception ex)
            {
                json = GetErrorJSONP(ex.Message);
            }
            return Ok(json);
        }

        /// <summary>
        /// 获取角色详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("Roles/{Id}")]
        public async Task<object> GetSystemRoleInfo(int Id = 0)
        {
            JsonpResult<object> json = GetErrorJSONP("初始化中...");
            try
            {
                if (Id == 0)
                {
                    json = GetErrorJSONP("必要参数传入错误");
                    return BadRequest(json);
                }
                SystemRole roleInfo = await SystemRoleService.TEntityInfo(Id.ToString());
                var getval = new
                {
                    success = true,
                    item = roleInfo,
                };
                json = new JsonpResult<object>(getval);
            }
            catch (Exception ex)
            {
                json = GetErrorJSONP(ex.Message);
            }
            return Ok(json);
        }
    }
}