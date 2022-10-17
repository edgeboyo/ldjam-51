using System.Collections;
using UnityEngine;
using Pathfinding;
using General;

namespace Enemy
{
    public class PermaDetectBeelineBehaviour : AIMovementBehaviourBase
    {
        private float timeSinceLastPathUpdate = 0f;
        [SerializeField] private float updateFrequency = 2.0f;

        // Use this for initialization
        protected internal override void Start()
        {
            base.Start();
            // disable auto path recalculation. We handle this manually
            ai.canSearch = false;
        }

        // Update is called once per frame
        void Update()
        {
            if(playerGO == null)
            {
                return;
            }
            timeSinceLastPathUpdate = Time.time;
            if ((timeSinceLastPathUpdate > updateFrequency && !ai.pathPending) || (!ai.pathPending && (ai.reachedEndOfPath || !ai.hasPath)))
            {
                ai.destination = playerGO.transform.position + Random.insideUnitSphere;
                ai.SearchPath();
            }
        }
    }
}