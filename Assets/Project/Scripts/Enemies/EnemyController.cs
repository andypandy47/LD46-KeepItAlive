using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class EnemyController : BaseController
    {
        public EnemyMovement movement;
        public EnemyHealth health;
        public EnemyAI ai;
        public EnemyAnimation anims;
        public bool leftEnemy;

        public void OnInstantiate()
        {
            GameController.instance.AddController(this);
        }

        public void Init(bool isOnLeft, Vector3 attackPos)
        {
            leftEnemy = isOnLeft;
            type = BaseType.Enemy;
            anims.Init();
            movement.Init(this);
            health.Init(this);
            ai.Init(this, attackPos);

        }

        public override void UpdateController()
        {
            ai.UpdateAI();
        }

        public override void Restart()
        {
            gameObject.SetActive(false);
        }
    }
}
