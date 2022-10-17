using System.Collections.Generic;
using Enums;
using Player;
using UnityEngine;

namespace Mutations
{
    [CreateAssetMenu(fileName = "Mutation", menuName = "Mutations/Overheal")]
    public class Overheal : MutationBase, IMutation<PlayerBehaviour>
    {
        [SerializeField] private float overhealAmount;
    
        public override string Title => $"Overheal by {overhealAmount}";
        public override MutationType Type => MutationType.Upgrade;
        public override IEnumerable<MutationTrigger> Triggers => new[] {MutationTrigger.OnMutateOnce};

        public void Mutate(PlayerBehaviour subject)
        {
            subject.CurrentHealth += overhealAmount;
        }
    }
}
