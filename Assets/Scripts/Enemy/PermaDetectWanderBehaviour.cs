using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEngine.Events;
using General;

namespace Enemy
{
    public class PermaDetectWanderBehaviour : AIMovementBehaviourBase
    {
        [SerializeField] private float rangeToleranceThreshold = 0.85f;
        [SerializeField] private float maxRangeFactor = 1.3f;

        protected internal override void Start()
        {
            base.Start();
        }

        Vector3 PickRandomPointInRange()
        {
            // pick random position which is within our attack range of player
            Vector3 pointInsideRange = Random.insideUnitCircle * behaviour.GetAttackRange() * (maxRangeFactor - rangeToleranceThreshold);
            Vector3 targetVector = playerGO.transform.position + pointInsideRange;
            return targetVector;
        }

        private void Update()
        {
            if(playerGO == null)
            {
                return;
            }
            if (Vector3.Distance(transform.position, playerGO.transform.position) > rangeToleranceThreshold * behaviour.GetAttackRange() || (!ai.pathPending && (ai.reachedEndOfPath || !ai.hasPath)))
            {
                // if approaching our range tolerance threshold/haven't got a destination.
                ai.destination = PickRandomPointInRange();
                ai.SearchPath();
            }
        }
    }
}
