using System.Net;
using System.Threading;
using System.Threading.Tasks;
using App.contracts;
using App.MidlewareError;
using Domain.entity;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace App.Security
{
    public class Login
    {
       public class Ejected : IRequest<UserData> {
           public string Email{ get; set; }
           public string password { get; set; }

        }
        public class EjectedValidation :AbstractValidator<Ejected>{
            public EjectedValidation(){
                RuleFor(x => x.Email).NotEmpty();
                RuleFor(x => x.password).NotEmpty();
                RuleFor(x => x.Email).EmailAddress();
            }
        }
        public class Handler : IRequestHandler<Ejected, UserData>
        {
            private readonly UserManager<User> _userManager;
            private readonly SignInManager<User> _signInManager;
            private readonly IJwtGenerate _jwtGenerate;
            public Handler(UserManager<User> manager, SignInManager<User> signInManager, IJwtGenerate generate){
                _userManager = manager;
                
                _signInManager = signInManager;

                _jwtGenerate = generate;


            }
            public async Task<UserData> Handle(Ejected request, CancellationToken cancellationToken)
            {
                var usuario = await _userManager.FindByEmailAsync(request.Email);
                if(usuario == null){
                    throw new HandlerError(HttpStatusCode.Unauthorized);
                }
                var result = await _signInManager.CheckPasswordSignInAsync(usuario, request.password,false);
                if(result.Succeeded){
                    return new UserData
                    {
                        name = usuario.name,
                        last_name = usuario.las_name,
                        Token = _jwtGenerate.CreateToken(usuario),
                        Email = usuario.Email,
                        Username = usuario.UserName,
                        Image = null
                    };
                }
                throw new HandlerError(HttpStatusCode.Unauthorized);
            }   
        }
    }
}