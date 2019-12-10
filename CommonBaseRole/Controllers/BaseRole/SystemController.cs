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
using Common;

namespace CommonBaseRole.Controllers.BaseRole
{
    [Route("api/[controller]")]
    [ApiController]
    public class SystemController : CommonController
    {
        private IAdminModuleService AdminModuleService;
        private ISystemRoleService SystemRoleService;
        private ISystemUserService SystemUserService;
        private ISystemFunctionService SystemFunctionService;
        private ILogger<SystemController> _logger;

        public SystemController(ILogger<SystemController> logger, IAdminModuleService adminModuleService
            , ISystemRoleService systemRoleService, ISystemUserService systemUserService
            , ISystemFunctionService systemFunctionService)
        {
            this.AdminModuleService = adminModuleService;
            this.SystemRoleService = systemRoleService;
            this.SystemUserService = systemUserService;
            this.SystemFunctionService = systemFunctionService;
            this._logger = logger;
        }

        #region 后台菜单操作
        #region 获取后台菜单全部数据（不分页）
        /// <summary>
        /// 获取后台菜单全部数据（不分页）
        /// </summary>
        /// <returns></returns>
        [HttpGet("Menus")]
        public async Task<object> GetMenuList()
        {
            JsonpResult<object> json = GetReturnJSONP("初始化中...");
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
                json = GetReturnJSONP(ex.Message);
            }
            return Ok(json);
        }
        #endregion

        #region 获取后台菜单详情
        /// <summary>
        /// 获取后台菜单详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("Menus/{Id}")]
        public async Task<object> GetMenuInfo(int Id = 0)
        {
            JsonpResult<object> json = GetReturnJSONP("初始化中...");
            try
            {
                AdminModule adminModule = await AdminModuleService.TEntityInfo(Id.ToString());
                var getval = new
                {
                    success = true,
                    item = adminModule,
                };
                json = new JsonpResult<object>(getval);
            }
            catch (Exception ex)
            {
                json = GetReturnJSONP(ex.Message);
                return BadRequest(json);
            }
            return Ok(json);
        }
        #endregion

        #region 保存、添加、逻辑删除菜单信息
        /// <summary>
        /// 保存、添加、逻辑删除菜单信息
        /// </summary>
        /// <param name="adminModule"></param>
        /// <returns></returns>
        [HttpPost("Menus/{adminModule.MuduleID}")]
        public async Task<object> SaveAdminModule(AdminModule adminModule)
        {
            JsonpResult<object> json = GetReturnJSONP("初始化中...");
            try
            {
                if (string.IsNullOrWhiteSpace(adminModule.ModuleName))
                {
                    json = GetReturnJSONP("必要参数不能为空!");
                    return BadRequest(json);
                }
                ReturnModel rm = await AdminModuleService.SaveEntityInfo(adminModule);
                json = GetReturnJSONP(rm.msg, rm.BooleanResult);
            }
            catch (Exception ex)
            {
                json = GetReturnJSONP(ex.Message);
                return BadRequest(json);
            }
            return json;
        }
        #endregion
        #endregion

        #region 角色操作
        #region 获取角色列表（分页）
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
            JsonpResult<object> json = GetReturnJSONP("初始化中...");
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
                json = GetReturnJSONP(ex.Message);
            }
            return Ok(json);
        }
        #endregion

        #region 获取角色详情
        /// <summary>
        /// 获取角色详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("Roles/{Id}")]
        public async Task<object> GetSystemRoleInfo(int Id = 0)
        {
            JsonpResult<object> json = GetReturnJSONP("初始化中...");
            try
            {
                if (Id == 0)
                {
                    json = GetReturnJSONP("必要参数传入错误");
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
                json = GetReturnJSONP(ex.Message);
            }
            return Ok(json);
        }
        #endregion

        #region 保存、新增、逻辑删除角色信息
        /// <summary>
        /// 保存、新增、逻辑删除角色信息
        /// </summary>
        /// <param name="systemRole"></param>
        /// <returns></returns>
        [HttpPost("Roles/{systemRole.RoleID}")]
        public async Task<object> SaveRoleInfo(SystemRole systemRole)
        {
            JsonpResult<object> json = GetReturnJSONP("初始化中...");
            try
            {
                if (string.IsNullOrWhiteSpace(systemRole.RoleName))
                {
                    json = GetReturnJSONP("必要参数不能为空!");
                    return BadRequest(json);
                }
                ReturnModel rm = await SystemRoleService.SaveEntityInfo(systemRole);
                json = GetReturnJSONP(rm.msg, rm.BooleanResult);
            }
            catch (Exception ex)
            {
                json = GetReturnJSONP(ex.Message);
                return BadRequest(json);
            }
            return Ok(json);
        }
        #endregion
        #endregion

        #region 用户登录
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        [HttpPost("/api/Login")]
        public async Task<object> GetSystemUser(string userName, string password)
        {
            JsonpResult<object> json = GetReturnJSONP("初始化中...");
            try
            {
                if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(password))
                {
                    json = GetReturnJSONP("必要参数不能为空!");
                    return BadRequest(json);
                }
                PageModel pm = new PageModel() { CurrentPage = 1, PageSize = 100 };
                pm.Condition.Add(new SearchCondition()
                {
                    ConditionField = "su.[UserLoginName]",
                    SearchType = SearchType.Equal,
                    ConditionValue1 = userName,
                });
                pm.Condition.Add(new SearchCondition()
                {
                    ConditionField = "su.[UserPassword]",
                    SearchType = SearchType.Equal,
                    ConditionValue1 = EncryptHelper.MD5Encode(password),
                });
                SystemUser user = await SystemUserService.GetSystemUsersRole(pm);
                if (string.IsNullOrWhiteSpace(user.UserID))
                {
                    json = GetReturnJSONP("用户名或密码错误!");
                    return BadRequest(json);
                }
                if (user.Roles == null || user.Roles.Count <= 0)
                {
                    json = GetReturnJSONP("该用户无角色，请联系管理员!");
                    return BadRequest(json);
                }
                var getval = new
                {
                    success = true,
                    msg = "登陆成功",
                };
                json = new JsonpResult<object>(getval);
            }
            catch (Exception ex)
            {
                json = GetReturnJSONP(ex.Message);
                return BadRequest(json);
            }
            return Ok(json);
        }
        #endregion

        #region 用户操作(用户编辑未做)
        #region 获取所有用户列表（分页）
        /// <summary>
        /// 获取所有用户列表（分页）
        /// </summary>
        /// <param name="p">当前页数</param>
        /// <param name="s">页容量</param>
        /// <param name="keyword">关键词</param>
        /// <returns></returns>
        [HttpGet("Users")]
        public async Task<object> GetSystemUsers(int p = 1, int s = 1, string keyword = "")
        {
            JsonpResult<object> json = GetReturnJSONP("初始化中...");
            try
            {
                PageModel pm = new PageModel { CurrentPage = p, PageSize = s };
                if (string.IsNullOrWhiteSpace(keyword))
                {
                    pm.OrCondition.Add(new SearchCondition()
                    {
                        ConditionField = "su.UserLoginName",
                        SearchType = SearchType.Like,
                        ConditionValue1 = keyword,
                    });
                    pm.OrCondition.Add(new SearchCondition()
                    {
                        ConditionField = "su.Descripts",
                        SearchType = SearchType.Like,
                        ConditionValue1 = keyword,
                    });
                    pm.OrCondition.Add(new SearchCondition()
                    {
                        ConditionField = "sui.UserShowName",
                        SearchType = SearchType.Like,
                        ConditionValue1 = keyword,
                    });
                    pm.OrCondition.Add(new SearchCondition()
                    {
                        ConditionField = "sui.Phone",
                        SearchType = SearchType.Like,
                        ConditionValue1 = keyword,
                    });
                    pm.OrCondition.Add(new SearchCondition()
                    {
                        ConditionField = "sui.EMail",
                        SearchType = SearchType.Like,
                        ConditionValue1 = keyword,
                    });
                    pm.OrCondition.Add(new SearchCondition()
                    {
                        ConditionField = "sui.IDCard",
                        SearchType = SearchType.Like,
                        ConditionValue1 = keyword,
                    });
                    pm.OrCondition.Add(new SearchCondition()
                    {
                        ConditionField = "sui.QQ",
                        SearchType = SearchType.Like,
                        ConditionValue1 = keyword,
                    });
                    pm.OrCondition.Add(new SearchCondition()
                    {
                        ConditionField = "sui.WeChat",
                        SearchType = SearchType.Like,
                        ConditionValue1 = keyword,
                    });
                }
                List<SystemUser> list = await SystemUserService.GetSystemUsers(pm);
                var getval = new
                {
                    success = true,
                    data = list,
                    maxPage = pm.MaxPage,
                    dataCount = pm.DataCount,
                };
                json = new JsonpResult<object>(getval);
            }
            catch (Exception ex)
            {
                json = GetReturnJSONP(ex.Message);
                return BadRequest(json);
            }
            return Ok(json);
        }
        #endregion

        #region 获取用户详情
        /// <summary>
        /// 获取用户详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("Users/{Id}")]
        public async Task<object> GetUserInfo(string Id = "")
        {
            JsonpResult<object> json = GetReturnJSONP("初始化中...");
            try
            {
                PageModel pm = new PageModel() { CurrentPage = 1, PageSize = 1 };
                pm.Condition.Add(new SearchCondition()
                {
                    ConditionField = "su.UserID",
                    SearchType = SearchType.Equal,
                    ConditionValue1 = Id,
                });
                List<SystemUser> users = await SystemUserService.GetSystemUsers(pm);
                SystemUser user = users.Count() <= 0 ? new SystemUser() : users.FirstOrDefault();
                var getval = new
                {
                    success = true,
                    item = user,
                };
                json = new JsonpResult<object>(getval);
            }
            catch (Exception ex)
            {
                json = GetReturnJSONP(ex.Message);
                return BadRequest(json);
            }
            return Ok(json);
        }
        #endregion
        #endregion

        #region 功能项操作
        #region 获取功能项列表（分页）
        /// <summary>
        /// 获取功能项列表（分页）
        /// </summary>
        /// <param name="p">当前页</param>
        /// <param name="s">页容量</param>
        /// <param name="keyword">关键词</param>
        /// <returns></returns>
        [HttpGet("Functions")]
        public async Task<object> GetSystemFunctions(int p = 1, int s = 1, string keyword = "")
        {
            JsonpResult<object> json = GetReturnJSONP("初始化中...");
            try
            {
                PageModel pm = new PageModel { CurrentPage = p, PageSize = s };
                if (string.IsNullOrWhiteSpace(keyword))
                {
                    pm.OrCondition.Add(new SearchCondition()
                    {
                        ConditionField = "FunctionCode",
                        SearchType = SearchType.Like,
                        ConditionValue1 = keyword,
                    });
                    pm.OrCondition.Add(new SearchCondition()
                    {
                        ConditionField = "FunctionName",
                        SearchType = SearchType.Like,
                        ConditionValue1 = keyword,
                    });
                    pm.OrCondition.Add(new SearchCondition()
                    {
                        ConditionField = "FunctionPath",
                        SearchType = SearchType.Like,
                        ConditionValue1 = keyword,
                    });
                    pm.OrCondition.Add(new SearchCondition()
                    {
                        ConditionField = "Describe",
                        SearchType = SearchType.Like,
                        ConditionValue1 = keyword,
                    });
                }
                List<SystemFunction> list = await SystemFunctionService.GetEntityPage(pm);
                var getval = new
                {
                    success = true,
                    code = "0000",
                    data = list,
                    dataCount = pm.DataCount,
                    maxPage = pm.MaxPage,
                };
                json = new JsonpResult<object>(getval);
            }
            catch (Exception ex)
            {
                json = GetReturnJSONP(ex.Message);
                return BadRequest(json);
            }
            return Ok(json);
        }
        #endregion

        #region 获取功能项详情
        /// <summary>
        /// 获取功能项详情
        /// </summary>
        /// <param name="Id">主键ID</param>
        /// <returns></returns>
        [HttpGet("Functions/{Id}")]
        public async Task<object> GetSystemFunctionInfo(int Id = 0)
        {
            JsonpResult<object> json = GetReturnJSONP("初始化中...");
            try
            {
                SystemFunction function = await SystemFunctionService.TEntityInfo(Id.ToString());
                var getval = new
                {
                    success = true,
                    item = function,
                };
                json = new JsonpResult<object>(getval);
            }
            catch (Exception ex)
            {
                json = GetReturnJSONP(ex.Message);
                return BadRequest(json);
            }
            return Ok(json);
        }
        #endregion

        #region 保存、添加、逻辑删除功能项
        /// <summary>
        /// 保存、添加、逻辑删除功能项
        /// </summary>
        /// <param name="systemFunction"></param>
        /// <returns></returns>
        [HttpPost("Functions/{systemFunction.FunctionID}")]
        public async Task<object> SaveFunctionInfo(SystemFunction systemFunction)
        {
            JsonpResult<object> json = GetReturnJSONP("初始化中...");
            try
            {
                if (string.IsNullOrWhiteSpace(systemFunction.FunctionName) || string.IsNullOrWhiteSpace(systemFunction.FunctionPath) || systemFunction.ModuleID <= 0)
                {
                    json = GetReturnJSONP("必要参数不能为空!");
                    return BadRequest(json);
                }
                ReturnModel rm = await SystemFunctionService.SaveEntityInfo(systemFunction);
                json = GetReturnJSONP(rm.msg, rm.BooleanResult);
            }
            catch (Exception ex)
            {
                json = GetReturnJSONP(ex.Message);
                return BadRequest(json);
            }
            return Ok(json);
        }
        #endregion
        #endregion
    }
}