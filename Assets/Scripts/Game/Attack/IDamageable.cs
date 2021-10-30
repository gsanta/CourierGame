
using UnityEngine;

namespace Attacks
{
    public interface IDamageable
    {
        public void TakeDamage(int damage);
        public Transform GetTransform();
    }
}
