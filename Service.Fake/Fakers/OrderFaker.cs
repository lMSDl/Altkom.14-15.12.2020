using Models;
using Service.Interfaces;
using System.Linq;

namespace Service.Fake.Fakers
{
    public class OrderFaker : BaseFaker<Order>
    {
        public OrderFaker(IEntityService<User> usersService, IEntityService<Product>  productsService)
        {
            RuleFor(p => p.User, f => f.PickRandom(usersService.ReadAsync().Result));
            RuleFor(p => p.Products, f => f.PickRandom(productsService.ReadAsync().Result, f.Random.Number(1, 10)).ToList());
        }
    }
}