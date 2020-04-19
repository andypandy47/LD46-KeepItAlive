using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using StateMachine;
using StateMachine.States;

namespace Player
{
    public enum State
    {
        Idling,
        Walking,
        Jumping,
        Sliding
    }
    public class PlayerStateManager : MonoBehaviour
    {
        public static PlayerStateManager instance;
        public void Awake()
        {
            if (instance == null)
                instance = this;
        }

        public State currentState;
        public bool grounded;
        public bool InputEnabled = true;

        public PlayerStateMachine stateMachine;

        private PlayerController controller;

        public void InitialiseStateMachine(PlayerController _controller)
        {
            controller = _controller;
            stateMachine = new PlayerStateMachine(this);
            stateMachine.ChangeState(IdlingState.Instance);
        }

        public void UpdateStateMachine()
        {
            stateMachine.Update();           
        }

        public void SetDisplayState(State newState)
        {
            currentState = newState;
        }

        public void SetCurrentState(State<PlayerStateManager> newState)
        {
            stateMachine.ChangeState(newState);
        }

        public PlayerController GetController()
        {
            return controller;
        }

        public void OnAttackEnd()
        {
            SetCurrentState(IdlingState.Instance);
        }
    }
}

