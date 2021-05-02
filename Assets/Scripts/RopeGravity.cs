using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeGravity : MonoBehaviour
{
    Rigidbody2D myBody;
    Collider2D myCollider;

    void Start()
    {
        myBody = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<Collider2D>();
    }

    void FixedUpdate()
    {
        myBody.velocity += new Vector2(0, Physics2D.gravity.y * myBody.gravityScale * Time.fixedDeltaTime);
    }
}
