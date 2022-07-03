using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaBreakable : MonoBehaviour
{
    [SerializeField] private int maxLife = 1;
    private int currentLife;
    private bool isDestroyed;

    void Awake()
    {
        currentLife = maxLife;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        var bullet = collision.gameObject.GetComponent<MagicalBullet>();
        if (bullet != null && collision.gameObject.layer == 8)
            TakeDamage(bullet.AttackStats.MagicalDamage);
    }

    private void TakeDamage(int damage)
    {
        if (currentLife > 0)
        {
            currentLife -= damage;
            if (currentLife <= 0 && !isDestroyed)
                Die();
        }
    }

    private void Die()
    {
        isDestroyed = true;
        Destroy(gameObject);
    }
}
