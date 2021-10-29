namespace ZDef.Core.EventBus
{
    public class EventBus
    {
        private readonly BusEventServiceLocator _locator = new BusEventServiceLocator();

        public void Subscribe<TArgs>(BusEventDelegate<TArgs> listener)
        {
            _locator.Locate<TArgs>().Subscribe(listener);
        }
        
        public void UnSubscribe<TArgs>(BusEventDelegate<TArgs> listener)
        {
            _locator.Locate<TArgs>().UnSubscribe(listener);
        }

        public void Send<TArgs>(TArgs args)
        {
            _locator.Locate<TArgs>().Send(args);
        }
    }
}
