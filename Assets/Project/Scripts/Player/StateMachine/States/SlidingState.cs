using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

namespace StateMachine.States
{
    public class SlidingState : State<PlayerStateManager>
    {
        private static SlidingState instance;
        private PlayerController pController;
        private float iTime = 1.0f;
        private SlidingState()
        {
            if (instance != null)
                return;
            state = State.Sliding;
            instance = this;
        }

        public static SlidingState Instance
        {
            get
            {
                if (instance == null)
                {
                    new SlidingState();
                }

                return instance;
            }
        }

        public override void EnterState(PlayerStateManager owner)
        {
            if (pController == null)
                pController = owner.GetController();

            pController.animController.SetTrigger("Sliding");
            pController.inputController.DisableMovementInput();
            pController.health.SetInvincibility(true);
            iTime = 1.0f;
        }

        public override void ExitState(PlayerStateManager owner)
        {
            pController.inputController.EnableMovementInput();
            pController.health.SetInvincibility(false);
        }

        public override void UpdateState(PlayerStateManager owner)
        {
            //if (iTime > 0.0f)
            //{
            //    iTime -= Time.deltaTime;
            //}
            //else
            //{
            //    if (pController.health.IsInvincible())
            //    {
            //        pController.health.SetInvincibility(false);
            //    }
            //}
        }
    }
}
