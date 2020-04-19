using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class EnemyAnimation : MonoBehaviour
    {
        private Animator anim;
        private IAttack attackController;
        private Vector3 playerPos;
        public bool facingRight = true;

        public void Init()
        {
            anim = GetComponent<Animator>();
            attackController = GetComponentInParent<IAttack>();
        }

        public void SetTrigger(string triggerName)
        {
            anim.SetTrigger(triggerName);
        }

        public void FacePlayer(Vector3 playerPos)
        {
            this.playerPos = playerPos;
            if (playerPos.x < transform.position.x && facingRight)
                Flip();

            if (playerPos.x > transform.position.x && !facingRight)
                Flip();
        }

        private void Flip()
        {
            facingRight = !facingRight;

            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }

        public void LaunchProjectile()
        {
            int attackDir = playerPos.x < transform.position.x ? -1 : 1;

            attackController.Attack(attackDir, GameController.instance.GetPlayerController().transform.position);
        }
    }
}
