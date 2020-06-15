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
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Common.RedisHelper;

namespace CommonBaseRole.Controllers.BaseRole
{
    [Route("api/[controller]")]
    [ApiController]
    public class SystemController : CommonController
    {
        private IAdminModuleService AdminModuleService;
        private ISystemRoleService SystemRoleService;
        private ISystemUserService SystemUserService;
        private IRedisCacheManager RedisCacheManager;
        private ILogger<SystemController> _logger;

        public SystemController(ILogger<SystemController> logger, IAdminModuleService adminModuleService
            , ISystemRoleService systemRoleService, ISystemUserService systemUserService, IRedisCacheManager redisCacheManager)
        {
            this.AdminModuleService = adminModuleService;
            this.SystemRoleService = systemRoleService;
            this.SystemUserService = systemUserService;
            this.RedisCacheManager = redisCacheManager;
            this._logger = logger;
        }

        #region 后台菜单操作
        #region 获取后台菜单全部数据（不分页）
        /// <summary>
        /// 获取后台菜单全部数据（不分页）
        /// </summary>
        /// <returns></returns>
        [HttpGet("Menus")]
        public async Task<IActionResult> GetMenuList()
        {
            string json = GetReturnJSONP("初始化中...");
            try
            {
                List<AdminModule> listOrder = new List<AdminModule>();
                if (RedisCacheManager.Get<object>("Redis.Menus") != null)
                {
                    listOrder = RedisCacheManager.Get<List<AdminModule>>("Redis.Menus");
                }
                else
                {
                    List<AdminModule> list = await AdminModuleService.GetEntity();
                    listOrder = await AdminModuleService.AdminModuleOrder(list);
                    RedisCacheManager.Set("Redis.Menus", listOrder, TimeSpan.FromHours(Consts.RedisExpTime));
                }

                json = GetListSuccessJSONP(listOrder);
            }
            catch (Exception ex)
            {
                json = GetReturnJSONP(ex.Message);
                return BadRequest(json);
            }
            return Ok(json);
        }
        #endregion

        #region 根据角色ID获取对应权限（角色ID格式：1,2）
        /// <summary>
        /// 角色权限
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        [HttpGet("RoleModules/{roleId}")]
        public async Task<IActionResult> GetRoleModule(string roleId = "")
        {
            string json = GetReturnJSONP("初始化中...");
            try
            {
                if (string.IsNullOrWhiteSpace(roleId))
                {
                    json = GetReturnJSONP("必要参数传入错误!");
                    return BadRequest(json);
                }
                List<AdminModule> listOrder = new List<AdminModule>();
                if (RedisCacheManager.Get<object>("Redis.RoleModules") != null)
                {
                    listOrder = RedisCacheManager.Get<List<AdminModule>>("Redis.RoleModules");
                }
                else
                {
                    PageModel pm = new PageModel() { CurrentPage = 1, PageSize = 1000 };
                    pm.Condition.Add(new SearchCondition()
                    {
                        ConditionField = "sr.[RoleID]",
                        SearchType = SearchType.In,
                        ConditionValue1 = roleId,
                    });
                    List<AdminModule> list = await AdminModuleService.GetRoleModules(pm);
                    listOrder = await AdminModuleService.AdminModuleOrder(list);
                    RedisCacheManager.Set("Redis.RoleModules", listOrder, TimeSpan.FromHours(Consts.RedisExpTime));
                }
                json = GetListSuccessJSONP(listOrder);
            }
            catch (Exception ex)
            {
                json = GetReturnJSONP(ex.Message);
                return BadRequest(json);
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
            string json = GetReturnJSONP("初始化中...");
            try
            {
                AdminModule adminModule = await AdminModuleService.TEntityInfo(Id.ToString());
                var getval = new
                {
                    success = true,
                    item = adminModule,
                };
                json = GetReturnJSONP(adminModule);
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
            string json = GetReturnJSONP("初始化中...");
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
            string json = GetReturnJSONP("初始化中...");
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

                //var getval = new
                //{
                //    success = true,
                //    data = list,
                //    pageSize = s,
                //    page = p,
                //    maxPage = pm.MaxPage,
                //    dataCount = pm.DataCount,
                //};
                json = GetListPageSuccessJSONP(list, s, pm.DataCount);
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
            string json = GetReturnJSONP("初始化中...");
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
                json = GetReturnJSONP(getval);
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
            string json = GetReturnJSONP("初始化中...");
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
            string json = GetReturnJSONP("初始化中...");
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
                if (user.IsUse == 0)
                {
                    json = GetReturnJSONP("该账户被禁用，请联系管理员!");
                    return BadRequest(json);
                }
                if (user.Roles == null || user.Roles.Count <= 0)
                {
                    json = GetReturnJSONP("该账户无角色，请联系管理员!");
                    return BadRequest(json);
                }

                var JwtToken = JwtHelper.JwtTokenHelper.GetJwtToken(user);

                var getval = new
                {
                    msg = "登录成功",
                    token = JwtToken,
                };
                json = GetReturnJSONP(getval);
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
            string json = GetReturnJSONP("初始化中...");
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
                json = GetListPageSuccessJSONP(list, s, pm.DataCount);
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
            string json = GetReturnJSONP("初始化中...");
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
                json = GetReturnJSONP(user);
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