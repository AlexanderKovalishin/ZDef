namespace ZDef.Core.EventBus
{
    public delegate void BusEventDelegate<in TArgs>(TArgs args);
}