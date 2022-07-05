using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadKillJump : MonoBehaviour
{
    [SerializeField] private bool isTrigger = true;
    [SerializeField] private Collider2D _collider;
    private LifeController _life = null;
        
    void Start()
    {
        _life = transform.parent.GetComponent<LifeController>();
        _collider = GetComponent<Collider2D>();
        _collider.isTrigger = isTrigger;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        print("collision");
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();
        if (player != null && player.CanHeadKill())
        {
            _life.TakeDamage(_life.CurrentLife);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("hola");
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();
        if (player != null && player.CanHeadKill())
        {
            _life.TakeDamage(_life.CurrentLife);
        }
    }
}
