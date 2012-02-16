using System;
using Uranium.Core.Models.Common;

namespace Uranium.Core.Models
{
    public class EntityBase<TPrimaryKey> : IEntityBase<TPrimaryKey>
    {
        #region Implementation of IEntityBase<TPrimaryKey>

        public TPrimaryKey Id { get; set; }
        public DateTime? Inserted { get; set; }
        public string InsertedBy { get; set; }
        public DateTime? Modified { get; set; }
        public string ModifiedBy { get; set; }
        public bool Deleted { get; set; }

        #endregion
    }
}