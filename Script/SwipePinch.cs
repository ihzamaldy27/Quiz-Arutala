using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipePinch : MonoBehaviour
{
    private Touch touch;
    private Vector2 _firstTouch;
    private Vector2 _secTouch;
    float distance_current;
    float distance_previous;
    bool pinch = true;

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved)
            {
                Quaternion rotationZ = Quaternion.Euler(0f, 0f, -touch.deltaPosition.x * 0.1f);
                transform.rotation = rotationZ * transform.rotation;
            }
        }

        if (Input.touchCount > 1)
        {
            _firstTouch = Input.GetTouch(0).position;
            _secTouch = Input.GetTouch(1).position;
            distance_current = _secTouch.magnitude - _firstTouch.magnitude;
            if (pinch)
            {
                distance_previous = distance_current;
                pinch = false;
            }
            if (distance_current != distance_previous)
            {
                Vector3 scale_value = this.transform.localScale * (distance_current / distance_previous);
                transform.localScale = scale_value;
                distance_previous = distance_current;
            }
        }
        else
        {
            pinch = true;
        }
    }
}
