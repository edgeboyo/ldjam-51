using System;
using DG.Tweening;
using Managers;
using Mutations;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class MutationChoiceUI : MonoBehaviour, IPointerClickHandler, 
        IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private string UIDescriptor;
        [SerializeField] private GameObject buffTextObject;   
        [SerializeField] private CanvasGroup content;
    
        private MutationBase mutation;
        [SerializeField] private Animals.Animal animal;

        public event Action<MutationChoiceUI> Clicked;

        private void Start()
        {
            content.alpha = 0.8f;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            Debug.Log("Clicked on option for " + mutation.Title + " in " + UIDescriptor);

            MutationManager.Instance.SelectNewMutator(mutation, animal);

            // Cursor.lockState = CursorLockMode.Locked;

            Clicked?.Invoke(this);
        }
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            content.DOFade(1f, 0.2f).SetUpdate(true);
            content.transform.DOScale(1.1f, 0.2f).SetUpdate(true);
        }
        
        public void OnPointerExit(PointerEventData eventData)
        {
            content.DOFade(0.8f, 0.2f).SetUpdate(true);
            content.transform.DOScale(1f, 0.2f).SetUpdate(true);
        }

        public void SetUpChoice(MutationBase mutation, Animals.Animal animal)
        {
            this.mutation = mutation;

            TextMeshProUGUI text = buffTextObject.GetComponent<TextMeshProUGUI>();

            text.text = mutation.Title;

            this.animal = animal;
        }
    }
}
