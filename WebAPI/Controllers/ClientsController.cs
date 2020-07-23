using System.Collections.Generic;
using System.Threading.Tasks;
using App.client;
using Domain.entity;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
   
    public class ClientsController : MyControllerBase
    {
       

        [HttpGet]
        public async Task<ActionResult<List<Client>>> Get(){
           
            return await Mediator.Send(new Consult.ConsultList());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Client>> ClientID(int id){
           
            return await Mediator.Send(new ConsultId.ClientId { Id = id });
        }

       [HttpPost]
       public async Task<ActionResult<Unit>> Create(Create.Ejected data){
           
           return await Mediator.Send(data);

        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Unit>> Edit(int id,Edit.Ejected data){

            data.ClientId = id;
            return await Mediator.Send(data);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> Delete(int id){
            return await Mediator.Send(new Delete.Ejected { ClientId = id });
        }
    }
}