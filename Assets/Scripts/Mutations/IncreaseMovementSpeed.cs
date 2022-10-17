using System.Collections.Generic;
using Enums;
using General;
using UnityEngine;

namespace Mutations
{
    [CreateAssetMenu(fileName = "Mutation", menuName = "Mutations/IncreaseMovementSpeed")]
    public class IncreaseMovementSpeed : MutationBase, IMutation<CharacterStats>
    {
        [SerializeField] private float increase;
    
        public override string Title => $"Increase movement speed by {increase}";
        public override MutationType Type => MutationType.Upgrade;
        public override IEnumerable<MutationTrigger> Triggers => new[] {MutationTrigger.OnMutateOnce};
        
        public void Mutate(CharacterStats subject)
        {
            subject.MovementSpeed += increase;
        }
    }
}
