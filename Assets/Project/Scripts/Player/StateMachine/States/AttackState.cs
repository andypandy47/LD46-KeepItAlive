using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

namespace StateMachine.States
{
    public class AttackState : State<PlayerStateManager>
    {
        private static AttackState instance;
        private PlayerController pController;
        private AttackState()
        {
            if (instance != null)
                return;
            state = State.Jumping;
            instance = this;
        }

        public static AttackState Instance
        {
            get
            {
                if (instance == null)
                {
                    new AttackState();
                }

                return instance;
            }
        }

        public override void EnterState(PlayerStateManager owner)
        {
            if (pController == null)
                pController = owner.GetController() as PlayerController;

            pController.animController.SetTrigger("Attacking");
            pController.inputController.DisableMovementInput();
        }

        public override void ExitState(PlayerStateManager owner)
        {
            pController.inputController.EnableMovementInput();
        }

        public override void UpdateState(PlayerStateManager owner)
        {

        }
    }
}
