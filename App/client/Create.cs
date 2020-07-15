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
                var client = new Client
                {
                    name = request.name,
                    last_name = request.last_name,
                    lugar = request.lugar
                };
                _reb.Client.Add(client);
                var valor =await _reb.SaveChangesAsync();
                if(valor >0){
                    return Unit.Value;
                }
                throw new Exception("error");

            }
        }
    }
}