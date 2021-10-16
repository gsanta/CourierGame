using Pedestrians;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Agents
{
    public class PathCache
    {
        private PedestrianTargetStore pedestrianTargetStore;
        private Dictionary<(Vector3, Vector3), NavMeshPath> pathes = new Dictionary<(Vector3, Vector3), NavMeshPath>();

        public void SetPedestrianTargetStore(PedestrianTargetStore pedestrianTargetStore)
        {
            this.pedestrianTargetStore = pedestrianTargetStore;
        }

        public NavMeshPath GetPath((Vector3, Vector3) sourceDest)
        {
            return this.pathes[sourceDest];
        }

        public void Init()
        {
            var targets = pedestrianTargetStore.GetTargets();
        
            foreach(var source in targets)
            {
                foreach(var target in targets)
                {
                    if (source != target)
                    {
                        var path = new NavMeshPath(); 
                        NavMesh.CalculatePath(source.transform.position, target.transform.position, NavMesh.AllAreas, path);
                        pathes.Add((source.transform.position, target.transform.position), path);

                    }
                }
            }
        }
    }
}
