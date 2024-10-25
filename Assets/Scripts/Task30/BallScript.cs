using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    void FixedUpdate()
    {
        if (IsThereTouch())
        {
            Vector2 deltaPos = GetTouchDelta() * speed;
            Physics.gravity = new Vector3(-deltaPos.y, Physics.gravity.y, deltaPos.x);
        }
    }
    private bool IsThereTouch()
    {
        return Input.touchCount > 0;
    }
    private Vector2 GetTouchDelta()
    {
        if (IsThereTouch())
        {
            return Input.GetTouch(0).deltaPosition;
        }
        return Vector2.zero;
    }
}
