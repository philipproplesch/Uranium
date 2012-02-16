namespace Uranium.Core.Data.Common
{
    public interface IDatabaseSeeder
    {
        void Seed();
        int Order { get; }
    }
}
