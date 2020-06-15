using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CommonBaseRoleUI.Models;
using IServices.BaseRole;
using Common.RedisHelper;
using Model.EntityModel.BaseRole;
using Model;
using Common;

namespace CommonBaseRoleUI.Controllers
{
    public class HomeController : Controller
    {
        private IAdminModuleService AdminModuleService;
        private ISystemRoleService SystemRoleService;
        private ISystemUserService SystemUserService;
        private IRedisCacheManager RedisCacheManager;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IAdminModuleService adminModuleService
            , ISystemRoleService systemRoleService, ISystemUserService systemUserService, IRedisCacheManager redisCacheManager)
        {
            this.AdminModuleService = adminModuleService;
            this.SystemRoleService = systemRoleService;
            this.SystemUserService = systemUserService;
            this.RedisCacheManager = redisCacheManager;
            _logger = logger;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        /// <summary>
        /// 母版页
        /// </summary>
        /// <returns></returns>
        public IActionResult _Master()
        {
            return View();
        }

        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 母版页头部
        /// </summary>
        /// <returns></returns>
        public IActionResult _Header()
        {
            //SystemUser systemUser = new SystemUser();
            return PartialView();
        }

        /// <summary>
        /// 母版页菜单部
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> _LeftMenuAsync()
        {
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
                    ConditionValue1 = "1",//暂为1，实际应获取当前用户的角色ID
                });
                List<AdminModule> list = await AdminModuleService.GetRoleModules(pm);
                listOrder = await AdminModuleService.AdminModuleOrder(list);
                RedisCacheManager.Set("Redis.RoleModules", listOrder, TimeSpan.FromHours(Consts.RedisExpTime));
            }
            return PartialView(listOrder);
        }
    }
}
