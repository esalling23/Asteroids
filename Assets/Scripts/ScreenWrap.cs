using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenWrap : MonoBehaviour
{

    float colliderHalfSize;

    float rightPos;
    float leftPos;
    float topPos;
    float bottomPos;

    // Start is called before the first frame update
    void Start()
    {
        CircleCollider2D collider = GetComponent<CircleCollider2D>();
        colliderHalfSize = collider.radius;
    }

    private void FixedUpdate()
    {
        rightPos = transform.position.x - colliderHalfSize;
        leftPos = transform.position.x + colliderHalfSize;
        topPos = transform.position.y - colliderHalfSize;
        bottomPos = transform.position.y + colliderHalfSize;

        if (rightPos > ScreenUtils.ScreenRight
            || leftPos < ScreenUtils.ScreenLeft
            || topPos > ScreenUtils.ScreenTop
            || bottomPos < ScreenUtils.ScreenBottom)
        {
            Wrap();
        }
    }

    private void Wrap()
    {
        Vector3 newPosition = transform.position;

        if (rightPos >= ScreenUtils.ScreenRight)
        {
            newPosition.x = -rightPos;
        }
        else if (leftPos <= ScreenUtils.ScreenLeft)
        {
            newPosition.x = -leftPos;
        }

        if (topPos >= ScreenUtils.ScreenTop)
        {
            newPosition.y = -topPos;
        }
        else if (bottomPos <= ScreenUtils.ScreenBottom)
        {
            newPosition.y = -bottomPos;
        }

        transform.position = newPosition;
    }
}
