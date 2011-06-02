using System;
using FluentNHibernate.Mapping;
using Uranium.Core.Data;

namespace Console.NH.Models
{
    public class User
    {
        public virtual Guid Id { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
    }

    public sealed class UserMap : ClassMap<User>, IMappedEntity
    {
        public UserMap()
        {
            Table("Users");

            Id(x => x.Id);
            Map(x => x.FirstName);
            Map(x => x.LastName);
        }
    }
}
