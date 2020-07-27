using System.Collections.Generic;
using System;
using System.Threading;
using System.Threading.Tasks;
using Domain.entity;
using FluentValidation;
using MediatR;
using Persistence;

namespace App.client
{
    public class Create
    {
        public class Ejected    :   IRequest{
            public string name { get; set; }
            public string last_name { get; set; }
            public string lugar { get; set; }
            public List<Guid> ListNumers { get; set; }
        }

        public class EjectedValidation : AbstractValidator<Ejected>{
            public EjectedValidation(){
                RuleFor(x => x.name).NotEmpty();
                RuleFor(x => x.last_name).NotEmpty();
                RuleFor(x => x.lugar).NotEmpty();

            }
        }

        public class handler : IRequestHandler<Ejected>
        {
            private readonly RebStoreContext _reb;
            public handler(RebStoreContext context){
                _reb = context;
            }
            public async Task<Unit> Handle(Ejected request, CancellationToken cancellationToken)
            {
                Guid clientId = Guid.NewGuid();
                var client = new Client
                {
                    ClientId = clientId,
                    name = request.name,
                    last_name = request.last_name,
                    lugar = request.lugar
                };
                _reb.Client.Add(client);
                
                if(request.ListNumers==null){
                    
                    foreach(var id in request.ListNumers){
                        var clientnumber = new ClientNumber
                        {
                            ClientId = client.ClientId,
                            NumberId = id
                        };
                        _reb.ClientNumber.Add(clientnumber);
                    }
                }

                var valor =await _reb.SaveChangesAsync();
                
                if(valor >0){
                    return Unit.Value;
                }
                throw new Exception("error");

            }
        }
    }
}