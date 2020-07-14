using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Persistence;

namespace App.client
{
    public class Delete
    {
        public class Ejected: IRequest{
            public int ClientId{ get; set; }
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
                if(client == null)
                    throw new Exception("No se encutra el curso");
                _rebStoreContext.Remove(client);
                var valor = await _rebStoreContext.SaveChangesAsync();
                if(valor>0)
                    return Unit.Value;
                throw new Exception("No se Guradaron los cambios");
            }
        }
    }
}