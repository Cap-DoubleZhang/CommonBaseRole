using System;

namespace Common
{
    public class Consts
    {
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public static readonly string ConnectionStr = CommonHelper.app(new string[] { "AppSetting", "SQLSetting", "ConnectionStr" });

        /// <summary>
        /// 密码加密方式
        /// </summary>
        public static string EncryptType = CommonHelper.app(new string[] { "AppSetting", "PasswordSetting", "EncryptType" });

        /// <summary>
        /// 密码截取开始索引
        /// </summary>
        public static string SubStart = CommonHelper.app(new string[] { "AppSetting", "PasswordSetting", "SubStart" });

        /// <summary>
        /// 密码截取位数
        /// </summary>
        public static string SubString = CommonHelper.app(new string[] { "AppSetting", "PasswordSetting", "SubString" });

        /// <summary>
        /// JWT秘钥
        /// </summary>
        public static string SecurityKey = CommonHelper.app(new string[] { "AppSetting", "JwtSetting", "SecurityKey" });

        /// <summary>
        /// JWT发送者
        /// </summary>
        public static string Issuer = CommonHelper.app(new string[] { "AppSetting", "JwtSetting", "issuer" });

        /// <summary>
        /// JWT接收者
        /// </summary>
        public static string Audience = CommonHelper.app(new string[] { "AppSetting", "JwtSetting", "audience" });

        /// <summary>
        /// JWT过期时间（DAY）
        /// </summary>
        public static string Exp = CommonHelper.app(new string[] { "AppSetting", "JwtSetting", "exp" });

        /// <summary>
        /// Redis IP地址及端口号
        /// </summary>
        public static string RedisIPAddress = CommonHelper.app(new string[] { "AppSetting", "RedisSetting", "IPAddress" });

        /// <summary>
        /// Redis 密码
        /// </summary>
        public static string RedisPassword = CommonHelper.app(new string[] { "AppSetting", "RedisSetting", "Password" });

        /// <summary>
        /// Redis 缓存随机小时
        /// </summary>
        public static int RedisExpTime = new Random().Next(Convert.ToInt32(CommonHelper.app(new string[] { "AppSetting", "RedisSetting", "StartTime" })), Convert.ToInt32(CommonHelper.app(new string[] { "AppSetting", "RedisSetting", "EndTime" })));
    }
}
