namespace Domain.entity {
    public class List_items {
        public int list_id { get; set; }
        public int cantidad { get; set; }
        public decimal precio { get; set; }
        public decimal comicion { get; set; }
        public int service_id { get; set; }
        public Service service { get; set; }
        public int factura_id { get; set; }
        public Factura factura { get; set; }

    }
}