using System.Collections.Generic;
using Mutations;
using UnityEngine;

namespace Config
{
    [CreateAssetMenu(fileName = "MutationConfig", menuName = "Config/MutationConfig")]
    public class MutationConfig : ScriptableObject
    {
        [field: SerializeField] public List<MutationBase> AvailablePlayerMutations { get; private set; }
        [field: SerializeField] public List<MutationBase> AvailableEnemyMutations { get; private set; }
        [field: SerializeField] public List<Animals.Animal> AvaliableAnimals { get; private set; }
    }
}