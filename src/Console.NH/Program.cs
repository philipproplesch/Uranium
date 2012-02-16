using System;
using Console.NH.Models;
using Microsoft.Practices.ServiceLocation;
using Uranium.Core.Data.Common;
using Uranium.Core.Infrastructure;

namespace Console.NH
{
    class Program
    {
        static void Main(string[] args)
        {
            Bootstrapper.Bootstrap();

            var repository = 
                ServiceLocator.Current.GetInstance<IRepository<User, Guid>>();

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
