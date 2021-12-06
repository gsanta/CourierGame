using UnityEngine;
using UnityEngine.AI;
using Zenject;
using System;
using AI;
using GameObjects;

namespace GameObjects
{
    public class GameCharacter : MonoBehaviour, IGameObject
    {
        private string characterName;
        private bool isPaused = false;
        private GoapAgent<GameCharacter> agent;
        public NavMeshAgent navMeshAgent;
        private IGoalProvider goalProvider;
        public GoapAgent<GameCharacter> Agent { get => agent; set => agent = value; }
        public IGoalProvider GoalProvider { get => goalProvider; set => goalProvider = value; }

        private void Awake()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
        }

        public bool Paused
        {
            get => isPaused;
            set
            {
                isPaused = value;
                UpdatePausedState();
            }
        }

        private void LateUpdate()
        {
            if (agent.Active)
            {
                agent.Update();
            }

            Updated?.Invoke(this, EventArgs.Empty);
        }

        private void UpdatePausedState()
        {
            if (isPaused)
            {
                gameObject.GetComponent<NavMeshAgent>().enabled = false;
                agent.Active = false;
            }
            else
            {
                gameObject.GetComponent<NavMeshAgent>().enabled = true;
                agent.Active = true;
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

        public string GetId()
        {
            return agent.AgentId;
        }

        public string GetName()
        {
            return characterName;
        }

        public void SetName(string characterName)
        {
            this.characterName = characterName;
        }

        public Transform GetTransform()
        {
            return transform;
        }

        public void CompleteAction()
        {
            agent.CompleteAction();
        }

        public NavMeshAgent GetNavMeshAgent()
        {
            return navMeshAgent;
        }

        public IGoalProvider GetGoalProvider()
        {
            return goalProvider;
        }

        public event EventHandler Updated;

        public class Factory : PlaceholderFactory<UnityEngine.Object, GameCharacter>
        {
        }
    }

    public class BikerInfo
    {
        public readonly string home;

        public BikerInfo(string home)
        {
            this.home = home;
        }
    }
}
