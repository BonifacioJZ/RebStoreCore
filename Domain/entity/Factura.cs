using System.Collections.Generic;
namespace Domain.entity {
    public class Factura {
        public int factura_id;
        public decimal total;
        public int client_id;
        public Client client { get; set; }
        public int user_id;
        public User user { get; set; }
        public ICollection<List_items> list_Items { get; set; }
    }
}