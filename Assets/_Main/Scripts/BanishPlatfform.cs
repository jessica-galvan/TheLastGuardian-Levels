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
        print("bu");
        if (col.CompareTag("Player"))
        {
            print("ok");
            _animator.SetTrigger("doBanish");
        }
    }
}
