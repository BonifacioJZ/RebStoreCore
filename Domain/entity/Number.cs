using System.Collections.Generic;
namespace Domain.entity {
    public class Number {
        public int NumberId { get; set; }
        public string number { get; set; }
        public int ServiceId { get; set; }
        public Service service { get; set; }

        public ICollection<ClientNumber> clientNumbers { get; set; }
    }
}