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
        public JsonpResult<object> GetErrorJSONP(string message)
        {
            var getval = new
            {
                success = false,
                code = "0000",
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