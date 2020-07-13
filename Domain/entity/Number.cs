using System.Collections.Generic;
namespace Domain.entity {
    public class Number {
        public int number_id { get; set; }
        public string number { get; set; }
        public int service_id { get; set; }
        public Service service { get; set; }

        public ICollection<ClientNumber> clientNumbers { get; set; }
    }
}