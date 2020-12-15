using Bogus;
using Models;

namespace Service.Fake.Fakers
{
    public class ProductFaker : BaseFaker<Product>
    {
        public ProductFaker() {
            RuleFor(x => x.Name, f => f.Commerce.ProductName());
            RuleFor(x => x.Price, f => float.Parse(f.Commerce.Price()));
        }   
    }
}