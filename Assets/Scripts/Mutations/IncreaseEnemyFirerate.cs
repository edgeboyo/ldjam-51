using Enums;
using General;
using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;

namespace Mutations
{
    [CreateAssetMenu(fileName = "Mutation", menuName = "Mutations/IncreaseEnemyFirerate")]
    public class IncreaseEnemyFirerate : MutationBase, IMutation<RespawnManager>
    {
        [SerializeField] private int firerateIncrease; // percentage?

        public override string Title => $"Increase enemy firerate by {firerateIncrease}%";
        public override MutationType Type => MutationType.Detriment;
        public override IEnumerable<MutationTrigger> Triggers => new[] { MutationTrigger.OnMutateOnce };

        public void Mutate(RespawnManager subject)
        {
            subject.IncreaseEnemyFirerate(firerateIncrease);
        }
    }
}