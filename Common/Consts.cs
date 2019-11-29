using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public class Consts
    {
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public static readonly string ConnectionStr = CommonHelper.app(new string[] { "AppSetting", "SQLSetting", "ConnectionStr" });
    }
}
