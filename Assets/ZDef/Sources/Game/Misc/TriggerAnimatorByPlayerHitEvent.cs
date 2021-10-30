using UnityEngine;
using ZDef.Core;
using ZDef.Core.EventBus;
using ZDef.Game.BusEvents;

namespace ZDef.Game.Misc
{
    public class TriggerAnimatorByPlayerHitEvent : TriggerAnimatorByEvent<PlayerHitEvent>
    {
    }
}