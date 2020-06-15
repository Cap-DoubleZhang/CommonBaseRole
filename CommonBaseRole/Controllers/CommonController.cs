using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

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
        public string GetReturnJSONP(string message, bool success = false)
        {
            var getval = new
            {
                success = success,
                msg = message
            };

            return JsonConvert.SerializeObject(getval);
        }

        /// <summary>
        /// 返回自定义数据
        /// </summary>
        /// <param name="message">自定义数据</param>
        /// <returns></returns>
        [ApiExplorerSettings(IgnoreApi = true)]
        public string GetReturnJSONP(object message)
        {
            var getval = new
            {
                success = true,
                msg = message
            };

            return JsonConvert.SerializeObject(getval);
        }

        /// <summary>
        /// 接口返回数据列表信息(分页)
        /// </summary>
        /// <typeparam name="Entity">数据实体类型</typeparam>
        /// <param name="list">数据实体</param>
        /// <param name="s">页容量</param>
        /// <param name="count">总条数</param>
        /// <param name="extData"></param>
        /// <returns></returns>
        [ApiExplorerSettings(IgnoreApi = true)]
        public static string GetListPageSuccessJSONP<Entity>(List<Entity> list, int s, int count, object extData = null)
        {
            var getval = new
            {
                success = true,
                code = 0,
                data = list,
                datacount = count,
                maxpage = (int)Math.Ceiling((Decimal)count / s),
                extdata = extData ?? "",
            };

            return JsonConvert.SerializeObject(getval, new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" });
        }

        /// <summary>
        /// 接口返回数据列表信息
        /// </summary>
        /// <typeparam name="Entity">数据实体类型</typeparam>
        /// <param name="list">数据实体</param>
        /// <returns></returns>
        [ApiExplorerSettings(IgnoreApi = true)]
        public static string GetListSuccessJSONP<Entity>(List<Entity> list)
        {
            var getval = new
            {
                success = true,
                code = 0,
                data = list,
            };

            return JsonConvert.SerializeObject(getval, new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" });
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