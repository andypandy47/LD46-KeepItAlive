using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

namespace StateMachine.States
{
    public class JumpingState : State<PlayerStateManager>
    {
        private static JumpingState instance;
        private PlayerController pController;
        private JumpingState()
        {
            if (instance != null)
                return;
            state = State.Jumping;
            instance = this;
        }

        public static JumpingState Instance
        {
            get
            {
                if (instance == null)
                {
                    new JumpingState();
                }

                return instance;
            }
        }

        public override void EnterState(PlayerStateManager owner)
        {
            if (pController == null)
                pController = owner.GetController() as PlayerController;

            pController.animController.SetTrigger("Jumping");
            pController.inputController.DisableMovementInput();
        }

        public override void ExitState(PlayerStateManager owner)
        {
            pController.inputController.EnableMovementInput();
        }

        public override void UpdateState(PlayerStateManager owner)
        {
            if(owner.grounded)
            {
                owner.stateMachine.ChangeState(IdlingState.Instance);
            }
        }
    }
}

