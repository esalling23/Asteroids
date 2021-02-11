﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Ship behavior
/// </summary>
public class Ship : MonoBehaviour
{
    // Speed of ship movement
    const float RotateUnitsPerSecond = 10;
    const float ThrustForce = 15;

    private Rigidbody2D body;

    // Initially thrust upward until user rotates ship
    private Vector2 thrustDirection = new Vector2(0, 1);

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    ///  Fixed Update is used for physics
    /// </summary>
    void FixedUpdate()
    {
        // Get Horizontal/Vertical axes 
        float horizontalInput = Input.GetAxis("Horizontal");

        // If using left/right keys to rotate ship
        if (horizontalInput != 0)
        {
            // Rotate game object on Z axis
            // Use negative horizontal input to reverse the rotation
            // So right arrow key maps to clockwise
            // Orginal Code: 
            transform.Rotate(0, 0, -horizontalInput * RotateUnitsPerSecond);

            // Solution code:
            float rotationAmount = RotateUnitsPerSecond * Time.deltaTime;
            if (horizontalInput < 0)
            {
                rotationAmount *= -1;
            }
            transform.Rotate(Vector3.forward, rotationAmount);

            // When we rotate, also update the thrustDirection vector
            // hint: eulerAngles & Mathf.RadToDeg & Cos/Sin
            float angle = transform.eulerAngles.z * Mathf.Deg2Rad;

            // Original code: Created a new vector2 each time
            // thrustDirection = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
            // Better code: Modify the x & y of the exisiting vector2
            thrustDirection.x = Mathf.Cos(angle);
            thrustDirection.y = Mathf.Sin(angle);
        }

        float thrustInput = Input.GetAxis("Thrust");

        if (thrustInput != 0)
        {
            // Add force to ship
            body.AddForce(ThrustForce * thrustDirection, ForceMode2D.Force);
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Asteroid")
        {
            Destroy(gameObject);
        }
    }
}
