using System.Collections.Generic;
using System.Linq;
using Enums;
using General;
using Player;
using Enemy;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Mutations
{
    public abstract class MutationBase : ScriptableObject
    {
        [Title("Base")]
        [ShowInInspector] public abstract string Title { get; }
        [ShowInInspector] public abstract MutationType Type { get; }
        [ShowInInspector] public abstract IEnumerable<MutationTrigger> Triggers { get; }

        [SerializeField] private Sprite icon;

        [ShowInInspector]
        public string Subject => 
            this is IMutation<CharacterStats> ? "Character stats"
            : this is IMutation<PlayerStats> ? "Player stats"
            : this is IMutation<PlayerBehaviour> ? "Player behaviour"
            : this is IMutation<EnemyStats> ? "Enemy stats" 
            : "none";

        private int _count = 0;

        // protected abstract void Mutate();

        // public abstract void Mutate(CharacterStats subject);

        public void AddInstance()
        {
            _count++;
        }

        public void Trigger<TSubject>(TSubject subject, MutationTrigger expectedTrigger)
        {
            if (this is IMutation<TSubject> mutation)
            {
                var isTriggered = Triggers.Contains(expectedTrigger);

                if (isTriggered)
                {
                    mutation.Mutate(subject);
                }
            }
        }
    }
}
