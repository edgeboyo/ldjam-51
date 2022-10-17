using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathScript : MonoBehaviour
{
    [SerializeField] GameObject deathParticles;

    Transform deathPositon;

    bool isPlayer = false;
    // Start is called before the first frame update
    void Start()
    {
        MonoBehaviour[] comps = GetComponents<MonoBehaviour>();
        
        foreach(MonoBehaviour comp in comps)
        {
            if(comp != this)
                comp.enabled = false;
        }

        gameObject.transform.DORotate(new Vector3(0, 0, 90), 1f);
        deathPositon = gameObject.transform;
        Destroy(gameObject, 1.11f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        GameObject go = Instantiate(Resources.Load("DeathParticles", typeof(GameObject)), gameObject.transform.position, Quaternion.Euler(-90, 0, 0)) as GameObject;
        Destroy(go, 5f);
        if(isPlayer)
            GameOverUI.Instance.FadeInUI();
    }

    public void playerDied()
    {
        isPlayer = true;
    }
}
