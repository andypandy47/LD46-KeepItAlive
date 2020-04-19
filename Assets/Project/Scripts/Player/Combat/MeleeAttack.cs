using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable obj = collision.GetComponent<IDamageable>();
        if (collision.tag == "Enemy")
        {
            obj.TakeDamage(100.0f);
        }
    }
}
