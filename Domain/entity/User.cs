using System.Collections.Generic;
namespace Domain.entity {
    public class User {
        public int UserId { get; set; }
        public string username { get; set; }
        public string name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public ICollection<Factura> facturas { get; set; }

    }
}