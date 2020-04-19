using System.Collections;
using UnityEngine;

namespace Enemy
{
    public class EnemyAI : MonoBehaviour
    {
        public float range, attackTime, heartBeatTime;
        private float nextHeartBeat;

        private EnemyController controller;
        private EnemyMovement movementController;
        private EnemyHealth healthController;
        private EnemyAnimation anims;

        private Vector3 attackLocation;

        public bool attackingEnabled = true;

        private void OnEnable()
        {
        }

        private void MoveToPosition()
        {
            var randomisedLoc = Random.Range(attackLocation.x - 1, attackLocation.x + 1);
            movementController.MoveTowardLocation(new Vector3(randomisedLoc, transform.position.y, 0));
        }

        private void ChasePlayer()
        {
            var playerPos = GameController.instance.GetPlayerController().transform.position;

            movementController.MoveTowardLocation(playerPos);
        }

        public void UpdateAI()
        {
            if (!healthController.interupted)
            {
                var playerPos = GameController.instance.GetPlayerController().transform.position;
                anims.FacePlayer(playerPos);

                if (nextHeartBeat <= 0.0f)
                {
                    if (attackingEnabled)
                    {
                        if (!movementController.isMoving)
                        {
                            anims.SetTrigger("Throwing");
                        }
                    }
                    nextHeartBeat = heartBeatTime;
                }
                else
                {
                    nextHeartBeat -= Time.deltaTime;
                }
            }
        }

        public void Init(EnemyController controller, Vector3 attackPos)
        {
            this.controller = controller;
            movementController = this.controller.movement;
            healthController = this.controller.health;
            anims = this.controller.anims;
            
            attackTime = 1.0f;
            attackLocation = attackPos;

            anims.SetTrigger("Idling");
            MoveToPosition();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(attackLocation, 1);
        }
    }
}
