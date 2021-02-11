using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    const float MinImpulseForce = 3f;
    const float MaxInpulseForce = 5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Initialize(Direction direction)
    {
        // Generate angle between 0 and 30 deg (in radians)
        float angle = Random.value * 30f * Mathf.Deg2Rad;

        switch(direction)
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
}
