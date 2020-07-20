using Domain.entity;

namespace App.contracts {
    public interface IJwtGenerate {
        string CreateToken (User user);
    }
}