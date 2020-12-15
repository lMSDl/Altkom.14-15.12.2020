using Bogus;
using Models;

namespace Service.Fake.Fakers
{
    public class UserFaker : BaseFaker<User>
    {
        public UserFaker() {
            RuleFor(x => x.Username, f => f.Person.UserName);
            RuleFor(x => x.Email, f => f.Person.Email);
            RuleFor(x => x.Gender, f => f.PickRandom<Gender>());
        }   
    }
}