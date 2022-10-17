using System.Collections;
using UnityEngine;
using Pathfinding;
using General;

namespace General
{
    public class AIMovementBehaviourBase : MonoBehaviour
    {
        protected internal IAstarAI ai;
        protected internal IEnemyAttackBehaviour behaviour;
        [SerializeField] protected internal GameObject playerGO;

        // Use this for initialization
        void Start()
        {
            playerGO = GameObject.Find("Player");
            ai = GetComponent<IAstarAI>();
            behaviour = GetComponent<IEnemyAttackBehaviour>();
            ai.maxSpeed = behaviour.Stats.MovementSpeed;
        }
    }
}