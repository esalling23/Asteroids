using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages Bullet Behavior
/// </summary>
public class Bullet : MonoBehaviour
{
    // Timer lifespan support
    float elapsedLifetime = 0f;
    float lifespan = 2f;
    // Note: this bool is kind of redundant bc we destroy the game obj when
    // it would be set to false. Leaving it in for my own learning clarity.
    bool living = true;

    // Damage value for killing things
    [SerializeField]
    int damage = 25;

    private void Update()
    {
        // Bullet should destroy itself after lifespan is reached
        if (living)
        {
            elapsedLifetime += Time.deltaTime;
            if (elapsedLifetime >= lifespan)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Asteroid")
        {
            // Destroy self
            Destroy(gameObject);
            // Have asteroid take damage
            collision.gameObject.GetComponent<Asteroid>().TakeDamage(damage);
        }
    }
}
