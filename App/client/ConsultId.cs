using System.Threading;
using System.Threading.Tasks;
using Domain.entity;
using MediatR;
using Persistence;

namespace App.client
{
    public class ConsultId
    {
        public class ClientId: IRequest<Client>{
            public int Id{ get; set; }
        }
        public class handler : IRequestHandler<ClientId, Client>
        {
            private readonly RebStoreContext reb;

            public handler (RebStoreContext context){
                reb = context;
            }
            public async Task<Client> Handle(ClientId request, CancellationToken cancellationToken)
            {
                var client = await reb.Client.FindAsync(request.Id);
                return client;
            }
        }
    }
}