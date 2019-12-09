using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Common
{
    /// <summary>
    /// 加密类
    /// </summary>
    public class EncryptHelper
    {
        /// <summary>
        /// 根据加密类型判断使用怎样的加密方式（默认MD5 大写32为加密）
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string MD5Encode(string source)
        {
            string newStr = "";
            if (Consts.EncryptType != "0")
            {
                using (var md5 = MD5.Create())
                {
                    var data = md5.ComputeHash(Encoding.UTF8.GetBytes(source));
                    StringBuilder builder = new StringBuilder();
                    // 循环遍历哈希数据的每一个字节并格式化为十六进制字符串 
                    for (int i = 0; i < data.Length; i++)
                    {
                        builder.Append(data[i].ToString("X2"));
                    }
                    if (Consts.EncryptType == "1")
                    {
                        newStr = builder.ToString();
                    }
                    else if (Consts.EncryptType == "2")
                    {
                        newStr = builder.ToString().Substring(Convert.ToInt32(Consts.SubStart), Convert.ToInt32(Consts.SubString));
                    }
                }
            }
            return newStr;
        }
    }
}
