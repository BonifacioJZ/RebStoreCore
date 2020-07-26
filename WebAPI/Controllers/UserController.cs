using System.Threading.Tasks;
using App.Security;
using Domain.entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{

    [AllowAnonymous]
    public class UserController : MyControllerBase
    {
        [HttpPost("login")]
        public async Task<ActionResult<UserData>> Login(Login.Ejected login){
            return await Mediator.Send(login);

        }
        [HttpPost("register")]
        public async Task<ActionResult<UserData>> Register(Register.Ejected data){
            return await Mediator.Send(data);
        }
        [HttpGet]
        public async Task<ActionResult<UserData>> GetUser(){
            return await Mediator.Send(new UserActual.Ejected());
        }
    }
}