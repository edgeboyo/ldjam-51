using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverEffects : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    [SerializeField] float scaleUpIncrease = 0.1f;
    [SerializeField] float animationDuration = 0.2f;
    // CanvasGroup canvasGroup;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // content.DOFade(1f, 0.2f).SetUpdate(true);
        Debug.Log("Do Enter");
        gameObject.transform.DOScale(1f + scaleUpIncrease, animationDuration).SetUpdate(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // content.DOFade(0.8f, 0.2f).SetUpdate(true);
        gameObject.transform.DOScale(1f, animationDuration).SetUpdate(true);
    }
}
