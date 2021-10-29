namespace ZDef
{
    public interface IFactoryInit<in TArgs>
    {
        void Init(TArgs args);
    }
}