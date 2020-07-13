using System.Collections.Generic;
namespace Domain.entity {
    public class Client {
        public int client_id { get; set; }
        public string name { get; set; }
        public string last_name { get; set; }
        public ICollection<Factura> facturas { get; set; }

        public ICollection<ClientNumber> clientNumbers { get; set; }
        

    }
}