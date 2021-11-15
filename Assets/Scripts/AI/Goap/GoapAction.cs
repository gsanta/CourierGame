using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public abstract class GoapAction<T> where T : IGameObject
    {
        public string actionName = "Action";
        public float cost = 1.0f;
        public Vector3 currentTarget;
        public string targetTag;
        public float duration = 1;
        private AIStateName[] preconditions;
        private AIStateName[] afterEffects;
        private ISet<AIStateName> afterEffectsSet = new HashSet<AIStateName>();
        public AIStates agentBeliefs;

        public bool running = false;
        protected bool finished = false;
        private bool paused = false;

        protected GoapAgent<T> agent;

        public GoapAction(AIStateName[] preconditions, AIStateName[] afterEffects)
        {
            this.preconditions = preconditions;
            this.afterEffects = afterEffects;

            foreach (var afterEffect in afterEffects)
            {
                afterEffectsSet.Add(afterEffect);
            }
        }

        public bool Pause
        {
            set {
                paused = true;
            }
        }

        public bool Finished { get => finished; set => finished = value; }

        public GoapAgent<T> GoapAgent { get => agent; }

        public void SetAgent(GoapAgent<T> agent)
        {
            this.agent = agent;
        }

        public bool IsAchievable()
        {
            return true;
        }

        public bool IsAchievableGiven(ISet<AIStateName> states)
        {
            foreach (AIStateName name in preconditions)
            {
                if (!states.Contains(name))
                {
                    return false;
                }
            }
            return true;
        }

        public AIStateName[] GetPreConditions()
        {
            return preconditions;
        }
        public AIStateName[] GetAfterEffects()
        {
            return afterEffects;
        }

        public ISet<AIStateName> GetAfterEffectsSet()
        {
            return afterEffectsSet;
        }

        public virtual bool PrePerform()
        {
            return true;
        }
        public virtual bool PostPerform()
        {
            return true;
        }
        public virtual bool PostAbort()
        {
            return true;
        }
        public virtual void Update() { }
        public abstract GoapAction<T> Clone(GoapAgent<T> agent = null);
    }
}
