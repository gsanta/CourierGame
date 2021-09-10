using Service.AI;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Domain
{
    public class SteeringComponent : MonoBehaviour
    {
        private PedestrianStore pedestrianStore;
        private BikerStore bikerStore;

        public Steering Steering { get; private set; } 

        public void Construct(PedestrianStore pedestrianStore, BikerStore bikerStore, Timer timer)
        {
            this.pedestrianStore = pedestrianStore;
            this.bikerStore = bikerStore;
            Steering = new Steering(this.gameObject);
            timer.SecondPassed += HandleSecondPassed;
        }

        private void HandleSecondPassed(object sender, EventArgs e)
        {
            var toRemove = new List<GameObject>();
            var toDecrease = new List<GameObject>();
            foreach (KeyValuePair<GameObject, int> entry in Steering.avoidedGameObjects)
            {
                if (entry.Value <= 0)
                {
                    toRemove.Add(entry.Key);
                } else
                {
                    toDecrease.Add(entry.Key);
                }
            }
            toRemove.ForEach(gameObject => Steering.avoidedGameObjects.Remove(gameObject));
            toDecrease.ForEach(gameObject => Steering.avoidedGameObjects[gameObject] -= 1);

            var obstacle = FindMostRelevantObstacle();

            if (obstacle)
            {
                var avoidance = Steering.GetAvoidance(obstacle.GetComponent<SteeringComponent>().Steering);

                GetComponent<WaypointNavigator>().controller.SetDestination(transform.position + avoidance);

                Steering.avoidedGameObjects.Add(obstacle, 3);
            }
        }

        private GameObject FindMostRelevantObstacle()
        {
            Pedestrian closest = null;
            float closesDistance = float.MaxValue;

            foreach(Pedestrian pedestrian in pedestrianStore.GetAll())
            {
                if (!suspendedAvoidanceMap.ContainsKey(pedestrian.gameObject))
                {
                    bool intersects = Steering.Intersects(pedestrian.GetComponent<SteeringComponent>().Steering);

                    if (intersects)
                    {
                        if (closest == null || Steering.Distance(pedestrian.GetComponent<SteeringComponent>().Steering) < closesDistance)
                        {
                            closest = pedestrian;
                        }
                    }
                }
            }

            if (closest != null)
            {
                return closest.gameObject;
            }

            return null;
        }

        public class Factory : PlaceholderFactory<UnityEngine.Object, SteeringComponent>
        {
        }
    }
}
