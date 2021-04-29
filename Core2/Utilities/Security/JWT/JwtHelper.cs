using Core.Entities.Concrete;
using Core.Extensions;
using Core.Utilities.Security.Encryption;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Core.Utilities.Security.JWT
{
    public class JwtHelper : ITokenHelper
    {
        public IConfiguration Configuration { get; }//IConfiguration:appsettings dosyasındaki değerleri okumamıza yarıyor
        private TokenOptions _tokenOptions;//appsettings'deki değerleri TokenOptions nesnesine atayacağız
        private DateTime _accessTokenExpiration;//AccessToken ne zaman geçersiz olacak
        public JwtHelper(IConfiguration configuration)//IConfigurationı enjekte ettik
        {
            Configuration = configuration;
            _tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>();
            //appsettings'deki TokenOptions section'ındaki bilgileri TokenOptions classına ata. 
        }
        public AccessToken CreateToken(User user, List<OperationClaim> operationClaims)//CreateToken methodunu implemente ediyoruz.
        {//Yukarıda bana user bilgisini ve claimleri ver ona göre token oluşturayım diyor
            _accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);//Token ne zaman bitecek,zamanı TokenOptions'dan al
            var securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey);//SecurityKey'i TokenOptions'dan al
            var signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey);//Hangi securityKey ve algoritmayı kullanacağımız
            var jwt = CreateJwtSecurityToken(_tokenOptions, user, signingCredentials, operationClaims);//JWTSecurityToken üretmek
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var token = jwtSecurityTokenHandler.WriteToken(jwt);

            return new AccessToken
            {
                Token = token,
                Expiration = _accessTokenExpiration
            };

        }

        public JwtSecurityToken CreateJwtSecurityToken(TokenOptions tokenOptions, User user,
            SigningCredentials signingCredentials, List<OperationClaim> operationClaims)//Bu bilgileri vererek JWT oluşturuyoruz
        {
            var jwt = new JwtSecurityToken(
                issuer: tokenOptions.Issuer,
                audience: tokenOptions.Audience,
                expires: _accessTokenExpiration,
                notBefore: DateTime.Now,//notBefore: şu andan önceki bir değer verilemez
                claims: SetClaims(user, operationClaims),
                signingCredentials: signingCredentials
            );
            return jwt;
        }

        private IEnumerable<Claim> SetClaims(User user, List<OperationClaim> operationClaims)//Claim listesi oluşturuyoruz
        {
            var claims = new List<Claim>();
            claims.AddNameIdentifier(user.Id.ToString());
            claims.AddEmail(user.Email);
            claims.AddName($"{user.FirstName} {user.LastName}");
            claims.AddRoles(operationClaims.Select(c => c.Name).ToArray());

            return claims;
        }
    }
}
