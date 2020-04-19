using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachine;
using Player;

namespace StateMachine.States
{   
    public class WalkingState : State<PlayerStateManager>
    {
        private static WalkingState instance;
        private PlayerController pController;

        private WalkingState()
        {
            if (instance != null)
                return;
            state = State.Walking;
            instance = this;
        }

        public static WalkingState Instance
        {
            get
            {
                if (instance == null)
                {
                    new WalkingState();
                }

                return instance;
            }
        }

        public override void EnterState(PlayerStateManager owner)
        {
            if (pController == null)
                pController = owner.GetController();

            pController.animController.SetTrigger("Walking");
        }

        public override void ExitState(PlayerStateManager owner)
        {
        }

        public override void UpdateState(PlayerStateManager owner)
        {
            float absXInput = Mathf.Abs(PlayerInput.xInput);

            if (!PlayerInput.HasHorizontalInput())
            {
                owner.stateMachine.ChangeState(IdlingState.Instance);
            }

        }
    }
}
