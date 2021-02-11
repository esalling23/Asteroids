using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenWrap : MonoBehaviour
{

    float colliderHalfSize;

    // Start is called before the first frame update
    void Start()
    {
        CircleCollider2D collider = GetComponent<CircleCollider2D>();
        colliderHalfSize = collider.radius;
    }

    private void OnBecameInvisible()
    {
        Wrap();
    }

    private void Wrap()
    {
        Vector3 position = transform.position;
        if (position.x - colliderHalfSize > ScreenUtils.ScreenRight
            || position.x + colliderHalfSize < ScreenUtils.ScreenLeft)
        {
            position.x = -position.x;
        }
        else if (position.y - colliderHalfSize > ScreenUtils.ScreenTop
            || position.y + colliderHalfSize < ScreenUtils.ScreenBottom)
        {
            position.y = -position.y;
        }
        transform.position = position;
    }
}
