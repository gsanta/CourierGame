
using GameObjects;

namespace Enemies
{
    public interface IEnemyInstantiator
    {
        GameCharacter InstantiateEnemy();
    }
}
