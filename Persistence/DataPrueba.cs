using System.Linq;
using System.Threading.Tasks;
using Domain.entity;
using Microsoft.AspNetCore.Identity;

namespace Persistence
{
    public class DataPrueba
    {
        public static async Task InsertData(RebStoreContext context,UserManager<User> manager ){

            if (!manager.Users.Any())
            {
                var user = new User
                {
                    name = "Bonifacio",
                    las_name = "Juarez",
                    UserName = "reb",
                    Email = "revanjz6@gmail.com",
                    
                };
                await manager.CreateAsync(user,"Ghost6699#");


            }

        }
    }
}