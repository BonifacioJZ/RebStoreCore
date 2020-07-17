using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.entity {
    public class List_items {
        public Guid List_itemsId { get; set; }
        public int cantidad { get; set; }
       [Column(TypeName="decimal(18,4)")]
        public decimal precio { get; set; }
        [Column(TypeName="decimal(18,4)")]
        public decimal comicion { get; set; }
        public Guid ServiceId { get; set; }
        public Service service { get; set; }
        public Guid FacturaId { get; set; }
        public Factura factura { get; set; }

    }
}