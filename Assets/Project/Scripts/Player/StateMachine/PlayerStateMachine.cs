using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachine;

namespace Player
{
    public class PlayerStateMachine : StateMachine<PlayerStateManager>
    {
        public PlayerStateMachine(PlayerStateManager _o) : base(_o)
        {
        }

        public override void ChangeState(State<PlayerStateManager> newState)
        {
            base.ChangeState(newState);
            owner.SetDisplayState(newState.state);
        }
    }
}
