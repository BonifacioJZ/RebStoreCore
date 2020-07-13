namespace Domain.entity {
    public class ClientNumber {
        public int client_id { get; set; }
        public Client client { get; set; }
        public int number_id{ get; set; }
        public Number number{ get; set; }
    }
}