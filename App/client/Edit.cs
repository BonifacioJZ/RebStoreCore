using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Persistence;

namespace App.client
{
    public class Edit
    {
        public class Ejected : IRequest{
            
            public int ClientId{ get; set; }
            public string name { get; set; }
            public string last_name { get; set; }
            public string lugar { get; set; }
        }

        public class handler : IRequestHandler<Ejected>
        {
            private readonly RebStoreContext _rebStoreContext;
            public handler(RebStoreContext context){
                _rebStoreContext = context;
            }
            public async Task<Unit> Handle(Ejected request, CancellationToken cancellationToken)
            {
                var client = await _rebStoreContext.Client.FindAsync(request.ClientId);
                if(client==null){
                    throw new Exception("El cliente no existe");
                }

                client.name = request.name ?? client.name;
                client.last_name = request.last_name ?? client.last_name;
                client.lugar = request.lugar ?? client.lugar;

                var valor = await _rebStoreContext.SaveChangesAsync();
                if(valor>0)
                    return Unit.Value;
                throw new Exception("error");


            }
        }
    }
}