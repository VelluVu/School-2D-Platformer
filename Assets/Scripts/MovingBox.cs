using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBox : MonoBehaviour {

    
    Vector3 maxUp;
    Vector3 startPos;
    Vector3 movement;
    bool max;
    float speed;
    float distance;
   
    private void Start()
    {
        max = false;
        speed = 5f;
        distance = 100f;
        maxUp.Set(0, distance, 0);
        startPos = transform.position;
        movement.Set(0, speed * Time.deltaTime, 0);
        
    }

    private void Update()
    {
        if (transform.position.y >= maxUp.y)
        {
            max = true;
        }
        if (transform.position.y <= startPos.y && max)
        {
            max = false;
        }

        if (transform.position.y <= maxUp.y && !max)
        {
            transform.Translate(movement);
        }
        else if (transform.position.y >= startPos.y && max)
        {
            transform.Translate(-movement);
        }
    }
}
