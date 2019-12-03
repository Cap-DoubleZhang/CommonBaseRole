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

namespace CommonBaseRole.Controllers.BaseRole
{
    [Route("api/[controller]")]
    [ApiController]
    public class SystemController : ControllerBase
    {
        private IAdminModuleService adminModuleService;
        private ILogger<SystemController> _logger;

        public SystemController(ILogger<SystemController> logger, IAdminModuleService adminModuleService)
        {
            this.adminModuleService = adminModuleService;
            this._logger = logger;
        }

        [HttpGet("/Menus")]
        public async Task<ActionResult> GetMenuList()
        {
            List<AdminModule> list = await adminModuleService.GetEntity();
            return Ok();
        }
    }
}