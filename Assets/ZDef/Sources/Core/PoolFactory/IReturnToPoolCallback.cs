namespace ZDef.Core.PoolFactory
{
    public interface IReturnToPoolCallback<out T>
    {
        event DestroyDelegate<T> ReturnToPool;
    }
}