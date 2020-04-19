using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerAnimation : MonoBehaviour
    {
        private Animator anims;

        public static bool facingRight = true, walkingBackwards;

        public void Init()
        {
            anims = GetComponent<Animator>();
        }

        public void SetBool(string param, bool value)
        {
            anims.SetBool(param, value);
        }

        public void SetTrigger(string triggerName)
        {
            anims.SetTrigger(triggerName);
        }

        public void HandleFlipping(float xInput)
        {
            if (facingRight && xInput < 0)
                Flip();
            if (!facingRight && xInput > 0)
                Flip();
        }

        private void Flip()
        {
            facingRight = !facingRight;

            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }
}
