using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace General
{
    public class AttackEffect : MonoBehaviour
    {
        public float AttackDamage;
        public float CriticalChance;
        public float CriticalDamage;
        public float Longevity;
        private string[] LayersToBreakOn = new string[] { "Ground", "Obstacles" };
        private void OnTriggerEnter(Collider other)
        {
            Debug.Log(other);
            if (LayersToBreakOn.Contains(LayerMask.LayerToName(other.gameObject.layer)))
            {
                // destroy on collision with ground
                Destroy(gameObject);
            }

            if (other.gameObject.name == "Player")
            {
                AudioSource hitSound = other.gameObject.GetComponent<AudioSource>();
                hitSound.Play();
            }
            
            // check it collides with a character
            MonoBehaviour[] list = other.gameObject.GetComponentsInChildren<MonoBehaviour>();

            foreach (var mb in list)
            {
                if (mb is IDamageable)
                {
                    // act upon it
                    IDamageable charEntity = mb as IDamageable;

                    charEntity.CurrentHealth -= RollCritical();
                    Debug.Log(string.Format("Entity {0} now has health of {1}", other.gameObject.name, charEntity.CurrentHealth));

                    // ((GameObject)charEntity);
                    //charEntity
                    Transform healthbar = other.gameObject.transform.Find("Healthbar");
                    if (healthbar != null)
                    {
                        healthbar.localScale = new Vector3(charEntity.CurrentHealth / 10f, 0.1f, 0.05f);
                    } 

                    Destroy(gameObject);
                }
            }
        }

        public void InitSetup(float atkdmg, float critchnc, float critdmg, float longevity)
        {
            AttackDamage = atkdmg;
            CriticalChance = critchnc;
            CriticalDamage = critdmg;
            Longevity = longevity;
            Destroy(gameObject, longevity);
        }


        private float RollCritical()
        {
            return Random.value < CriticalChance ? CriticalDamage * AttackDamage : AttackDamage;
        }
    }
}