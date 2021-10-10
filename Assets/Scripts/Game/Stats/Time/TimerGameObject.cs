using UnityEngine;
using Zenject;

namespace Stats
{
    public class TimerGameObject : MonoBehaviour
    {
        private Timer timer;

        [Inject]
        public void Construct(Timer timer)
        {
            this.timer = timer;
        }

        void Update()
        {
            timer.Tick();
        }
    }
}
