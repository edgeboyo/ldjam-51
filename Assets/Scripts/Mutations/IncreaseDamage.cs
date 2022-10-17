using System.Collections.Generic;
using Enums;
using General;
using UnityEngine;

namespace Mutations
{
    [CreateAssetMenu(fileName = "Mutation", menuName = "Mutations/IncreaseDamage")]
    public class IncreaseDamage : MutationBase, IMutation<CharacterStats>
    {
        [SerializeField] private float damageIncrease;
    
        public override string Title => $"Increase damage by {damageIncrease}%";
        public override MutationType Type => MutationType.Upgrade;
        public override IEnumerable<MutationTrigger> Triggers => new[] {MutationTrigger.OnMutateOnce};
        
        public void Mutate(CharacterStats subject)
        {
            subject.AttackDamage *= (1 + damageIncrease/100);
        }
    }
}
