using System.Collections.Generic;
namespace Domain.entity {
    public class Service {
        public int ServiceId { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public decimal precio { get; set; }
        public decimal comicion { get; set; }

        public int TipoId { get; set; }
        public Tipo Tipo { get; set; }
        public ICollection<List_items> list_Items { get; set; }
    }
}