using System;
namespace Domain.entity {
    public class ClientNumber {
        public Guid ClientId { get; set; }
        public Client client { get; set; }
        public Guid NumberId{ get; set; }
        public Number number{ get; set; }
    }
}