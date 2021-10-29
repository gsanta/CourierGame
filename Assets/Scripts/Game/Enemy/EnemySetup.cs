
namespace Enemies
{
    public class EnemySetup
    {
        private readonly EnemySpawner enemySpawner;

        public EnemySetup(EnemySpawner enemySpawner)
        {
            this.enemySpawner = enemySpawner;
        }

        public void Setup()
        {
            this.enemySpawner.Spawn();
        }
    }
}
