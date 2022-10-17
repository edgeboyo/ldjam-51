using Enums;
using General;
using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;

namespace Mutations
{
    [CreateAssetMenu(fileName = "Mutation", menuName = "Mutations/IncreaseEnemyAttackDamage")]
    public class IncreaseEnemyAttackDamage : MutationBase, IMutation<RespawnManager>
    {
        [SerializeField] private int attackDamageIncrease; // percentage?

        public override string Title => $"Increase enemy maximum attack damage by {attackDamageIncrease}%";
        public override MutationType Type => MutationType.Detriment;
        public override IEnumerable<MutationTrigger> Triggers => new[] { MutationTrigger.OnMutateOnce };

        public void Mutate(RespawnManager subject)
        {
            subject.IncreaseEnemyAttackDamage(attackDamageIncrease);
        }
    }
}