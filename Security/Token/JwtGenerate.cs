using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using App.contracts;
using Domain.entity;
using Microsoft.IdentityModel.Tokens;

namespace Security.Token {
    public class JwtGenerate : IJwtGenerate {
        public string CreateToken (User user) {

            var claims = new List<Claim> {
                new Claim (JwtRegisteredClaimNames.NameId, user.UserName)
            };

            var key = new SymmetricSecurityKey (Encoding.UTF8.GetBytes ("mi palabra secreta"));

            var credential = new SigningCredentials (key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescription = new SecurityTokenDescriptor {
                Subject = new ClaimsIdentity (claims),
                Expires = DateTime.Now.AddDays (10),
                SigningCredentials = credential
            };

            var tokenHandler = new JwtSecurityTokenHandler ();

            var token = tokenHandler.CreateToken (tokenDescription);

            return tokenHandler.WriteToken (token);
        }
    }
}