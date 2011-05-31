namespace Uranium.Core.Data
{
    public interface IDatabaseInitializer
    {
        void Execute();
        int Order { get; }
    }
}
