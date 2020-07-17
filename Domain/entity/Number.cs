using System;
using System.Collections.Generic;
namespace Domain.entity {
    public class Number {
        public Guid NumberId { get; set; }
        public string number { get; set; }
        public Guid ServiceId { get; set; }
        public Service service { get; set; }

        public ICollection<ClientNumber> clientNumbers { get; set; }
    }
}