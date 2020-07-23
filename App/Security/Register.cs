using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using App.contracts;
using App.MidlewareError;
using Domain.entity;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace App.Security
{
    public class Register
    {
       public class Ejected : IRequest<UserData>{
           public string name { get; set; }
           public string last_name { get; set; }
           public string username { get; set; }
            public string Email { get; set; }
           public string Password { get; set; }
        }
        public class Handler : IRequestHandler<Ejected,UserData>
        {

            private readonly RebStoreContext _rebStoreContext;
            private readonly UserManager<User> _userManager;
            private readonly IJwtGenerate _jwtGenerate;
            public Handler(RebStoreContext rebStoreContext,UserManager<User> userManager,IJwtGenerate jwtGenerate){
                
                this._rebStoreContext = rebStoreContext;
                
                this._userManager = userManager;
                
                this._jwtGenerate = jwtGenerate;


            }
            public class EjectedValid: AbstractValidator<Ejected>{
                public EjectedValid(){
                    RuleFor(x => x.name).NotEmpty();
                    RuleFor(x=> x.last_name).NotEmpty();
                    RuleFor(x => x.Email).NotEmpty();
                    RuleFor(x => x.username).NotEmpty();
                    RuleFor(x => x.Email).NotEmpty();
                    RuleFor(x => x.Password).NotEmpty();
                }
            }
            public async Task<UserData> Handle(Ejected request, CancellationToken cancellationToken)
            {
                var exist = await _rebStoreContext.Users.Where(x => x.Email == request.Email).AnyAsync();

                if(exist)
                    throw new HandlerError(HttpStatusCode.BadRequest,new {mensaje= "El email ya esta Registrado"});
                exist = await _rebStoreContext.Users.Where(x => x.UserName == request.username).AnyAsync();
                if(exist)
                    throw new HandlerError(HttpStatusCode.BadRequest, "El nombre de ususario ya esta registrado");

                var user = new User
                {
                    name = request.name,
                    las_name = request.last_name,
                    Email = request.Email,
                    UserName = request.username
                };
                var result = await _userManager.CreateAsync(user,request.Password);
                
                if (result.Succeeded){
                    return new UserData
                    {
                        name = user.name,
                        last_name = user.las_name,
                        Token = _jwtGenerate.CreateToken(user),
                        Username = user.UserName,
                        Email = user.Email
                        

                };
                }
                    
                throw new HandlerError(HttpStatusCode.BadRequest);
        
            }
        }
    }
}