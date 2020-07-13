namespace Domain.entity {
    public class List_items {
        public int List_itemsId { get; set; }
        public int cantidad { get; set; }
        public decimal precio { get; set; }
        public decimal comicion { get; set; }
        public int ServiceId { get; set; }
        public Service service { get; set; }
        public int FacturaId { get; set; }
        public Factura factura { get; set; }

    }
}