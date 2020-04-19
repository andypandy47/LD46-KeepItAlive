using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using StateMachine;

namespace Player
{
    [System.Serializable]
    public class UnityEventVectorParam : UnityEvent<Vector2>
    { }

    [System.Serializable]
    public class UnityEventStateParam : UnityEvent<State<PlayerStateManager>>
    { }

    public class PlayerInput : MonoBehaviour
    {
        public UnityEventVectorParam horizontalMovementEvent;
        public UnityEventStateParam onAttackDownState;
        public UnityEvent onJumpDownEvent, onJumpUpEvent, onAttackDown, onPauseDown;
        public static bool inputEnabled = true;
        public static float xInput, yInput, rightStickXInput;

        public void UpdateInput()
        {
            if (inputEnabled)
            {
                horizontalMovementEvent.Invoke(CalculateDirectionalInput());
                rightStickXInput = GetRightStickInput();

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    onJumpDownEvent.Invoke();
                }

                if (Input.GetMouseButtonDown(0))
                {
                    onAttackDown.Invoke();
                    onAttackDownState.Invoke(StateMachine.States.AttackState.Instance);
                }

                if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
                {
                    onPauseDown.Invoke();
                }
            }
        }

        private Vector2 CalculateDirectionalInput()
        {
            xInput = Input.GetAxisRaw("Horizontal");
            yInput = Input.GetAxisRaw("Vertical");

            float absXInput = Mathf.Abs(xInput);

            if (absXInput < 0.2f)
                xInput = 0.0f;

            return new Vector2(xInput, yInput);
        }

        private float GetRightStickInput()
        {
            float inputVal = Input.GetAxis("RightJoystickHorizontal");

            float absInput = Mathf.Abs(inputVal);

            if (absInput < 0.2f)
                inputVal = 0.0f;

            return inputVal;
        }

        public static bool HasHorizontalInput()
        {
            return Mathf.Abs(xInput) > 0.2f ? true : false;
        }

        public void DisableMovementInput()
        {
            inputEnabled = false;
            horizontalMovementEvent.Invoke(Vector2.zero);
        }

        public void EnableMovementInput()
        {
            inputEnabled = true;
        }
    }
}
