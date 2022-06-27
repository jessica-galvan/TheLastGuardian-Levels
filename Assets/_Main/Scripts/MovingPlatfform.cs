using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatfform : MonoBehaviour
{
    [SerializeField] private Transform _pointA, _pointB, _platfform;
    [SerializeField] private float _speed = 0.1f;

    private float _time;

    private void Start()
    {
        _platfform.position = _pointA.position;
    }

    private void FixedUpdate()
    {
        _time += Time.deltaTime * _speed;

        _platfform.position = Vector3.Lerp(_pointA.position, _pointB.position, _time);

        if (Vector3.Distance(_platfform.position, _pointA.position) < 0.01f && _speed < 0)
        {
            _speed *= -1;
        }
        
        if (Vector3.Distance(_platfform.position, _pointB.position) < 0.01f && _speed > 0)
        {
            _speed *= -1;
        }
    }
}
