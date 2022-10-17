using UnityEngine;

using Player;

public class HealthBarUI : MonoBehaviour
{

    [SerializeField] RectTransform maxHealthBar;
    [SerializeField] RectTransform currentHealthBar;

    public float startingLength = 200f;

    private float _startingHealth;
    public float _perHealthLengthModifier;

    private void Start()
    {
        _startingHealth = PlayerBehaviour.Instance.Stats.MaxHealth;
        _perHealthLengthModifier = startingLength / _startingHealth;
    }

    private void Update()
    {
        float maxHealth = PlayerBehaviour.Instance.Stats.MaxHealth;
        float curHealth = PlayerBehaviour.Instance.CurrentHealth;

        float maxHealthLen = startingLength - ((maxHealth - _startingHealth) * _perHealthLengthModifier);
        float curHealthLen = startingLength - ((curHealth - _startingHealth) * _perHealthLengthModifier);

        //Debug.Log(maxHealthBar.offsetMax.x);

        maxHealthBar.offsetMax = new Vector2(-maxHealthLen, maxHealthBar.offsetMax.y);
        currentHealthBar.offsetMax = new Vector2(-curHealthLen, currentHealthBar.offsetMax.y);
    }
}
