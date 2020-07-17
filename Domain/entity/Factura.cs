using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.entity {
    public class Factura {
        public Guid FacturaId{ get; set; }
        [Column(TypeName="decimal(18,0)")]
        public decimal total{ get; set; }
        public DateTime fecha { get; set; }
        public Guid ClientId{ get; set; }
        public Client client { get; set; }
        public ICollection<List_items> list_Items { get; set; }
    }
}