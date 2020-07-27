using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Domain.entity;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace App.client
{
    public class Consult
    {
        public class ConsultList:IRequest<List<ClientDto>>{

        }
        public class handler : IRequestHandler<ConsultList, List<ClientDto>>
        {
            private readonly RebStoreContext _rebStore;
            private readonly IMapper _mapper;
            public handler(RebStoreContext context,IMapper mapper){
                _rebStore = context;
                _mapper = mapper;
            }
            public async Task<List<ClientDto>> Handle(ConsultList request, CancellationToken cancellationToken)
            {
                var clients = await _rebStore.Client
                .Include(x => x.clientNumbers)
                .ThenInclude(x=>x.number).ToListAsync();

                var clientDto = _mapper.Map<List<Client>, List<ClientDto>>(clients);
                
                return clientDto;
            }
        }
    }
}