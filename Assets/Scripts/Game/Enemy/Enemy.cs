using AI;
using Attacks;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Enemies {
    public class Enemy : MonoBehaviour, IGameObject, IDamageable
    {
        public GoapAgent<Enemy> agent;
        public NavMeshAgent navMeshAgent;
        private IGoalProvider goalProvider;
        private int health = 100;
        public IGoalProvider GoalProvider { get => goalProvider; set => goalProvider = value; }
        public GoapAgent<Enemy> Agent { get => agent; set => agent = value; }
        [SerializeField]
        private AttackRadius attackRadius;

        public event System.EventHandler Updated;

        private void Awake()
        {
            //attackRadius.OnAttack += OnAttack;
        }

        private void OnAttack(IDamageable target)
        {
        }

        private void Start()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            agent.Active = true;
        }

        public void CompleteAction()
        {
            agent.CompleteAction();
        }

        private void LateUpdate()
        {
            if (agent.Active)
            {
                agent.Update();
            }
        }

        public GameObject GetGameObject()
        {
            return gameObject;
        }

        public MonoBehaviour GetMonoBehaviour()
        {
            return this;
        }

        public NavMeshAgent GetNavMeshAgent()
        {
            return navMeshAgent;
        }

        public IGoalProvider GetGoalProvider()
        {
            return goalProvider;
        }

        public void TakeDamage(int damage)
        {
            health -= damage;
            if (health <= 0)
            {
                gameObject.SetActive(false);
            }
            throw new System.NotImplementedException();
        }

        public Transform GetTransform()
        {
            return transform;
        }

        public class Factory : PlaceholderFactory<Object, Enemy>
        {
        }
    }
}
