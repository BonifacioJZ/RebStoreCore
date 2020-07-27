using System.Collections.Generic;
using System;

namespace App.client
{
    public class ClientDto
    {
        public Guid ClientId { get; set; }
        public string name { get; set; }
        public string last_name { get; set; }
        public string lugar { get; set; }
        public ICollection<NumberDto> Numbers { get; set; }
    }
}