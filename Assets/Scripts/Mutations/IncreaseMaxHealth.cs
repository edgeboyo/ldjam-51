using System.Collections.Generic;
using Enums;
using General;
using Player;
using UnityEngine;

namespace Mutations
{
    [CreateAssetMenu(fileName = "Mutation", menuName = "Mutations/IncreaseMaxHealth")]
    public class IncreaseMaxHealth : MutationBase, IMutation<CharacterStats>
    {
        [SerializeField] private float healthIncrease;
    
        public override string Title => $"Increase max health by {healthIncrease}";
        public override MutationType Type => MutationType.Upgrade;
        public override IEnumerable<MutationTrigger> Triggers => new[] {MutationTrigger.OnMutateOnce};
        
        public void Mutate(CharacterStats subject)
        {
            subject.MaxHealth += healthIncrease;
        }
    }
}
