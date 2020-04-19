using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Enemy
{
    public class EnemyMovement : MonoBehaviour
    {
        public float moveSpeed;
        public bool isMoving = true;
        private EnemyController controller;

        public void Init(EnemyController controller)
        {
            this.controller = controller;
        }
        public void MoveTowardLocation(Vector3 location)
        {
            isMoving = true;
            controller.anims.SetTrigger("Walking");
            float duration = (Vector3.Distance(transform.position, location) / moveSpeed);
            Debug.Log("move toward: " + location + " over: " + duration + "s");
            transform.DOMove(location, duration).SetEase(Ease.Linear).OnComplete(() => { isMoving = false; controller.anims.SetTrigger("Idling"); } );
        }

        public static void Pause()
        {
        }
    }
}
