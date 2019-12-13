using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Common;
using Microsoft.IdentityModel.Tokens;
using Model.EntityModel.BaseRole;

namespace CommonBaseRole.JwtHelper
{
    public class JwtTokenHelper
    {
        /// <summary>
        /// 获取JwtToken字符串
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static string GetJwtToken(SystemUser user)
        {
            var claims = new Claim[] {
                    //new Claim(ClaimTypes.Name,user.UserLoginName),
                    new Claim(JwtRegisteredClaimNames.UniqueName,user.UserLoginName),
                    new Claim(JwtRegisteredClaimNames.Email,user.UserInfo.EMail),
                    new Claim(JwtRegisteredClaimNames.Sub,user.UserID),
                };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Consts.SecurityKey));
            var token = new JwtSecurityToken(
                issuer: Consts.Issuer,
                audience: Consts.Audience,
                claims: claims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddDays(Convert.ToInt32(Consts.Exp)),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
                );
            var JwtToken = new JwtSecurityTokenHandler().WriteToken(token);
            return JwtToken;
        }
    }
}
