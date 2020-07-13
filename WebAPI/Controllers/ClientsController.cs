using System.Collections.Generic;
using System.Threading.Tasks;
using App.client;
using Domain.entity;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ClientsController(IMediator mediator){
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<Client>>> Get(){
           
            return await _mediator.Send(new Consult.ConsultList());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Client>> ClientID(int id){
           
            return await _mediator.Send(new ConsultId.ClientId { Id = id });
        }

       [HttpPost]
       public async Task<ActionResult<Unit>> Create(Create.Ejected data){
           
           return await _mediator.Send(data);

        } 
    }
}