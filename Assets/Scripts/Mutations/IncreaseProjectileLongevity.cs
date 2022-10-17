using System;
using System.Collections.Generic;
using UnityEngine;
using Enums;

namespace Mutations
{
    [Obsolete]
    [CreateAssetMenu(fileName = "Mutation", menuName = "Mutations/IncreaseProjectileLongevity")]
    public class IncreaseProjectileRange : MutationBase
    {
        [Range(0, 10)]
        [SerializeField] private float projectileRangePercentIncrease;

        public override string Title => $"Increase Your Projectile Range By {projectileRangePercentIncrease}%";
        public override MutationType Type => MutationType.Upgrade;
        public override IEnumerable<MutationTrigger> Triggers => new[] { MutationTrigger.OnMutate };

        // protected override void Mutate()
        // {
        //     // var actualIncrease = 1f * projectileRangePercentIncrease / 100f;
        //     // PlayerBehaviour.Instance.IncreaseProjectileLongevity(actualIncrease);
        // }
    }
}
