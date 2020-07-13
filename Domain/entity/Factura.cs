using System;
using System.Collections.Generic;
namespace Domain.entity {
    public class Factura {
        public int FacturaId{ get; set; }
        public decimal total{ get; set; }
        public DateTime fecha { get; set; }
        public int ClientId{ get; set; }
        public Client client { get; set; }
        public int UserId{ get; set; }
        public User user { get; set; }
        public ICollection<List_items> list_Items { get; set; }
    }
}