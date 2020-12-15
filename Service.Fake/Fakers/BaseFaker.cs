using Bogus;
using Models;

namespace Service.Fake.Fakers
{
    public abstract class BaseFaker<T> : Faker<T> where T : Entity
    {
        public BaseFaker() : base("pl")
        {
            //StrictMode(true);
            RuleFor(p => p.Id, f => f.IndexFaker + 1);
        }
    }
}