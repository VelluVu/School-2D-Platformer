﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickAxe : MonoBehaviour {

    Rigidbody2D rb;
    BoxCollider2D hitCol;
    EdgeCollider2D col;
    float speed;
    float spinSpeed;
    Vector3 throwForce;

    private void Start()
    {
        Destroy(gameObject, 10f);
        spinSpeed = 5f;
        speed = 15f;
        throwForce.Set(speed, 0, 0);
        rb = gameObject.GetComponent<Rigidbody2D>();
        hitCol = gameObject.GetComponent<BoxCollider2D>();
        col = gameObject.GetComponent<EdgeCollider2D>();
        
        rb.AddForce(throwForce, ForceMode2D.Impulse);
    }

    private void Update()
    {
        gameObject.transform.Rotate(Vector3.back * spinSpeed);
    }


}
