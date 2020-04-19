using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerHealth : MonoBehaviour, IDamageable
    {
        private bool invincible = false;

        public bool IsInvincible() { return invincible; }

        public void Kill()
        {
            Debug.Log("Player dead");
        }

        public void SetInvincibility(bool value)
        {
            invincible = value;
        }

        public void TakeDamage(float amount)
        {
            if (!invincible)
            {
                Effects.CamShake();
                FMODUnity.RuntimeManager.PlayOneShot(SfxManager.instance.playerHit, transform.position);
                GameController.funkLevel -= amount;
            }
        }
    }
}
