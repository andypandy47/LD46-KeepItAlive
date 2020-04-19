using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachine;
using Player;

namespace StateMachine.States
{
    public class IdlingState : State<PlayerStateManager>
    {
        private static IdlingState instance;
        private PlayerController pController;

        private IdlingState()
        {
            if (instance != null)
                return;
            state = State.Idling;
            instance = this;
        }

        public static IdlingState Instance
        {
            get
            {
                if (instance == null)
                {
                    new IdlingState();
                }

                return instance;
            }
        }

        public override void EnterState(PlayerStateManager owner)
        {
            if (pController == null)
                pController = owner.GetController();

            pController.animController.SetTrigger("Idling");
        }

        public override void ExitState(PlayerStateManager owner)
        {
        }

        public override void UpdateState(PlayerStateManager owner)
        {
            float absXInput = Mathf.Abs(PlayerInput.xInput);

            if (PlayerInput.HasHorizontalInput())
            {
                owner.stateMachine.ChangeState(WalkingState.Instance);
            }
        }
    }
}

