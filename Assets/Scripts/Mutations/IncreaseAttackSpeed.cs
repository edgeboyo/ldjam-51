using System.Collections.Generic;
using UnityEngine;
using Enums;
using General;

namespace Mutations
{
    [CreateAssetMenu(fileName = "Mutation", menuName = "Mutations/IncreaseAttackSpeed")]
    public class IncreaseAttackSpeed : MutationBase, IMutation<CharacterStats>
    {
        [Range(0, 100)]
        [SerializeField] private int increase;

        public override string Title => $"Increase attack speed by {increase}%";
        public override MutationType Type => MutationType.Upgrade;
        public override IEnumerable<MutationTrigger> Triggers => new[] { MutationTrigger.OnMutateOnce };

        public void Mutate(CharacterStats subject)
        {
            subject.AttackSpeed *= (1 + increase / 100);
        }
    }
}
