using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
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

        if (transform.position.y >= -1.8f)
            actualSpeed = -speed;
        else if (transform.position.y <= -10f)
            actualSpeed = speed;

    }
}
