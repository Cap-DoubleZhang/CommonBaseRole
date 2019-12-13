using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CommonBaseRole.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommonController : ControllerBase
    {
        //public string IPAddress;
        //public CommonController()
        //{
        //    IPAddress = HttpContext.Connection.RemoteIpAddress.ToString();
        //}

        /// <summary>
        /// 接口处返回提示信息
        /// </summary>
        /// <param name="message">提示</param>
        /// <param name="success">是否成功，默认False</param>
        /// <returns></returns>
        [ApiExplorerSettings(IgnoreApi = true)]
        public JsonpResult<object> GetReturnJSONP(string message, bool success = false)
        {
            var getval = new
            {
                success = success,
                msg = message
            };

            return new JsonpResult<object>(getval);
        }

        public class JsonpResult<T> : OkResult
        {
            public JsonpResult(T obj, string callback)
            {
                this.Obj = obj;
                this.CallbackName = callback;
            }

            /// <summary>
            /// 前端返回数据类型
            /// </summary>
            /// <param name="obj"></param>
            public JsonpResult(T obj)
            {
                this.Obj = obj;
                CallbackName = "";
            }

            public T Obj { get; set; }
            public string CallbackName { get; set; }
        }
    }
}