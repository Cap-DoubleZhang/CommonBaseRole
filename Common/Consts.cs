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
    }
}
