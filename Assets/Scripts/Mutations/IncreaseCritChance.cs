using System.Collections.Generic;
using Enums;
using Player;
using UnityEngine;

namespace Mutations
{
    [CreateAssetMenu(fileName = "Mutation", menuName = "Mutations/IncreaseCritChance")]
    public class IncreaseCritChance : MutationBase, IMutation<PlayerStats>
    {
        [Range(0, 100)]
        [SerializeField] private int criticalChancePercentIncrease;
    
        public override string Title => $"Increase critical hit chance by a flat {criticalChancePercentIncrease}%";
        public override MutationType Type => MutationType.Upgrade;
        public override IEnumerable<MutationTrigger> Triggers => new []{MutationTrigger.OnMutateOnce};
        
        public void Mutate(PlayerStats subject)
        {
            var actualIncrease = 1f * criticalChancePercentIncrease / 100f;
            subject.CriticalChance += actualIncrease;
        }
    }
}
