using System.Collections;
using UnityEngine;
using Pathfinding;
using General;

namespace Enemy
{
    public class AIMovementBehaviourBase : MonoBehaviour
    {
        protected internal IAstarAI ai;
        protected internal IEnemyAttackBehaviour behaviour;
        protected internal CharacterController controller;
        [SerializeField] protected internal GameObject playerGO;

        // Use this for initialization
        protected virtual internal void Start()
        {
            playerGO = GameObject.Find("Player");
            ai = GetComponent<IAstarAI>();
            behaviour = GetComponent<IEnemyAttackBehaviour>();
            
        }

        protected internal void Setup()
        {
            ai.maxSpeed = behaviour.Stats.MovementSpeed;
        }
    }
}