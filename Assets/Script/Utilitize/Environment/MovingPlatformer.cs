using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovingPlatformer : MonoBehaviour
{
    public Transform platform;
    public Transform startPosition;
    public Transform endPosition;

    public float speed = 1.5f;

    int direction = 1;

    public bool OnlyOneTime;

    private void Update()
    {
        Vector2 target = currentMovementTarget();

        if (!OnlyOneTime)
        {
            platform.position = Vector2.MoveTowards(platform.position, target, speed * Time.deltaTime);
        }
        else
        {
            platform.position = Vector2.Lerp(platform.position, target, speed * Time.deltaTime);
        }

        float distance = (target - (Vector2)platform.position).magnitude;

        if (distance <= 0.5f && !OnlyOneTime)
        {
            direction *= -1;
        }
    }

    Vector2 currentMovementTarget()
    {
        if(direction == 1)
        {
            return startPosition.position;
        }
        else
        {
            return endPosition.position;
        }
    }

    private void OnDrawGizmos()
    {
        if(platform != null && startPosition != null && endPosition != null)
        {
            Gizmos.DrawLine(platform.transform.position, startPosition.position);
            Gizmos.DrawLine(platform.transform.position, endPosition.position);
        }
    }
}
