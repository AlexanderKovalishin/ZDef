namespace ZDef
{
    public interface IReturnToPoolCallback<out T>
    {
        event DestroyDelegate<T> ReturnToPool;
    }
}