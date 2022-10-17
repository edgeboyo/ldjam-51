using System.Collections.Generic;
using General;
using UnityEngine;

namespace Managers
{
    public class TimeManager : SingletonMonoBehaviour<TimeManager>
    {
        private const KeyCode PauseKey = KeyCode.BackQuote;
        private const KeyCode ReverseKey = KeyCode.LeftShift;

        private readonly List<KeyCode> _timeKeys = new()
        {
            KeyCode.Alpha1,
            KeyCode.Alpha2,
            KeyCode.Alpha3,
            KeyCode.Alpha4,
            KeyCode.Alpha5
        };

        private void Update()
        {
            if (Input.GetKeyDown(PauseKey))
            {
                Time.timeScale = 0f;
            }
            
            foreach (var key in _timeKeys)
            {
                if (Input.GetKeyDown(key))
                {
                    var reverse = Input.GetKey(ReverseKey);
                    var index = _timeKeys.IndexOf(key);
                    var exponent = reverse ? 0.5f : 2f;
                    
                    Time.timeScale = Mathf.Pow(exponent, index);
                }
            }  
        }
    }
}