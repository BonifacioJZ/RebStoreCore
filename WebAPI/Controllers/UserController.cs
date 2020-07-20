using System.Threading.Tasks;
using App.Security;
using Domain.entity;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{


    public class UserController : MyControllerBase
    {
        [HttpPost("login")]
        public async Task<ActionResult<UserData>> Login(Login.Ejected login){
            return await Mediator.Send(login);

        }
    }
}