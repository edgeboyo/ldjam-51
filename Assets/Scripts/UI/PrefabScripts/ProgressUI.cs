using Managers;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ProgressUI : MonoBehaviour
    {
        [SerializeField] private Image fillImage; 
        
        // [ShowInInspector] private float FillAmount => MutationManager.Instance.MutationProgress;

        private void Update()
        {
            fillImage.fillAmount = MutationManager.Instance.MutationProgress;
        }
    }
}