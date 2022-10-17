using System.Collections.Generic;
using System.Linq;
using General;
using UnityEngine;

namespace Managers
{
    public class InputManager : SingletonMonoBehaviour<InputManager>
    {
        // [SerializeField] private List<KeyCode> jumpKeys;
        
        [SerializeField] private List<KeyCode> sprintKeys;
        [SerializeField] private List<KeyCode> attackKeys;

        private const string HorizontalMovementAxis = "Horizontal";
        private const string VerticalMovementAxis = "Vertical";
    
        // private const string HorizontalMouseAxis = "Mouse X";
        // private const string VerticalMouseAxis = "Mouse Y";

        public Vector2 Move { get; private set; }
        public Vector2 Look { get; private set; }
        public bool Jump { get; set; }
        public bool Sprint { get; private set; }

        public bool Attack { get; set; }

        private void OnApplicationFocus(bool hasFocus)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Cursor.lockState = CursorLockMode.None;
            }

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (Cursor.lockState == CursorLockMode.None)
                {
                    Cursor.lockState = CursorLockMode.Locked;
                }
            }
        
            var horizontalMove = Input.GetAxisRaw(HorizontalMovementAxis);
            var verticalMove = Input.GetAxisRaw(VerticalMovementAxis);

            Move = new Vector2(horizontalMove, verticalMove);

            // no looking around in top-down mode
            // if (Cursor.lockState == CursorLockMode.Locked)
            // {
            //     var horizontalMouseDelta = Input.GetAxisRaw(HorizontalMouseAxis);
            //     var verticalMouseDelta = Input.GetAxisRaw(VerticalMouseAxis);
            //
            //     Look = new Vector2(horizontalMouseDelta, -verticalMouseDelta);
            // }
            // else
            // {
            //     Look = Vector2.zero;
            // }
            
            // no jumping around in top-down mode
            // if (jumpKeys.Any(Input.GetKeyDown))
            // {
            //     Jump = true;
            // }

            Sprint = sprintKeys.Any(Input.GetKey);

            Attack = attackKeys.Any(Input.GetKeyDown);
        }
    }
}