using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        public float accelerationTime;
        public float currentMoveSpeed;
        private float acceleration;
        private float velocityXSmoothing;

        protected CollisionController controller;

        [HideInInspector]
        public Vector2 directionalInput;
        public Vector3 velocity;
        public static float xVelocity, yVelocity;

        public float maxJumpHeight = 4;
        public float minJumpHeight = 1;
        public float timeToJumpApex = .4f;

        [SerializeField] private float gravity, maxJumpVelocity, minJumpVelocity;

        public float slideDistance, slideSpeed;

        public Vector2 attackDistance;

        private PlayerAnimation anims;

        public virtual void Start()
        {
            controller = GetComponent<CollisionController>();
            anims = GetComponentInChildren<PlayerAnimation>();

            gravity = -(2 * maxJumpHeight) / Mathf.Pow(timeToJumpApex, 2);
            maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
            minJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(gravity) * minJumpHeight);
        }

        public virtual void UpdateMovement()
        {
            CalculateVelocity();
            xVelocity = velocity.x;
            yVelocity = velocity.y;

            controller.Move(velocity * Effects.myTimeScale, directionalInput);

            PlayerStateManager.instance.grounded  = controller.collisions.below;

             anims.HandleFlipping(directionalInput.x);
        }

        public void SetDirectionalInput(Vector2 input)
        {
            directionalInput = input;
        }

        public void OnJumpInputDown()
        {

            if (PlayerStateManager.instance.currentState != State.Sliding)
            {
                float slideDirection = directionalInput.x;
                if (directionalInput.x == 0)
                {
                    slideDirection = PlayerAnimation.facingRight ? 1.0f : -1.0f;
                }

                transform.DOMoveX(transform.position.x + slideDistance * slideDirection, 0.5f)
                .SetEase(Ease.OutExpo)
                .OnComplete(() =>
                {
                    PlayerStateManager.instance.SetCurrentState(StateMachine.States.IdlingState.Instance);
                });

                PlayerStateManager.instance.SetCurrentState(StateMachine.States.SlidingState.Instance);
                FMODUnity.RuntimeManager.PlayOneShot(SfxManager.instance.dodgeWhoosh, transform.position);
            }
            //else
            //{
            //    PlayerStateManager.instance.SetCurrentState(StateMachine.States.JumpingState.Instance);
            //    velocity.y = maxJumpVelocity;
            //}
        }

        public void OnJumpInputUp()
        {
            //if (velocity.y > minJumpVelocity)
            //{
            //    velocity.y = minJumpVelocity;
            //}
        }

        private void CalculateVelocity()
        {
            float targetVelocityX;

            targetVelocityX = directionalInput.x * currentMoveSpeed * Time.deltaTime;

            velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, accelerationTime);
           // velocity.y += gravity * Time.deltaTime;
        }

        public void Attack()
        {
            // velocity.x = PlayerAnimation.facingRight ? attackDistance.x : -attackDistance.x;
            float attackDist = PlayerAnimation.facingRight ? transform.position.x + attackDistance.x : transform.position.x - attackDistance.x;
            transform.DOMoveX(attackDist, 0.1f).SetEase(Ease.Linear);
        }
    }
}

