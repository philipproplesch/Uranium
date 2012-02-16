using System;
using Uranium.Core.Models;

namespace Console.NH.Models
{
    public class User : EntityBase<Guid>
    {
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
    }
}
