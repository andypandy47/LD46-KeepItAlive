using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace Enemy
{
    public class EnemyHealth : MonoBehaviour, IDamageable
    {
        public float maxHealth = 100.0f;
        public float currentHealth, interuptTime;
        public bool interupted = false;

        private EnemyController controller;

        public void Init(EnemyController controller)
        {
            this.controller = controller;
            currentHealth = maxHealth;
            interupted = false;
        }

        public bool IsInvincible()
        {
            return false;
        }

        public IEnumerator Kill()
        {
            //yield return StartCoroutine(Effects.instance.PauseFor(0.15f));
            FMODUnity.RuntimeManager.PlayOneShot(SfxManager.instance.enemyHit, transform.position);
            EnemySpawnManager.instance.SpawnEnemy(controller.leftEnemy);
            gameObject.SetActive(false);
            yield return null;
        }

        public void SetInvincibility(bool value)
        {
            
        }

        public void TakeDamage(float amount)
        {
            Debug.Log("Enemy took damage");

            interupted = true;
            currentHealth -= amount;
            
            int scoreAmount = 200;
            GameController.score += scoreAmount;
            Effects.instance.SpawnFloatyScore(transform.position, scoreAmount);
            if (currentHealth <= 0.0f)
            {
                StartCoroutine(Kill());
                return;
            }

            StartCoroutine(Interupt());
        }

        private IEnumerator Interupt()
        {
            interupted = true;
            transform.DOTogglePause();
            yield return new WaitForSeconds(interuptTime);
            interupted = false;
            transform.DOTogglePause();
        }
    }
}
