using Enums;
using General;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mutations
{
    [CreateAssetMenu(fileName = "Mutation", menuName = "Mutations/IncreaseEnemyAmount")]
    public class IncreaseEnemyAmount : MutationBase, IMutation<RespawnManager>
    {
        [SerializeField] private int enemyIncrease;

        public override string Title => $"Increase maximum enemy count by {enemyIncrease}";
        public override MutationType Type => MutationType.Detriment;
        public override IEnumerable<MutationTrigger> Triggers => new[] { MutationTrigger.OnMutateOnce };

        public void Mutate(RespawnManager subject)
        {
            subject.IncreaseEnemyCount(enemyIncrease);
        }
    }
}