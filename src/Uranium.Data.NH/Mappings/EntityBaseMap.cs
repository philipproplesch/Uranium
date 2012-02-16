using FluentNHibernate.Mapping;
using Uranium.Core.Data;
using Uranium.Core.Data.Common;
using Uranium.Core.Models.Common;

namespace Uranium.Data.NH.Mappings
{
    public class EntityBaseMap<T, TPrimaryKey> : ClassMap<T>, IMappedEntity where T : IEntityBase<TPrimaryKey>
    {
        public EntityBaseMap()
        {
            Id(x => x.Id);
            Map(x => x.Inserted);
            Map(x => x.InsertedBy);
            Map(x => x.Modified);
            Map(x => x.ModifiedBy);
            Map(x => x.Deleted);
        }
    }
}