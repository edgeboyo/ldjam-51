using DG.Tweening;
using General;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameOverUI : SingletonMonoBehaviour<GameOverUI>
{
    public CanvasGroup content;
    // public TextMeshProUGUI scoreboard;
    // Start is called before the first frame update
    void Start()
    {
        content.alpha = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FadeInUI()
    {
        content.gameObject.SetActive(true);
        content.DOFade(1f, .5f);
    }
}
