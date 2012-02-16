using System;

namespace Uranium.Core.Models.Common
{
    public interface IEntityBase<TPrimaryKey>
    {
        TPrimaryKey Id { get; set; }

        DateTime? Inserted { get; set; }
        string InsertedBy { get; set; }

        DateTime? Modified { get; set; }
        string ModifiedBy { get; set; }

        bool Deleted { get; set; }
    }
}