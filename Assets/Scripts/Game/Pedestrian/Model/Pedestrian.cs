using AI;
using Attacks;
using GameObjects;
using Shaders;
using States;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Pedestrians
{
    public class Pedestrian : MonoBehaviour, IGameObject, IDamageable, ISelectableGameObject
    {
        private GoapAgent<Pedestrian> agent;
        public NavMeshAgent navMeshAgent;
        public PedestrianInfo pedestrianInfo;
        private IGoalProvider goalProvider;
        public bool walked = false;
        [SerializeField]
        private AttackRadius attackRadius;
        private int health = 100;
        private OutlineGameObjectSelector gameObjectSelector;
        private SelectionStore selectionStore;

        [Inject]
        public void Construct(OutlineGameObjectSelector gameObjectSelector, SelectionStore selectionStore)
        {
            this.gameObjectSelector = gameObjectSelector;
            this.selectionStore = selectionStore;
        }

        public IGoalProvider GoalProvider { get => goalProvider; set => goalProvider = value; }
        public GoapAgent<Pedestrian> Agent { get => agent; set => agent = value; }

        private void Awake()
        {
            gameObjectSelector.SetGameObject(this);
            gameObjectSelector.SetOutlineGameObject(transform.GetChild(0).gameObject);
            attackRadius.OnAttack += OnAttack;
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

        public IGameObjectSelector GetGameObjectSelector()
        {
            return gameObjectSelector;
        }

        public void Select()
        {
            selectionStore.AddPedestrian(this);
            gameObjectSelector.Select();

        }

        public void Deselect()
        {
            gameObjectSelector.Deselect();
        }

        public class Factory : PlaceholderFactory<Object, Pedestrian>
        {
        }
    }

    public class PedestrianInfo
    {
        public readonly string home;

        public PedestrianInfo(string home)
        {
            this.home = home;
        }
    }
}
