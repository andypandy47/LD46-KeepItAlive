using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerAttack : MonoBehaviour
    {
        public GameObject attackCol;

        private void Start()
        {
            attackCol.SetActive(false);
        }

        public void BeginAttack()
        {
            StartCoroutine(Attack());
        }

        public IEnumerator Attack()
        {
            attackCol.SetActive(true);
            yield return new WaitForSeconds(0.1f);
            attackCol.SetActive(false);
        }
    }
}
