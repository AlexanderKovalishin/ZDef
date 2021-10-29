using UnityEngine;
using ZDef.Core.PoolFactory;

namespace ZDef.Game.Enemies
{
    public class EnemiesSpawner
    {
    }

    public class EnemyController : MonoBehaviour, IFactoryInit<EnemyControllerInitArgs>, IReturnToPoolCallback<EnemyController>
    {
        public void Init(EnemyControllerInitArgs args)
        {
        }

        public event DestroyDelegate<EnemyController> ReturnToPool;
    }
}
