using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain.entity;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace App.client
{
    public class Consult
    {
        public class ConsultList:IRequest<List<Client>>{

        }
        public class handler : IRequestHandler<ConsultList, List<Client>>
        {
            private readonly RebStoreContext _rebStore;
            public handler(RebStoreContext context){
                _rebStore = context;
            }
            public async Task<List<Client>> Handle(ConsultList request, CancellationToken cancellationToken)
            {
                var clients = await _rebStore.Client.ToListAsync();
                return clients;
            }
        }
    }
}