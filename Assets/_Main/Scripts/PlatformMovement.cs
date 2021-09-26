using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    //Walls
    [SerializeField] private float bottomWall;
    [SerializeField] private float topWall;
    [SerializeField] private float speed;

    private float actualSpeed;

    // Start is called before the first frame update
    void Start()
    {
        actualSpeed = speed;    
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.up * actualSpeed * Time.deltaTime;

        if (transform.position.y >= topWall) actualSpeed = -speed;
        else if (transform.position.y <= bottomWall) actualSpeed = speed;
    }
}
