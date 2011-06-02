using Console.NH.Models;
using Microsoft.Practices.ServiceLocation;
using Uranium.Core.Data;
using Uranium.Core.Infrastructure;

namespace Console.NH
{
    class Program
    {
        static void Main(string[] args)
        {
            Bootstrapper.Bootstrap(
                asm => asm.FullName.StartsWith("Console."));

            var repository =
                ServiceLocator.Current.GetInstance<IRepository<User>>();

            var user =
                new User
                    {
                        FirstName = "Philip",
                        LastName = "Proplesch"
                    };

            repository.Insert(user);
        }
    }
}
