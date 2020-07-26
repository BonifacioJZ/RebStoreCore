using System.Threading;
using System.Threading.Tasks;
using App.contracts;
using Domain.entity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Persistence;

namespace App.Security
{
    public class UserActual
    {
        public class Ejected : IRequest<UserData>{}
        public class Handler : IRequestHandler<Ejected, UserData>
        {
            private readonly UserManager<User> _userManager;
            private readonly IJwtGenerate _jwtGenerate;
            private readonly IUserSession _userSession;
        

            public Handler(IUserSession userSession,UserManager<User> userManager,IJwtGenerate jwtGenerate){
            _jwtGenerate = jwtGenerate;
            _userManager = userManager;
            _userSession = userSession;

        }
            public async Task<UserData> Handle(Ejected request, CancellationToken cancellationToken)
            {
               var user = await _userManager.FindByNameAsync(_userSession.GetUserSession());
                return new UserData
                {
                    name = user.name,
                    last_name = user.las_name,
                    Username = user.UserName,
                    Token = _jwtGenerate.CreateToken(user),
                    Image = null,
                    Email = user.Email
            };

            }
        }
    }
}