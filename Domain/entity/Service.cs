using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.entity {
    public class Service {
        public Guid ServiceId { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        [Column(TypeName="decimal(18,4)")]
        public decimal precio { get; set; }
        [Column(TypeName="decimal(18,4)")]
        public decimal comicion { get; set; }

        public Guid TipoId { get; set; }
        public Tipo Tipo { get; set; }
        public ICollection<List_items> list_Items { get; set; }
    }
}