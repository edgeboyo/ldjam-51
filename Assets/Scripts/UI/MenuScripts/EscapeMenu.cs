using General;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeMenu : SingletonMonoBehaviour<EscapeMenu>
{
    public CanvasGroup content;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            FlipUI();
        }  
    }

    public void FlipUI()
    {
        if(content.gameObject.activeInHierarchy)
        {
            content.gameObject.SetActive(false);
            Time.timeScale = 1f;
        }
        else
        {
            content.gameObject.SetActive(true);
            Time.timeScale = 0f;
        }

    }
}
