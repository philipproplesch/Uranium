using System;
using Console.NH.Models;
using Uranium.Data.NH.Mappings;

namespace Console.NH.Mappings
{
    public sealed class UserMap : EntityBaseMap<User, Guid>
    {
        public UserMap()
        {
            Map(x => x.FirstName);
            Map(x => x.LastName);
        }
    }
}