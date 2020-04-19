using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerController : BaseController
    {
        public PlayerStateManager stateController;
        public PlayerMovement movementController;
        public PlayerInput inputController;
        public PlayerAnimation animController;
        public PlayerHealth health;

        private Vector3 spawnPos;

        public void OnEnable()
        {
            spawnPos = transform.position;
            type = BaseType.Player;
            animController.Init();
            GameController.instance.AddController(this);
            stateController.InitialiseStateMachine(this);
        }

        public override void UpdateController()
        {
            inputController.UpdateInput();
            movementController.UpdateMovement();
            stateController.UpdateStateMachine();
        }

        public override void Restart()
        {
            transform.position = spawnPos;
        }
    }
}
