using System.Collections.Generic;
using UnityEngine;
using Enums;
using Player;

namespace Mutations
{
    [CreateAssetMenu(fileName = "Mutation", menuName = "Mutations/IncreaseCritDamage")]
    public class IncreaseCritDamage : MutationBase, IMutation<PlayerStats>
    {
        [Range(0f, 1f)]
        [SerializeField] private float criticalDamageMultiplierIncrease;

        public override string Title => $"Increase critical hit damage multiplier by {criticalDamageMultiplierIncrease}x";
        public override MutationType Type => MutationType.Upgrade;
        public override IEnumerable<MutationTrigger> Triggers => new[] { MutationTrigger.OnMutateOnce };
        
        public void Mutate(PlayerStats subject)
        {
            subject.CriticalDamageMultiplier += criticalDamageMultiplierIncrease;
        }
    }
}
