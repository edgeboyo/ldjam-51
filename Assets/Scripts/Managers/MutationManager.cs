using System.Collections.Generic;
using Config;
using Enums;
using Extensions;
using General;
using Mutations;
using Player;
using Sirenix.OdinInspector;
using UI;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

namespace Managers
{
    public class MutationManager : SingletonMonoBehaviour<MutationManager>
    {
        [SerializeField] private MutationConfig config;     // ideally would be injected 
        [SerializeField] private List<MutationBase> selectedMutations;
    
        private const float MutationInterval = 10f;
        
        // [SerializeField] private AnimationController animctrl;
        private AnimationController animctrl;

        private ScoreManager scoreManager;
        
        private float _mutationTimer;

        [ShowInInspector] private RespawnManager Respawn => RespawnManager.Instance;
        private PlayerBehaviour Player => PlayerBehaviour.Instance;
        private PlayerStats PlayerStats => Player.Stats;
        
        public float MutationProgress => Mathf.Clamp(_mutationTimer / MutationInterval, 0f, 1f);

        private void Start()
        {
            animctrl = GameObject.Find("Player").GetComponentInChildren<AnimationController>();
            scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
        }

        private void Update()
        {
            _mutationTimer += Time.deltaTime;
            
            if (_mutationTimer >= MutationInterval)
            {
                Debug.Log("Interval reached!");

                OnMutationTimeReached();
                
                _mutationTimer -= MutationInterval;
            }
        }

        private void OnMutationTimeReached()
        {
            // Debug.Log("Time for a new mutation!");

            // MutationSelector selector = new MutationSelector();

            if (PlayerBehaviour.Instance.IsDead)
                return;

            scoreManager.score += 200f;

            Time.timeScale = 0f;

            // selector.SetUpMutationChoice(this);

            // MutationUI mutationUI = GameObject.FindGameObjectWithTag("MutationUI").GetComponent<MutationUI>();

            var mutations = config.AvailablePlayerMutations.GetRandomRange(3);
            var enemyMutation = config.AvailableEnemyMutations.GetRandom();
            enemyMutation.Trigger(Respawn, MutationTrigger.OnMutateOnce);

            MutationUI.Instance.SetUpUI(mutations, config.AvaliableAnimals.GetRandomRange(3));
        }

        private bool IncrementIfExists(MutationBase newMutation)
        {
            foreach (MutationBase mutation in selectedMutations)
            {
                if (mutation.GetType() == newMutation.GetType())
                {
                    mutation.AddInstance();
                    return true;
                }
            }

            return false;
        }

        public void SelectNewMutator(MutationBase mutation, Animals.Animal nextAnimal)
        {
            Player.ChangeBaseStats(nextAnimal.AnimalBaseStats);

            OnMutateMutate();

            if (!IncrementIfExists(mutation))
            {
                selectedMutations.Add(mutation);
            }

            // trigger both PlayerStats and PlayerBehaviour mutations
            mutation.Trigger(PlayerStats, MutationTrigger.OnMutateOnce);
            mutation.Trigger(Player, MutationTrigger.OnMutateOnce);
            mutation.Trigger(Respawn, MutationTrigger.OnMutateOnce);

            Debug.LogWarning("Changing animal");

            animctrl.chooseAnimal(nextAnimal.AnimalName);
            
            Time.timeScale = 1f;
        }

        public void MutateAll()
        {
            foreach(MutationBase mutation in selectedMutations) {
                // muta
            }
        }

        private void OnTrigger(MutationTrigger trigger)
        {
            foreach (MutationBase mutation in selectedMutations)
            {
                mutation.Trigger(PlayerStats, trigger);
                mutation.Trigger(Player, trigger);
            }
        }

        public void OnShootMutate()
        {
            OnTrigger(MutationTrigger.OnShoot);
        }

        public void OnDamageMutate()
        {
            OnTrigger(MutationTrigger.OnDamage);
        }

        public void OnMutateMutate()
        {
            OnTrigger(MutationTrigger.OnMutateOnce);
        }
    }
}
