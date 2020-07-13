using System.Collections.Generic;
namespace Domain.entity {
    public class Tipo {
        public int type_id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public ICollection<Service> services { get; set; }

    }
}