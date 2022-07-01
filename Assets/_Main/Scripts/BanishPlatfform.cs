using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanishPlatfform : MonoBehaviour
{
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            _animator.SetTrigger("doBanish");
        }
    }
}
