using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class LaunchProjectile : MonoBehaviour, IAttack
    {
        public Transform projLaunchPos;
        public void Attack(int direction = 0, Vector3 target = new Vector3())
        {
            //temp
            Projectile projectile = Pool.instance.GetProjectile();

            projectile.Spawn(projLaunchPos.position, CalculateBalisticVelocityVector(projLaunchPos.position, target, 5), direction == -1 ? true : false);
        }

        private Vector3 CalculateBalisticVelocityVector(Vector3 source, Vector3 target, float angle)
        {
            Vector3 direction = target - source;
            float h = direction.y;
            direction.y = 0;
            float distance = direction.magnitude;

            float a = angle * Mathf.Deg2Rad;

            if (distance < 5.0f)
            {
                return 20.0f * direction.normalized;
            }

            direction.y = distance * Mathf.Tan(a);
            
            distance += h / Mathf.Tan(a);

            float velocity = Mathf.Sqrt(distance * Physics.gravity.magnitude / Mathf.Sin(2 * a));
            return velocity * direction.normalized;
        }

    }
}
