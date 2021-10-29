namespace ZDef.Core.PoolFactory
{
    public interface IFactoryInit<in TArgs>
    {
        void Init(TArgs args);
    }
}