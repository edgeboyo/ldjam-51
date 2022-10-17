using Enums;
using General;
using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;

namespace Mutations
{
    [CreateAssetMenu(fileName = "Mutation", menuName = "Mutations/IncreaseEnemyMaxHealth")]
    public class IncreaseEnemyMaxHealth : MutationBase, IMutation<RespawnManager>
    {
        [SerializeField] private int maxHealthIncrease; // percentage?

        public override string Title => $"Increase Enemies maximum health by {maxHealthIncrease}%";
        public override MutationType Type => MutationType.Detriment;
        public override IEnumerable<MutationTrigger> Triggers => new[] { MutationTrigger.OnMutateOnce };

        public void Mutate(RespawnManager subject)
        {
            subject.IncreaseEnemyMaxHealth(maxHealthIncrease);
        }
    }
}