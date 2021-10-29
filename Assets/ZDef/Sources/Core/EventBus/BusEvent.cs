namespace ZDef.Core.EventBus
{
    internal class BusEvent<TArgs>
    {
        private event BusEventDelegate<TArgs> _listener;

        public void Subscribe(BusEventDelegate<TArgs> listener)
        {
            _listener += listener;
        }

        public void UnSubscribe(BusEventDelegate<TArgs> listener)
        {
            _listener -= listener;
        }

        public void Send(TArgs args)
        {
            _listener?.Invoke(args);
        }
    }
}