namespace Domain.entity {
    public class ClientNumber {
        public int ClientId { get; set; }
        public Client client { get; set; }
        public int NumberId{ get; set; }
        public Number number{ get; set; }
    }
}