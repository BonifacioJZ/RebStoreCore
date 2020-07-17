using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Domain.entity {
    public class User : IdentityUser {
        public string name{ get; set; }
        public string las_name { get; set; }
    }
}