using FluentValidator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ModernStore2.Api.Security;
using ModernStore2.Domain.Commands;
using ModernStore2.Domain.Entities;
using ModernStore2.Domain.Repositories;
using ModernStore2.Infra.Transactions;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace ModernStore2.Api.Controllers
{
    public class AccountController:BaseController
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly TokenOptions _tokenOptions;
        private Customer _customer;

        public AccountController(IOptions<TokenOptions> jwtOptions, IUow uow, ICustomerRepository customerRepository):base(uow)
        {
            _customerRepository = customerRepository;
            _tokenOptions = jwtOptions.Value;
            ThrowIfInvalidOptions(_tokenOptions);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("v1/authenticate")]
        public async Task<IActionResult> Post([FromForm] AuthenticateUserCommand command)
        {
            if(command == null)
                return await Response(null, new List<Notification> { new Notification("User", "Usuário ou senha inválidos") });

            var identity = await GetClaims(command);
            if (identity == null)
                return await Response(null, new List<Notification> { new Notification("User", "Usuário ou senha inválidos") });

            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, command.Username),
                new Claim(JwtRegisteredClaimNames.NameId, command.Username),
                new Claim(JwtRegisteredClaimNames.Email, command.Username),
                new Claim(JwtRegisteredClaimNames.Sub, command.Username),
                new Claim(JwtRegisteredClaimNames.Jti, await _tokenOptions.JtiGenerator()),
                new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(_tokenOptions.IssuedAt).ToString(), ClaimValueTypes.Integer64),
            };

            claims.AddRange(identity.Claims);

            var jwt = new JwtSecurityToken(
                issuer: _tokenOptions.Issuer,
                audience: _tokenOptions.Audience,
                claims: claims.AsEnumerable(),
                notBefore: _tokenOptions.NotBefore,
                expires: _tokenOptions.Expiration,
                signingCredentials: _tokenOptions.SigningCredentials);

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt,
                expires = (int)_tokenOptions.ValidFor.TotalSeconds,
                user = new
                {
                    id = _customer.Id,
                    name = _customer.Name.ToString(),
                    email = _customer.Email.Address,
                    username = _customer.User.Username
                }
            };

            return new OkObjectResult(response);
        }

        private static long ToUnixEpochDate(DateTime date)
          => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);

        private static void ThrowIfInvalidOptions(TokenOptions options)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));

            if (options.ValidFor <= TimeSpan.Zero)
                throw new ArgumentException("O período deve ser maior que zero", nameof(TokenOptions.ValidFor));

            if (options.SigningCredentials == null)
                throw new ArgumentNullException(nameof(TokenOptions.SigningCredentials));

            if (options.JtiGenerator == null)
                throw new ArgumentNullException(nameof(TokenOptions.JtiGenerator));
        }

        private Task<ClaimsIdentity> GetClaims(AuthenticateUserCommand command)
        {
            var customer = _customerRepository.GetByUsername(command.Username);

            if (customer == null)
                return Task.FromResult<ClaimsIdentity>(null);

            if(!customer.User.Authenticate(command.Username, command.Password))
                return Task.FromResult<ClaimsIdentity>(null);

            _customer = customer;

            return Task.FromResult(new ClaimsIdentity(
                                new GenericIdentity(customer.User.Username, "Token"),
                                new[] {
                                    new Claim("ModernStore", "User"),
                                    new Claim("ModernStore", "Admin")
                                }));
        }
    }
}
