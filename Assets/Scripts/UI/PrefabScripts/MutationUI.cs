using System.Collections.Generic;
using DG.Tweening;
using General;
using Managers;
using Mutations;
using Player;
using Unity.VisualScripting;
using UnityEngine;

namespace UI
{
    public class MutationUI : SingletonMonoBehaviour<MutationUI>
    {
        [SerializeField] MutationChoiceUI[] choices;
        [SerializeField] CharacterRotator[] rotators;
        [SerializeField] CanvasGroup content;

        private void OnEnable()
        {
            foreach (var choice in choices)
            {
                choice.Clicked += OnChoiceSelected;
            }
        }

        private void OnDisable()
        {
            foreach (var choice in choices)
            {
                choice.Clicked -= OnChoiceSelected;
            }
        }

        private void OnChoiceSelected(MutationChoiceUI choice)
        {
            content.DOFade(0f, 0.2f).SetUpdate(true)
                .OnComplete(() => content.gameObject.SetActive(false));

            ScoreManager.Instance.startUpCount();
        }
        
        public void SetUpUI(List<MutationBase> mutations, List<Animals.Animal> animals)
        {
            // Debug.Log("Set up UI...");
            ScoreManager.Instance.pauseScoreCount();

            content.gameObject.SetActive(true);
            content.DOFade(1f, 0.3f).SetUpdate(true);

            for (int i=0; i<choices.Length; i++)
            {
                var choice = choices[i];
                var mutation = mutations[i];
                var animal = animals[i];
                choice.SetUpChoice(mutation, animal);

                rotators[i].ChangeAnimal(animal.AnimalPrefab);
            }

            // Cursor.lockState = CursorLockMode.Confined;

            // Debug.Log("Choices set up...");
        }
    }
}
