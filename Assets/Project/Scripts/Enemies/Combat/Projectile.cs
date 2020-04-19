using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private IDamageable playerObj;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    public void Spawn(Vector3 position, Vector3 force, bool faceLeft = false)
    {
        transform.position = position;
        sr.flipX = faceLeft;
        transform.rotation = Quaternion.identity;
        rb.AddForce(force, ForceMode2D.Impulse);
        rb.AddTorque(faceLeft ? 10 : -10);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerObj = collision.GetComponent<IDamageable>();
            if (!playerObj.IsInvincible())
            {
                playerObj.TakeDamage(10.0f);
                gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (playerObj != null && playerObj.IsInvincible())
            {
                GameController.funkLevel += 1.0f;
                GameController.score += 100;
                Effects.instance.SpawnFloatyScore(collision.gameObject.transform.position, 100);
            }
        }
    }

    private void Destroy()
    {
        Destroy(this.gameObject);
    }
}
