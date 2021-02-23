using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    const float MinImpulseForce = 3000f;
    const float MaxInpulseForce = 5000f;

    bool halfLife = false;
    int health = 100;
    int maxHealth;
    SpriteRenderer spriteRenderer;

    [SerializeField]
    GameObject asteroid;

    AsteroidSpawner spawner;

    // Start is called before the first frame update
    void Start()
    {
        spawner = GameObject.FindWithTag("MainCamera").GetComponent<AsteroidSpawner>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        maxHealth = health;
    }

    public void Initialize(Direction direction)
    {
        // Generate angle between 0 and 30 deg (in radians)
        float angle = Random.value * 30f * Mathf.Deg2Rad;

        switch (direction)
        {
            case Direction.Up:
                angle = 75f * Mathf.Deg2Rad + angle;
                break;
            case Direction.Down:
                angle = 255f * Mathf.Deg2Rad + angle;
                break;
            case Direction.Right:
                angle = -15 * Mathf.Deg2Rad + angle;
                break;
            case Direction.Left:
                angle = 165f * Mathf.Deg2Rad + angle;
                break;
        }

        Vector2 moveDirection = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
        float magnitude = Random.Range(MinImpulseForce, MaxInpulseForce);
        GetComponent<Rigidbody2D>().AddForce(moveDirection * magnitude, ForceMode2D.Impulse);
    }

    public void HalfLife()
    {
        halfLife = true;

        if (health > maxHealth / 2)
        {
            health = maxHealth / 2;
        }

        Vector3 halfSizeScale = transform.localScale;
        halfSizeScale.x /= 2;
        halfSizeScale.y /= 2;
        transform.localScale = halfSizeScale;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        // Modify color alpha
        Color color = spriteRenderer.color;
        color.a -= (float)damage / (float)maxHealth;
        spriteRenderer.color = color;

        if (health == 0)
        {
            Destroy(gameObject);
            spawner.ReplenishAsteroid();
        }
        else if (maxHealth / health >= 2 && !halfLife)
        {
            // If we hit half-health, go half size & spawn another half-size
            HalfLife();

            Asteroid duplicate = Instantiate(asteroid, transform.position, Quaternion.identity).GetComponent<Asteroid>();
            Direction direction = (Direction)Random.Range(0, 4);
            print(direction);
            duplicate.Initialize(direction);
        }

        
    }
}